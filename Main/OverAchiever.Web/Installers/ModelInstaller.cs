using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OverAchiever.Web.Models;

namespace OverAchiever.Web.Installers
{
    public class ModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                   .InSameNamespaceAs<IGoal>()
                                   .WithServiceDefaultInterfaces()
                                   .LifestyleTransient());
        }
    }
}