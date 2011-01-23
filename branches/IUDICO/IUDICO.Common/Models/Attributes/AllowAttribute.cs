using System.Web;
using System.Web.Mvc;

namespace IUDICO.Common.Models.Attributes
{
    public class AllowAttribute : AuthorizeAttribute
    {
        protected string Roles { get; set; }
        public Role Role { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return System.Web.Security.Roles.Provider.IsUserInRole(httpContext.User.Identity.Name, Role.ToString());
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
            }
        }
    }
}
