using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.CourseMgt.Models.Storage;
//using MvcContrib.PortableAreas;
//using MvcContrib.UI.InputBuilder;
using System.Web.Hosting;
using IUDICO.LMS.Libraries;
using System.Reflection;
using IUDICO.Common;

namespace IUDICO.LMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterAssemblies();
            
            
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            

            //InputBuilder.BootStrap();
        }

        public void RegisterAssemblies()
        {
            List<String> ViewLocations = new List<string>();

            IEnumerable<Assembly> pluginAssemblies =
                        AppDomain.CurrentDomain.GetAssemblies().
                        Where(a => a.GetCustomAttributes(typeof(PluginAttribute), false).Count() > 0).AsEnumerable();

            // Register Assembly provider to load ~/Plugins/
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider(pluginAssemblies));

            foreach (Assembly a in pluginAssemblies)
            {
                ViewLocations.Add("~/Plugins/" + a.ManifestModule.ScopeName + "/Views.{1}.{0}.aspx");
            }

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new PluginViewEngine(ViewLocations.ToArray()));
        }
    }
}