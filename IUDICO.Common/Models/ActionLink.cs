using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;


namespace IUDICO.Common.Models
{
    /// <summary>
    /// Represents value object for passing routing data.
    /// May be used in Html.ActionLink helper.
    /// </summary>
    public class ActionLink
    {
        public readonly string ActionName;
        public readonly string ControllerName;
        public readonly RouteValueDictionary RouteValues;

        public ActionLink(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.RouteValues = routeValues;
        }

        public override string ToString()
        {
            String routeValuesString = "";
            if (this.RouteValues.ContainsKey("id"))
            {
                routeValuesString = "/" + this.RouteValues["id"].ToString();
            }
            String result = String.Format("{0}/{1}{2}", this.ControllerName, this.ActionName, routeValuesString);
            return result;
        }
    }
}
