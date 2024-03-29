﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class Controls_UserList : UserControl
{
    public Func<TblUsers, string> ActionUrl;
    public Func<TblUsers, string> ActionTitle;
    public Func<TblUsers, bool> ActionEnabled;

    public object DataSource
    {
        set
        {
            gvUsers.DataSource = value;
        }
    }

    public override void DataBind()
    {
        gvUsers.DataBind();
    }

    protected void gvUsers_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var user = (TblUsers)e.Row.DataItem;

            var lbLogin = (LinkButton)e.Row.FindControl("lbLogin");
            var lbFirstName = (Label)e.Row.FindControl("lbFirstName");
            var lbSecondName = (Label)e.Row.FindControl("lbSecondName");
            var lbEmail = (Label)e.Row.FindControl("lbEmail");
            var btnAction = (Button) e.Row.FindControl("btnAction");

            lbLogin.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_EditUserController { BackUrl = Request.RawUrl, UserID = user.ID });
            lbLogin.Text = user.Login;
            lbFirstName.Text = user.FirstName;
            lbSecondName.Text = user.LastName;
            lbEmail.Text = user.Email;
            btnAction.Text = ActionTitle(user);
            if (btnAction.Enabled = ActionEnabled(user))
            {
                btnAction.PostBackUrl = ActionUrl(user);
            }
        }
    }
}
