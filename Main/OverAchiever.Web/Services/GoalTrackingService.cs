using System.Collections.Generic;
using OverAchiever.Web.Models;

namespace OverAchiever.Web.Services
{
    public class GoalTrackingService : IGoalTrackingService
    {
        private readonly IList<IGoal> _trackedGoals;

        public GoalTrackingService()
        {
            _trackedGoals = new List<IGoal>();
        }

        public void Track(IGoal goal)
        {
            _trackedGoals.Add(goal);
        }

        public void StopTracking(IGoal goal)
        {
            if (_trackedGoals.Contains(goal))
            {
                _trackedGoals.Remove(goal);
            }
        }

        public int CheckStatus(IGoal goal)
        {
            if(_trackedGoals.Contains(goal))
            {
                return _trackedGoals[_trackedGoals.IndexOf(goal)].Calculator.GetCurrent();
            }

            return 0;
        }

        public IEnumerable<IGoal> ActiveGoals
        {
            get { return _trackedGoals; }
        }
    }
}