using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class Admin_IncludeUserIntoGroup : ControlledPage<Admin_IncludeUserIntoGroupController>
{
    public override void DataBind()
    {
        base.DataBind();
        lbConfirmationText.Text = string.Format("Do you really want to include {0} to {1}?", Controller.User.DisplayName, Controller.Group.Name);
        btnNo.PostBackUrl = Controller.BackUrl;
    }

    protected void DoInclude(object sender, EventArgs e)
    {
        Controller.DoInclude();
        Response.Redirect(btnNo.PostBackUrl);
    }
}
