using DevilleSolutions.Commons.Proxies;
using OverAchiever.Infrastructure;

namespace OverAchiever.Web.StartupTasks
{
    // We currently only want to spit out the proxy assembly if in debug mode for looking at with Reflector, in the future
    // may wish to do some fancy assembly saving/loading if exists, but for now just gonna regen at runtime.
#if DEBUG
    public class SaveProxyAssemblyStartupTask : IStartupTask
    {
        public void Run()
        {
            // Proxy.SaveProxyAssembly();
        }
    }
#endif
}