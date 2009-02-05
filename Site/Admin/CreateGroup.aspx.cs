using System;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using IUDICO.DataModel;
using IUDICO.DataModel.Security;

public class CreateGroupController : ControllerBase
{
    public int Create(string name)
    {
        var group = new TblGroups { Name = name };
        ServerModel.DB.Insert(group);

        int uID = ServerModel.User.Current.ID;
        PermissionsManager.Grand(group, FxGroupOperations.ChangeMembers, uID, null, DateTimeInterval.Full);
        PermissionsManager.Grand(group, FxGroupOperations.Rename, uID, null, DateTimeInterval.Full);
        PermissionsManager.Grand(group, FxGroupOperations.View, uID, null, DateTimeInterval.Full);

        return group.ID;
    }
}

public partial class CreateGroup : ControlledPage<CreateGroupController>
{
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Validate();
        if (IsValid)
        {
            var gID = Controller.Create(tbGroupName.Text);
            Response.Redirect("~/Admin/EditGroup.aspx?" + ControllerParametersUtility<Admin_EditGroupController>.BuildUrlParams(
                new Admin_EditGroupController
                    {
                        BackUrl = Request.RawUrl,
                        GroupID = gID
                    }, Server));
        }
    }
}