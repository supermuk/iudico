using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;

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

        public User GetCurrentUser()
        {
            var identity = HttpContext.Current.User.Identity;

            if (!identity.IsAuthenticated)
            {
                return null;
            }

            var db = GetDbContext();

            return db.Users.Where(u => u.Username == identity.Name).FirstOrDefault();
        }

        public User GetUser(Guid id)
        {
            var db = GetDbContext();

            return db.Users.Single(user => user.Id == id && !user.Deleted);
        }

        public User GetUser(string openId)
        {
            var db = GetDbContext();

            return db.Users.Single(user => user.OpenId == openId && !user.Deleted);
        }

        public IEnumerable<User> GetUsers()
        {
            var db = GetDbContext();

            return db.Users.Where(u => !u.Deleted);
        }

        public bool UsernameExists(string username)
        {
            var db = GetDbContext();

            return db.Users.Count(u => u.Username == username) > 0;
        }

        public void ActivateUser(Guid id)
        {
            var db = GetDbContext();

            User user = db.Users.Single(u => u.Id == id);
            user.IsApproved = true;

            db.SubmitChanges();
        }

        public void DeactivateUser(Guid id)
        {
            var db = GetDbContext();

            User user = db.Users.Single(u => u.Id == id);
            user.IsApproved = false;

            db.SubmitChanges();
        }

        public void CreateUser(User user)
        {
            var db = GetDbContext();

            user.Deleted = false;
            user.IsApproved = true;

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserCreate, user);
        }

        public void EditUser(Guid id, User user)
        {
            var db = GetDbContext();
            var oldUser = db.Users.Single(u => u.Id == id);

            oldUser.Name = user.Name;
            oldUser.Password = user.Password;
            oldUser.Email = user.Email;
            oldUser.OpenId = user.OpenId;
            oldUser.RoleId = user.RoleId;
            oldUser.Username = user.Username;
            oldUser.IsApproved = user.IsApproved;
            
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserEdit, oldUser);
        }

        public void DeleteUser(Guid id)
        {
            var db = GetDbContext();
            var user = db.Users.Single(u => u.Id == id);

            user.Deleted = true;
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserDelete, user);
        }

        public IEnumerable<User> GetUsersByGroup(Group group)
        {
            var db = GetDbContext();

            return db.GroupUsers.Where(g => g.GroupRef == group.Id && !g.User.Deleted).Select(g => g.User);
        }

        #endregion

        #region Role members

        public Role GetRole(int id)
        {
            return (Role) id;
        }

        public IEnumerable<Role> GetRoles()
        {
            return Roles.GetAllRoles()/*.Skip(1)*/.Select(r => (Role) Enum.Parse(typeof (Role), r)).AsEnumerable();
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

            _LmsService.Inform(UserNotifications.GroupEdit, group);
        }

        public void DeleteGroup(int id)
        {
            var db = GetDbContext();
            var group = GetGroup(id);

            db.Groups.DeleteOnSubmit(group);
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.GroupDelete, group);
        }

        public void AddUserToGroup(Group group, User user)
        {
            var db = GetDbContext();

            GroupUser groupUser = new GroupUser();
            groupUser.GroupRef = group.Id;
            groupUser.UserRef = user.Id;

            db.GroupUsers.InsertOnSubmit(groupUser);
            db.SubmitChanges();
        }

        public void RemoveUserFromGroup(Group group, User user)
        {
            var db = GetDbContext();

            GroupUser groupUser = db.GroupUsers.Single(g => g.GroupRef == group.Id && g.UserRef == user.Id);
            db.GroupUsers.DeleteOnSubmit(groupUser);
            db.SubmitChanges();
        }

        #endregion

        #endregion
    }
}