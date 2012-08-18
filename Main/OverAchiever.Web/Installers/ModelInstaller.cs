using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DevilleSolutions.Commons.Proxies;
using OverAchiever.Web.Models;
using OverAchiever.Web.Models.Calculators;

namespace OverAchiever.Web.Installers
{
    public class ModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IGoal>().ImplementedBy(Proxy.Implementing<IGoal>()).LifestyleTransient());

            container.Register(Component.For<IGoalCalculator>().ImplementedBy<FakeGoalCalculator>().LifestyleTransient());
        }
    }
}