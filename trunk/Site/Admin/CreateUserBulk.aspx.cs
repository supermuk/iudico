using System;
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
        Bind(cbGroups, c.Groups, c.SelectedGroupID);
        BindChecked2Ways(cbMakeStudent, c.MakeStudent);
        Bind2Ways(tbNewGroup, c.NewGroupName);
        //BindVisible(tbNewGroup, new MoreThan<int>(c.SelectedGroupID, 0.AsVariable())););
    }
}
