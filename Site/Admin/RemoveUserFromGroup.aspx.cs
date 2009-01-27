using System;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel;
using IUDICO.DataModel.DB;

public class Admin_User_GroupOperationControllerBase : ControllerBase
{
    public override void Initialize()
    {
        base.Initialize();
        User = ServerModel.DB.Load<TblUsers>(UserID);
        Group = ServerModel.DB.Load<TblGroups>(GroupID);
    }

    [PersistantField]
    public TblUsers User;
    [PersistantField]
    public TblGroups Group;

    [ControllerParameter]
    public int UserID;
    [ControllerParameter]
    public int GroupID;
}

public class Admin_RemoveUserFromGroupController : Admin_User_GroupOperationControllerBase
{
    public void DoExclude()
    {
        ServerModel.DB.UnLink(User, Group);
    }
}

public partial class Admin_RemoveUserFromGroup : ControlledPage<Admin_RemoveUserFromGroupController>
{
    public override void DataBind()
    {
        base.DataBind();
        lbConfirmationText.Text = string.Format("Do you really want to exclude {0} from {1}?", Controller.User.DisplayName, Controller.Group.Name);
        btnNo.PostBackUrl = Controller.BackUrl;
    }

    protected void DoExclude(object sender, EventArgs e)
    {
        Controller.DoExclude();
        Response.Redirect(btnNo.PostBackUrl);
    }
}
