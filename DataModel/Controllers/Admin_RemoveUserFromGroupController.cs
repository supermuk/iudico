using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_User_GroupOperationControllerBase : ControllerBase
    {
        public override void Initialize()
        {
            base.Initialize();
            User = ServerModel.DB.Load<TblUsers>(UserID);
            Group = ServerModel.DB.Load<TblGroups>(GroupID);
        }

        [PersistantField]
        public TblUsers User;
        [PersistantField]
        public TblGroups Group;

        [ControllerParameter]
        public int UserID;
        [ControllerParameter]
        public int GroupID;
    }

    public class Admin_RemoveUserFromGroupController : Admin_User_GroupOperationControllerBase
    {
        public void DoExclude()
        {
            ServerModel.DB.UnLink(User, Group);
            ServerModel.User.NotifyUpdated(User);
        }
    }

}
