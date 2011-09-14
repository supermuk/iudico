using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Resources;

namespace IUDICO.CurriculumManagement
{
    public class Localization : System.Web.Mvc.ViewPage
    {
        private static Dictionary<string, Dictionary<string, string>> resource;

        public static void Initialize()
        {
            string path = HttpContext.Current.Server.MapPath("/").Replace("IUDICO.LMS", "IUDICO.CurriculumManagement");
            ResXResourceReader rsxr = new ResXResourceReader(path + "Resource" + ".resx");

            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (DictionaryEntry d in rsxr)
            {
                temp.Add(d.Key.ToString(), d.Value.ToString());
            }
            resource = new Dictionary<string, Dictionary<string, string>>();
            resource.Add("en-US", temp);
            rsxr = new ResXResourceReader(path + "Resource" + ".uk" + ".resx");
            temp = new Dictionary<string, string>();
            foreach (DictionaryEntry d in rsxr)
            {
                temp.Add(d.Key.ToString(), d.Value.ToString());
            }
            resource.Add("uk-UA", temp);
        }
        public static string getMessage(string search)
        {
            try
            {
                return resource[Thread.CurrentThread.CurrentUICulture.Name][search];
            }
            catch (Exception)
            {
                return search;
            }
        }
    }
}