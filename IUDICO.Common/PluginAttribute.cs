using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Common
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class PluginAttribute : Attribute
    {
        protected string ViewPath;

        public PluginAttribute()
        {
            ViewPath = this.GetType().Namespace;
        }
    }
}