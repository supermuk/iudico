using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Reflection;
using System.IO;

namespace IUDICO.LMS.Libraries
{
    public class AssemblyResourceProvider : VirtualPathProvider
    {
        private IEnumerable<Assembly> pluginAssemblies;

        public AssemblyResourceProvider(IEnumerable<Assembly> pluginAssemblies)
        {
            this.pluginAssemblies = pluginAssemblies;
        }

        public override bool FileExists(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                string path = VirtualPathUtility.ToAppRelative(virtualPath);
                string[] parts = path.Split(new char[] {'/'}, 4);
                string assemblyName = parts[2];
                string resourceName = parts[3];

                /*
                Assembly assembly = pluginAssemblies.Where(a => a.FullName == assemblyName).SingleOrDefault() ;

                if (assembly == null)
                {
                    return false;
                }
                */

                assemblyName = Path.Combine(HttpRuntime.BinDirectory, assemblyName);
                byte[] assemblyBytes = File.ReadAllBytes(assemblyName);
                Assembly assembly = Assembly.Load(assemblyBytes);

                if (assembly != null)
                {
                    string[] resourceList = assembly.GetManifestResourceNames();
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
                return new AssemblyResourceVirtualFile(virtualPath);
            }
            else
            {
                return base.GetFile(virtualPath);
            }
        }

        public override System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath, System.Collections.IEnumerable virtualPathDependencies, DateTime utcStart)
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