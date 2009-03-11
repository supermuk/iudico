using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CreateUser : ControlledPage<Admin_CreateUserController>
{
    public override void DataBind()
    {
        base.DataBind();
        lbCreateMultiple.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_CreateBulkUserController{BackUrl = Request.RawUrl});
    }
}
