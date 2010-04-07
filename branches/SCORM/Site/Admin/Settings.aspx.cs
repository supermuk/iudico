using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Admin_Settings : ControlledPage<Admin_SettingsController>
{
    protected override void BindController(Admin_SettingsController c)
    {
        base.BindController(c);
        Bind2Ways(tbSearchPattern, c.SearchPattern);
        Bind(btnSearch, DataBind);
    }

    public override void DataBind()
    {
        base.DataBind();
        SettingList.DataSource = Controller.GetSettings();
        SettingList.DataBind();
        btnCreateSetting.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_CreateSettingController { BackUrl = Request.Url.AbsolutePath });
    }
}
