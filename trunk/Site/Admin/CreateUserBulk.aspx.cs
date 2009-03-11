using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class Admin_CreateUserBulk : ControlledPage<Admin_CreateBulkUserController>
{
    protected override void BindController(Admin_CreateBulkUserController c)
    {
        base.BindController(c);

        Bind(btnCreate, c.DoCreate);
        Bind2Ways(tbPrefix, c.Prefix);
        Bind2Ways(tbCount, c.Count);
        Bind2Ways(tbPassword, c.Password);
        Bind(lbErrors, c.ErrorText);
    }
}
