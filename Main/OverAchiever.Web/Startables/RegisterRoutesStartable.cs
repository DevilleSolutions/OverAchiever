using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core;

namespace OverAchiever.Web.Startables
{
    public class RegisterRoutesStartable : IStartable
    {
        private readonly RouteCollection _routes;

        public RegisterRoutesStartable(RouteCollection routes)
        {
            _routes = routes;
        }

        public void Start()
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

        public void Stop()
        {
        }
    }
}