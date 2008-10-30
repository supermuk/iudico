using System.Web.Security;
using System.Web.UI.WebControls;

namespace IUDICO.DataModel.Controllers
{
    public class LoginController : PageControllerBase
    {
        public void Authenticate(object sender, AuthenticateEventArgs e)
        {
            var l = (Login) sender;
            e.Authenticated = FormsAuthentication.Authenticate(l.UserName, l.Password);
        }
    }
}
