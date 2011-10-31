namespace IUDICO.LMS
{
    [System.Obsolete("Use Common istead")]
    public class Localization : IUDICO.Common.Localization { }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Reflection;
//using System.Threading;

//namespace IUDICO.LMS
//{
//    public class Localization : System.Web.Mvc.ViewPage
//    {
//        private static System.Resources.ResourceManager Manager;
//        private static bool already_initialized = false;

//        public static void Initialize()
//        {
//            Manager = new System.Resources.ResourceManager("IUDICO.LMS.Resource", Assembly.GetExecutingAssembly());
//            already_initialized = true;
//        }
//        public static string getMessage(string search)
//        {
//            if (!already_initialized) { Initialize(); }
//            try
//            {
//                return Manager.GetString(search, Thread.CurrentThread.CurrentUICulture);
//            }
//            catch (Exception)
//            {
//                return search;
//            }
//        }
//    }
//}