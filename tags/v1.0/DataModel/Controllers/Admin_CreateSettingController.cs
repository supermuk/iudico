using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using System;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_CreateSettingController : ControllerBase
    {
        [PersistantField]
        public readonly IVariable<string> Name = string.Empty.AsVariable();

        [PersistantField]
        public readonly IVariable<string> Value = string.Empty.AsVariable();

        [PersistantField]
        public readonly IVariable<string> ErrorText = string.Empty.AsVariable();

        public void CreateSetting()
        {
            try
            {
                ServerModel.Settings.SetValue(Name.Value, Value.Value);

                RedirectToController(new Admin_SettingsController { BackUrl = HttpContext.Current.Request.RawUrl });
            }
            catch (Exception ex)
            {
                ErrorText.Value = Translations.Admin_CreateSettingController_CreateSetting_Error_creating_setting___possibly_setting_with_such_name_already_exists;
            }
        }
    }
}
