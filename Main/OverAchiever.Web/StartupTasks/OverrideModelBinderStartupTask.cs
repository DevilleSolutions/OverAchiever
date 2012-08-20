using System.Web.Mvc;
using OverAchiever.Infrastructure;

namespace OverAchiever.Web.StartupTasks
{
    public class OverrideModelBinderStartupTask : IStartupTask
    {
        private readonly IModelBinder _binder;

        public OverrideModelBinderStartupTask(IModelBinder binder)
        {
            _binder = binder;
        }

        public void Run()
        {
            ModelBinders.Binders.DefaultBinder = _binder;
        }
    }
}