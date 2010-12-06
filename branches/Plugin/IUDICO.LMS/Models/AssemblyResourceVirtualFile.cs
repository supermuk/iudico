using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Reflection;
using IUDICO.Common.Models.Plugin;

namespace IUDICO.LMS.Models
{
    public class AssemblyResourceVirtualFile : VirtualFile
    {
        private IEnumerable<IIudicoPlugin> plugins;
        private string path;

        public AssemblyResourceVirtualFile(IEnumerable<IIudicoPlugin> plugins, string virtualPath)
            : base(virtualPath)
        {
            this.plugins = plugins;
            path = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            string[] parts = path.Split(new char[] { '/' }, 4);
            string assemblyName = parts[2];
            string resourceName = parts[3];

            IIudicoPlugin plugin = plugins.Where(a => a.GetType().Assembly.ManifestModule.Name.ToLower() == assemblyName.ToLower()).SingleOrDefault();

            if (plugin != null)
            {
                return plugin.GetType().Assembly.GetManifestResourceStream(resourceName);
            }

            return null;
        }
    }
}