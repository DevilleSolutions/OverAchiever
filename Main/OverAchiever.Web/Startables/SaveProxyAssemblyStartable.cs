using Castle.Core;
using DevilleSolutions.Commons.Proxies;

namespace OverAchiever.Web.Startables
{
#if DEBUG
    public class SaveProxyAssemblyStartable : IStartable
    {
        public void Start()
        {
            // Proxy.SaveProxyAssembly();
        }

        public void Stop()
        {
        }
    }
#endif
}