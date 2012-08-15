using System.Web.Mvc;
using OverAchiever.Infrastructure;

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