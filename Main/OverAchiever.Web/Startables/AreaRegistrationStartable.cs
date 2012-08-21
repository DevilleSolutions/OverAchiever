using System.Web.Mvc;
using Castle.Core;

namespace OverAchiever.Web.Startables
{
    public class AreaRegistrationStartable : IStartable
    {
        public void Start()
        {
            AreaRegistration.RegisterAllAreas();
        }

        public void Stop()
        {
        }
    }
}