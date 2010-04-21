namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for IncludeUserIntoGroup.aspx page
    /// </summary>
    public class Admin_IncludeUserIntoGroupController : Admin_User_GroupOperationControllerBase
    {
        public void DoInclude()
        {
            ServerModel.DB.Link(User, Group);
            ServerModel.User.NotifyUpdated(User);
        }
    }
}
