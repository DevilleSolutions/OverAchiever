using System.Web.Optimization;
using OverAchiever.Infrastructure;

namespace OverAchiever.Web.StartupTasks
{
    public class RegisterBundlesStartupTask : IStartupTask
    {
        private readonly BundleCollection _bundles;

        public RegisterBundlesStartupTask(BundleCollection bundles)
        {
            _bundles = bundles;
        }

        public void Run()
        {
            BundleConfig.RegisterBundles(_bundles);
        }
    }
}