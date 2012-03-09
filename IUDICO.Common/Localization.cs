using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading;
using System.IO;
using System.Resources;
using System.Reflection;

namespace IUDICO.Common
{
    public class LocalizationMessageProvider : System.Web.Mvc.ViewPage
    {
        private static string[] cultures = new[] { "en-US", "uk-UA" };
        private Dictionary<string, Dictionary<string, string>> resource = new Dictionary<string, Dictionary<string, string>>();

        public LocalizationMessageProvider(string pluginName)
        {
            var pluginNameDymanic = Assembly.GetCallingAssembly().GetName().Name;

            string path = "";
            //NOTE: Modified by terminadoor@gmail.com original is at revision 1668
            try
            {
                path = HttpContext.Current.Server.MapPath("/").Replace("IUDICO.LMS", "IUDICO." + pluginName);
            }
            catch(Exception exception)
            {
                path = Assembly.GetExecutingAssembly().CodeBase;
                path = Path.GetDirectoryName(path);
                path = Path.GetDirectoryName(path);
                path = Path.GetDirectoryName(path);
                path = Path.GetDirectoryName(path);
                path = Path.Combine(path, "IUDICO.LMS");
                path=path.Replace("IUDICO.LMS", "IUDICO." + pluginName);
                path = path.Remove(0, 6);
                path += @"\";
            }   
            //NOTE: End of modifiation from revision 1668
            foreach (var culture in cultures)
            {
                var rsxr = new ResXResourceReader(path + "Resource." + culture + ".resx");

                Dictionary<string, string> temp = new Dictionary<string, string>();
                foreach (DictionaryEntry d in rsxr)
                {
                    temp.Add(d.Key.ToString(), d.Value.ToString());
                }

                resource.Add(culture, temp);
            }
        }
        public string getMessage(string search)
        {
            try
            {
                return resource[Thread.CurrentThread.CurrentUICulture.Name][search];
            }
            catch (Exception)
            {
                return "#" + search;
            }
        }
    }

    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("Common");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}