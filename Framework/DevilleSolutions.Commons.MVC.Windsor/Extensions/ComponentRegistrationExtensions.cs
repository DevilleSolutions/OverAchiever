using System;
using Castle.MicroKernel.Registration;
using DevilleSolutions.Commons.Proxies;

namespace DevilleSolutions.Commons.MVC.Windsor.Extensions
{
    public static class ComponentRegistrationExtensions
    {
        public static ComponentRegistration<T> UsingProxy<T>(this ComponentRegistration<T> registration) where T : class
        {
            return registration.ImplementedBy(Proxy.Implementing<T>());
        }

        public static ComponentRegistration<object> UsingProxy(this ComponentRegistration registration, Type typeToProxy)
        {
            return registration.ImplementedBy(Proxy.Implementing(typeToProxy));
        }
    }
}