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
using System.Net.Mail;
using IUDICO.UserManagement.Models.Database;

namespace IUDICO.UserManagement.Models.Storage
{
    public class DatabaseUserStorage : IUserStorage
    {
        protected ILmsService _LmsService;

        public DatabaseUserStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        public string EncryptPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(password);
            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "");
        }

        public bool SendEmail(string fromAddress, string toAddress, string subject, string body)
        {
            try
            {
                var message = new MailMessage(new MailAddress(fromAddress), new MailAddress(toAddress)) { Subject = subject, Body = body };
                var client = new SmtpClient("krez.lviv.ua");
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
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

            using (var db = new UserManagementDBContext())
            {
                return db.Users.Where(u => u.Username == identity.Name).FirstOrDefault();
            }
        }

        public User GetUser(Func<User, bool> predicate)
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Users.Where(user => !user.Deleted).SingleOrDefault(predicate);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Users.Where(u => !u.Deleted);
            }
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Users.Where(u => !u.Deleted).Where(predicate);
            }
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize)
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Users.Skip(pageIndex).Take(pageSize);
            }
        }

        public bool UsernameExists(string username)
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Users.Count(u => u.Username == username && u.Deleted == false) > 0;
            }
        }

        public void ActivateUser(Guid id)
        {
            using (var db = new UserManagementDBContext())
            {
                var user = db.Users.Single(u => u.Id == id);
                user.IsApproved = true;
                user.ApprovedBy = GetCurrentUser().Id;

                db.SaveChanges();
            }
        }

        public void DeactivateUser(Guid id)
        {
            using (var db = new UserManagementDBContext())
            {
                var user = db.Users.Single(u => u.Id == id);
                user.IsApproved = false;
                user.ApprovedBy = null;

                db.SaveChanges();
            }
        }

        public void CreateUser(User user)
        {
            using (var db = new UserManagementDBContext())
            {
                user.Password = EncryptPassword(user.Password);
                user.OpenId = user.OpenId ?? string.Empty;
                user.Deleted = false;
                user.IsApproved = true;
                user.CreationDate = DateTime.Now;
                user.ApprovedBy = GetCurrentUser().Id;

                db.Users.Add(user);
                db.SaveChanges();
            }
            
            _LmsService.Inform(UserNotifications.UserCreate, user);
        }

        public void EditUser(Guid id, User user)
        {
            using (var db = new UserManagementDBContext())
            {
                var oldUser = db.Users.Single(u => u.Id == id);

                oldUser.Name = user.Name;
                if (user.Password != null && user.Password != string.Empty)
                    oldUser.Password = EncryptPassword(user.Password);
                oldUser.Email = user.Email;
                oldUser.OpenId = user.OpenId ?? string.Empty;
                oldUser.RoleId = user.RoleId;
                oldUser.Username = user.Username;
                oldUser.IsApproved = user.IsApproved;

                db.SaveChanges();

                _LmsService.Inform(UserNotifications.UserEdit, oldUser);
            }
        }

        public void EditUser(Guid id, EditUserModel user)
        {
            using (var db = new UserManagementDBContext())
            {
                var oldUser = db.Users.Single(u => u.Id == id);

                oldUser.Name = user.Name;
                if (user.Password != null && user.Password != string.Empty)
                    oldUser.Password = EncryptPassword(user.Password);
                oldUser.Email = user.Email;
                oldUser.OpenId = user.OpenId ?? string.Empty;
                oldUser.RoleId = user.RoleId;

                db.SaveChanges();

                _LmsService.Inform(UserNotifications.UserEdit, oldUser);
            }
        }

        public void DeleteUser(Func<User, bool> predicate)
        {
            using (var db = new UserManagementDBContext())
            {
                var user = db.Users.Where(u => !u.Deleted).Single(predicate);

                user.Deleted = true;
                user.Groups.Clear();

                db.SaveChanges();

                _LmsService.Inform(UserNotifications.UserDelete, user);
            }
        }

        public IEnumerable<User> GetUsersInGroup(Group group)
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Users.Where(u => u.Groups.Contains(group));
                //return db.GroupUsers.Where(g => g.GroupRef == group.Id && !g.User.Deleted).Select(g => g.User);
            }
        }

        public IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Users.Where(u => !u.Deleted).Except(db.Users.Where(m => m.Groups.Contains(group)));
                //return db.Users.Where(u => !u.Deleted).Except(db.GroupUsers.Where(g => g.GroupRef == group.Id).Select(g => g.User));
            }
        }

        public void RegisterUser(RegisterModel registerModel)
        {
            using (var db = new UserManagementDBContext())
            {
                var user = new User
                                {
                                    Username = registerModel.Username,
                                    Password = EncryptPassword(registerModel.Password),
                                    OpenId = registerModel.OpenId ?? string.Empty,
                                    Email = registerModel.Email,
                                    Name = registerModel.Name,
                                    Role = Role.Student,
                                    IsApproved = false,
                                    Deleted = false,
                                    CreationDate = DateTime.Now,
                                    ApprovedBy = null
                                };

                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void EditAccount(EditModel editModel)
        {
            var identity = HttpContext.Current.User.Identity;

            using (var db = new UserManagementDBContext())
            {
                var user = db.Users.Single(u => u.Username == identity.Name);

                user.Name = editModel.Name;
                user.OpenId = editModel.OpenId ?? string.Empty;
                user.Email = editModel.Email;

                db.SaveChanges();

                SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your details have been changed.");
            }
        }

        public void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = GetCurrentUser();

            using (var db = new UserManagementDBContext())
            {
                user.Password = EncryptPassword(changePasswordModel.NewPassword);

                db.SaveChanges();
            }

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your passord has been changed.");
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
            using (var db = new UserManagementDBContext())
            {
                return db.Groups.First(group => group.Id == id && !group.Deleted);
            }
        }

        public IEnumerable<Group> GetGroups()
        {
            using (var db = new UserManagementDBContext())
            {
                return db.Groups.Where(g => !g.Deleted);
            }
        }

        public void CreateGroup(Group group)
        {
            using (var db = new UserManagementDBContext())
            {
                group.Deleted = false;

                db.Groups.Add(group);
                db.SaveChanges();
            }
        }

        public void EditGroup(int id, Group group)
        {
            using (var db = new UserManagementDBContext())
            {
                var oldGroup = db.Groups.Single(g => g.Id == id && !g.Deleted);

                oldGroup.Name = group.Name;
                db.SaveChanges();
            }

            _LmsService.Inform(UserNotifications.GroupEdit, group);
        }

        public void DeleteGroup(int id)
        {
            using (var db = new UserManagementDBContext())
            {
                var group = db.Groups.Single(g => g.Id == id && !g.Deleted);

                group.Users.Clear();
                group.Deleted = true;
                
                db.SaveChanges();

                _LmsService.Inform(UserNotifications.GroupDelete, group);
            }
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            using (var db = new UserManagementDBContext())
            {
                db.Users.Attach(user);

                return user.Groups;
            }
        }

        public IEnumerable<Group> GetGroupsAvaliableForUser(User user)
        {
            using (var db = new UserManagementDBContext())
            {
                var groupRefsByUser = GetGroupsByUser(user).Select(g => g.Id);

                return db.Groups.Where(g => !groupRefsByUser.Contains(g.Id));
            }
        }

        public void AddUserToGroup(Group group, User user)
        {
            using (var db = new UserManagementDBContext())
            {
                db.Users.Attach(user);
                db.Groups.Attach(group);
                
                group.Users.Add(user);

                db.SaveChanges();
            }
        }

        public void RemoveUserFromGroup(Group group, User user)
        {
            using (var db = new UserManagementDBContext())
            {
                db.Users.Attach(user);
                db.Groups.Attach(group);
                
                db.Users.Remove(user);

                db.SaveChanges();
            }
        }

        #endregion

        #endregion
    }
}