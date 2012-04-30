using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IUDICO.Common.Models.Attributes
{
    public class AllowAttribute : AuthorizeAttribute
    {
        protected new string Roles { get; set; }
        public Role Role { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if (Role == Role.None)
            {
                return true;
            }

            var userRoles = System.Web.Security.Roles.Provider.GetRolesForUser(httpContext.User.Identity.Name).Select(UserRoles.GetRole);

            return userRoles.Any(r => (r & Role) != 0);
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
