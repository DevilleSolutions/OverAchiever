namespace DevilleSolutions.Commons.Proxies
{
    public static class ProxyBuilderExtensions
    {
         public static IProxyBuilder Implementing<T>(this IProxyBuilder builder)
         {
             return builder.Implementing(typeof (T));
         }
    }
}