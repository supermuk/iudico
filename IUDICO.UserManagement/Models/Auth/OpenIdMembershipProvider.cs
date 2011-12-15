using System;
using System.Linq;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Models.Auth
{
    public class OpenIdMembershipProvider : MembershipProvider
    {
        protected ILmsService _LmsService;
        protected IUserStorage _UserStorage;

        public OpenIdMembershipProvider(IUserStorage userStorage)
        {
            _UserStorage = userStorage;
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        public MembershipCreateStatus CreateUser(string username, string password, string email, string openId, bool isApproved)
        {
            try
            {
                var user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    IsApproved = isApproved
                };

                _UserStorage.CreateUser(user);

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
           var user = _UserStorage.GetUser(u => u.Username == username);

            return user != null ? user.Password : null;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                if (username == _UserStorage.GetCurrentUser().Username)
                {
                    _UserStorage.ChangePassword(new ChangePasswordModel { NewPassword = newPassword, OldPassword = oldPassword });

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
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = _UserStorage.GetUser(u => u.Username == username && u.Password == _UserStorage.EncryptPassword(password));

            return user != null && !user.Deleted;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return GetMembershipUser(_UserStorage.GetUser(u => u.Id == (Guid)providerUserKey));
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return GetMembershipUser(_UserStorage.GetUser(u => u.Username == username));
        }

        public MembershipUser GetMembershipUser(User user)
        {
            return new OpenIdMembershipUser(Name, user.Name, user.Id, user.Email, user.OpenId, "", "", user.IsApproved,
                                            false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override string GetUserNameByEmail(string email)
        {
            return _UserStorage.GetUser(u => u.Email == email).Username;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                _UserStorage.DeleteUser(u => u.Username == username);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();
            var users = _UserStorage.GetUsers(pageIndex, pageSize);

            totalRecords = users.Count();

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

            var users = _UserStorage.GetUsers(pageIndex, pageSize).Where(u => u.Username.Contains(usernameToMatch));
            totalRecords = users.Count();

            foreach (var user in users)
            {
                collection.Add(GetMembershipUser(user));
            }

            return collection;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();

            var users = _UserStorage.GetUsers(pageIndex, pageSize).Where(u => u.Email.Contains(emailToMatch));
            totalRecords = users.Count();

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