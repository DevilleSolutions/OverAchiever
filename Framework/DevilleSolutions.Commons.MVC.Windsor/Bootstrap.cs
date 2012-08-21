using System.Linq;
using System.Reflection;
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

        public static IWindsorContainer WithWindsor(params Assembly[] assemblies)
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
                _container.AddFacility<TypedFactoryFacility>();
                _container.AddFacility<StartableFacility>();


                var allAssemblies = assemblies.Union(new[] { Assembly.GetExecutingAssembly(), Assembly.GetCallingAssembly() }).Distinct().ToArray();

                allAssemblies.ForEach(assembly => _container.Register(Component.For<Assembly>().NamedAutomatically(assembly.FullName).Instance(assembly)));

                _container.InstallAssemblies(allAssemblies);

                var installers = _container.ResolveAll<IWindsorInstaller>();

                installers.ForEach(installer => _container.Install(installer));
                installers.ForEach(_container.Release);
            }
            return _container;
        }

        public static void InstallAssemblies(this IWindsorContainer container, params Assembly[] assemblies)
        {
            Classes.FromAssemblies(assemblies)
                   .BasedOn<IWindsorInstaller>()
                   .ForEach(installer => container.Register(Component.For<IWindsorInstaller>()
                                                     .ImplementedBy(installer)
                                                     .LifestyleTransient()));
        }
    }
}