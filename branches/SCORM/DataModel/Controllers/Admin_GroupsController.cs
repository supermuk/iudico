using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for Groups.aspx page
    /// </summary>
    public class Admin_GroupsController : ControllerBase
    {
        public IList<TblGroups> GetGroups()
        {
            return ServerModel.DB.Query<TblGroups>(null);
        }
    }
}
