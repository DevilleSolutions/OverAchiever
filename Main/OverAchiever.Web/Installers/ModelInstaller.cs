using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DevilleSolutions.Commons;
using DevilleSolutions.Commons.Extensions;
using DevilleSolutions.Commons.MVC.Windsor.Extensions;
using OverAchiever.Infrastructure;
using OverAchiever.Web.Models;
using OverAchiever.Web.Models.Calculators;

namespace OverAchiever.Web.Installers
{
    public class ModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Interfaces.FromThisAssembly()
                      .InTheSameNamespaceAs<IGoal>()
                      .ForEach(model => container.Register(Component.For(model).UsingProxy(model).LifestyleTransient()));

            container.Register(Component.For<IGoalCalculator>().ImplementedBy<FakeGoalCalculator>().LifestyleTransient());
        }
    }
}