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
            var setting = new TblSettings
            {
                Name = Name.Value,
                Value = Value.Value
            };

            try
            {
                ServerModel.DB.Insert(setting);

                RedirectToController(new Admin_SettingsController { BackUrl = HttpContext.Current.Request.RawUrl });
            }
            catch (Exception ex)
            {
                ErrorText.Value = "Error creating setting - possibly setting with such name already exists";
            }
        }
    }
}
