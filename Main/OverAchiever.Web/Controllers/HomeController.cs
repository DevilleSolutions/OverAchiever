using System.Web.Mvc;
using OverAchiever.Web.Extensions;
using OverAchiever.Web.Models.Factories;
using OverAchiever.Web.Services;

namespace OverAchiever.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGoalFactory _goalFactory;
        private readonly IGoalTrackingService _goalTracker;

        public HomeController(IGoalFactory goalFactory, IGoalTrackingService goalTracker)
        {
            _goalFactory = goalFactory;
            _goalTracker = goalTracker;

            10.Times(() => _goalTracker.Track(_goalFactory.Create(100)));
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View(_goalTracker.ActiveGoals);
        }
    }
}
