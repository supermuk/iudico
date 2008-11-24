using System.Web.Security;
using System.Web.UI.WebControls;

namespace IUDICO.DataModel.Controllers
{
    public class LoginController : ControllerBase
    {
        public void Authenticate(object sender, AuthenticateEventArgs e)
        {
            var l = (Login) sender;
            e.Authenticated = Membership.ValidateUser(l.UserName, l.Password);
        }
    }
}
