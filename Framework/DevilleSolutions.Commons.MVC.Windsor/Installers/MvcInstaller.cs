using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DevilleSolutions.Commons.Extensions;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace DevilleSolutions.Commons.MVC.Windsor.Installers
{
    public class MvcInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<GlobalFilterCollection>().Instance(GlobalFilters.Filters));
            container.Register(Component.For<RouteCollection>().Instance(RouteTable.Routes));
            container.Register(Component.For<BundleCollection>().Instance(BundleTable.Bundles));
            container.Register(Component.For<ControllerBuilder>().Instance(ControllerBuilder.Current));
            container.Register(Component.For<IDependencyResolver>().ImplementedBy<WindsorDependencyResolver>());
            container.Register(Component.For<IModelBinder>().ImplementedBy<ServiceLocatorModelBinder>());
            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerManager>());

            var assemblies = container.ResolveAll<Assembly>();

            Classes.FromAssemblies(assemblies)
                   .BasedOn<IHttpController>()
                   .ForEach(controller => container.Register(Component.For(controller).ImplementedBy(controller).LifestyleScoped()));

            Classes.FromAssemblies(assemblies)
                   .BasedOn<Controller>()
                   .ExcludingTypesBasedOn<IHttpController>()
                   .ForEach(controller => container.Register(Component.For(controller).ImplementedBy(controller).LifestyleTransient()));
        }
    }
}