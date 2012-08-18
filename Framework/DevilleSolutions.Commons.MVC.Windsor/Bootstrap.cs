using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DevilleSolutions.Commons.Extensions;

namespace DevilleSolutions.Commons.MVC.Windsor
{
    public static class Bootstrap
    {
        public static IWindsorContainer WithWindsor(params Type[] facilities)
        {
            var container = new WindsorContainer();

            if (facilities != null)
            {
                facilities.InParallel(facility => container.AddFacility((IFacility) Activator.CreateInstance(facility)));
            }

            Classes.FromThisAssembly()
                .BasedOn<IWindsorInstaller>()
                .InParallel(installer => container.Register(Component.For<IWindsorInstaller>()
                                                                     .Named(installer.FullName)
                                                                     .ImplementedBy(installer)
                                                                     .LifestyleTransient()));

            var installers = container.ResolveAll<IWindsorInstaller>();

            installers.InParallel(installer => container.Install(installer));
            installers.InParallel(container.Release);

            return container;
        }
    }
}