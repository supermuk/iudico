using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;


namespace IUDICO.Common.Models
{
    public class ActionLink
    {
        public string ActionName;
        public string ControllerName;
        public RouteValueDictionary RouteValues;

        public ActionLink(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
        }
    }
}
