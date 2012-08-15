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
            RouteConfig.RegisterRoutes(_routes);
        }
    }
}