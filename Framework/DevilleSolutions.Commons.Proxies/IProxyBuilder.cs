using System;

namespace DevilleSolutions.Commons.Proxies
{
    public interface IProxyBuilder
    {
        IProxyBuilder Implementing(Type t);
        IProxyBuilder IncludeADefaultConstructor();

        Type Build();

        void SaveProxyAssemblies();
    }
}