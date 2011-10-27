﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using IUDICO.Common.Controllers;

namespace IUDICO.Common.Models
{
    public static class Helpers
    {
        public static MvcHtmlString ResolveUrl(this HtmlHelper htmlHelper, string url)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var controller = htmlHelper.ViewContext.Controller;

            
            if (url[0] == '~' && controller is PluginController)
            {
                var assembly = controller.GetType().Assembly;
                var assemblyFileName = Path.GetFileName(assembly.Location);
                var assemblyName = assembly.GetName().Name;

                var pluginPath = string.Format("/Plugins/{0}/{1}", assemblyFileName, assemblyName);
                
                url = url.Insert(1, pluginPath);
            }

            return MvcHtmlString.Create(urlHelper.Content(url));
        }

        public static string Image(this HtmlHelper helper, string name, Guid id)
        {
            return Image(helper, name, id, null);
        }

        public static string Image(this HtmlHelper helper, string name, Guid id, object htmlAttributes)
        {
            string fileName = id.ToString() + ".png";
            string url = Path.Combine("~/Data/Avatars", Path.GetFileName(fileName));
            var tagBuilder = new TagBuilder("img");
            tagBuilder.GenerateId(name);
            tagBuilder.Attributes["src"] = new UrlHelper(helper.ViewContext.RequestContext).Content(url);
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return tagBuilder.ToString();
        }
    }
}
