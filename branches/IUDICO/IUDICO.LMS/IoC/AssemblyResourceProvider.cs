using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Reflection;
using System.IO;
using System.Web.Caching;
using System.Collections;

namespace IUDICO.LMS.IoC
{
    public class AssemblyResourceProvider : VirtualPathProvider
    {
        private readonly Dictionary<string, Assembly> nameAssemblyCache;

        public AssemblyResourceProvider()
        {
            nameAssemblyCache = new Dictionary<string, Assembly>(StringComparer.InvariantCultureIgnoreCase);
        }

        private bool IsAppResourcePath(string virtualPath)
        {
            string checkPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return checkPath.StartsWith("~/Plugins/",
                                        StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool FileExists(string virtualPath)
        {
            return (IsAppResourcePath(virtualPath) ||
                    base.FileExists(virtualPath));
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return new AssemblyResourceFile(nameAssemblyCache, virtualPath);
            }

            return base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(
            string virtualPath,
            IEnumerable virtualPathDependencies,
            DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return null;
            }

            return base.GetCacheDependency(virtualPath,
                                           virtualPathDependencies, utcStart);
        }

        private class AssemblyResourceFile : VirtualFile
        {
            private readonly IDictionary<string, Assembly> nameAssemblyCache;
            private readonly string assemblyPath;

            public AssemblyResourceFile(IDictionary<string, Assembly> nameAssemblyCache, string virtualPath) :
                base(virtualPath)
            {
                this.nameAssemblyCache = nameAssemblyCache;
                assemblyPath = VirtualPathUtility.ToAppRelative(virtualPath);
            }

            public override Stream Open()
            {
                // ~/Plugins/IUDICO.CourseManagment.dll/IUDICO.CourseManagment/Presentation/Views/Wiki/Index.aspx
                // ~/Plugins/IUDICO.CourseManagment.dll/IUDICO.CourseManagment/Scripts/Custom/jquery/jquery.js
                //          /   DLL name               /    Assembly Name     /           Path
                string[] parts = assemblyPath.Split(new[] { '/' }, 4);

                // TODO: should assert and sanitize 'parts' first
                string assemblyName = parts[2];
                string resourceName = parts[3].Replace('/', '.');

                Assembly assembly;

                lock (nameAssemblyCache)
                {
                    if (!nameAssemblyCache.TryGetValue(assemblyName, out assembly))
                    {
                        var path = Path.Combine(HttpContext.Current.Server.MapPath("/Plugins"), assemblyName);
                        assembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
                        

                        // TODO: Assert is not null
                        nameAssemblyCache[assemblyName] = assembly;
                    }
                }

                Stream resourceStream = null;

                if (assembly != null)
                {
                    resourceStream = assembly.GetManifestResourceStream(resourceName);
                }

                return resourceStream;
            }
        }
    }
}