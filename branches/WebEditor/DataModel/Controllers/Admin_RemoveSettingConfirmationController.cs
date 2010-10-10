using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_RemoveSettingConfirmationController : ControllerBase
    {
        public override void Initialize()
        {
            base.Initialize();
            Setting = ServerModel.DB.Load<TblSettings>(SettingID);
        }

        public void DoRemove()
        {
            ServerModel.DB.Delete<TblSettings>(SettingID);
            RedirectToController(new Admin_SettingsController { BackUrl = string.Empty });
        }

        [ControllerParameter]
        public int SettingID;

        [PersistantField]
        public TblSettings Setting;
    }
}
