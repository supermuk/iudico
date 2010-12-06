using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Reflection;

namespace IUDICO.LMS.Libraries
{
    [Obsolete("Move '/Plugins/' to config")]
    public class AssemblyResourceVirtualFile : VirtualFile
    {
        private string path;

        public AssemblyResourceVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            path = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            string[] parts = path.Split(new char[] { '/' }, 4);
            string assemblyName = parts[2];
            string resourceName = parts[3];

            assemblyName = Path.Combine("/Plugins/", assemblyName);
            byte[] assemblyBytes = File.ReadAllBytes(assemblyName);
            Assembly assembly = Assembly.Load(assemblyBytes);

            if (assembly != null)
            {
                return assembly.GetManifestResourceStream(resourceName);
            }

            return null;
        }
    }
}