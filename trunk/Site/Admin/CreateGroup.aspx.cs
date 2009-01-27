using System;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel;
using IUDICO.DataModel.Security;

public partial class CreateGroup : System.Web.UI.Page
{
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Validate();
        if (IsValid)
        {
            var group = new TblGroups {Name = tbGroupName.Text};
            ServerModel.DB.Insert(group);

            int uID = ServerModel.User.Current.ID;
            PermissionsManager.Grand(group, FxGroupOperations.ChangeMembers, uID, null, DateTimeInterval.Full);
            PermissionsManager.Grand(group, FxGroupOperations.Rename, uID, null, DateTimeInterval.Full);
            PermissionsManager.Grand(group, FxGroupOperations.View, uID, null, DateTimeInterval.Full);
        }
    }
}