using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Threading;

namespace IUDICO.TestingSystem
{
    public class Localization : System.Web.Mvc.ViewPage
    {
        private static System.Resources.ResourceManager ManagerEN;
        private static System.Resources.ResourceManager ManagerUK;

        public static void Initialize()
        {
            string a = Assembly.GetExecutingAssembly().FullName;
            ManagerEN = new System.Resources.ResourceManager("IUDICO.TestingSystem.FramesetResources", Assembly.GetExecutingAssembly());
            ManagerUK = new System.Resources.ResourceManager("IUDICO.TestingSystem.FramesetResourcesuk", Assembly.GetExecutingAssembly());
        }
        public static string getMessage(string search)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == "en")
            {
                return ManagerEN.GetString(search, Thread.CurrentThread.CurrentUICulture);
            }
            else
            {
                return ManagerUK.GetString(search, Thread.CurrentThread.CurrentUICulture);
            }
        }
    }
}