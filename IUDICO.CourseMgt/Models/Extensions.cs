using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.Common.Models;

namespace IUDICO.CourseMgt.Models
{
    public static class RouteCollectionExtentions
    {
        public static Route CustomMapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            var route = new Route(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            routes.Add(name, route);

            return route;
        }

        public static Route CustomMapRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return CustomMapRoute(routes, name, url, defaults, null);
        }
    }

    public static class DataContextExtensions
    {
        public static void ClearCache(this DataContext context)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var method = context.GetType().GetMethod("ClearCache", flags);

            method.Invoke(context, null);
        }
    }

    public static class ListExtensions
    {
        public static List<JsTreeNode> ToJsTreeList(this List<Node> list)
        {
            return (from n in list select new JsTreeNode(n.Id, n.Name, n.IsFolder)).ToList();
        }
    }
}