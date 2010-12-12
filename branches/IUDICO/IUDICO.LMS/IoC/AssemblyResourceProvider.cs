using System;
using System.Collections.Generic;
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
        private readonly Dictionary<string, Assembly> _nameAssemblyCache;

        public AssemblyResourceProvider()
        {
            _nameAssemblyCache = new Dictionary<string, Assembly>(StringComparer.InvariantCultureIgnoreCase);
        }

        private static bool IsAppResourcePath(string virtualPath)
        {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return checkPath.StartsWith("~/Plugins/",
                                        StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool FileExists(string virtualPath)
        {
            return (IsAppResourcePath(virtualPath) || base.FileExists(virtualPath));
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return IsAppResourcePath(virtualPath)
                       ? new AssemblyResourceFile(_nameAssemblyCache, virtualPath)
                       : base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(
            string virtualPath,
            IEnumerable virtualPathDependencies,
            DateTime utcStart)
        {
            return IsAppResourcePath(virtualPath)
                       ? null
                       : base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private class AssemblyResourceFile : VirtualFile
        {
            private readonly IDictionary<string, Assembly> _NameAssemblyCache;
            private readonly string _AssemblyPath;

            public AssemblyResourceFile(IDictionary<string, Assembly> nameAssemblyCache, string virtualPath) :
                base(virtualPath)
            {
                this._NameAssemblyCache = nameAssemblyCache;
                _AssemblyPath = VirtualPathUtility.ToAppRelative(virtualPath);
            }

            public override Stream Open()
            {
                // ~/Plugins/IUDICO.CourseManagment.dll/IUDICO.CourseManagment/Scripts/Custom/jquery/jquery.js
                //          /   DLL name               /    Assembly Name     /           Path
                var parts = _AssemblyPath.Split(new[] { '/' }, 4);

                // TODO: should assert and sanitize 'parts' first
                var assemblyName = parts[2];
                var resourceName = parts[3].Replace('/', '.');

                Assembly assembly;

                lock (_NameAssemblyCache)
                {
                    if (!_NameAssemblyCache.TryGetValue(assemblyName, out assembly))
                    {
                        var path = Path.Combine(HttpContext.Current.Server.MapPath("/Plugins"), assemblyName);
                        assembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
                        

                        // TODO: Assert is not null
                        _NameAssemblyCache[assemblyName] = assembly;
                    }
                }

                Stream resourceStream = null;

                if (assembly != null)
                {
                    resourceStream = assembly.GetManifestResourceStream(resourceName);
                }

                if (resourceStream == null)
                    throw new HttpException(404, "Resource " + resourceName + " not found.");

                return resourceStream;
            }
        }
    }
}