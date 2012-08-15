using System.Web.Mvc;
using OverAchiever.Infrastructure;

namespace OverAchiever.Web.StartupTasks
{
    public class RegisterGlobalFiltersStartupTask : IStartupTask
    {
        private readonly GlobalFilterCollection _filters;

        public RegisterGlobalFiltersStartupTask(GlobalFilterCollection filters)
        {
            _filters = filters;
        }

        public void Run()
        {
            FilterConfig.RegisterGlobalFilters(_filters);
        }
    }
}