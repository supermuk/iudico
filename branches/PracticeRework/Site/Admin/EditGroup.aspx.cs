using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Admin_EditGroup : ControlledPage<Admin_EditGroupController>
{
    protected override void BindController(Admin_EditGroupController c)
    {
        base.BindController(c);
        Bind(btnApply, c.ApplyChanges);
        Bind2Ways(tbGroupName, c.GroupName);
        BindTitle(c.GroupName, gn => "Edit " + gn);
        Bind(lbGroupUsers, c.GroupName, gn => string.Format("Users participate in '{0}'", gn));
        UserList.ActionEnabled = UserList_ActionEnabled;
        UserList.ActionTitle = UserList_ActionTitle;
        UserList.ActionUrl = UserList_ActionUrl;
    }

    private string UserList_ActionUrl(TblUsers user)
    {
        return ServerModel.Forms.BuildRedirectUrl(new Admin_RemoveUserFromGroupController
        {
            BackUrl = Request.RawUrl, 
            GroupID = Controller.GroupID, 
            UserID = user.ID
        });
    }

    private bool UserList_ActionEnabled(TblUsers arg)
    {
        return true;
    }

    private string UserList_ActionTitle(TblUsers arg)
    {
        return "Exclude";
    }

    public override void DataBind()
    {
        base.DataBind();
        UserList.DataSource = Controller.GetUsers();
        UserList.DataBind();
    }
}
