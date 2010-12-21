using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Models.Storage
{
    public class DatabaseUserStorage : IUserStorage
    {
        protected ILmsService _LmsService;

        public DatabaseUserStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        protected DBDataContext GetDbContext()
        {
            return _LmsService.GetDbDataContext();
        }

        #region Implementation of IUMStorage

        #region Role members

        public Role GetRole(int id)
        {
            return GetDbContext().Roles.First(role => role.Id == id);
        }

        public IEnumerable<Role> GetRoles()
        {
            return GetDbContext().Roles.AsEnumerable();
        }

        public bool CreateRole(Role role)
        {
            try
            {
                var db = GetDbContext();

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
                var oldRole = GetRole(id);
                oldRole.Name = role.Name;

                GetDbContext().SubmitChanges();
                
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
                var db = GetDbContext();

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
            return GetDbContext().Groups.First(group => group.Id == id);
        }

        public IEnumerable<Group> GetGroups()
        {
            return GetDbContext().Groups.AsEnumerable();
        }

        public bool CreateGroup(Group group)
        {
            try
            {
                var db = GetDbContext();

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
                var oldGroup = GetGroup(id);
                oldGroup.Name = group.Name;

                GetDbContext().SubmitChanges();

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
                var db = GetDbContext();

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