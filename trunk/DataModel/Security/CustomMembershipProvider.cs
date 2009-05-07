using System;
using System.Security;
using System.Web.Security;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
        #region Overrides of MembershipProvider

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ServerModel.User.Create(username, password, email);
            var res = ServerModel.User.ByLogin(username);
            status = res != null ? MembershipCreateStatus.Success : MembershipCreateStatus.UserRejected;
            return res;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new InvalidOperationException("GetPassword is not allowed");
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (oldPassword != newPassword)
            {
                var user = ServerModel.User.Current;
                if (user.UserName != username)
                    throw new SecurityException("You cann't change password of another user");
                var u = ServerModel.DB.Load<TblUsers>(user.ID);
                var oldHash = ServerModel.User.GetPasswordHash(oldPassword);

                if (u.PasswordHash != oldHash)
                    return false;

                var newHash = ServerModel.User.GetPasswordHash(newPassword);
                u.PasswordHash = newHash;
                ServerModel.DB.Update(u);
                return true;
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
            return ServerModel.User.ByLogin(username).PasswordHash == ServerModel.User.GetPasswordHash(password);
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return ServerModel.User.ByLogin((string)providerUserKey);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return ServerModel.User.ByLogin(username);
        }

        public override string GetUserNameByEmail(string email)
        {
            return ServerModel.User.ByEmail(email).UserName;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
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

        public override string ApplicationName
        {
            get { return "IUDICO"; }
            set { throw new InvalidOperationException("Changing ApplicationName is not allowed"); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return int.MaxValue; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 3; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return ".*"; }
        }

        #endregion
    }
}
