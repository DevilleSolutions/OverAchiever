using System.Web.Mvc;
using Castle.Core;

namespace OverAchiever.Web.Startables
{
    public class RegisterGlobalFiltersStartable : IStartable
    {
        private readonly GlobalFilterCollection _filters;

        public RegisterGlobalFiltersStartable(GlobalFilterCollection filters)
        {
            _filters = filters;
        }

        public void Start()
        {
            _filters.Add(new HandleErrorAttribute());
        }

        public void Stop()
        {
        }
    }
}