using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Threading;
using System.IO;
using System.Resources;
using System.Reflection;

namespace IUDICO.Common
{
    public class Localization : System.Web.Mvc.ViewPage
    {
        private static System.Resources.ResourceManager Manager;

        public static void Initialize(System.Resources.ResourceManager recourceManager)
        {
            Manager = recourceManager;
        }
        public static string getMessage(string search)
        {
            try
            {
                return Manager.GetString(search, Thread.CurrentThread.CurrentUICulture);
            }
            catch (Exception)
            {
                return search;
            }
        }
    }
}