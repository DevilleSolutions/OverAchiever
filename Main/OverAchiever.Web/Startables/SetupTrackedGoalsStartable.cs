using System;
using Castle.Core;
using DevilleSolutions.Commons.Extensions;
using OverAchiever.Web.Models.Factories;
using OverAchiever.Web.Services;

namespace OverAchiever.Web.Startables
{
    public class SetupTrackedGoalsStartable : IStartable
    {
        private readonly IGoalFactory _goalFactory;
        private readonly IGoalTrackingService _goalTrackingService;

        public SetupTrackedGoalsStartable(IGoalFactory goalFactory,
                                            IGoalTrackingService goalTrackingService)
        {
            _goalFactory = goalFactory;
            _goalTrackingService = goalTrackingService;
        }

        public void Start()
        {
            var goals = 10.Times(i =>
                                   {
                                       var goal = _goalFactory.Create(new Random(Guid.NewGuid().GetHashCode()).Next(1, 200));
                                       goal.Name = "Goal" + i;
                                       goal.Summary = "Want to achieve a total score of " + goal.Desired;
                                       goal.Description = string.Format("I plan to achieve this by counting to {0} every time you hit refresh", goal.Desired);

                                       return goal;
                                   });
            goals.ForEach(_goalTrackingService.Track);
        }

        public void Stop()
        {
        }
    }
}