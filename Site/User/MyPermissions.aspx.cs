using System;
using System.Web.Security;
using IUDICO.DataModel.Security;

public partial class User_MyPermissions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataBind();
    }

    protected int UserID
    {
        set
        {
            CoursePermissions.UserID = value;
            ThemePermissions.UserID = value;
        }
    }

    public override void DataBind()
    {
        UserID = ((CustomUser) Membership.GetUser()).ID;

        CoursePermissions.DataBind();
        CoursePermissionsLabel.Text = GetPermissionsLabel(CoursePermissions.IsEmpty, "Courses");
        ThemePermissions.DataBind();
        ThemePermissionsLabel.Text = GetPermissionsLabel(ThemePermissions.IsEmpty, "Themes");
    }

    private static string GetPermissionsLabel(bool v, string title)
    {
        return v ? "You don't have permissions to any of " + title : title + " you have access to:";
    }
}
