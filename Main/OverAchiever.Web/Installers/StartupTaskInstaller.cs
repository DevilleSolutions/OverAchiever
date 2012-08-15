using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OverAchiever.Web.StartupTasks;

namespace OverAchiever.Web.Installers
{
    public class StartupTaskInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                              .BasedOn<IStartupTask>()
                              .WithServiceBase().LifestyleSingleton());
        }
    }
}