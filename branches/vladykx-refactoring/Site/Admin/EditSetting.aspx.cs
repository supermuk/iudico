using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Admin_EditSetting : ControlledPage<Admin_EditSettingController>
{
    protected override void BindController(Admin_EditSettingController c)
    {
        base.BindController(c);

        Bind(btnApply, c.EditSetting);
        Bind2Ways(tbValue, c.Value);

        btnCancel.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_SettingsController { BackUrl = Request.RawUrl });
    }

    public override void DataBind()
    {
        base.DataBind();

        lbName.Text = Controller.Setting.Name;
    }
}
