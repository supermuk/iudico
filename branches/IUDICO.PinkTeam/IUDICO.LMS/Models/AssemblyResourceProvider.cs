using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Reflection;
using System.IO;
using System.Web.Caching;
using System.Collections;
using IUDICO.Common.Models.Plugin;

namespace IUDICO.LMS.Models
{
    public class AssemblyResourceProvider : VirtualPathProvider
    {
        private IEnumerable<IIudicoPlugin> plugins;

        public AssemblyResourceProvider(IEnumerable<IIudicoPlugin> plugins)
        {
            this.plugins = plugins;
        }

        public override bool FileExists(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                string path = VirtualPathUtility.ToAppRelative(virtualPath);
                string[] parts = path.Split(new char[] {'/'}, 4);
                string assemblyName = parts[2];
                string resourceName = parts[3];

                IIudicoPlugin plugin = plugins.Where(a => a.GetType().Assembly.ManifestModule.Name.ToLower() == assemblyName.ToLower()).SingleOrDefault();

                if (plugin != null)
                {
                    string[] resourceList = plugin.GetType().Assembly.GetManifestResourceNames();
                    bool found = Array.Exists(resourceList, delegate(string r) { return r.Equals(resourceName); });

                    return found;
                }

                return false;
            }
            else
            {
                return base.FileExists(virtualPath);
            }
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return new AssemblyResourceVirtualFile(plugins, virtualPath);
            }
            else
            {
                return base.GetFile(virtualPath);
            }
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return null;
            }

            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return checkPath.StartsWith("~/Plugins/", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}