using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_RemoveUserConfirmationController : ControllerBase
    {
        public override void Initialize()
        {
            base.Initialize();
            User = ServerModel.DB.Load<TblUsers>(UserID);
        }

        public void DoRemove()
        {
            ServerModel.DB.Delete<TblUsers>(UserID);
            RedirectToController(new Admin_UsersController{ BackUrl = string.Empty });
        }

        [ControllerParameter]
        public int UserID;

        [PersistantField]
        public TblUsers User;
    }
}
