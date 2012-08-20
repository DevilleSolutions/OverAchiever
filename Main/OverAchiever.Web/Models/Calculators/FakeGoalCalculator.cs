using OverAchiever.Infrastructure;

namespace OverAchiever.Web.Models.Calculators
{
    public class FakeGoalCalculator : IGoalCalculator
    {
        private int _last;

        public FakeGoalCalculator()
        {
            _last = 0;
        }

        public int GetCurrent()
        {
            _last++;
            return _last;
        }
    }
}