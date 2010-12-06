using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using IUDICO.Common.Controllers;
using Microsoft.Win32;

namespace IUDICO.LMS.Controllers
{
    class ResourceController : BaseController
    {
        public ActionResult Index(string plugin, string resourceName)
        {
            var contentType = GetContentType(resourceName);
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            return this.File(resourceStream, contentType);
        }

        private static string GetContentTypeByExtension(string resourceName)
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

        private static string GetContentType(string resourceName)
        {
            string mimeType = string.Empty;
            var extention = resourceName.Substring(resourceName.LastIndexOf('.')).ToLower();

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(
                extention
            );

            if (regKey != null)
            {
                object contentType = regKey.GetValue("Content Type");

                if (contentType != null)
                    mimeType = contentType.ToString();
            }

            if (mimeType == string.Empty)
            {
                mimeType = GetContentTypeByExtension(resourceName);
            }

            return mimeType;
        }
    }
}