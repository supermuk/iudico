using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class LoginPage : ControlledPage<LoginController>
{
    protected override void BindController(LoginController c)
    {
        base.BindController(c);
        Login1.Authenticate += c.Authenticate;
    }
    protected override void OnLoad(EventArgs e)
    {
        HyperLink hyp = Master.FindControl("hypLogout") as HyperLink;
        hyp.Attributes.Add("style", "display:none;");
        base.OnLoad(e);
    }
}
