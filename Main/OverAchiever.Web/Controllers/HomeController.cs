using System.Web.Mvc;
using OverAchiever.Web.Extensions;
using OverAchiever.Web.Models;
using OverAchiever.Web.Models.Factories;
using OverAchiever.Web.Services;

namespace OverAchiever.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGoalFactory _goalFactory;
        private readonly IDescriptorFactory<IGoal> _descriptorFactory;
        private readonly IGoalTrackingService _goalTracker;

        public HomeController(IGoalFactory goalFactory, 
                              IDescriptorFactory<IGoal> descriptorFactory,
                              IGoalTrackingService goalTracker)
        {
            _goalFactory = goalFactory;
            _descriptorFactory = descriptorFactory;
            _goalTracker = goalTracker;

            10.Times(() => _goalTracker.Track(_descriptorFactory.Create(_goalFactory.Create(100))));
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View(_goalTracker.ActiveGoals);
        }
    }
}
