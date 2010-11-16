using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_SettingsController : ControllerBase
    {
        [PersistantField]
        public readonly IVariable<string> SearchPattern = string.Empty.AsVariable();

        public List<TblSettings> GetSettings()
        {
            string ptrn = SearchPattern.Value;

            if (ptrn.IsNull())
            {
                return ServerModel.Settings.GetAll();
            }
            else
            {
                return ServerModel.Settings.Query(ptrn);
            }
        }
    }
}
