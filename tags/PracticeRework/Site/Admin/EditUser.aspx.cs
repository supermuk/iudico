using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Admin_EditUser : ControlledPage<Admin_EditUserController>
{
    protected override void BindController(Admin_EditUserController c)
    {
        base.BindController(c);

        var currentUser = ServerModel.User.Current;
        var isAdmin = currentUser.IsAdmin();

        Bind(btnApply, c.ApplyRoles);
        BindEnabled(btnApply, isAdmin);
        BindEnabled(btnInclude, isAdmin);

        Bind(lbStudentRoleTitle, FxRoles.STUDENT.Name);
        Bind(lbTrainerRoleTitle, FxRoles.TRAINER.Name);
        Bind(lbLectorRoleTitle, FxRoles.LECTOR.Name);
        Bind(lbAdminRoleTitle, FxRoles.ADMIN.Name);
        Bind(lbSuperAdminRoleTitle, FxRoles.SUPER_ADMIN.Name);

        BindEnabled(cbStudentRole, isAdmin);
        BindEnabled(cbTrainerRole, isAdmin);
        BindEnabled(cbLectorRole, isAdmin);
        BindEnabled(cbAdminRole, currentUser.IsSuperAdmin());
        BindEnabled(cbSuperAdminRole, currentUser.IsSuperAdmin());

        BindChecked2Ways(cbStudentRole, c.StudentRoleChecked);
        BindChecked2Ways(cbTrainerRole, c.TrainerRoleChecked);
        BindChecked2Ways(cbLectorRole, c.LectorRoleChecked);
        BindChecked2Ways(cbAdminRole, c.AdminRoleChecked);
        BindChecked2Ways(cbSuperAdminRole, c.SuperAdminRoleChecked);

        GroupList.ActionEnabled = GroupList_ActionEnabled;
        GroupList.ActionTitle = GroupList_ActionTitle;
        GroupList.ActionUrl = GroupList_ActionUrl;
    }

    public override void DataBind()
    {
        base.DataBind();
        var groups = ServerModel.DB.Load<TblGroups>(ServerModel.DB.LookupMany2ManyIds<TblGroups>(Controller.User, null));
        GroupList.DataSource = Controller.GetGroups();
        GroupList.DataBind();
        var userName = Controller.User.DisplayName;
        lbUserGroups.Text = (GroupList.Visible = groups.Count > 0) ?
            string.Format("{0} participating in following groups:", userName) :
            string.Format("{0} are not participating in any groups", userName);
        lbUserRoles.Text = "Roles for " + userName;
        Title = "Edit User Profile - " + userName;
        btnInclude.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_SelectGroupController
        {
            UserID = Controller.User.ID,
            Operation = SELECT_GROUP_OPERATION.INCLUDE,
            BackUrl = Request.RawUrl
        });
    }

    private string GroupList_ActionUrl(TblGroups group)
    {
        return ServerModel.Forms.BuildRedirectUrl(new Admin_RemoveUserFromGroupController
        {
            UserID = Controller.User.ID,
            GroupID = group.ID,
            BackUrl = Request.RawUrl
        });
    }

    private string GroupList_ActionTitle(TblGroups arg)
    {
        return "Exclude";
    }

    private bool GroupList_ActionEnabled(TblGroups arg)
    {
        return ServerModel.User.Current.IsUpperStudent();
    }
}
