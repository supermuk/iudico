using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class Admin_RemoveSettingConfirmation : ControlledPage<Admin_RemoveSettingConfirmationController>
{
    protected override void BindController(Admin_RemoveSettingConfirmationController c)
    {
        base.BindController(c);
        Bind(btnYes, c.DoRemove);
        Title = "Removing Confirmation";
        btnNo.PostBackUrl = Controller.BackUrl;
    }

    public override void DataBind()
    {
        base.DataBind();
        lbConfirmationText.Text = "Do you really want to delete " + Controller.Setting.Name + "?";
    }
}
