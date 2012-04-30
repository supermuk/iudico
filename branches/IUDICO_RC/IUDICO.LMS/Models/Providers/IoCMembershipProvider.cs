using System;
using System.Collections.Specialized;
using System.Web.Security;

namespace IUDICO.LMS.Models.Providers
{
    public class IoCMembershipProvider : MembershipProvider
    {
        private MembershipProvider provider;

        public void Initialize(MembershipProvider provider)
        {
            this.provider = provider;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            this.ApplicationName = this.GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            return string.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        #region Overrides of MembershipProvider

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return this.provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return this.provider.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
        }

        public override string GetPassword(string username, string answer)
        {
            return this.provider.GetPassword(username, answer);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return this.provider.ChangePassword(username, oldPassword, newPassword);
        }

        public override string ResetPassword(string username, string answer)
        {
            return this.provider.ResetPassword(username, answer);
        }

        public override void UpdateUser(MembershipUser user)
        {
            this.provider.UpdateUser(user);
        }

        public override bool ValidateUser(string username, string password)
        {
            return this.provider.ValidateUser(username, password);
        }

        public override bool UnlockUser(string userName)
        {
            return this.provider.UnlockUser(userName);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return this.provider.GetUser(providerUserKey, userIsOnline);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return this.provider.GetUser(username, userIsOnline);
        }

        public override string GetUserNameByEmail(string email)
        {
            return this.provider.GetUserNameByEmail(email);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return this.provider.DeleteUser(username, deleteAllRelatedData);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return this.provider.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public override int GetNumberOfUsersOnline()
        {
            return this.provider.GetNumberOfUsersOnline();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return this.provider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return this.provider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override bool EnablePasswordRetrieval
        {
            get { return this.provider.EnablePasswordRetrieval; }
        }

        public override bool EnablePasswordReset
        {
            get { return this.provider.EnablePasswordRetrieval; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return this.provider.EnablePasswordRetrieval; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return this.provider.MaxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow
        {
            get { return this.provider.PasswordAttemptWindow; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return this.provider.EnablePasswordRetrieval; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return this.provider.PasswordFormat; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return this.provider.MinRequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return this.provider.MinRequiredNonAlphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return this.provider.PasswordStrengthRegularExpression; }
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}