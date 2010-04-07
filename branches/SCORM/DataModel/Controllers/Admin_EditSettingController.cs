using System;
using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using System.Web;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_EditSettingController : ControllerBase
    {
        public override void Initialize()
        {
            base.Initialize();

            Setting = ServerModel.DB.Load<TblSettings>(SettingID);
            Value.Value = Setting.Value;
        }

        public void EditSetting()
        {
            Setting.Value = Value.Value;

            ServerModel.DB.Update(Setting);

            RedirectToController(new Admin_SettingsController { BackUrl = HttpContext.Current.Request.RawUrl });
        }

        [PersistantField]
        public readonly IVariable<string> Value = string.Empty.AsVariable();

        [PersistantField]
        public TblSettings Setting;

        [ControllerParameter]
        public int SettingID;
    }

}
