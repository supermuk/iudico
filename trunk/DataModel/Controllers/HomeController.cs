using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class HomeController : PageControllerBase
    {
        public void Test1()
        {
            IList<TblPermissions> r = ServerModel.DB.Load<TblPermissions>(new[] { 2, 3 });

            r[0].DateSince = DateTime.Now;
            r[1].DateTill = DateTime.Now;

            ServerModel.DB.Update(r[0]);

            r[0].DateTill = DateTime.Now;

            ServerModel.DB.Update(r);
        }

        public void Test2()
        {
            
        }
    }
}
