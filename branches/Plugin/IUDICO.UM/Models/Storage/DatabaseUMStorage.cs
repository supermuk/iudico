using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;

namespace IUDICO.UM.Models.Storage
{
    public class DatabaseUMStorage : IUMStorage
    {
        protected DB db = DB.Instance;

        #region Implementation of IUMStorage

        #region Role members

        public Role GetRole(int id)
        {
            return db.Roles.First(role => role.ID == id);
        }

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

        public bool EditRole(int id, Role role)
        {
            try
            {
                Role oldRole = GetRole(id);
                oldRole.Name = role.Name;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteRole(int id)
        {
            try
            {
                db.Roles.DeleteOnSubmit(GetRole(id));
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Group members

        public Group GetGroup(int id)
        {
            return db.Groups.First(group => group.ID == id);
        }

        public IEnumerable<Group> GetGroups()
        {
            return db.Groups.AsEnumerable();
        }

        public bool CreateGroup(Group group)
        {
            try
            {
                db.Groups.InsertOnSubmit(group);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditGroup(int id, Group group)
        {
            try
            {
                Group oldGroup = GetGroup(id);
                oldGroup.Name = group.Name;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteGroup(int id)
        {
            try
            {
                db.Groups.DeleteOnSubmit(GetGroup(id));
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #endregion
    }
}