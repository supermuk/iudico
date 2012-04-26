using System;
using CompileSystem.Classes.Compiling;

namespace CompileSystem
{
    public class Global : System.Web.HttpApplication
    {
        private static Compilers compilers;
        public static string CompilerDirectory = "Compilers";

        public static Compilers CurrentCompilers
        {
            get { return compilers; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            compilers = new Compilers(CompilerDirectory);
            compilers.Load();
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