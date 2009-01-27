using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

public enum SELECT_GROUP_OPERATION
{
    INCLUDE
}

public class Admin_SelectGroupController : ControllerBase
{
    public override void Initialize()
    {
        base.Initialize();
        User = ServerModel.DB.Load<TblUsers>(UserID);
    }

    public IList<TblGroups> SelectGroups()
    {
        return ServerModel.DB.Query<TblGroups>(new InCondition(
            DataObject.Schema.ID,
            new SubSelectCondition<RelUserGroups>("GroupRef",
                new CompareCondition(
                    DataObject.Schema.UserRef,
                    new ValueCondition<int>(User.ID),
                    COMPARE_KIND.EQUAL
                )
            ),
            IN_CONDITION_KIND.NOT_IN
        ));
    }

    [PersistantField]
    public TblUsers User;

    [ControllerParameter]
    public int UserID;

    [ControllerParameter]
    public SELECT_GROUP_OPERATION Operation;
}

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
            lnkGroupName.PostBackUrl = "~/Admin/EditGroup.aspx?" +
                ControllerParametersUtility<Admin_EditGroupController>.BuildUrlParams(
                   new Admin_EditGroupController
                   {
                       GroupID = group.ID
                   }, Server);
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
                operationUrl = "~/Admin/IncludeUserIntoGroup.aspx?" +
                ControllerParametersUtility<Admin_IncludeUserIntoGroupController>.BuildUrlParams(
                new Admin_IncludeUserIntoGroupController
                {
                    UserID = Controller.User.ID,
                    GroupID = groupID,
                    BackUrl = Controller.BackUrl
                }, Server);
                return "Include";

            default:
                throw new InvalidOperationException();
        }
    }
}
