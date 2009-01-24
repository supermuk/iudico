using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Security;
using IUDICO.Web.Controls;

public partial class User_MyPermissions : ControlledPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            DataBind();
        }
    }

    protected int UserID
    {
        set
        {
            CoursePermissions.UserID = value;
            ThemePermissions.UserID = value;
            GroupPermissions.UserID = value;
        }
    }

    public override void DataBind()
    {
        UserID = ((CustomUser) Membership.GetUser()).ID;

        Bind(CoursePermissions, CoursePermissionsLabel, "Courses");
        Bind(ThemePermissions, ThemePermissionsLabel, "Themes");
        Bind(GroupPermissions, GroupPermissionsLabel, "Groups");
    }

    private static void Bind(UserPermissions permissionsList, ITextControl permissionsLabel, string title)
    {
        permissionsList.DataBind();
        permissionsLabel.Text = GetPermissionsLabel(permissionsList.IsEmpty, title);
    }

    private static string GetPermissionsLabel(bool v, string title)
    {
        return v ? "You don't have permissions to any of " + title : title + " you have access to:";
    }
}
