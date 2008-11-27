using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using System.Web.Security;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.DB;
using System.Web.UI;

public partial class User_MyInfo : ControlledPage<UserInfoController>
{
    protected override void BindController(UserInfoController c)
    {
        base.BindController(c);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        int userId = ((CustomUser)Membership.GetUser()).ID;
        TblUsers[] user = new TblUsers[] { ServerModel.DB.Load<TblUsers>(userId) };

        DetailsView1.DataSource = user;
        DetailsView1.DataBind();
        DetailsView1.PageIndex = 0;
    }

}
