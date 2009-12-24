using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class LoginPage : ControlledPage<LoginController>
{
    protected override void BindController(LoginController c)
    {
        base.BindController(c);
        Login1.Authenticate += c.Authenticate;
    }
}
