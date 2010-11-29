using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IUDICO.LMS.Libraries
{
    public class PluginViewEngine : WebFormViewEngine
    {
        public PluginViewEngine(string[] viewLocations)
            : base()
        {
            string[] allViewLocations = new string[ViewLocationFormats.Length + viewLocations.Length];
            
            ViewLocationFormats.CopyTo(allViewLocations, 0);
            viewLocations.CopyTo(allViewLocations, ViewLocationFormats.Length);

            PartialViewLocationFormats = ViewLocationFormats = allViewLocations;
        }

        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith("~/Areas/", StringComparison.InvariantCultureIgnoreCase);
        }

        //If we have a virtual path, we need to override the super class behavior,
        //its implementation ignores custom VirtualPathProviders, unlike the super's super class. 
        //This code basically just reimplements the super-super class (VirtualPathProviderViewEngine) behavior for virtual paths.
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.FileExists(virtualPath);
            }
            else
            {
                return base.FileExists(controllerContext, virtualPath);
            }
        }
    }
}