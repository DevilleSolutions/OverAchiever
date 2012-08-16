using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace OverAchiever.Web.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                   .Where(t => t.Name.EndsWith("Service"))
                                   .WithServiceDefaultInterfaces()
                                   .LifestyleSingleton());
        }
    }
}