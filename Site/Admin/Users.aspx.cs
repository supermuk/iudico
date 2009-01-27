using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public class Admin_UsersController : ControllerBase
{
}

public partial class Admin_Users : ControlledPage<Admin_UsersController>
{
    public override void DataBind()
    {
        base.DataBind();
        var users = ServerModel.DB.Query<TblUsers>(null);
        gvUsers.DataSource = users;
        gvUsers.DataBind();
    }

    protected void gvUsers_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var user = (TblUsers)e.Row.DataItem;

            var lbLogin = (LinkButton) e.Row.FindControl("lbLogin");
            var lbFirstName = (Label) e.Row.FindControl("lbFirstName");
            var lbSecondName = (Label) e.Row.FindControl("lbSecondName");
            var lbEmail = (Label) e.Row.FindControl("lbEmail");

            lbLogin.PostBackUrl = "~/Admin/EditUser.aspx?UserID=" + user.ID;
            lbLogin.Text = user.Login;
            lbFirstName.Text = user.FirstName;
            lbSecondName.Text = user.LastName;
            lbEmail.Text = user.Email;
        }

    }
}
