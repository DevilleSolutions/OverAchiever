using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DevilleSolutions.Commons.Extensions;

namespace DevilleSolutions.Commons.MVC.Windsor.Installers
{
    public class StartablesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Classes.FromAssemblies(container.ResolveAll<Assembly>())
                   .BasedOn<IStartable>()
                   .ForEach(startable => container.Register(Component.For<IStartable>().ImplementedBy(startable).LifestyleSingleton()));
        }
    }
}