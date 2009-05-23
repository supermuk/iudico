using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Admin_SelectGroup : ControlledPage<Admin_SelectGroupController>
{
    public override void DataBind()
    {
        base.DataBind();
        IList<TblGroups> groups = Controller.SelectGroups();
        gvGroups.DataSource = groups;
        gvGroups.DataBind();
        btnCancel.PostBackUrl = Controller.BackUrl;
        gvGroups.Visible = groups.Count > 0;
        lbMessage.Text = groups.Count == 0 ? 
            "No group available" :
            string.Format("Groups, {0} can be added to:", Controller.User.DisplayName);
    }

    protected void gvGroups_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var group = (TblGroups)e.Row.DataItem;
            var lnkSelect = (Button)e.Row.FindControl("lnkSelect");
            var lnkGroupName = (LinkButton)e.Row.FindControl("lnkGroupName");

            lnkGroupName.Text = group.Name;
            lnkGroupName.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_EditGroupController
            {
                GroupID = group.ID,
                BackUrl = Request.RawUrl
            });
            string pbu;
            lnkSelect.Text = OperationToTitle(Controller.Operation, group.ID, out pbu);
            lnkSelect.PostBackUrl = pbu;
        }
    }

    private string OperationToTitle(SELECT_GROUP_OPERATION op, int groupID, out string operationUrl)
    {
        switch(op)
        {
            case SELECT_GROUP_OPERATION.INCLUDE:
                operationUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_IncludeUserIntoGroupController
                {
                    UserID = Controller.User.ID,
                    GroupID = groupID,
                    BackUrl = Controller.BackUrl
                });
                return "Include";

            default:
                throw new InvalidOperationException();
        }
    }
}
