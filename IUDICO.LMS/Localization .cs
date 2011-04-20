using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Threading;

namespace IUDICO.LMS
{
    public class Localization : System.Web.Mvc.ViewPage
    {
        private static System.Resources.ResourceManager Manager;

        public static void Initialize()
        {
            string a = Assembly.GetExecutingAssembly().FullName;
            Manager = new System.Resources.ResourceManager("IUDICO.LMS.Resource", Assembly.GetExecutingAssembly());
        }
        public static string getMessage(string search)
        {
            Initialize();
            var text = Manager.GetString(search, Thread.CurrentThread.CurrentUICulture);

            if (text == null)
                return "null";
            else if (text == string.Empty)
                return "empty";
            else
                return text;
        }
    }
}