using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public class Admin_EditGroupController : ControllerBase
{
    [ControllerParameter]
    public int GroupID;
}

public partial class Admin_EditGroup : ControlledPage<Admin_EditGroupController>
{
}
