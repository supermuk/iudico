using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Threading;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Linq;
using Castle.Windsor;

namespace IUDICO.Common
{
    using System.Web.Mvc;

    public class Localization
    {
        private static readonly string[] Cultures = new[] { "en-US", "uk-UA" };
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> resource = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        private static Localization instance;
        protected IWindsorContainer container;
        
        protected Localization(IWindsorContainer container)
        {
            this.container = container;
            this.LoadResource("IUDICO.LMS");
        }
        
        public static void Init(IWindsorContainer container)
        {
            if (instance == null)
            {
                instance = new Localization(container);
            }
        }

        protected void LoadResource(string pluginName)
        {
            string path;

            try
            {
                path = HttpContext.Current.Server.MapPath("/").Replace("IUDICO.LMS", pluginName);
            }
            catch (Exception)
            {
                path = Path.Combine(Assembly.GetExecutingAssembly().CodeBase.Remove(0, 6) + @"\..\..\..\..\", pluginName) + @"\";
            }

            this.resource.Add(pluginName, new Dictionary<string, Dictionary<string, string>>());

            foreach (var culture in Cultures)
            {
                try
                {
                    var resourceReader = new ResXResourceReader(path + "Resource." + culture + ".resx");
                    var resourceEntries = resourceReader.Cast<DictionaryEntry>().ToDictionary(d => d.Key.ToString(), d => d.Value.ToString());

                    this.resource[pluginName].Add(culture, resourceEntries);
                }
                catch (Exception)
                {
                    
                }
            }
        }

        public static string GetMessage(string search)
        {
            var pluginName = Assembly.GetCallingAssembly().GetName().Name;
            
            if (pluginName.IndexOf("IUDICO.") != 0)
            {
                var c = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                var controller = instance.container.Resolve<IControllerFactory>().CreateController(HttpContext.Current.Request.RequestContext, c);
                pluginName = controller.GetType().Assembly.GetName().Name;
            }

            return GetMessage(search, pluginName);
        }

        public static string GetMessage(string search, string pluginName)
        {
            return instance.FindMessage(pluginName, search);
        }

        protected string FindMessage(string pluginName, string search)
        {
            if (!this.resource.ContainsKey(pluginName))
            {
                this.LoadResource(pluginName);
            }

            try
            {
                if (this.resource[pluginName][Thread.CurrentThread.CurrentUICulture.Name].ContainsKey(search))
                {
                    return this.resource[pluginName][Thread.CurrentThread.CurrentUICulture.Name][search];
                }
                else if (this.resource["IUDICO.LMS"][Thread.CurrentThread.CurrentUICulture.Name].ContainsKey(search))
                {
                    return this.resource["IUDICO.LMS"][Thread.CurrentThread.CurrentUICulture.Name][search];
                }
                else if (this.resource["IUDICO.Common"][Thread.CurrentThread.CurrentUICulture.Name].ContainsKey(search))
                {
                    return this.resource["IUDICO.Common"][Thread.CurrentThread.CurrentUICulture.Name][search];
                }
            }
            catch (Exception)
            {
            }

            return "#" + search;
        }
    }
}