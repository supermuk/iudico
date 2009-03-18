
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.ImportManagers.RemoveManager;
using IUDICO.DataModel.Security;
using System.Data;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    public class BaseTeacherController : ControllerBase
    {
        public IVariable<string> Caption = string.Empty.AsVariable();
        public IVariable<string> Description = string.Empty.AsVariable();
        public IVariable<string> Message = "Default message".AsVariable();
        public IVariable<string> Title = string.Empty.AsVariable();
        public string RawUrl = "";
        public bool IsPostBack = false;

        public override void Loaded()
        {
            base.Loaded();

            Message.Value = "";
        }
    }


}
