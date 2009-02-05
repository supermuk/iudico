using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;

public class Admin_EditUserController : ControllerBase
{
    public override void Initialize()
    {
        base.Initialize();

        User = ServerModel.DB.Load<TblUsers>(UserID);
        var roles = ServerModel.User.ByLogin(User.Login).Roles;

        StudentRoleChecked.Value = roles.Contains(FxRoles.STUDENT.Name);
        TrainerRoleChecked.Value = roles.Contains(FxRoles.TRAINER.Name);
        LectorRoleChecked.Value = roles.Contains(FxRoles.LECTOR.Name);
        AdminRoleChecked.Value = roles.Contains(FxRoles.ADMIN.Name);
        SuperAdminRoleChecked.Value = roles.Contains(FxRoles.SUPER_ADMIN.Name);
    }

    public void ApplyRoles()
    {
        var roles = ServerModel.DB.LookupMany2ManyIds<FxRoles>(User, null);
        Action<IValue<bool>, FxRoles> updateProc = (v, r) =>
        {
            if (v.Value != roles.Contains(r.ID))
            {
                if (v.Value)
                {
                    ServerModel.DB.Link(User, r);
                }
                else
                {
                    ServerModel.DB.UnLink(User, r);
                }
            }
        };

        updateProc(StudentRoleChecked, FxRoles.STUDENT);
        updateProc(TrainerRoleChecked, FxRoles.TRAINER);
        updateProc(LectorRoleChecked, FxRoles.LECTOR);
        updateProc(AdminRoleChecked, FxRoles.ADMIN);
        updateProc(SuperAdminRoleChecked, FxRoles.SUPER_ADMIN);
        ServerModel.User.NotifyUpdated(User);
    }

    [PersistantField]
    public readonly IVariable<bool> StudentRoleChecked = false.AsVariable();
    [PersistantField]
    public readonly IVariable<bool> TrainerRoleChecked = false.AsVariable();
    [PersistantField]
    public readonly IVariable<bool> LectorRoleChecked = false.AsVariable();
    [PersistantField]
    public readonly IVariable<bool> AdminRoleChecked = false.AsVariable();
    [PersistantField]
    public readonly IVariable<bool> SuperAdminRoleChecked = false.AsVariable();

    [PersistantField]
    public TblUsers User;

    [ControllerParameter]
    private int UserID;
}

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
    }

    public override void DataBind()
    {
        base.DataBind();
        var groups = ServerModel.DB.Load<TblGroups>(
            ServerModel.DB.LookupMany2ManyIds<TblGroups>(Controller.User, null));
        gvGroups.DataSource = groups;
        var userName = Controller.User.DisplayName;
        gvGroups.DataBind();
        lbUserGroups.Text = (gvGroups.Visible = groups.Count > 0) ?
            string.Format("{0} participating in following groups:", userName) :
            string.Format("{0} are not participating in any groups", userName);
        lbUserRoles.Text = "Roles for " + userName;
        Title = "Edit User Profile - " + userName;
        btnInclude.PostBackUrl = "~/Admin/SelectGroup.aspx?" +
            ControllerParametersUtility<Admin_SelectGroupController>.BuildUrlParams(
                new Admin_SelectGroupController
                    {
                        UserID = Controller.User.ID,
                        Operation = SELECT_GROUP_OPERATION.INCLUDE,
                        BackUrl = Request.RawUrl
                    }, Server);
    }

    protected void gvGroups_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var group = (TblGroups) e.Row.DataItem;
            var lnkExclude = (LinkButton) e.Row.FindControl("lnkExclude");
            var lnkGroupName = (LinkButton) e.Row.FindControl("lnkGroupName");

            lnkGroupName.Text = group.Name;
            lnkGroupName.PostBackUrl = "~/Admin/EditGroup.aspx?" +
                ControllerParametersUtility<Admin_EditGroupController>.BuildUrlParams(
                   new Admin_EditGroupController
                   {
                        GroupID = group.ID
                   }, Server);
            lnkExclude.PostBackUrl = "~/Admin/RemoveUserFromGroup.aspx?" +
                ControllerParametersUtility<Admin_RemoveUserFromGroupController>.BuildUrlParams(
                new Admin_RemoveUserFromGroupController
                    {
                        UserID = Controller.User.ID,
                        GroupID = group.ID,
                        BackUrl = Request.RawUrl
                    }, Server);
            lnkExclude.Enabled = ServerModel.User.Current.IsUpperStudent();
        }
    }
}
