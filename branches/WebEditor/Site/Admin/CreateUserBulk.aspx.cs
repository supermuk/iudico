using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using LEX.CONTROLS.Expressions;

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
        BindChecked2Ways(cbMakeStudent, c.MakeStudent);
        Bind2Ways(tbNewGroup, c.NewGroupName);
        Bind(cbGroups, c.Groups, c.SelectedGroupID);

        BindChecked2Ways(cbAddToGroup, c.AddToGroup);
        var newGroup = c.AddToGroup.And(new Equal<int>(c.SelectedGroupID, (-1).AsVariable()));
        BindVisible(lbNewGroup, newGroup);
        BindVisible(tbNewGroup, newGroup);
        BindVisible(cbGroups, c.AddToGroup);
    }
}
