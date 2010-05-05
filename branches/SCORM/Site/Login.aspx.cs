using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using System.Threading;
using System.Globalization;

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
        HyperLink hypSearch = Master.FindControl("HyperLink1") as HyperLink;
        hypSearch.Attributes.Add("style", "display:none;");
        base.OnLoad(e);
    }
    protected void Login1_Load(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
