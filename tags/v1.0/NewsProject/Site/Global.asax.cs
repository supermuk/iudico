using System;
using System.Web.Configuration;
using LEX.CONTROLS;
using System.Web;

namespace Site
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ServerModel.Initialize(WebConfigurationManager.ConnectionStrings["NEWS"].ConnectionString, HttpRuntime.Cache);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            ServerModel.UnInitialize();
        }
    }
}