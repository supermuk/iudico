using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controls;
using IUDICO.DataModel;
using IUDICO.DataModel.DB;
using System.Collections.Generic;

public class UserPermissionList_ : WebControl
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
        foreach(var ot in ObjectTypeHelper.All)
        {
            var p = new Panel();
            var uo = new UserObjectPermissions {ObjectType = ot};
            var lb = new Label();

            p.Controls.Add(lb);
            p.Controls.Add(uo);

            Controls.Add(p);
            _ObjectPermissions.Add(new KeyValuePair<ITextControl,UserObjectPermissions>(lb,uo));
        }

        base.OnInit(e);
    }

    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);
        var uid = UserID;
        if (uid != 0)
            foreach (var c in _ObjectPermissions)
                c.Value.UserID = uid;
    }

    public override void DataBind()
    {
        foreach (var c in _ObjectPermissions)
        {
            c.Value.DataBind();
            c.Key.Text = GetPermissionsLabel(c.Value.IsEmpty, c.Value.ObjectType.Value.GetSecurityAtr().Name);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (((IControlledPage)Page).IsFirstTimeRequest)
        {
            DataBind();
        }
    }

    private static string GetPermissionsLabel(bool v, string title)
    {
        return v ? "You don't have permissions to any of " + title : title + " you have access to:";
    }

    private readonly List<KeyValuePair<ITextControl, UserObjectPermissions>> _ObjectPermissions = new List<KeyValuePair<ITextControl, UserObjectPermissions>>();
}
