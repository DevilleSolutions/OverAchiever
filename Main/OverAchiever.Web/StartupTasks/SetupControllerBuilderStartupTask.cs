using System.Web.Mvc;
using OverAchiever.Infrastructure;

namespace OverAchiever.Web.StartupTasks
{
    public class SetupControllerBuilderStartupTask : IStartupTask
    {
        private readonly ControllerBuilder _controllerBuilder;
        private readonly IControllerFactory _controllerFactory;

        public SetupControllerBuilderStartupTask(ControllerBuilder controllerBuilder, IControllerFactory controllerFactory)
        {
            _controllerBuilder = controllerBuilder;
            _controllerFactory = controllerFactory;
        }

        public void Run()
        {
            _controllerBuilder.SetControllerFactory(_controllerFactory);
        }
    }
}