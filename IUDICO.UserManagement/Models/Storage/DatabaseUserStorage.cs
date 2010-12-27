using System;
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

        #region Implementation of IUserStorage

        #region User members

        public User GetUser(Guid id)
        {
            var db = GetDbContext();

            return db.Users.Single(user => user.Id == id);
        }

        public User GetUser(string openId)
        {
            var db = GetDbContext();

            return db.Users.Single(user => user.OpenId == openId);
        }

        public IEnumerable<User> GetUsers()
        {
            var db = GetDbContext();

            return db.Users.AsEnumerable();
        }

        public void CreateUser(User user)
        {
            var db = GetDbContext();

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();
        }

        public void EditUser(Guid id, EditUserModel editor)
        {
            var db = GetDbContext();
            var oldUser = db.Users.Single(u => u.Id == id);

            oldUser.Name = editor.Name;
            oldUser.Password = editor.Password;
            oldUser.Email = editor.Email;
            oldUser.OpenId = editor.OpenId;
            oldUser.RoleRef = editor.RoleRef;
            
            db.SubmitChanges();
        }

        public void DeleteUser(Guid id)
        {
            var db = GetDbContext();
            var user = db.Users.Single(u => u.Id == id);

            db.Users.DeleteOnSubmit(user);
            db.SubmitChanges();
        }

        public IEnumerable<User> GetUsersByGroup(Group group)
        {
            var db = GetDbContext();

            return db.GroupUsers.Where(g => g.GroupRef == group.Id).Select(g => g.User);
        }

        #endregion

        #region Role members

        public Role GetRole(int id)
        {
            var db = GetDbContext();

            return db.Roles.Single(role => role.Id == id);
        }

        public IEnumerable<Role> GetRoles()
        {
            var db = GetDbContext();

            return db.Roles.AsEnumerable();
        }

        public void CreateRole(Role role)
        {
            var db = GetDbContext();

            db.Roles.InsertOnSubmit(role);
            db.SubmitChanges();
        }

        public void EditRole(int id, Role role)
        {
            var oldRole = GetRole(id);
            var db = GetDbContext();

            oldRole.Name = role.Name;

            db.SubmitChanges();
        }

        public void DeleteRole(int id)
        {
            var db = GetDbContext();

            db.Roles.DeleteOnSubmit(GetRole(id));
            db.SubmitChanges();
        }

        #endregion

        #region Group members

        public Group GetGroup(int id)
        {
            var db = GetDbContext();

            return db.Groups.First(group => group.Id == id);
        }

        public IEnumerable<Group> GetGroups()
        {
            var db = GetDbContext();

            return db.Groups.AsEnumerable();
        }

        public void CreateGroup(Group group)
        {
            var db = GetDbContext();

            db.Groups.InsertOnSubmit(group);
            db.SubmitChanges();
        }

        public void EditGroup(int id, Group group)
        {
            var oldGroup = GetGroup(id);
            var db = GetDbContext();

            oldGroup.Name = group.Name;
            db.SubmitChanges();
        }

        public void DeleteGroup(int id)
        {
            var db = GetDbContext();

            db.Groups.DeleteOnSubmit(GetGroup(id));
            db.SubmitChanges();
        }

        #endregion

        #endregion
    }
}