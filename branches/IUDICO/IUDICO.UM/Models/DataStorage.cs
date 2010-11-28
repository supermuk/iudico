using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.UM.Models
{
    public class DataStorage
    {
        private ButterflyDB db = ButterflyDB.Instance;

        public IEnumerable<Role> GetRoles()
        {
            return db.Roles.AsEnumerable();
        }

        public bool CreateRole(Role role)
        {
            try
            {
                db.Roles.InsertOnSubmit(role);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}