using System.Web.Security;
using IUDICO.DataModel;
using IUDICO.DataModel.Security;

public partial class User_MyPermissions : ControlledPage
{
    protected int UserID
    {
        set
        {
            uplPermissions.UserID = value;
        }
    }

    public override void DataBind()
    {
        UserID = ((CustomUser) Membership.GetUser()).ID;
        uplPermissions.DataBind();
    }
}
