using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace IUDICO.Common.Models
{
    public static class Helpers
    {
        public static string Content(this UrlHelper urlHelper, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
            {
                throw new ArgumentException("Null or Empty", "contentPath");
            }
            
            if (contentPath[0] == '~')
            {
                return VirtualPathUtility.ToAbsolute(contentPath, urlHelper.RequestContext.HttpContext.Request.ApplicationPath);
            }

            return contentPath;
        }

        public static MvcHtmlString ResolveUrl(this HtmlHelper htmlHelper, string url)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return MvcHtmlString.Create(urlHelper.Content(url));
        }
    }
}
