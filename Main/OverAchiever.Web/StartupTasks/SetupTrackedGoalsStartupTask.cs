using System;
using DevilleSolutions.Commons.Extensions;
using OverAchiever.Infrastructure;
using OverAchiever.Web.Models.Factories;
using OverAchiever.Web.Services;

namespace OverAchiever.Web.StartupTasks
{
    public class SetupTrackedGoalsStartupTask : IStartupTask
    {
        private readonly IGoalFactory _goalFactory;
        private readonly IGoalTrackingService _goalTrackingService;

        public SetupTrackedGoalsStartupTask(IGoalFactory goalFactory,
                                            IGoalTrackingService goalTrackingService)
        {
            _goalFactory = goalFactory;
            _goalTrackingService = goalTrackingService;
        }

        public void Run()
        {
            var goals = 10.TimesInParallel(i =>
                                   {
                                       var goal = _goalFactory.Create(new Random(Guid.NewGuid().GetHashCode()).Next(1, 200));
                                       goal.Name = "Goal" + i;
                                       goal.Summary = "Want to achieve a total score of " + goal.Desired;
                                       goal.Description = string.Format("I plan to achieve this by counting to {0} every time you hit refresh", goal.Desired);

                                       return goal;
                                   });
            goals.InParallel(_goalTrackingService.Track);
        }
    }
}