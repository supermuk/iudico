using System;
using CompileSystem.Classes.Compiling;

namespace CompileSystem
{
    public class Global : System.Web.HttpApplication
    {
        private static Compilers _compilers;
        public static string compilerDirectory = "Compilers";

        public static Compilers CurrentCompilers
        {
            get { return _compilers; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            _compilers = new Compilers(compilerDirectory);
            _compilers.Load();
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

        }
    }
}