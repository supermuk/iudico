using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class HomeController : PageControllerBase
    {
        [ControllerValue]
        private int SomeControllerValue;

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
            TblPermissions r = new TblPermissions
            {
                UserRef = 2
            };
            TblPermissions r2 = new TblPermissions
            {
                UserRef = 2
            };
            TblPermissions r3 = new TblPermissions
            {
                UserRef = 2
            };
            ServerModel.DB.Insert(r);
            ServerModel.DB.Insert<TblPermissions>(new[] { r2, r3 });
        }

        public void Test3()
        {
            SomeControllerValue++;
        }
    }
}
