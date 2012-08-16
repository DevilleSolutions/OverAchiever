using System.Collections.Generic;
using OverAchiever.Web.Models;

namespace OverAchiever.Web.Services
{
    public interface IGoalTrackingService
    {
        void Track(IGoal goal);

        void StopTracking(IGoal goal);

        int CheckStatus(IGoal goal);

        IEnumerable<IGoal> ActiveGoals { get; }
    }
}