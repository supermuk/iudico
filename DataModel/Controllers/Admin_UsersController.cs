using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_UsersController : ControllerBase
    {
        public List<TblUsers> GetUsers()
        {
            return ServerModel.DB.Query<TblUsers>(null);
        }
    }
}
