using System;
using System.Collections.Generic;
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
    using IUDICO.Common.Models.Shared.Statistics;

    public class DatabaseUserStorage : IUserStorage
    {
        protected ILmsService lmsService;
        protected readonly LinqLogger Logger;
        protected const string AllowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        protected const string EmailHost = "mail.lviv.ua";
        protected const int EmailPort = 25;
        protected const string EmailUser = "report@tests-ua.com";
        /*protected const string EmailPassword = "iudico2012";*/

        public DatabaseUserStorage(ILmsService lmsService, LinqLogger logger)
        {
            this.lmsService = lmsService;
            this.Logger = logger;
        }

        public DatabaseUserStorage(ILmsService lmsService) : this(lmsService, null)
        {
        }

        protected virtual IDataContext GetDbContext()
        {
            var db = new DBDataContext();

#if DEBUG
            db.Log = this.Logger;
#endif

            return db;
        }

        protected virtual string GetIdentityName()
        {
            return HttpContext.Current.User.Identity.Name;
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

        public virtual bool SendEmail(string addressFrom, string addressTo, string subject, string body)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                var message = new MailMessage(new MailAddress(EmailUser), new MailAddress(addressTo)) { Subject = subject, Body = body };

                var client = new SmtpClient(EmailHost, EmailPort)
                                 {
                                     EnableSsl = false,
                                     DeliveryMethod = SmtpDeliveryMethod.Network,
                                     UseDefaultCredentials = false,
                                     /*Credentials = new NetworkCredential(EmailUser, EmailPassword),*/
                                 };

                client.SendAsync(message, null);
            }
            catch
            {
            }

            return true;
        }

        protected string RandomPassword()
        {
            var builder = new StringBuilder();
            var random = new Random();

            for (var i = 0; i < 6; i++)
            {
                builder.Append(AllowedChars[random.Next(AllowedChars.Length)]);
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

            return this.GetUser(u => u.Username == this.GetIdentityName());
        }

        public User GetUser(Guid userId)
        {
            return this.GetDbContext().Users.SingleOrDefault(user => !user.Deleted && user.Id == userId);
        }

        public User GetUser(string username)
        {
            return this.GetUser(user => user.Username == username);
        }

        public User GetUser(Func<User, bool> predicate)
        {
            return this.GetDbContext().Users.Where(u => !u.Deleted).SingleOrDefault(predicate);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.GetDbContext().Users.Where(u => !u.Deleted);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return this.GetDbContext().Users.Where(u => !u.Deleted).Where(predicate);
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize)
        {
            return this.GetDbContext().Users.Skip(pageIndex).Take(pageSize);
        }

        public bool UsernameExists(string username)
        {
            var db = this.GetDbContext();

            return db.Users.Count(u => u.Username == username && u.Deleted == false) > 0;
        }

        public bool UserUniqueIdAvailable(string userUniqueId, Guid userId)
        {
            var db = this.GetDbContext();
            var users = db.Users.Where(u => u.UserId == userUniqueId && u.Deleted == false);
            var count = users.Count();

            if (count == 0 || (count == 1 && users.First().Id == userId))
            {
                return true;
            }

            return false;
        }

        public void ActivateUser(Guid id)
        {
            var db = this.GetDbContext();

            ////var user = GetUser(id);
            var user = db.Users.Single(u => u.Id == id);
            ////db.Users.Attach(user);

            user.IsApproved = true;
            user.ApprovedBy = this.GetCurrentUser().Id;

            db.SubmitChanges();
        }

        public void DeactivateUser(Guid id)
        {
            var db = this.GetDbContext();

            ////var user = GetUser(id);
            var user = db.Users.Single(u => u.Id == id);
            ////db.Users.Attach(user);

            user.IsApproved = false;
            user.ApprovedBy = null;

            db.SubmitChanges();
        }

        public string EncryptPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(password);

            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", string.Empty);
        }

        public User RestorePassword(RestorePasswordModel restorePasswordModel)
        {
            var db = this.GetDbContext();

            var user = db.Users.SingleOrDefault(u => u.Email == restorePasswordModel.Email);
            var password = this.RandomPassword();

            user.Password = this.EncryptPassword(password);

            db.SubmitChanges();

            this.SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your password has been changed:" + password);

            return user;
        }

        public bool CreateUser(User user)
        {
            if (this.UsernameExists(user.Username))
            {
                return false;
            }

            var db = this.GetDbContext();

            user.Id = Guid.NewGuid();
            user.Password = this.EncryptPassword(user.Password);
            user.OpenId = user.OpenId ?? string.Empty;
            user.Deleted = false;
            user.IsApproved = true;
            user.CreationDate = DateTime.Now;
            user.ApprovedBy = this.GetCurrentUser().Id;

            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();

            this.lmsService.Inform(UserNotifications.UserCreate, user);

            return true;
        }

        protected User CreateCSVUser(DataRecord record)
        {
            var role = (int)Enum.Parse(typeof(Role), record.GetValueOrNull("Role") ?? "Student");
            var password = record.GetValueOrNull("Password");

            if (string.IsNullOrEmpty(password))
            {
                password = this.RandomPassword();
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = record.GetValueOrNull("Username") ?? string.Empty,
                Password = password,
                Email = record.GetValueOrNull("Email") ?? string.Empty,
                Name = record.GetValueOrNull("Name") ?? string.Empty,
                OpenId = record.GetValueOrNull("OpenId") ?? string.Empty,
                Deleted = false,
                IsApproved = true,
                ApprovedBy = this.GetCurrentUser().Id,
                CreationDate = DateTime.Now,
            };

            user.UserRoles.Add(new UserRole { RoleRef = role, UserRef = user.Id });

            return user;
        }

        public Dictionary<string, string> CreateUsersFromCSV(string csvPath)
        {
            var users = new List<User>();
            var passwords = new Dictionary<string, string>();
            var db = this.GetDbContext();

            using (var reader = new CsvReader(csvPath))
            {
                reader.ReadHeaderRecord();

                foreach (var record in reader.DataRecords)
                {
                    var username = record.GetValueOrNull("Username");

                    if (string.IsNullOrEmpty(username))
                    {
                        throw new ArgumentNullException("Username", "Invalid username");
                    }

                    if (this.UsernameExists(username))
                    {
                        continue;
                    }

                    var user = this.CreateCSVUser(record);
                    var password = user.Password;

                    user.Password = this.EncryptPassword(user.Password);

                    users.Add(user);
                    passwords.Add(user.Username, password);
                }
            }

            db.Users.InsertAllOnSubmit(users);
            db.SubmitChanges();

            foreach (var user in users)
            {
                var message = "Your account has been created:\nUsername: " + user.Username + "\nPassword: " + passwords[user.Username];
                this.SendEmail("admin@iudico", user.Email, "Iudico Notification", message);
            }

            this.lmsService.Inform(UserNotifications.UserCreateMultiple, users);

            return passwords;
        }

        public void EditUser(Guid id, User user)
        {
            var db = this.GetDbContext();
            ////var oldUser = GetUser(id);
            var oldUser = db.Users.Single(u => u.Id == id);

            ////db.Users.Attach(oldUser);

            oldUser.Name = user.Name;

            if (!string.IsNullOrEmpty(user.Password))
            {
                oldUser.Password = this.EncryptPassword(user.Password);
            }

            oldUser.Email = user.Email;
            oldUser.OpenId = user.OpenId ?? string.Empty;
            oldUser.Username = user.Username;
            oldUser.IsApproved = user.IsApproved;

            db.SubmitChanges();

            this.lmsService.Inform(UserNotifications.UserEdit, oldUser);
        }

        public void EditUser(Guid id, EditUserModel user)
        {
            var db = this.GetDbContext();
            ////var oldUser = GetUser(id);
            ////var newUser = GetUser(id);
            var newUser = db.Users.Single(u => u.Id == id);

            ////db.Users.Attach(newUser);
            object[] data = new object[2];

            newUser.Name = user.Name;

            if (!string.IsNullOrEmpty(user.Password))
            {
                newUser.Password = this.EncryptPassword(user.Password);
            }

            newUser.Email = user.Email;
            newUser.OpenId = user.OpenId ?? string.Empty;
            newUser.UserId = user.UserId;

            db.SubmitChanges();
            ////data[0] = oldUser;
            ////data[1] = newUser;
            this.lmsService.Inform(UserNotifications.UserEdit, newUser);
        }

        public User DeleteUser(Func<User, bool> predicate)
        {
            var db = this.GetDbContext();

            var user = db.Users.Where(u => !u.Deleted).Single(predicate);
            var links = db.GroupUsers.Where(g => g.UserRef == user.Id);

            user.Deleted = true;

            db.GroupUsers.DeleteAllOnSubmit(links);
            db.SubmitChanges();

            this.lmsService.Inform(UserNotifications.UserDelete, user);

            return user;
        }

        public IEnumerable<User> GetUsersInGroup(Group group)
        {
            var db = this.GetDbContext();

            return db.GroupUsers.Where(g => g.GroupRef == group.Id && !g.User.Deleted).Select(g => g.User);
        }

        public IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            var db = this.GetDbContext();

            return
                db.Users.Where(u => !u.Deleted).Except(
                    db.GroupUsers.Where(g => g.GroupRef == group.Id).Select(g => g.User));
        }

        public User RegisterUser(RegisterModel registerModel)
        {
            var db = this.GetDbContext();

            var userrole = new UserRole
                               {
                                   RoleRef = (int)Role.Student
                               };

            var user = new User
                           {
                               Id = Guid.NewGuid(),
                               Username = registerModel.Username,
                               Password = this.EncryptPassword(registerModel.Password),
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

            var message = "Your account has been created:\nUsername: " + registerModel.Username + "\nPassword: " + registerModel.Password;
            this.SendEmail("admin@iudico", user.Email, "Iudico Notification", message);

            return user;
        }

        public void EditAccount(EditModel editModel)
        {
            var db = this.GetDbContext();
            var user = db.Users.Single(u => u.Username == this.GetIdentityName() && u.Deleted == false);

            user.Name = editModel.Name;
            user.OpenId = editModel.OpenId ?? string.Empty;
            user.Email = editModel.Email;
            user.UserId = editModel.UserId;

            db.SubmitChanges();

            this.SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your details have been changed.");
        }

        public void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var db = this.GetDbContext();

            var user = db.Users.Single(u => u.Username == this.GetIdentityName() && u.Deleted == false);

            user.Password = this.EncryptPassword(changePasswordModel.NewPassword);

            db.SubmitChanges();

            this.SendEmail("admin@iudico", user.Email, "Iudico Notification", "Your password has been changed.");
        }

        #endregion

        #region Roles members

        public IEnumerable<User> AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var db = this.GetDbContext();
            var users = this.GetUsers(u => usernames.Contains(u.Username));

            var userRoles = new List<UserRole>();

            foreach (var user in users)
            {
                userRoles.AddRange(roles.Select(role => new UserRole { UserRef = user.Id, RoleRef = (int)role }));
            }

            db.UserRoles.InsertAllOnSubmit(userRoles);
            db.SubmitChanges();

            return users;
        }

        public IEnumerable<User> RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var db = this.GetDbContext();
            var users = this.GetUsers(u => usernames.Contains(u.Username));
            var userIds = users.Select(u => u.Id);
            var intRoles = roles.Select(r => (int)r);

            var userRoles = db.UserRoles.Where(ur => userIds.Contains(ur.UserRef) && intRoles.Contains(ur.RoleRef));

            db.UserRoles.DeleteAllOnSubmit(userRoles);
            db.SubmitChanges();

            return users;
        }

        public IEnumerable<User> GetUsersInRole(Role role)
        {
            var db = this.GetDbContext();

            return db.UserRoles.Where(ur => ur.RoleRef == (int)role).Select(ur => ur.User);
        }

        public IEnumerable<Role> GetUserRoles(string username)
        {
            var db = this.GetDbContext();
            var userId = this.GetUser(username).Id;

            var roles = db.UserRoles.Where(ur => ur.UserRef == userId).Select(ur => (Role)ur.RoleRef).ToList();

            if (this.IsPromotedToAdmin() && !roles.Contains(Role.Admin))
            {
                roles.Add(Role.Admin);
            }

            return roles;
        }

        public void RemoveUserFromRole(Role role, User user)
        {
            var db = this.GetDbContext();

            var userRole = db.UserRoles.Single(g => g.RoleRef == (int)role && g.UserRef == user.Id);

            db.UserRoles.DeleteOnSubmit(userRole);
            db.SubmitChanges();
        }

        public void AddUserToRole(Role role, User user)
        {
            var db = this.GetDbContext();

            var userRole = new UserRole { RoleRef = (int)role, UserRef = user.Id };

            db.UserRoles.InsertOnSubmit(userRole);
            db.SubmitChanges();
        }

        public IEnumerable<Role> GetRolesAvailableToUser(User user)
        {
            var roles = this.GetUserRoles(user.Username);

            return UserRoles.GetRoles().Where(r => !roles.Contains(r));
        }

        #endregion

        #region Group members

        public Group GetGroup(int id)
        {
            var db = this.GetDbContext();

            return db.Groups.FirstOrDefault(group => group.Id == id && !group.Deleted);
        }

        public IEnumerable<Group> GetGroups()
        {
            var db = this.GetDbContext();

            return db.Groups.Where(g => !g.Deleted);
        }

        public void CreateGroup(Group group)
        {
            var db = this.GetDbContext();

            group.Deleted = false;

            db.Groups.InsertOnSubmit(group);
            db.SubmitChanges();

            this.lmsService.Inform(UserNotifications.GroupCreate, group);
        }

        public void EditGroup(int id, Group group)
        {
            var db = this.GetDbContext();
            var oldGroup = db.Groups.Single(g => g.Id == id && !g.Deleted);
            var newGroup = oldGroup;

            newGroup.Name = group.Name;
            db.SubmitChanges();

            var data = new object[] { oldGroup, newGroup };

            this.lmsService.Inform(UserNotifications.GroupEdit, data);
        }

        public void DeleteGroup(int id)
        {
            var db = this.GetDbContext();
            var group = db.Groups.Single(g => g.Id == id && !g.Deleted);

            var links = db.GroupUsers.Where(g => g.GroupRef == group.Id);

            db.GroupUsers.DeleteAllOnSubmit(links);

            group.Deleted = true;
            db.SubmitChanges();

            this.lmsService.Inform(UserNotifications.GroupDelete, group);
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            var db = this.GetDbContext();
            var groupIds = db.GroupUsers.Where(g => g.UserRef == user.Id).Select(g => g.GroupRef);

            return db.Groups.Where(g => groupIds.Contains(g.Id));
        }

        public IEnumerable<Group> GetGroupsAvailableToUser(User user)
        {
            var db = this.GetDbContext();

            var groupRefsByUser = this.GetGroupsByUser(user).Select(g => g.Id);

            return db.Groups.Where(g => !groupRefsByUser.Contains(g.Id));
        }

        public void AddUserToGroup(Group group, User user)
        {
            var db = this.GetDbContext();

            var groupUser = new GroupUser { GroupRef = group.Id, UserRef = user.Id };

            db.GroupUsers.InsertOnSubmit(groupUser);
            db.SubmitChanges();
        }

        public void RemoveUserFromGroup(Group group, User user)
        {
            var db = this.GetDbContext();

            var groupUser = db.GroupUsers.Single(g => g.GroupRef == group.Id && g.UserRef == user.Id);

            db.GroupUsers.DeleteOnSubmit(groupUser);
            db.SubmitChanges();
        }

        #endregion

        public bool IsPromotedToAdmin()
        {
            try
            {
                return (bool)HttpContext.Current.Session["AllowAdmin"];
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void RateTopic(int topicId, int rating)
        {
            var db = this.GetDbContext();

            var r = new UserTopicRating { Rating = rating, TopicId = topicId, UserId = this.GetCurrentUser().Id };
            db.UserTopicRatings.InsertOnSubmit(r);

            db.SubmitChanges();
        }

        public void UpdateUserAverage(AttemptResult attemptResult)
        {
            var db = this.GetDbContext();
            var user = db.Users.Single(u => u.Id == attemptResult.User.Id);
            var score = attemptResult.Score.ToPercents();
            
            if (score != null)
            {
                user.TestsTotal += 1;
                user.TestsSum += (int)score.Value;
            }

            db.SubmitChanges();
        }

        public int UploadAvatar(Guid id, HttpPostedFileBase file)
        {
            var resultCode = -1; // if no file had been passed

            if (file != null)
            {
                var fileName = Path.GetFileName(id + ".png");

                var fullPath = Path.Combine(Path.Combine(this.GetPath(), @"Data\Avatars"), fileName);
                var fileInfo = new FileInfo(fullPath);

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

        public int DeleteAvatar(Guid id)
        {
            var fileName = Path.GetFileName(id + ".png");
            var fullPath = Path.Combine(Path.Combine(this.GetPath(), @"Data\Avatars"), fileName);
            var fileInfo = new FileInfo(fullPath);

            if (fileInfo.Exists)
            {
                // if the file had been removed successfully
                fileInfo.Delete();
                return 1;
            }

            // if there was no file with such name in directory
            return -1;
        }

        #endregion
    }
}