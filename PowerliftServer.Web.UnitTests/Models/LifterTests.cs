using PowerliftServer.Web.Models;
using System.Collections.Generic;
using Xunit;

namespace PowerliftServer.Web.UnitTests.Models
{
    public class LifterTests
    {
        #region CurrentBestSquat
        [Fact]
        public void CurrentBestSquat_AllLiftsSuccessful_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100, 120, 130 },
                SquatStatus = new List<int> { 1, 1, 1 }
            };
            var expectedBest = 130;

            Assert.Equal(expectedBest, lifter.CurrentBestSquat);
        }

        [Fact]
        public void CurrentBestSquat_AllLiftsFailed_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100, 100, 100 },
                SquatStatus = new List<int> { -1, -1, -1 }
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestSquat);
        }

        [Fact]
        public void CurrentBestSquat_NoLiftsAttempted_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100, 120, 130 },
                SquatStatus = new List<int> { 0, 0, 0, 0, 0}
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestSquat);
        }

        [Fact]
        public void CurrentBestSquat_LiftFailed_LiftNotAttempted_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double>{ 100, 120, 130 },
                SquatStatus = new List<int>{ 1, -1, 0 }
            };
            var expectedBest = 100;

            Assert.Equal(expectedBest, lifter.CurrentBestSquat);
        }

        [Fact]
        public void CurrentBestSquat_OnlyThirdSuccessful_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100, 110, 120 },
                SquatStatus = new List<int> { -1, -1, 1 }
            };
            var expectedBest = 120;

            Assert.Equal(expectedBest, lifter.CurrentBestSquat);
        }

        [Fact]
        public void CurrentBestSquat_ListsZeroLengthTest()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double>(),
                SquatStatus = new List<int>()
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestSquat);
        }

        [Fact]
        public void CurrentBestSquat_ListsNotInitialized_Test()
        {
            var lifter = new Lifter();
            var expectedBest = 0;
            Assert.Equal(expectedBest, lifter.CurrentBestSquat);
        }
        #endregion

        #region CurrentBestBench
        [Fact]
        public void CurrentBestBench_AllLiftsSuccessful_Test()
        {
            var lifter = new Lifter
            {
                BenchKg = new List<double> { 100, 120, 130 },
                BenchStatus = new List<int> { 1, 1, 1 }
            };
            var expectedBest = 130;

            Assert.Equal(expectedBest, lifter.CurrentBestBench);
        }

        [Fact]
        public void CurrentBestBench_AllLiftsFailed_Test()
        {
            var lifter = new Lifter
            {
                BenchKg = new List<double> { 100, 100, 100 },
                BenchStatus = new List<int> { -1, -1, -1 }
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestBench);
        }

        [Fact]
        public void CurrentBestBench_NoLiftsAttempted_Test()
        {
            var lifter = new Lifter
            {
                BenchKg = new List<double> { 100, 120, 130 },
                BenchStatus = new List<int> { 0, 0, 0, 0, 0 }
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestBench);
        }

        [Fact]
        public void CurrentBestBench_LiftFailed_LiftNotAttempted_Test()
        {
            var lifter = new Lifter
            {
                BenchKg = new List<double> { 100, 120, 130 },
                BenchStatus = new List<int> { 1, -1, 0 }
            };
            var expectedBest = 100;

            Assert.Equal(expectedBest, lifter.CurrentBestBench);
        }

        [Fact]
        public void CurrentBestBench_OnlyThirdSuccessful_Test()
        {
            var lifter = new Lifter
            {
                BenchKg = new List<double> { 100, 110, 120 },
                BenchStatus = new List<int> { -1, -1, 1 }
            };
            var expectedBest = 120;

            Assert.Equal(expectedBest, lifter.CurrentBestBench);
        }

        [Fact]
        public void CurrentBestBench_ListsZeroLengthTest()
        {
            var lifter = new Lifter
            {
                BenchKg = new List<double>(),
                BenchStatus = new List<int>()
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestBench);
        }

        [Fact]
        public void CurrentBestBench_ListsNotInitialized_Test()
        {
            var lifter = new Lifter();
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestBench);
        }
        #endregion

        #region CurrentBestDeadlift
        [Fact]
        public void CurrentBestDeadlift_AllLiftsSuccessful_Test()
        {
            var lifter = new Lifter
            {
                DeadliftKg = new List<double> { 100, 120, 130 },
                DeadliftStatus = new List<int> { 1, 1, 1 }
            };
            var expectedBest = 130;

            Assert.Equal(expectedBest, lifter.CurrentBestDeadlift);
        }

        [Fact]
        public void CurrentBestDeadlift_AllLiftsFailed_Test()
        {
            var lifter = new Lifter
            {
                DeadliftKg = new List<double> { 100, 100, 100 },
                DeadliftStatus = new List<int> { -1, -1, -1 }
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestDeadlift);
        }

        [Fact]
        public void CurrentBestDeadlift_NoLiftsAttempted_Test()
        {
            var lifter = new Lifter
            {
                DeadliftKg = new List<double> { 100, 120, 130 },
                DeadliftStatus = new List<int> { 0, 0, 0, 0, 0 }
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestDeadlift);
        }

        [Fact]
        public void CurrentBestDeadlift_LiftFailed_LiftNotAttempted_Test()
        {
            var lifter = new Lifter
            {
                DeadliftKg = new List<double> { 100, 120, 130 },
                DeadliftStatus = new List<int> { 1, -1, 0 }
            };
            var expectedBest = 100;

            Assert.Equal(expectedBest, lifter.CurrentBestDeadlift);
        }

        [Fact]
        public void CurrentBestDeadlift_OnlyThirdSuccessful_Test()
        {
            var lifter = new Lifter
            {
                DeadliftKg = new List<double> { 100, 110, 120 },
                DeadliftStatus = new List<int> { -1, -1, 1 }
            };
            var expectedBest = 120;

            Assert.Equal(expectedBest, lifter.CurrentBestDeadlift);
        }

        [Fact]
        public void CurrentBestDeadlift_ListsZeroLengthTest()
        {
            var lifter = new Lifter
            {
                DeadliftKg = new List<double>(),
                DeadliftStatus = new List<int>()
            };
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestDeadlift);
        }

        [Fact]
        public void CurrentBestDeadlift_ListsNotInitialized_Test()
        {
            var lifter = new Lifter();
            var expectedBest = 0;

            Assert.Equal(expectedBest, lifter.CurrentBestDeadlift);
        }
        #endregion

        #region CurrentTotal

        [Fact]
        public void CurrentTotal_ScoreForAllLists_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100 },
                SquatStatus = new List<int> { 1 },
                BenchKg = new List<double> { 100 },
                BenchStatus = new List<int> { 1 },
                DeadliftKg = new List<double> { 100 },
                DeadliftStatus = new List<int> { 1 }
            };
            var expectedTotal = 300;

            Assert.Equal(expectedTotal, lifter.CurrentTotal);
        }

        [Fact]
        public void CurrentTotal_NoScoreForAnyLifts_Test()
        {
            var lifter = new Lifter();
            var expectedTotal = 0;
            Assert.Equal(expectedTotal, lifter.CurrentTotal);
        }

        [Fact]
        public void CurrentTotal_NoScoreForSquat_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100 },
                SquatStatus = new List<int> { -1 },
                BenchKg = new List<double> { 100 },
                BenchStatus = new List<int> { 1 },
                DeadliftKg = new List<double> { 100 },
                DeadliftStatus = new List<int> { 1 }
            };
            var expectedTotal = 0;

            Assert.Equal(expectedTotal, lifter.CurrentTotal);
        }

        [Fact]
        public void CurrentTotal_NoScoreForBench_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100 },
                SquatStatus = new List<int> { 1 },
                BenchKg = new List<double> { 100 },
                BenchStatus = new List<int> { 0 },
                DeadliftKg = new List<double> { 100 },
                DeadliftStatus = new List<int> { 1 }
            };
            var expectedTotal = 0;

            Assert.Equal(expectedTotal, lifter.CurrentTotal);
        }

        [Fact]
        public void CurrentTotal_NoScoreForDeadlift_Test()
        {
            var lifter = new Lifter
            {
                SquatKg = new List<double> { 100 },
                SquatStatus = new List<int> { 1 },
                BenchKg = new List<double> { 100 },
                BenchStatus = new List<int> { 1 },
                DeadliftKg = new List<double>(),
                DeadliftStatus = new List<int>()
            };
            var expectedTotal = 0;

            Assert.Equal(expectedTotal, lifter.CurrentTotal);
        }

        #endregion

        #region GetClassLongName
        [Fact]
        public void GetClassLongName_ReturnsNull_IfDivisionsListNotInitialised_Test()
        {
            var lifter = new Lifter();
            Assert.Null(lifter.GetClassLongName());
        }

        [Fact]
        public void GetClassLongName_ReturnsNull_IfDivisionsListEmpty_Test()
        {
            var lifter = new Lifter
            {
                Divisions = new List<string>()
            };
            Assert.Null(lifter.GetClassLongName());
        }

        [Fact]
        public void GetClassLongName_ReturnsNull_IfClassCodeNotParseable_Test()
        { 
            var lifter = new Lifter
            {

                Divisions = new List<string> { "Unparseable class code" }
            };
            Assert.Null(lifter.GetClassLongName());
        }

        [Fact]
        public void GetClassLongName_CorrectlyParses_ShortCodes_Test()
        {
            // Classic men
            var lifter = new Lifter
            {

                Divisions = new List<string> { "MR-O" }
            };
            var expectedClassName = "Classic Men (Open)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "MR-SJr" };
            expectedClassName = "Classic Men (14-18)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "MR-Jr" };
            expectedClassName = "Classic Men (18-23)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "MR-M1" };
            expectedClassName = "Classic Men (40-49)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "MR-M2" };
            expectedClassName = "Classic Men (50-59)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "MR-M3" };
            expectedClassName = "Classic Men (60-69)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "MR-M4" };
            expectedClassName = "Classic Men (70-79+)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());


            // Equipped men
            lifter.Divisions = new List<string> { "M-O" };
            expectedClassName = "Equipped Men (Open)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "M-SJr" };
            expectedClassName = "Equipped Men (14-18)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "M-Jr" };
            expectedClassName = "Equipped Men (18-23)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "M-M1" };
            expectedClassName = "Equipped Men (40-49)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "M-M2" };
            expectedClassName = "Equipped Men (50-59)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "M-M3" };
            expectedClassName = "Equipped Men (60-69)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "M-M4" };
            expectedClassName = "Equipped Men (70-79+)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());


            // Classic women
            lifter.Divisions = new List<string> { "FR-O" };
            expectedClassName = "Classic Women (Open)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "FR-SJr" };
            expectedClassName = "Classic Women (14-18)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "FR-Jr" };
            expectedClassName = "Classic Women (18-23)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "FR-M1" };
            expectedClassName = "Classic Women (40-49)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "FR-M2" };
            expectedClassName = "Classic Women (50-59)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "FR-M3" };
            expectedClassName = "Classic Women (60-69)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "FR-M4" };
            expectedClassName = "Classic Women (70-79+)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());


            // Equipped women
            lifter.Divisions = new List<string> { "F-O" };
            expectedClassName = "Equipped Women (Open)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "F-SJr" };
            expectedClassName = "Equipped Women (14-18)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "F-Jr" };
            expectedClassName = "Equipped Women (18-23)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "F-M1" };
            expectedClassName = "Equipped Women (40-49)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "F-M2" };
            expectedClassName = "Equipped Women (50-59)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "F-M3" };
            expectedClassName = "Equipped Women (60-69)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());

            lifter.Divisions = new List<string> { "F-M4" };
            expectedClassName = "Equipped Women (70-79+)";
            Assert.Equal(expectedClassName, lifter.GetClassLongName());
        }
        #endregion
    }
}