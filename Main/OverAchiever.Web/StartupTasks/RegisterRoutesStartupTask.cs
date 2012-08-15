using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using OverAchiever.Infrastructure;

namespace OverAchiever.Web.StartupTasks
{
    public class RegisterRoutesStartupTask : IStartupTask
    {
        private readonly RouteCollection _routes;

        public RegisterRoutesStartupTask(RouteCollection routes)
        {
            _routes = routes;
        }

        public void Run()
        {
            _routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            _routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            _routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}