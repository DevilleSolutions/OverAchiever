using System.Web.Mvc;
using Castle.Core;

namespace DevilleSolutions.Commons.MVC.Windsor.Startables
{
    public class SetupControllerBuilderStartable : IStartable
    {
        private readonly ControllerBuilder _controllerBuilder;
        private readonly IControllerFactory _controllerFactory;

        public SetupControllerBuilderStartable(ControllerBuilder controllerBuilder, IControllerFactory controllerFactory)
        {
            _controllerBuilder = controllerBuilder;
            _controllerFactory = controllerFactory;
        }

        public void Start()
        {
            _controllerBuilder.SetControllerFactory(_controllerFactory);
        }

        public void Stop()
        {
        }
    }
}