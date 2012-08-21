using DevilleSolutions.Commons.MVC.Windsor;

namespace OverAchiever.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrap.WithWindsor();
        }
    }
}