using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_RemoveGroupConfirmationController : ControllerBase
    {
        public override void Initialize()
        {
            base.Initialize();
            Group = ServerModel.DB.Load<TblGroups>(GroupID);
        }

        public void DoRemove()
        {
            ServerModel.DB.Delete<TblGroups>(GroupID);
            RedirectToController(new Admin_GroupsController { BackUrl = string.Empty });
        }

        [ControllerParameter]
        public int GroupID;

        [PersistantField] public TblGroups Group;
    }
}
