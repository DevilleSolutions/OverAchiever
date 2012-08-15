﻿using System.Web.Mvc;

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