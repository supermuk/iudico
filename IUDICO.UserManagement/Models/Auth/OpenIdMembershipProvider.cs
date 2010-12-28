using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Models.Auth
{
    public class OpenIdMembershipProvider : MembershipProvider
    {
        protected ILmsService _LmsService;

        public OpenIdMembershipProvider(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        protected DBDataContext GetDbContext()
        {
            return _LmsService.GetDbDataContext();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            ApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }


        public MembershipCreateStatus CreateUser(string username, string password, string email, string openId, bool isApproved)
        {
            try
            {
                var db = GetDbContext();

                var currentUser = db.Users.SingleOrDefault(u => u.Username == username || u.Email == email);

                if (currentUser != null)
                {
                    if (currentUser.Username == username)
                    {
                        return MembershipCreateStatus.DuplicateUserName;
                    }
                    else if (currentUser.Email == email)
                    {
                        return MembershipCreateStatus.DuplicateEmail;
                    }
                    else
                    {
                        return MembershipCreateStatus.ProviderError;
                    }
                }

                var user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    IsApproved = isApproved
                };

                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();

                return MembershipCreateStatus.Success;
            }
            catch (Exception)
            {
                return MembershipCreateStatus.ProviderError;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = CreateUser(username, password, email, null, isApproved);

            return status == MembershipCreateStatus.Success ? GetUser(username, false) : null;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            var user = GetDbContext().Users.SingleOrDefault(u => u.Username == username);

            return user != null ? user.Password : null;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                var db = GetDbContext();

                var user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == oldPassword);

                if (user != null)
                {
                    user.Password = newPassword;

                    db.SubmitChanges();

                    return true;
                }
            }
            catch (Exception)
            {

            }

            return false;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            try
            {
                var db = GetDbContext();

                var oldUser = db.Users.SingleOrDefault(u => u.Username == user.UserName);

                if (oldUser != null)
                {
                    oldUser.IsApproved = user.IsApproved;
                    oldUser.Email = user.Email;

                    db.SubmitChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = GetDbContext().Users.SingleOrDefault(u => u.Username == username && u.Password == password);

            return user != null && !user.Deleted;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return GetMembershipUser(GetDbContext().Users.SingleOrDefault(u => u.Id == (Guid)providerUserKey));
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return GetMembershipUser(GetDbContext().Users.SingleOrDefault(u => u.Username == username));
        }

        public MembershipUser GetMembershipUser(User user)
        {
            return new OpenIdMembershipUser(Name, user.Name, user.Id, user.Email, user.OpenId, "", "", user.IsApproved,
                                            false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override string GetUserNameByEmail(string email)
        {
            return GetDbContext().Users.SingleOrDefault(u => u.Email == email).Username;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                var db = GetDbContext();

                var user = db.Users.SingleOrDefault(u => u.Username == username);

                user.Deleted = true;
                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var db = GetDbContext();
            var collection = new MembershipUserCollection();

            var users = db.Users.Skip(pageIndex).Take(pageSize).ToList();
            totalRecords = users.Count;

            foreach (var user in users)
            {
                collection.Add(GetMembershipUser(user));
            }

            return collection;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();

            var users = GetDbContext().Users.Skip(pageIndex).Take(pageSize).Where(u => u.Username.Contains(usernameToMatch)).ToList();
            totalRecords = users.Count;

            foreach (var user in users)
            {
                collection.Add(GetMembershipUser(user));
            }

            return collection;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();
            var db = GetDbContext();

            var users = db.Users.Skip(pageIndex).Take(pageSize).Where(u => u.Email.Contains(emailToMatch)).ToList();
            totalRecords = users.Count;

            foreach (var user in users)
            {
                collection.Add(GetMembershipUser(user));
            }

            return collection;
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 0; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 0; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 0; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return ""; }
        }
    }
}