using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;

namespace IUDICO.Common.Controllers
{
    class ResourceController : BaseController
    {
        public ActionResult Index(string plugin, string resourceName)
        {
            var contentType = GetContentType(resourceName);
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            return this.File(resourceStream, contentType);
        }

        private static string GetContentType(string resourceName)
        {
            var extention = resourceName.Substring(resourceName.LastIndexOf('.')).ToLower();
            switch (extention)
            {
                case ".gif":
                    return "image/gif";
                case ".js":
                    return "text/javascript";
                case ".css":
                    return "text/css";
                default:
                    return "text/html";
            }
        }
    }
}