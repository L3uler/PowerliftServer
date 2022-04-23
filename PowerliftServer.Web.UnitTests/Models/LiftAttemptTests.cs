using PowerliftServer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PowerliftServer.Web.UnitTests.Models
{
    public class LiftAttemptTests
    {
        #region Weight
        [Fact]
        public void Weight_ReturnsZero_WhenLiftCodeNull_Test()
        {
            var liftAttempt = new LiftAttempt();
            var expectedWeight = 0;
            Assert.Equal(expectedWeight, liftAttempt.Weight);
        }

        [Fact]
        public void Weight_ReturnsZero_WhenLiftCodeNotParseable_Test()
        {
            var liftAttempt = new LiftAttempt { LiftCode = "something" };
            var expectedWeight = 0;
            Assert.Equal(expectedWeight, liftAttempt.Weight);
        }

        [Fact]
        public void Weight_ResturnsZero_WhenNoNonZeroWeights_Test()
        {
            var liftAttempt = new LiftAttempt 
            { 
                LiftCode = "S",
                Lifter = new Lifter
                {
                    SquatKg = new List<double> { 0, 0, 0, 0, 0}
                }
            };
            var expectedWeight = 0;
            Assert.Equal(expectedWeight, liftAttempt.Weight);
        }

        [Fact]
        public void Weight_ReturnsLastNonZeroValue_ForGivenLift_Test()
        {
            var liftAttempt = new LiftAttempt
            {
                LiftCode = "S",
                Lifter = new Lifter
                {
                    SquatKg = new List<double> { 100, 120, 150, 0, 0 },
                    BenchKg = new List<double> { 100, 120, 160, 0, 0 },
                    DeadliftKg = new List<double> { 100, 120, 170, 0, 0 }
                }
            };
            Assert.Equal(150, liftAttempt.Weight);

            liftAttempt.LiftCode = "B";
            Assert.Equal(160, liftAttempt.Weight);

            liftAttempt.LiftCode = "D";
            Assert.Equal(170, liftAttempt.Weight);
        }
        #endregion

        #region Lift
        [Fact]
        public void Lift_ReturnsEmpty_WhenLiftCodeNull_Test()
        {
            var liftAttempt = new LiftAttempt();
            Assert.Equal(string.Empty, liftAttempt.Lift);
        }

        [Fact]
        public void Lift_ReturnsEmpty_WhenLiftCodeNotParseable_Test()
        {
            var liftAttempt = new LiftAttempt { LiftCode = "something" };
            Assert.Equal(string.Empty, liftAttempt.Lift);
        }

        [Fact]
        public void Lift_ReturnsOnlyLiftName_WhenNumberCantBeCalculated_Test()
        {
            // When Lifter is null we should just get lift name
            var liftAttempt = new LiftAttempt { LiftCode = "S" };
            Assert.Equal("Squat", liftAttempt.Lift);

            // When Lifter.BenchKg is null we should just get lift name.
            liftAttempt.LiftCode = "B";
            liftAttempt.Lifter = new Lifter();
            Assert.Equal("Bench", liftAttempt.Lift);

            // When Lifter.DeadliftKg is all zero we should just get lift name.
            liftAttempt.LiftCode = "D";
            liftAttempt.Lifter = new Lifter
            {
                DeadliftKg = new List<double> { 0, 0, 0, 0, 0}
            };
            Assert.Equal("Deadlift", liftAttempt.Lift);
        }

        [Fact]
        public void Lift_CorrectlyReturnsLiftNameAndNumber_Test()
        {
            var liftAttempt = new LiftAttempt
            {
                LiftCode = "S",
                Lifter = new Lifter
                {
                    SquatKg = new List<double> { 100, 0, 0, 0, 0 },
                    BenchKg = new List<double> { 100, 110, 0, 0, 0 },
                    DeadliftKg = new List<double> { 100, 110, 120, 0, 0 }
                }
            };

            Assert.Equal("Squat 1", liftAttempt.Lift);

            liftAttempt.LiftCode = "B";
            Assert.Equal("Bench 2", liftAttempt.Lift);

            liftAttempt.LiftCode = "D";
            Assert.Equal("Deadlift 3", liftAttempt.Lift);
        }
        #endregion

        #region CategoryName
        [Fact]
        public void CategoryName_LifterNull_Test()
        {
            var liftAttempt = new LiftAttempt();
            Assert.Equal(string.Empty, liftAttempt.CategoryName);
        }

        [Fact]
        public void CategoryName_ClassNameNull_Test()
        {
            var liftAttempt = new LiftAttempt
            {
                LifterWeightClass = "83",
                WeightUnit = "kg",
                Lifter = new Lifter()
            };
            Assert.Equal("83kg", liftAttempt.CategoryName);
        }

        [Fact]
        public void CategoryName_LifterWeightClassNull_Test()
        {
            var liftAttempt = new LiftAttempt
            {
                Lifter = new Lifter
                {
                    Divisions = new List<string> { "MR-O" }
                }
            };
            Assert.Equal("Classic Men (Open)", liftAttempt.CategoryName);
        }

        [Fact]
        public void CategoryName_WeightClassAndClassName_Test()
        {
            var liftAttempt = new LiftAttempt
            {
                Lifter = new Lifter
                {
                    Divisions = new List<string> { "F-O" }
                },
                LifterWeightClass = "93",
                WeightUnit = "lb"
            };
            Assert.Equal("Equipped Women (Open) - 93lb", liftAttempt.CategoryName);
        }
        #endregion
    }
}
