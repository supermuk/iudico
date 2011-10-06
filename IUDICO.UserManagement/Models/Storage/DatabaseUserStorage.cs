using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;
using System.Net.Mail;
using Kent.Boogaart.KBCsv;

namespace IUDICO.UserManagement.Models.Storage
{
    public class DatabaseUserStorage : IUserStorage
    {
        protected ILmsService _LmsService;
        protected const string _AllowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        protected const string EmailHost = "smtp.gmail.com";
        protected const int EmailPort = 587;
        protected const string EmailUser = "iudico.report@gmail.com";
        protected const string EmailPassword = "iudico2011";
        
        public DatabaseUserStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        protected DBDataContext GetDbContext()
        {
            return _LmsService.GetDbDataContext();
        }

        public static void SendEmail(string fromAddress, string toAddress, string subject, string body)
        {
            try
            {
                var message = new MailMessage(new MailAddress(EmailUser), new MailAddress(toAddress)) { Subject = subject, Body = body };
                
                var client = new SmtpClient(EmailHost, EmailPort)
                                 {
                                     EnableSsl = true,
                                     DeliveryMethod = SmtpDeliveryMethod.Network,
                                     UseDefaultCredentials = false,
                                     Credentials = new NetworkCredential(EmailUser, EmailPassword)
                                 };

                client.Send(message);
            }
            catch
            {
            }
        }

        protected string RandomPassword()
        {
            var builder = new StringBuilder();
            var random = new Random();

            for (var i = 0; i < 6; i++)
            {
                builder.Append(_AllowedChars[random.Next(_AllowedChars.Length)]);
            }

            return builder.ToString();
        }

        #region Implementation of IUserStorage

        #region User members

        public User GetCurrentUser()
        {
            if (HttpContext.Current.User == null)
            {
                var userrole = new UserRole {RoleRef = (int)Role.None};
                var user = new User();

                user.UserRoles.Add(userrole);

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

            return db.Users.Count(u => u.Username == username && u.Deleted == false) > 0;
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

        public string EncryptPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(password);

            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "");
        }

        public void RestorePassword(RestorePasswordModel restorePasswordModel)
        {
            var db = GetDbContext();

            var user = db.Users.Single(u => u.Email == restorePasswordModel.Email);
            var password = RandomPassword();

            user.Password = EncryptPassword(password);

            db.SubmitChanges();

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your password has been changed:" + password);
        }

        public void CreateUser(User user)
        {
            var db = GetDbContext();

            user.Password = EncryptPassword(user.Password);
            user.OpenId = user.OpenId ?? string.Empty;
            user.Deleted = false;
            user.IsApproved = true;
            user.CreationDate = DateTime.Now;
            user.ApprovedBy = GetCurrentUser().Id;

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserCreate, user);
        }

        public Dictionary<string, string> CreateUsersFromCSV(string csvPath)
        {
            var users = new List<User>();
            var passwords = new Dictionary<string, string>();
            var db = GetDbContext();

            using (var reader = new CsvReader(csvPath))
            {
                reader.ReadHeaderRecord();

                foreach (var record in reader.DataRecords)
                {
                    var role = (int) Enum.Parse(typeof (Role), record.GetValueOrNull("Role") ?? "Student");
                    var password = record.GetValueOrNull("Password") ?? RandomPassword();

                    var user = new User
                                   {
                                       Username = record.GetValueOrNull("Username"),
                                       Password = EncryptPassword(password),
                                       Email = record.GetValueOrNull("Email"),
                                       Name = record.GetValueOrNull("Name") ?? string.Empty,
                                       OpenId = record.GetValueOrNull("OpenId") ?? string.Empty,
                                       Deleted = false,
                                       IsApproved = true,
                                       ApprovedBy = GetCurrentUser().Id,
                                       CreationDate = DateTime.Now,
                                   };

                    user.UserRoles.Add(new UserRole {RoleRef = role});

                    users.Add(user);
                    passwords.Add(user.Username, password);
                }
            }

            db.Users.InsertAllOnSubmit(users);
            db.SubmitChanges();

            foreach (var user in users)
            {
                SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your account has been created:\nUsername: " + user.Username + "\nPassword: " + passwords[user.Username]);
            }

            _LmsService.Inform(UserNotifications.UserCreateMultiple, users);

            return passwords;
        }

        public void EditUser(Guid id, User user)
        {
            var db = GetDbContext();
            var oldUser = db.Users.Single(u => u.Id == id);

            oldUser.Name = user.Name;

            if (!string.IsNullOrEmpty(user.Password))
            {
                oldUser.Password = EncryptPassword(user.Password);
            }
                
            oldUser.Email = user.Email;
            oldUser.OpenId = user.OpenId ?? string.Empty;
            oldUser.Username = user.Username;
            oldUser.IsApproved = user.IsApproved;
            
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserEdit, oldUser);
        }

        public void EditUser(Guid id, EditUserModel user)
        {
            var db = GetDbContext();
            var oldUser = db.Users.Single(u => u.Id == id);

            oldUser.Name = user.Name;
            
            if (!string.IsNullOrEmpty(user.Password))
            {
                oldUser.Password = EncryptPassword(user.Password);
            }
                
            oldUser.Email = user.Email;
            oldUser.OpenId = user.OpenId ?? string.Empty;

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

        public void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var db = GetDbContext();
            var users = db.Users.Where(u => usernames.Contains(u.Username));

            foreach (var user in users)
            {
                foreach (var role in roles)
                {
                    user.UserRoles.Add(new UserRole
                    {
                        UserRef = user.Id,
                        RoleRef = (int)role
                    });
                }
            }

            db.SubmitChanges();
        }

        public void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var db = GetDbContext();
            var users = db.Users.Where(u => usernames.Contains(u.Username)).Select(u => u.Id);
            var intRoles = roles.Select(r => (int) r);

            var userRoles = db.UserRoles.Where(ur => users.Contains(ur.UserRef) && intRoles.Contains(ur.RoleRef));

            db.UserRoles.DeleteAllOnSubmit(userRoles);
            db.SubmitChanges();
        }

        public IEnumerable<User> GetUsersInRole(Role role)
        {
            var db = GetDbContext();

            return db.UserRoles.Where(ur => ur.RoleRef == (int) role).Select(ur => ur.User);
        }

        public IEnumerable<Role> GetUserRoles(string username)
        {
            var db = GetDbContext();

            return db.UserRoles.Where(ur => ur.User.Username == username).Select(ur => (Role) ur.RoleRef);
        }

        public void RemoveUserFromRole(Role role, User user)
        {
            var db = GetDbContext();

            var userRole = db.UserRoles.Single(g => g.RoleRef == (int)role && g.UserRef == user.Id);

            db.UserRoles.DeleteOnSubmit(userRole);
            db.SubmitChanges();
        }

        public void AddUserToRole(Role role, User user)
        {
            var db = GetDbContext();

            var userRole = new UserRole { RoleRef = (int)role, UserRef = user.Id };

            db.UserRoles.InsertOnSubmit(userRole);
            db.SubmitChanges();
        }

        public IEnumerable<Role> GetRolesAvailableToUser(User user)
        {
            var roles = GetUserRoles(user.Username);

            return GetRoles().Where(r => !roles.Contains(r));
        }

        public IEnumerable<User> GetUsersInGroup(Group group)
        {
            var db = GetDbContext();

            return db.GroupUsers.Where(g => g.GroupRef == group.Id && !g.User.Deleted).Select(g => g.User);
        }

        public IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            var db = GetDbContext();

            return db.Users.Where(u => !u.Deleted).Except(db.GroupUsers.Where(g => g.GroupRef == group.Id).Select(g => g.User));
        }

        public void RegisterUser(RegisterModel registerModel)
        {
            var db = GetDbContext();

            var userrole = new UserRole
                               {
                                   RoleRef = (int) Role.Student
                               };

            var user = new User
                            {
                                Username = registerModel.Username,
                                Password = EncryptPassword(registerModel.Password),
                                OpenId = registerModel.OpenId ?? string.Empty,
                                Email = registerModel.Email,
                                Name = registerModel.Name,
                                IsApproved = false,
                                Deleted = false,
                                CreationDate = DateTime.Now,
                                ApprovedBy = null
                            };

            user.UserRoles.Add(userrole);

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your account has been created:\nUsername: " + registerModel.Username + "\nPassword: " + registerModel.Password);
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

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your details have been changed.");
        }

        public void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var db = GetDbContext();

            var user = GetCurrentUser();
            user.Password = EncryptPassword(changePasswordModel.NewPassword);

            db.SubmitChanges();

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your password has been changed.");
        }

        #endregion

        #region Role members

        public Role GetRole(int id)
        {
            return (Role) id;
        }

        public IEnumerable<Role> GetRoles()
        {
            return Enum.GetValues(typeof (Role)).Cast<Role>().Skip(1);
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
            var db = GetDbContext();
            var oldGroup = db.Groups.Single(g => g.Id == id && !g.Deleted);

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

        public IEnumerable<Group> GetGroupsAvailableToUser(User user)
        {
            var db = GetDbContext();

            var groupRefsByUser = GetGroupsByUser(user).Select(g => g.Id);

            return db.Groups.Where(g => !groupRefsByUser.Contains(g.Id));
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