namespace PowerliftServer.Web.Models
{
    public class Lifter
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Platform { get; set; }
        public string Flight { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string BirthDate { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string IntendedWeightClassKg { get; set; }
        public string Equipment { get; set; }
        public List<string> Divisions { get; set; }
        public List<string> Events { get; set; }
        public int Lot { get; set; }
        public string MemberId { get; set; }
        public bool Paid { get; set; }
        public string Team { get; set; }
        public bool Guest { get; set; }
        public string Instagram { get; set; }
        public string Notes { get; set; }
        public double BodyweightKg { get; set; }
        public string SquatRackInfo { get; set; }
        public string BenchRackInfo { get; set; }
        public List<double> SquatKg { get; set; }
        public List<double> BenchKg { get; set; }
        public List<double> DeadliftKg { get; set; }
        public List<int> SquatStatus { get; set; }
        public List<int> BenchStatus { get; set; }
        public List<int> DeadliftStatus { get; set; }
        public double CurrentBestSquat
        {
            get
            {
                return GetBestLift(SquatKg, SquatStatus);
            }
        }

        public double CurrentBestBench
        {
            get
            {
                return GetBestLift(BenchKg, BenchStatus);
            }
        }

        public double CurrentBestDeadlift
        {
            get
            {
                return GetBestLift(DeadliftKg, DeadliftStatus);
            }
        }

        public double CurrentTotal
        {
            get
            { 
                // In order to total a lifter must have had a successful lift in each exercise.
                if (CurrentBestSquat > 0 && CurrentBestDeadlift > 0 && CurrentBestBench > 0)
                    return CurrentBestSquat + CurrentBestBench+ CurrentBestDeadlift;
                return 0;
            }
        }

        public string GetClassLongName()
        {
            try
            {
                string shortClass = Divisions?[0];
                if (shortClass == null) return null;

                var longClass = "";

                if (shortClass[1] == 'R') longClass = "Classic "; //raw or 
                if (shortClass[1] == '-') longClass = "Equipped "; //equipped (which is not written, the short category only has R for raw and everything else is equipped
                if (shortClass[0] == 'F') longClass += "Women "; //female or 
                if (shortClass[0] == 'M') longClass += "Men "; //male

                var shortClassSplit = shortClass.Split("-")[1]; //only want the bits after the hyphen

                if (shortClassSplit == "SJr") longClass += "(14-18)";
                if (shortClassSplit == "Jr") longClass += "(18-23)";
                if (shortClassSplit == "M1") longClass += "(40-49)";
                if (shortClassSplit == "M2") longClass += "(50-59)";
                if (shortClassSplit == "M3") longClass += "(60-69)";
                if (shortClassSplit == "M4") longClass += "(70-79+)";
                if (shortClassSplit == "O") longClass += "(Open)";

                return longClass;
            }
            catch 
            {
                return null;
            }
        }

        private static double GetBestLift(List<double> liftAmounts, List<int> liftStatuses)
        {
            if (liftAmounts is null || liftStatuses is null)
                return 0;

            if (liftAmounts.Count != liftStatuses.Count)
                return 0;

            double bestLift = 0;
            for (int i = 0; i < liftAmounts.Count; i++)
            {
                double liftAmount = liftAmounts[i];
                int liftStatus = liftStatuses[i];

                // If lift successful and heavier than current best, set as best.
                if (liftStatus == 1 && liftAmount > bestLift) bestLift = liftAmount;
            }
            return bestLift;
        }
    }
}
