﻿using Castle.MicroKernel.Registration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace OverAchiever.Web.Installers
{
    public class FactoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Types.FromThisAssembly()
                                   .Where(t => t.Name.EndsWith("Factory"))
                                   .Configure(cr => cr.AsFactory())
                                   .LifestyleSingleton());
        }
    }
}