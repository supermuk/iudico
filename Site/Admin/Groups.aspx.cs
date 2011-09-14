using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Admin_Groups : ControlledPage<Admin_GroupsController>
{
    protected override void BindController(Admin_GroupsController c)
    {
        base.BindController(c);
        Title = "Groups";
        GroupList.ActionEnabled = GroupList_ActionEnabled;
        GroupList.ActionTitle = GroupList_ActionTitle;
        GroupList.ActionUrl = GroupList_ActionUrl;
    }

    public override void DataBind()
    {
        base.DataBind();
        GroupList.DataSource = Controller.GetGroups();
        GroupList.DataBind();
        btnCreateGroup.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new CreateGroupController {BackUrl = Request.RawUrl});
    }

    private string GroupList_ActionUrl(TblGroups group)
    {
        return "";// ServerModel.Forms.BuildRedirectUrl(new Admin_RemoveGroupConfirmationController { BackUrl = Request.RawUrl, GroupID = group.ID });
    }

    private string GroupList_ActionTitle(TblGroups group)
    {
        return "Remove";
    }

    private bool GroupList_ActionEnabled(TblGroups group)
    {
        return true;
    }
}
