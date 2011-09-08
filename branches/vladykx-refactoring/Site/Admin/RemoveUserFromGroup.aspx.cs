using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class Admin_RemoveUserFromGroup : ControlledPage<Admin_RemoveUserFromGroupController>
{
    public override void DataBind()
    {
        base.DataBind();
        lbConfirmationText.Text = string.Format("Do you really want to exclude {0} from {1}?", Controller.User.DisplayName, Controller.Group.Name);
        btnNo.PostBackUrl = Controller.BackUrl;
    }

    protected void DoExclude(object sender, EventArgs e)
    {
        Controller.DoExclude();
        Response.Redirect(btnNo.PostBackUrl);
    }
}
