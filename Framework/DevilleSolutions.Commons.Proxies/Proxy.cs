using System;

namespace DevilleSolutions.Commons.Proxies
{
    public static class Proxy
    {
        private static readonly ProxyBuilder ProxyBuilder = new ProxyBuilder();

        public static Type Implementing(Type iface)
        {
            return ProxyBuilder.BuildTypeImplementing(iface);
        }

        public static Type Implementing<T>()
            where T : class
        {
            return Implementing(typeof (T));
        }
    }
}