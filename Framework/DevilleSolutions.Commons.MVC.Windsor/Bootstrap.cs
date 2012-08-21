using System.Reflection;
using Castle.Core;
using Castle.Facilities.Startable;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DevilleSolutions.Commons.Extensions;

namespace DevilleSolutions.Commons.MVC.Windsor
{
    public static class Bootstrap
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer WithWindsor()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
                _container.AddFacility<TypedFactoryFacility>();
                _container.AddFacility<StartableFacility>();

                _container.InstallAssemblies(Assembly.GetExecutingAssembly(), Assembly.GetCallingAssembly());

                var installers = _container.ResolveAll<IWindsorInstaller>();

                installers.InParallel(installer => _container.Install(installer));
                installers.InParallel(_container.Release);

                _container.RegisterStartables(Assembly.GetExecutingAssembly(), Assembly.GetCallingAssembly());

            }
            return _container;
        }

        public static void InstallAssemblies(this IWindsorContainer container, params Assembly[] assemblies)
        {
            Classes.FromAssemblies(assemblies)
                   .BasedOn<IWindsorInstaller>()
                   .InParallel(installer => container.Register(Component.For<IWindsorInstaller>()
                                                     .ImplementedBy(installer)
                                                     .LifestyleTransient()));
        }

        public static void RegisterStartables(this IWindsorContainer container, params Assembly[] assemblies)
        {
            Classes.FromAssemblies(assemblies)
                   .BasedOn<IStartable>()
                   .ForEach(startable => container.Register(Component.For<IStartable>()
                                                     .ImplementedBy(startable)
                                                     .LifestyleSingleton()));
        }
    }
}