using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using Kent.Boogaart.KBCsv;

namespace IUDICO.UserManagement.Models.Storage
{
    public class DatabaseUserStorage : IUserStorage
    {
        protected ILmsService _LmsService;
        protected const string _AllowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        protected const string EmailHost = "mail.lviv.ua";
        protected const int EmailPort = 25;
        protected const string EmailUser = "report@tests-ua.com";
        /*protected const string EmailPassword = "iudico2012";*/

        public DatabaseUserStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        protected virtual IDataContext GetDbContext()
        {
            return new DBDataContext();
        }

        protected virtual string GetPath()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath("~/");
            }

            var localPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetFullPath(localPath + @"\..\..\..\..\IUDICO.LMS");
        }

        public virtual bool SendEmail(string fromAddress, string toAddress, string subject, string body)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate(object sender, X509Certificate certificate, X509Chain chain,
                             SslPolicyErrors sslPolicyErrors)
                        { return true; };

                var message = new MailMessage(new MailAddress(EmailUser), new MailAddress(toAddress))
                                  {Subject = subject, Body = body};

                var client = new SmtpClient(EmailHost, EmailPort)
                                 {
                                     EnableSsl = false,
                                     DeliveryMethod = SmtpDeliveryMethod.Network,
                                     UseDefaultCredentials = false,
                                     /*Credentials = new NetworkCredential(EmailUser, EmailPassword),*/
                                 };

                client.Send(message);

                return true;
            }
            catch
            {
                return false;
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

        public virtual User GetCurrentUser()
        {
            if (HttpContext.Current == null || HttpContext.Current.User == null ||
                !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userrole = new UserRole { RoleRef = (int)Role.None };
                var user = new User();

                user.UserRoles.Add(userrole);

                return user;
            }

            var identity = HttpContext.Current.User.Identity;
            
            return GetUser(u => u.Username == identity.Name);

            //var db = GetDbContext();
            //return db.Users.Where(u => u.Username == identity.Name).FirstOrDefault();
        }

        public virtual User GetUser(Guid userId)
        {
            return GetDbContext().Users.Where(user => !user.Deleted && user.Id == userId).SingleOrDefault();
        }

        public virtual User GetUser(Func<User, bool> predicate)
        {
            return GetDbContext().Users.Where(user => !user.Deleted).SingleOrDefault(predicate);
        }

        public virtual IEnumerable<User> GetUsers()
        {
            return GetDbContext().Users.Where(u => !u.Deleted);
        }

        public virtual IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return GetDbContext().Users.Where(u => !u.Deleted).Where(predicate);
        }

        public virtual IEnumerable<User> GetUsers(int pageIndex, int pageSize)
        {
            return GetDbContext().Users.Skip(pageIndex).Take(pageSize);
        }

        public virtual bool UsernameExists(string username)
        {
            var db = GetDbContext();

            return db.Users.Count(u => u.Username == username && u.Deleted == false) > 0;
        }

        public virtual bool UserUniqueIdAvailable(string userUniqueId, Guid userId)
        {
            var db = GetDbContext();
            var users = db.Users.Where(u => u.UserId == userUniqueId && u.Deleted == false);
            var count = users.Count();

            if (count == 0 || (count == 1 && users.First().Id == userId))
            {
                return true;
            }

            return false;
        }

        public virtual void ActivateUser(Guid id)
        {
            var db = GetDbContext();

            var user = GetUser(id);
            // db.Users.Single(u => u.Id == id);
            db.Users.Attach(user);

            user.IsApproved = true;
            user.ApprovedBy = GetCurrentUser().Id;

            db.SubmitChanges();
        }

        public virtual void DeactivateUser(Guid id)
        {
            var db = GetDbContext();

            var user = GetUser(id);
            //db.Users.Single(u => u.Id == id);
            db.Users.Attach(user);
            
            user.IsApproved = false;
            user.ApprovedBy = null;

            db.SubmitChanges();
        }

        public virtual string EncryptPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(password);

            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "");
        }

        public virtual User RestorePassword(RestorePasswordModel restorePasswordModel)
        {
            var db = GetDbContext();

            var user = GetUser(u => u.Email == restorePasswordModel.Email);
            var password = RandomPassword();

            db.Users.Attach(user);

            user.Password = EncryptPassword(password);

            db.SubmitChanges();

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your password has been changed:" + password);

            return user;
        }

        public virtual bool CreateUser(User user)
        {
            if (UsernameExists(user.Username))
            {
                return false;
            }

            var db = GetDbContext();

            user.Id = Guid.NewGuid();
            user.Password = EncryptPassword(user.Password);
            user.OpenId = user.OpenId ?? string.Empty;
            user.Deleted = false;
            user.IsApproved = true;
            user.CreationDate = DateTime.Now;
            user.ApprovedBy = GetCurrentUser().Id;

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserCreate, user);

            return true;
        }

        public virtual Dictionary<string, string> CreateUsersFromCSV(string csvPath)
        {
            var users = new List<User>();
            var passwords = new Dictionary<string, string>();
            var db = GetDbContext();

            using (var reader = new CsvReader(csvPath))
            {
                reader.ReadHeaderRecord();

                foreach (var record in reader.DataRecords)
                {
                    var username = record.GetValueOrNull("Username");

                    if (UsernameExists(username))
                    {
                        continue;
                    }

                    var role = (int) Enum.Parse(typeof (Role), record.GetValueOrNull("Role") ?? "Student");
                    var password = record.GetValueOrNull("Password");

                    if (string.IsNullOrEmpty(password))
                    {
                        password = RandomPassword();
                    }

                    var user = new User
                                   {
                                       Id = Guid.NewGuid(),
                                       Username = username,
                                       Password = EncryptPassword(password),
                                       Email = record.GetValueOrNull("Email") ?? string.Empty,
                                       Name = record.GetValueOrNull("Name") ?? string.Empty,
                                       OpenId = record.GetValueOrNull("OpenId") ?? string.Empty,
                                       Deleted = false,
                                       IsApproved = true,
                                       ApprovedBy = GetCurrentUser().Id,
                                       CreationDate = DateTime.Now,
                                   };

                    user.UserRoles.Add(new UserRole {RoleRef = role, UserRef = user.Id});

                    users.Add(user);
                    passwords.Add(user.Username, password);
                }
            }

            db.Users.InsertAllOnSubmit(users);
            db.SubmitChanges();

            foreach (var user in users)
            {
                SendEmail("admin@iudico", user.Email, "Iudico Notification",
                          "Your account has been created:\nUsername: " + user.Username + "\nPassword: " +
                          passwords[user.Username]);
            }

            _LmsService.Inform(UserNotifications.UserCreateMultiple, users);

            return passwords;
        }

        public virtual void EditUser(Guid id, User user)
        {
            var db = GetDbContext();
            var oldUser = GetUser(id);
            //db.Users.Single(u => u.Id == id);

            db.Users.Attach(oldUser);

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

        public virtual void EditUser(Guid id, EditUserModel user)
        {
            var db = GetDbContext();
            //var oldUser = GetUser(id);
            var newUser = GetUser(id);
            //db.Users.Single(u => u.Id == id);

            db.Users.Attach(newUser);
            object[] data = new object[2];

            newUser.Name = user.Name;

            if (!string.IsNullOrEmpty(user.Password))
            {
                newUser.Password = EncryptPassword(user.Password);
            }

            newUser.Email = user.Email;
            newUser.OpenId = user.OpenId ?? string.Empty;
            newUser.UserId = user.UserId;

            db.SubmitChanges();
            //data[0] = oldUser;
            //data[1] = newUser;
            _LmsService.Inform(UserNotifications.UserEdit, newUser);
        }

        public virtual User DeleteUser(Func<User, bool> predicate)
        {
            var db = GetDbContext();

            var user = db.Users.Where(u => !u.Deleted).Single(predicate);
            var links = db.GroupUsers.Where(g => g.UserRef == user.Id);

            user.Deleted = true;

            db.GroupUsers.DeleteAllOnSubmit(links);
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.UserDelete, user);

            return user;
        }

        public virtual IEnumerable<User> GetUsersInGroup(Group group)
        {
            var db = GetDbContext();

            return db.GroupUsers.Where(g => g.GroupRef == group.Id && !g.User.Deleted).Select(g => g.User);
        }

        public virtual IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            var db = GetDbContext();

            return
                db.Users.Where(u => !u.Deleted).Except(
                    db.GroupUsers.Where(g => g.GroupRef == group.Id).Select(g => g.User));
        }

        public virtual User RegisterUser(RegisterModel registerModel)
        {
            var db = GetDbContext();

            var userrole = new UserRole
                               {
                                   RoleRef = (int) Role.Student
                               };

            var user = new User
                           {
                               Id = Guid.NewGuid(),
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

            // TODO: CreateUser(user);

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();

            SendEmail("admin@iudico", user.Email, "Iudico Notification",
                      "Your account has been created:\nUsername: " + registerModel.Username + "\nPassword: " +
                      registerModel.Password);

            return user;
        }

        public virtual void EditAccount(EditModel editModel)
        {
            var identity = HttpContext.Current.User.Identity;

            var db = GetDbContext();

            var user = GetUser(editModel.Id);
            // db.Users.Single(u => u.Username == identity.Name);

            db.Users.Attach(user);

            user.Name = editModel.Name;
            user.OpenId = editModel.OpenId ?? string.Empty;
            user.Email = editModel.Email;
            user.UserId = editModel.UserId;
            
            db.SubmitChanges();

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your details have been changed.");
        }

        public virtual void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            /*
            var identity = HttpContext.Current.User.Identity;

            var user = db.Users.Single(u => u.Username == identity.Name);
            */

            var db = GetDbContext();
            var user = GetCurrentUser();

            db.Users.Attach(user);

            user.Password = EncryptPassword(changePasswordModel.NewPassword);

            db.SubmitChanges();

            SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your password has been changed.");
        }

        #endregion

        #region Roles members

        public virtual IEnumerable<User> AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var db = GetDbContext();
            var users = GetUsers(u => usernames.Contains(u.Username));

            var userRoles = new List<UserRole>();

            // users.ForEach(u => roles.ForEach(r => userRoles.Add(new UserRole { UserRef = u.Id, RoleRef = (int)r })));

            foreach (var user in users)
            {
                foreach (var role in roles)
                {
                    userRoles.Add(new UserRole
                                    {
                                        UserRef = user.Id,
                                        RoleRef = (int) role
                                    });
                }
            }

            db.UserRoles.InsertAllOnSubmit(userRoles);
            db.SubmitChanges();

            return users;
        }

        public virtual IEnumerable<User> RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var db = GetDbContext();
            var users = GetUsers(u => usernames.Contains(u.Username));
            var userIds = users.Select(u => u.Id);
            var intRoles = roles.Select(r => (int) r);

            var userRoles = db.UserRoles.Where(ur => userIds.Contains(ur.UserRef) && intRoles.Contains(ur.RoleRef));

            db.UserRoles.DeleteAllOnSubmit(userRoles);
            db.SubmitChanges();

            return users;
        }

        public virtual IEnumerable<User> GetUsersInRole(Role role)
        {
            var db = GetDbContext();

            return db.UserRoles.Where(ur => ur.RoleRef == (int) role).Select(ur => ur.User);
        }

        public virtual IEnumerable<Role> GetUserRoles(string username)
        {
            var db = GetDbContext();

            var roles = db.UserRoles.Where(ur => ur.User.Username == username).Select(ur => (Role) ur.RoleRef).ToList();

            if (IsPromotedToAdmin() && !roles.Contains(Role.Admin))
            {
                roles.Add(Role.Admin);
            }

            return roles;
        }

        public virtual void RemoveUserFromRole(Role role, User user)
        {
            var db = GetDbContext();

            var userRole = db.UserRoles.Single(g => g.RoleRef == (int) role && g.UserRef == user.Id);

            db.UserRoles.DeleteOnSubmit(userRole);
            db.SubmitChanges();
        }

        public virtual void AddUserToRole(Role role, User user)
        {
            var db = GetDbContext();

            var userRole = new UserRole {RoleRef = (int) role, UserRef = user.Id};

            db.UserRoles.InsertOnSubmit(userRole);
            db.SubmitChanges();
        }

        public virtual IEnumerable<Role> GetRolesAvailableToUser(User user)
        {
            var roles = GetUserRoles(user.Username);

            return UserRoles.GetRoles().Where(r => !roles.Contains(r));
        }

        public virtual bool IsPromotedToAdmin()
        {
            try
            {
                return (bool) HttpContext.Current.Session["AllowAdmin"];
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual void RateTopic(int topicId, int rating)
        {
            var db = GetDbContext();

            var r = new UserTopicRating {Rating = rating, TopicId = topicId, UserId = GetCurrentUser().Id};
            db.UserTopicRatings.InsertOnSubmit(r);

            db.SubmitChanges();
        }

        #endregion

        #region Group members

        public virtual Group GetGroup(int id)
        {
            var db = GetDbContext();

            return db.Groups.FirstOrDefault(group => group.Id == id && !group.Deleted);
        }

        public virtual IEnumerable<Group> GetGroups()
        {
            var db = GetDbContext();

            return db.Groups.Where(g => !g.Deleted);
        }

        public virtual void CreateGroup(Group group)
        {
            var db = GetDbContext();

            group.Deleted = false;

            db.Groups.InsertOnSubmit(group);
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.GroupCreate, group);
        }

        public virtual void EditGroup(int id, Group group)
        {
            var db = GetDbContext();
            var oldGroup = db.Groups.Single(g => g.Id == id && !g.Deleted);
            var newGroup = oldGroup;

            newGroup.Name = group.Name;
            db.SubmitChanges();

            var data = new object[] { oldGroup, newGroup };

            _LmsService.Inform(UserNotifications.GroupEdit, data);
        }

        public virtual void DeleteGroup(int id)
        {
            var db = GetDbContext();
            var group = db.Groups.Single(g => g.Id == id && !g.Deleted);

            var links = db.GroupUsers.Where(g => g.GroupRef == group.Id);

            db.GroupUsers.DeleteAllOnSubmit(links);

            group.Deleted = true;
            db.SubmitChanges();

            _LmsService.Inform(UserNotifications.GroupDelete, group);
        }

        public virtual IEnumerable<Group> GetGroupsByUser(User user)
        {
            var db = GetDbContext();

            return db.GroupUsers.Where(g => g.UserRef == user.Id).Select(g => g.Group);
        }

        public virtual IEnumerable<Group> GetGroupsAvailableToUser(User user)
        {
            var db = GetDbContext();

            var groupRefsByUser = GetGroupsByUser(user).Select(g => g.Id);

            return db.Groups.Where(g => !groupRefsByUser.Contains(g.Id));
        }

        public virtual void AddUserToGroup(Group group, User user)
        {
            var db = GetDbContext();

            var groupUser = new GroupUser {GroupRef = group.Id, UserRef = user.Id};

            db.GroupUsers.InsertOnSubmit(groupUser);
            db.SubmitChanges();
        }

        public virtual void RemoveUserFromGroup(Group group, User user)
        {
            var db = GetDbContext();

            var groupUser = db.GroupUsers.Single(g => g.GroupRef == group.Id && g.UserRef == user.Id);

            db.GroupUsers.DeleteOnSubmit(groupUser);
            db.SubmitChanges();
        }

        #endregion

        public virtual int UploadAvatar(Guid id, HttpPostedFileBase file)
        {
            int resultCode = -1; // if no file had been passed
            if (file != null)
            {
                string fileName = Path.GetFileName(id + ".png");
                
                string fullPath = Path.Combine(Path.Combine(GetPath(), @"Data\Avatars"), fileName);
                FileInfo fileInfo = new FileInfo(fullPath);

                resultCode = 1; // if some file had been passed

                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                    resultCode = 2; // if the file with the same name already exists
                }

                file.SaveAs(fullPath);
            }
            return resultCode;
        }

        public virtual int DeleteAvatar(Guid id)
        {
            string fileName = Path.GetFileName(id + ".png");
            string fullPath = Path.Combine(Path.Combine(GetPath(), @"Data\Avatars"), fileName);
            FileInfo fileInfo = new FileInfo(fullPath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                return 1; // if the file had been removed successfully
            }
            return -1; // if there was no file with such name in directory
        }

        #endregion
    }
}