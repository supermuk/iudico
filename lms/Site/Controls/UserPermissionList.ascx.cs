using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

public partial class UserPermissionList : UserControl
{
    public int UserID
    {
        get
        {
            var obj = ViewState["UserID"];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        foreach (var ot in ObjectTypeHelper.All)
        {
            var p = new Panel();
            var uo = (UserObjectPermissions) LoadControl("~/Controls/UserObjectPermissions.ascx");
            uo.ObjectType = ot;
            var lb = new Label();
            lb.Font.Bold = true;

            p.Controls.Add(lb);
            p.Controls.Add(uo);

            Controls.Add(p);
            _ObjectPermissions.Add(new KeyValuePair<ITextControl, UserObjectPermissions>(lb, uo));
        }

        base.OnInit(e);
    }

    public override void DataBind()
    {
        var uid = UserID;
        foreach (var c in _ObjectPermissions)
        {
            c.Value.UserID = uid;
            c.Value.DataBind();
            c.Key.Text = GetPermissionsLabel(c.Value.IsEmpty, c.Value.ObjectType.Value.GetSecurityAtr().Name);
        }
    }

    private static string GetPermissionsLabel(bool v, string title)
    {
        return v ? "You don't have permissions to any of " + title : title.Pluralize() + " you have access to:";
    }

    private readonly List<KeyValuePair<ITextControl, UserObjectPermissions>> _ObjectPermissions = new List<KeyValuePair<ITextControl, UserObjectPermissions>>();
}