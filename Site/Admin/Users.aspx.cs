using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Admin_Users : ControlledPage<Admin_UsersController>
{
    protected override void BindController(Admin_UsersController c)
    {
        base.BindController(c);
        UserList.ActionEnabled = UserList_ActionEnabled;
        UserList.ActionTitle = UserList_ActionTitle;
        UserList.ActionUrl = UserList_ActionUrl;
    }

    public override void DataBind()
    {
        base.DataBind();
        UserList.DataSource = Controller.GetUsers();
        UserList.DataBind();
        btnCreateUser.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_CreateUserController {BackUrl = Request.RawUrl});
    }

    private string UserList_ActionUrl(TblUsers arg)
    {
        return ServerModel.Forms.BuildRedirectUrl(new Admin_RemoveUserConfirmationController{BackUrl = Request.RawUrl, UserID = arg.ID});
    }

    private string UserList_ActionTitle(TblUsers arg)
    {
        return "Remove";
    }

    private bool UserList_ActionEnabled(TblUsers arg)
    {
        return true;
    }
}
