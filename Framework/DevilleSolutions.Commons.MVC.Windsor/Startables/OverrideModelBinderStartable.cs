using System.Web.Mvc;
using Castle.Core;

namespace DevilleSolutions.Commons.MVC.Windsor.Startables
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