using System.Web.Mvc;
using IUDICO.Common.Controllers;
using System.IO;

namespace IUDICO.Common.Models
{
    public static class Helpers
    {
        public static MvcHtmlString ResolveUrl(this HtmlHelper htmlHelper, string url)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var controller = htmlHelper.ViewContext.Controller;

            
            if (url.StartsWith("~/") && controller is PluginController)
            {
                var assembly = controller.GetType().Assembly;
                var assemblyFileName = Path.GetFileName(assembly.Location);
                var assemblyName = assembly.GetName().Name;

                var pluginPath = string.Format("Plugins/{0}/{1}/", assemblyFileName, assemblyName);
                
                url = url.Insert(2, pluginPath);
            }

            return MvcHtmlString.Create(urlHelper.Content(url));
        }
    }
}
