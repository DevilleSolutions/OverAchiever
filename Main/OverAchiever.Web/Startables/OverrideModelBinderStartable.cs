using System.Web.Mvc;
using Castle.Core;

namespace OverAchiever.Web.Startables
{
    public class OverrideModelBinderStartable : IStartable
    {
        private readonly IModelBinder _binder;

        public OverrideModelBinderStartable(IModelBinder binder)
        {
            _binder = binder;
        }

        public void Start()
        {
            ModelBinders.Binders.DefaultBinder = _binder;
        }

        public void Stop()
        {
        }
    }
}