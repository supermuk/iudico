using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Common.Models.Plugin
{
    [Obsolete("Not in use")]
    [AttributeUsage(AttributeTargets.Assembly)]
    public class IudicoPluginAttribute : Attribute
    {
        protected string PluginName;

        public IudicoPluginAttribute(string pluginName)
        {
            PluginName = pluginName;
        }
    }
}