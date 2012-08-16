namespace OverAchiever.Web.Models
{
    public class Goal : IGoal
    {
        private readonly IGoalCalculator _calculator;

        public int Current
        {
            get { return _calculator.GetCurrent(); }
        }

        public int Desired { get; set; }

        public bool Achieved
        {
            get { return Current >= Desired; }
        }

        public Goal(IGoalCalculator calculator, int desired)
        {
            Desired = desired;
            _calculator = calculator;
        }
    }
}