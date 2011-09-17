using System;
using System.Collections.Specialized;
using System.Web.Security;

namespace IUDICO.LMS.Models.Providers
{
    public class IoCMembershipProvider : MembershipProvider
    {
        private MembershipProvider _Provider;

        public void Initialize(MembershipProvider provider)
        {
            _Provider = provider;
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

        #region Overrides of MembershipProvider

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return _Provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return _Provider.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
        }

        public override string GetPassword(string username, string answer)
        {
            return _Provider.GetPassword(username, answer);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _Provider.ChangePassword(username, oldPassword, newPassword);
        }

        public override string ResetPassword(string username, string answer)
        {
            return _Provider.ResetPassword(username, answer);
        }

        public override void UpdateUser(MembershipUser user)
        {
            _Provider.UpdateUser(user);
        }

        public override bool ValidateUser(string username, string password)
        {
            return _Provider.ValidateUser(username, password);
        }

        public override bool UnlockUser(string userName)
        {
            return _Provider.UnlockUser(userName);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return _Provider.GetUser(providerUserKey, userIsOnline);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return _Provider.GetUser(username, userIsOnline);
        }

        public override string GetUserNameByEmail(string email)
        {
            return _Provider.GetUserNameByEmail(email);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return _Provider.DeleteUser(username, deleteAllRelatedData);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return _Provider.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public override int GetNumberOfUsersOnline()
        {
            return _Provider.GetNumberOfUsersOnline();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return _Provider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return _Provider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _Provider.EnablePasswordRetrieval; }
        }

        public override bool EnablePasswordReset
        {
            get { return _Provider.EnablePasswordRetrieval; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _Provider.EnablePasswordRetrieval; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _Provider.MaxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _Provider.PasswordAttemptWindow; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _Provider.EnablePasswordRetrieval; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _Provider.PasswordFormat; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _Provider.MinRequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _Provider.MinRequiredNonAlphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _Provider.PasswordStrengthRegularExpression; }
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}