using System.Collections.Generic;
using Castle.Core.Internal;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using Castle.Windsor.Installer;
using OverAchiever.Infrastructure;

namespace OverAchiever.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start()
        {
            _container = new WindsorContainer();
            _container.AddFacility<TypedFactoryFacility>();
            _container.Install(FromAssembly.This());

            RunStartupTasks();

            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RunStartupTasks()
        {
            IEnumerable<IStartupTask> startupTasks = _container.ResolveAll<IStartupTask>();
            startupTasks.ForEach(task => task.Run());
        }

        public override void Dispose()
        {
            _container.Dispose();

            base.Dispose();
        }
    }
}