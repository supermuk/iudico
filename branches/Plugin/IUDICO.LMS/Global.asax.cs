using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Hosting;
using System.Reflection;
using System.IO;
using System.Security;
using System.Security.Permissions;
using IUDICO.LMS.Models;
using IUDICO.Common;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace IUDICO.LMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            

            /*
             * TODO ControllerBuilder.Current.SetControllerFactory(
             *  new ExtensionControllerFactory()
             * );
            */
            /*
            AppDomain pluginDomain = AppDomain.CreateDomain("pluginDomain");
            try
            {
                pluginDomain.DoCallBack(AssemblyChecker.GetCorrectAssemblies);
                //LoadPlugins();
            }
            finally
            {
                AppDomain.Unload(pluginDomain);
            }
            */
            

            Application["LMS"] = IUDICO.Common.Models.LMS.Instance;

            IEnumerable<IIudicoPlugin> plugins = LoadPlugins();
            RegisterViews(plugins);
            
            //AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes, plugins);
        }

        public IEnumerable<IIudicoPlugin> LoadPlugins()
        {
            IUDICO.Common.Models.LMS lms = Application["LMS"] as IUDICO.Common.Models.LMS;

            List<IIudicoPlugin> plugins = new List<IIudicoPlugin>();
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("/Plugins"));

            //foreach (String file in AssemblyChecker.correctAssemblies)
            foreach (FileInfo file in directory.GetFiles("*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(file.FullName);
                Type type = assembly.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface && typeof(IIudicoPlugin).IsAssignableFrom(t) /*t.GetInterface(typeof(IIudicoPlugin).FullName != null*/).SingleOrDefault();

                if (type != null)
                {
                    IIudicoPlugin plugin = Activator.CreateInstance(type) as IIudicoPlugin;
                    plugin.Initialize(lms);
                    lms.RegisterService(plugin.GetService());

                    plugins.Add(plugin);
                }
            }

            return plugins;
        }

        public void RegisterViews(IEnumerable<IIudicoPlugin> plugins)
        {
            // Register provider to load plugin resources
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider(plugins));

            // Register view engine to load views for loaded plugins
            List<String> ViewLocations = new List<string>();

            foreach (var plugin in plugins)
            {
                ViewLocations.Add("~/Plugins/" + plugin.GetType().Assembly.ManifestModule.Name + "/Views.{1}.{0}.aspx");
            }

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AssemblyViewEngine(ViewLocations.ToArray()));
        }

        public void RegisterRoutes(RouteCollection routes, IEnumerable<IIudicoPlugin> plugins)
        {
            // register routes from plugins
            foreach (var plugin in plugins)
            {
                plugin.RegisterRoutes(routes);
            }

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}