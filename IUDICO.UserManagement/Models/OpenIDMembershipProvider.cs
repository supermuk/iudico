using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using IUDICO.Common.Models;
using System.Collections.Specialized;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Models
{
    public class OpenIDMembershipProvider : MembershipProvider
    {
        protected DBDataContext db;

        public OpenIDMembershipProvider(ILmsService lmsService)
        {
            db = lmsService.GetDBDataContext();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            ApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }


        public MembershipCreateStatus CreateUser(string username, string password, string email, string openId, bool isApproved)
        {
            try
            {
                User currentUser = db.Users.SingleOrDefault(u => u.Username == username || u.Email == email);

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

                User user = new User
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
            catch (Exception ex)
            {
                return MembershipCreateStatus.ProviderError;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = CreateUser(username, password, email, null, isApproved);

            if (status == MembershipCreateStatus.Success)
            {
                return GetUser(username, false);
            }

            return null;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            User user = db.Users.SingleOrDefault(u => u.Username == username);

            if (user != null)
            {
                return user.Password;
            }

            return null;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                User user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == oldPassword);

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
                User oldUser = db.Users.SingleOrDefault(u => u.Username == user.UserName);

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
            User user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return GetMembershipUser(db.Users.SingleOrDefault(u => u.ID == (Guid)providerUserKey));
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return GetMembershipUser(db.Users.SingleOrDefault(u => u.Username == username));
        }

        public MembershipUser GetMembershipUser(User user)
        {
            return new OpenIDMembershipUser(Name, user.Name, user.ID, user.Email, user.OpenID, "", "", user.IsApproved,
                                            false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override string GetUserNameByEmail(string email)
        {
            return db.Users.SingleOrDefault(u => u.Email == email).Username;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                User user = db.Users.SingleOrDefault(u => u.Username == username);
                
                db.Users.DeleteOnSubmit(user);
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
            MembershipUserCollection collection = new MembershipUserCollection();

            List<User> users = db.Users.Skip(pageIndex).Take(pageSize).ToList();
            totalRecords = users.Count;

            foreach(User user in users)
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
            MembershipUserCollection collection = new MembershipUserCollection();

            List<User> users = db.Users.Skip(pageIndex).Take(pageSize).Where(u => u.Username.Contains(usernameToMatch)).ToList();
            totalRecords = users.Count;

            foreach (User user in users)
            {
                collection.Add(GetMembershipUser(user));
            }

            return collection;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection collection = new MembershipUserCollection();

            List<User> users = db.Users.Skip(pageIndex).Take(pageSize).Where(u => u.Email.Contains(emailToMatch)).ToList();
            totalRecords = users.Count;

            foreach (User user in users)
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