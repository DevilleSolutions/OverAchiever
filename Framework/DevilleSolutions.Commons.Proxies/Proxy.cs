using System;

namespace DevilleSolutions.Commons.Proxies
{
    public static class Proxy
    {
        private static IProxyBuilder _proxyBuilder = new ProxyBuilder();

        public static Type Implementing(Type iface)
        {
            return _proxyBuilder.Implementing(iface).Build();
        }

        public static Type Implementing<T>()
            where T : class
        {
            return Implementing(typeof (T));
        }

        public static void SaveProxyAssembly()
        {
            _proxyBuilder.SaveProxyAssemblies();
        }
    }
}