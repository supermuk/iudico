using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Controls_GroupList : UserControl
{
    public Func<TblGroups, string> ActionUrl;
    public Func<TblGroups, string> ActionTitle;
    public Func<TblGroups, bool> ActionEnabled;

    public object DataSource
    {
        set
        {
            gvGroups.DataSource = value;
        }
    }

    public override void DataBind()
    {
        gvGroups.DataBind();
    }

    protected void gvGroups_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var group = (TblGroups)e.Row.DataItem;
            var lnkAction = (Button)e.Row.FindControl("lnkAction");
            var lnkGroupName = (LinkButton)e.Row.FindControl("lnkGroupName");

            lnkGroupName.Text = group.Name;
            lnkGroupName.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_EditGroupController
            {
                GroupID = group.ID,
                BackUrl = Request.RawUrl
            });
            lnkAction.Text = ActionTitle(group);
            if (lnkAction.Enabled = ActionEnabled(group))
            {
                lnkAction.PostBackUrl = ActionUrl(group);
            }
        }
    }
}
