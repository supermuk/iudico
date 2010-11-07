using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebEditor.Models
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
}