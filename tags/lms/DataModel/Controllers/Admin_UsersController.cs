using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for Users.aspx page
    /// </summary>
    public class Admin_UsersController : ControllerBase
    {
        [PersistantField]
        public readonly IVariable<string> SearchPattern = string.Empty.AsVariable();

        public List<TblUsers> GetUsers()
        {
            string ptrn = SearchPattern.Value;
            if (ptrn.IsNull())
            {
                return ServerModel.DB.Query<TblUsers>(null);    
            }
            else
            {
                var vc = new ValueCondition<string>("%" + ptrn + "%");
                return ServerModel.DB.Query<TblUsers>(new OrCondtion(
                    new CompareCondition<string>(
                        DataObject.Schema.Login,
                        vc,
                        COMPARE_KIND.LIKE),
                    new CompareCondition<string>(
                        DataObject.Schema.FirstName,
                        vc,
                        COMPARE_KIND.LIKE),
                    new CompareCondition<string>(
                        DataObject.Schema.Email,
                        vc,
                        COMPARE_KIND.LIKE)
                 ));
            }
        }
    }
}
