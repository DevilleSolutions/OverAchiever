﻿using DevilleSolutions.Commons.MVC.Windsor;

namespace OverAchiever.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrap.WithWindsor();
        }
    }
}