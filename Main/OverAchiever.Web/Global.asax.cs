using System.Linq;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DevilleSolutions.Commons.Extensions;
using DevilleSolutions.Commons.MVC.Windsor;
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
            _container = Bootstrap.WithWindsor(typeof (TypedFactoryFacility));
            _container.Install(FromAssembly.This());

            RunStartupTasks();
        }

        private void RunStartupTasks()
        {
            var startupTasks = _container.ResolveAll<IStartupTask>().AsEnumerable();

            // startupTasks.InParallel(task => task.Run());
            startupTasks.ForEach(task => task.Run());
        }
    }
}