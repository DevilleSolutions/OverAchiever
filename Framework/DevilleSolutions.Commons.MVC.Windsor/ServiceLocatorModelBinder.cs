using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace DevilleSolutions.Commons.MVC.Windsor
{
    public class ServiceLocatorModelBinder : DefaultModelBinder
    {
        private readonly IServiceLocator _serviceLocator;

        public ServiceLocatorModelBinder(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var model = _serviceLocator.GetInstance(modelType);
            return model;
        }
    }
}