using System.Web.Mvc;

namespace OverAchiever.Web.StartupTasks
{
    public class AreaRegistrationStartupTask : IStartupTask
    {
        public void Run()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}