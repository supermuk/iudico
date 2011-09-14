using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using System.Linq;

public partial class Controls_SettingList : UserControl
{
    public object DataSource
    {
        set
        {
            gvSettings.DataSource = value;
        }
    }

    public override void DataBind()
    {
        gvSettings.DataBind();
    }

    protected void gvSettings_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var setting = (TblSettings)e.Row.DataItem;

            var lbName = (LinkButton)e.Row.FindControl("lbName");
            var lbValue = (Label)e.Row.FindControl("lbValue");
            var btnAction = (Button)e.Row.FindControl("btnAction");

            lbName.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_EditSettingController { BackUrl = Request.Url.AbsolutePath, SettingID = setting.ID });
            lbName.Text = setting.Name;
            lbValue.Text = setting.Value;
            
            btnAction.Text = "Remove";
            btnAction.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_RemoveSettingConfirmationController { BackUrl = Request.Url.AbsolutePath, SettingID = setting.ID });
        }
    }

    protected void SettingsPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView) sender).PageIndex = e.NewPageIndex;
        Page.DataBind();
    }
}
