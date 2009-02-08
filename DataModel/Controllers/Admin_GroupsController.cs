using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_GroupsController : ControllerBase
    {
        public IList<TblGroups> GetGroups()
        {
            return ServerModel.DB.Query<TblGroups>(null);
        }
    }
}
