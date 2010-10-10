using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class Admin_CreateSetting : ControlledPage<Admin_CreateSettingController>
{
    protected override void BindController(Admin_CreateSettingController c)
    {
        base.BindController(c);

        Bind(btnCreate, c.CreateSetting);
        Bind2Ways(tbName, c.Name);
        Bind2Ways(tbValue, c.Value);
        Bind(lbErrors, c.ErrorText);
    }
}
