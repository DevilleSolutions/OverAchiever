using System.Web.Mvc;
using OverAchiever.Web.Services;

namespace OverAchiever.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGoalTrackingService _goalTracker;

        public HomeController(IGoalTrackingService goalTracker)
        {
            _goalTracker = goalTracker;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View(_goalTracker.ActiveGoals);
        }
    }
}
