using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;

public class Admin_EditUserController : ControllerBase, IDataModelPresenter
{
    public bool IsDirty
    {
        get { return false; }
    }

    public ReadOnlyCollection<IDataModelChange> Changes
    {
        get { return null; }
    }

    public override void Initialize()
    {
        base.Initialize();

        User = ServerModel.DB.Load<TblUsers>(UserID);
        var roles = ServerModel.DB.LookupMany2ManyIds<FxRoles>(User, null);

        StudentRoleChecked.Value = roles.Contains(FxRoles.STUDENT.ID);
        TrainerRoleChecked.Value = roles.Contains(FxRoles.TRAINER.ID);
        LectorRoleChecked.Value = roles.Contains(FxRoles.LECTOR.ID);
        AdminRoleChecked.Value = roles.Contains(FxRoles.ADMIN.ID);
        SuperAdminRoleChecked.Value = roles.Contains(FxRoles.SUPER_ADMIN.ID);

        superAdminRoleEnable.Value = AdminRoleEnable.Value = ServerModel.User.Current.Roles.Contains(FxRoles.SUPER_ADMIN.Name);
    }

    public override void Loaded()
    {
        base.Loaded();

        StudentRoleTitle.Value = FxRoles.STUDENT.Name;
        TrainerRoleTitle.Value = FxRoles.TRAINER.Name;
        LectorRoleTitle.Value = FxRoles.LECTOR.Name;
        AdminRoleTitle.Value = FxRoles.ADMIN.Name;
        SuperAdminRoleTitle.Value = FxRoles.SUPER_ADMIN.Name;
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

    public readonly IVariable<string>
        StudentRoleTitle = string.Empty.AsVariable(),
        TrainerRoleTitle = string.Empty.AsVariable(),
        LectorRoleTitle = string.Empty.AsVariable(),
        AdminRoleTitle = string.Empty.AsVariable(),
        SuperAdminRoleTitle = string.Empty.AsVariable();

    public readonly IVariable<bool>
        AdminRoleEnable = false.AsVariable(),
        superAdminRoleEnable = false.AsVariable();

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
        Bind(lbStudentRoleTitle, c.StudentRoleTitle);
        Bind(lbTrainerRoleTitle, c.TrainerRoleTitle);
        Bind(lbLectorRoleTitle, c.LectorRoleTitle);
        Bind(lbAdminRoleTitle, c.AdminRoleTitle);
        Bind(lbSuperAdminRoleTitle, c.SuperAdminRoleTitle);

        BindEnabled(cbAdminRole, c.AdminRoleEnable);
        BindEnabled(cbSuperAdminRole, c.superAdminRoleEnable);

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
        }
    }
}
