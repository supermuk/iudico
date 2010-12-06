using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using IUDICO.Common.Models.Plugin;

namespace IUDICO.LMS.Models
{
    public class PluginAssembly
    {
        protected Assembly assembly;
        protected IIudicoPlugin plugin;

        public String Name
        {
            get
            {
                return assembly.ManifestModule.Name;
            }
        }

        public Assembly Assembly
        {
            get
            {
                return assembly;
            }
        }

        public IIudicoPlugin Plugin
        {
            get
            {
                return plugin;
            }
        }

        public PluginAssembly(Assembly a, IIudicoPlugin p)
        {
            assembly = a;
            plugin = p;
        }
    }
}