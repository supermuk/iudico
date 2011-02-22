using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
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

        public string EncryptPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(password);
            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "");
        }

        #region Implementation of IUserStorage

        #region User members

        public User GetCurrentUser()
        {
            if (HttpContext.Current.User == null)
            {
                var user = new User {RoleId = (int) Role.None};

                return user;
            }

            var identity = HttpContext.Current.User.Identity;

            if (!identity.IsAuthenticated)
            {
                return null;
            }

            var db = GetDbContext();

            return db.Users.Where(u => u.Username == identity.Name).FirstOrDefault();
        }

        public User GetUser(Func<User, bool> predicate)
        {
            var db = GetDbContext();

            return db.Users.Where(user => !user.Deleted).SingleOrDefault(predicate);
        }

        public IEnumerable<User> GetUsers()
        {
            var db = GetDbContext();

            return db.Users.Where(u => !u.Deleted);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            var db = GetDbContext();

            return db.Users.Where(u => !u.Deleted).Where(predicate);
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize)
        {
            var db = GetDbContext();

            return db.Users.Skip(pageIndex).Take(pageSize);
        }

        public bool UsernameExists(string username)
        {
            var db = GetDbContext();

            return db.Users.Count(u => u.Username == username) > 0;
        }

        public void ActivateUser(Guid id)
        {
            var db = GetDbContext();

            var user = db.Users.Single(u => u.Id == id);
            user.IsApproved = true;
            user.ApprovedBy = GetCurrentUser().Id;

            db.SubmitChanges();
        }

        public void DeactivateUser(Guid id)
        {
            var db = GetDbContext();

            var user = db.Users.Single(u => u.Id == id);
            user.IsApproved = false;
            user.ApprovedBy = null;

            db.SubmitChanges();
        }

        public void CreateUser(User user)
        {
            var db = GetDbContext();

            user.Password = EncryptPassword(user.Password);
            user.OpenId = user.OpenId ?? string.Empty;
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
            oldUser.Password = EncryptPassword(user.Password);
            oldUser.Email = user.Email;
            oldUser.OpenId = user.OpenId ?? string.Empty;
            oldUser.RoleId = user.RoleId;
            oldUser.Username = user.Username;
            oldUser.IsApproved = user.IsApproved;
            
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserEdit, oldUser);
        }

        public void DeleteUser(Func<User, bool> predicate)
        {
            var db = GetDbContext();

            var user = db.Users.Where(u => !u.Deleted).Single(predicate);
            var links = db.GroupUsers.Where(g => g.UserRef == user.Id);

            user.Deleted = true;

            db.GroupUsers.DeleteAllOnSubmit(links);
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserDelete, user);
        }

        public IEnumerable<User> GetUsersInGroup(Group group)
        {
            var db = GetDbContext();

            return db.GroupUsers.Where(g => g.GroupRef == group.Id && !g.User.Deleted).Select(g => g.User);
        }

        public IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            var db = GetDbContext();

            return db.Users.Except(db.GroupUsers.Where(g => g.GroupRef == group.Id).Select(g => g.User));
        }

        public void RegisterUser(RegisterModel registerModel)
        {
            var db = GetDbContext();

            var user = new User
                            {
                                Username = registerModel.Username,
                                Password = EncryptPassword(registerModel.Password),
                                OpenId = registerModel.OpenId ?? string.Empty,
                                Email = registerModel.Email,
                                Name = registerModel.Name,
                                Role = Role.Student,
                                IsApproved = false,
                                Deleted = false
                            };

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();
        }

        public void EditAccount(EditModel editModel)
        {
            var identity = HttpContext.Current.User.Identity;

            var db = GetDbContext();

            var user = db.Users.Single(u => u.Username == identity.Name);

            user.Name = editModel.Name;
            user.OpenId = editModel.OpenId ?? string.Empty;
            user.Email = editModel.Email;

            db.SubmitChanges();
        }

        public void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var db = GetDbContext();

            var user = GetCurrentUser();
            user.Password = EncryptPassword(changePasswordModel.NewPassword);

            db.SubmitChanges();
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

            return db.Groups.First(group => group.Id == id && !group.Deleted);
        }

        public IEnumerable<Group> GetGroups()
        {
            var db = GetDbContext();

            return db.Groups.Where(g => !g.Deleted);
        }

        public void CreateGroup(Group group)
        {
            var db = GetDbContext();

            group.Deleted = false;

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
            var group = db.Groups.Single(g => g.Id == id && !g.Deleted);

            var links = db.GroupUsers.Where(g => g.GroupRef == group.Id);

            db.GroupUsers.DeleteAllOnSubmit(links);

            group.Deleted = true;
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.GroupDelete, group);
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            var db = GetDbContext();

            return db.GroupUsers.Where(g => g.UserRef == user.Id).Select(g => g.Group);
        }

        public void AddUserToGroup(Group group, User user)
        {
            var db = GetDbContext();

            var groupUser = new GroupUser {GroupRef = group.Id, UserRef = user.Id};

            db.GroupUsers.InsertOnSubmit(groupUser);
            db.SubmitChanges();
        }

        public void RemoveUserFromGroup(Group group, User user)
        {
            var db = GetDbContext();

            var groupUser = db.GroupUsers.Single(g => g.GroupRef == group.Id && g.UserRef == user.Id);

            db.GroupUsers.DeleteOnSubmit(groupUser);
            db.SubmitChanges();
        }

        #endregion

        #endregion
    }
}