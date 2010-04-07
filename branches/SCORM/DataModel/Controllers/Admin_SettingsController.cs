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
                return ServerModel.DB.Query<TblSettings>(null);
            }
            else
            {
                var vc = new ValueCondition<string>("%" + ptrn + "%");
                return ServerModel.DB.Query<TblSettings>(new OrCondtion(
                    new CompareCondition<string>(
                        DataObject.Schema.Name,
                        vc,
                        COMPARE_KIND.LIKE),
                    new CompareCondition<string>(
                        DataObject.Schema.Value,
                        vc,
                        COMPARE_KIND.LIKE)
                 ));
            }
        }
    }
}
