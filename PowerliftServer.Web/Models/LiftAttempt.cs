namespace PowerliftServer.Web.Models
{
    /// <summary>
    /// Models a powerlifting lift attempt.
    /// </summary>
    public class LiftAttempt
    {
        /// <summary>
        /// The name of the competition in which the lift attempt is occurring.
        /// </summary>
        public string CompetitionName { get; set; }

        /// <summary>
        /// The Lifter performing the attempt.
        /// </summary>
        public Lifter Lifter { get; set; }

        /// <summary>
        /// The code of the lift being performed. S for Squat, B for Bench, D for Deadlift.
        /// </summary>
        public string LiftCode { get; set; }

        /// <summary>
        /// The weight of the attempt.
        /// </summary>
        public double Weight 
        {
            get 
            {
                var currentWeight = GetCurrentWeight();
                return currentWeight ?? 0;
            }
        }

        /// <summary>
        /// The unit of weight for the attempt.
        /// </summary>
        public string WeightUnit { get; set; }

        /// <summary>
        /// The weight class of the lifter performing the attempt.
        /// </summary>
        public string LifterWeightClass { get; set; }

        /// <summary>
        /// The lift that is being performed in the attempt.
        /// </summary>
        public string Lift
        { 
            get 
            {
                return GetCurrentLift();
            }
        }

        /// <summary>
        /// The category in which the lift attempt is occurring.
        /// </summary>
        public string CategoryName
        {
            get
            {
                var catName = "";
                if (Lifter is null) return catName;
                var className = Lifter.GetClassLongName();
                if (!string.IsNullOrEmpty(className)) catName += className;
                if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(LifterWeightClass)) catName += " - ";
                if (!string.IsNullOrEmpty(LifterWeightClass)) catName += $"{LifterWeightClass}{WeightUnit}";
                return catName;
            }
        }

        /// <summary>
        /// Gets the weight that is being lifted in the lift attempt.
        /// </summary>
        /// <returns></returns>
        private double? GetCurrentWeight()
        {
            return LiftCode switch
            {
                "S" => Lifter?.SquatKg?.FindLast(x => x != 0),
                "B" => Lifter?.BenchKg?.FindLast(x => x != 0),
                "D" => Lifter?.DeadliftKg?.FindLast(x => x != 0),
                _ => 0,
            };
        }

        /// <summary>
        /// Gets the type and number of lift that is being performed in the lift attempt, eg "Bench 2".
        /// </summary>
        /// <returns></returns>
        private string GetCurrentLift()
        {
            return LiftCode switch
            {
                "S" => $"Squat {ConvertLiftNumber(Lifter?.SquatKg?.FindLastIndex(x => x != 0) + 1)}".Trim(),
                "B" => $"Bench {ConvertLiftNumber(Lifter?.BenchKg?.FindLastIndex(x => x != 0) + 1)}".Trim(),
                "D" => $"Deadlift {ConvertLiftNumber(Lifter?.DeadliftKg?.FindLastIndex(x => x != 0) + 1)}".Trim(),
                _ => ""
            };
        }

        /// <summary>
        /// Converts a lift number into a string to be inserted into the Lift property.
        /// </summary>
        /// <param name="liftNumber">The number of lift to be converted to a string.</param>
        /// <returns>null if liftNumber is null or 0, liftNumber as string otherwise.</returns>
        private static string ConvertLiftNumber(int? liftNumber)
        {
            if (liftNumber is null) return null;
            return liftNumber == 0 ? null : liftNumber.ToString();
        }
    }
}
