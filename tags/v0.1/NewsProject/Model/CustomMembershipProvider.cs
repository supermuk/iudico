using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Security;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.NewsProject.Model.DB;

namespace LEX.NewsProject.Model
{
//    public class CustomMembershipProvider : MembershipProvider
//    {
//        #region Overrides of MembershipProvider
//
//        internal static bool TryGetUser(string login, out TblUser user)
//        {
//            var users = ServerModel.DB.Query<TblUser>(
//                new CompareCondition(
//                    new PropertyCondition("Email"),
//                    new ValueCondition(login),
//                    COMPARE_KIND.EQUAL));
//            if (users.Count == 1)
//            {
//                user = users[0];
//                return true;
//            }
//            else
//            {
//                user = null;
//                return false;
//            }
//        }
//
//        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
//        {
//            // TODO: Implement user inserting
//            throw new NotImplementedException();
////            status = res != null ? MembershipCreateStatus.Success : MembershipCreateStatus.UserRejected;
////            return res;
//        }
//
//        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override string GetPassword(string username, string answer)
//        {
//            throw new InvalidOperationException("GetPassword is not allowed");
//        }
//
//        public override bool ChangePassword(string username, string oldPassword, string newPassword)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override string ResetPassword(string username, string answer)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override void UpdateUser(MembershipUser user)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override bool ValidateUser(string username, string password)
//        {
//            TblUser u;
//            return TryGetUser(username, out u) &&
//                   FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5") == u.PasswordHash;
//        }
//
//        public override bool UnlockUser(string userName)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
//        {
//            return ServerModel.User.ByLogin((string)providerUserKey);
//        }
//
//        public override MembershipUser GetUser(string username, bool userIsOnline)
//        {
//            return ServerModel.User.ByLogin(username);
//        }
//
//        public override string GetUserNameByEmail(string email)
//        {
//            return ServerModel.User.ByEmail(email).UserName;
//        }
//
//        public override bool DeleteUser(string username, bool deleteAllRelatedData)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override int GetNumberOfUsersOnline()
//        {
//            throw new NotImplementedException();
//        }
//
//        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
//        {
//            throw new NotImplementedException();
//        }
//
//        public override bool EnablePasswordRetrieval
//        {
//            get { return false; }
//        }
//
//        public override bool EnablePasswordReset
//        {
//            get { return false; }
//        }
//
//        public override bool RequiresQuestionAndAnswer
//        {
//            get { return false; }
//        }
//
//        public override string ApplicationName
//        {
//            get { return "IUDICO"; }
//            set { throw new InvalidOperationException("Changing ApplicationName is not allowed"); }
//        }
//
//        public override int MaxInvalidPasswordAttempts
//        {
//            get { return int.MaxValue; }
//        }
//
//        public override int PasswordAttemptWindow
//        {
//            get { throw new NotImplementedException(); }
//        }
//
//        public override bool RequiresUniqueEmail
//        {
//            get { return true; }
//        }
//
//        public override MembershipPasswordFormat PasswordFormat
//        {
//            get { return MembershipPasswordFormat.Hashed; }
//        }
//
//        public override int MinRequiredPasswordLength
//        {
//            get { return 3; }
//        }
//
//        public override int MinRequiredNonAlphanumericCharacters
//        {
//            get { return 0; }
//        }
//
//        public override string PasswordStrengthRegularExpression
//        {
//            get { return ".*"; }
//        }
//
//        #endregion
//    }
//
//
//    public class CustomUser : MembershipUser
//    {
//        public CustomUser(int id, string firstName, string lastName, string login, string passwordHash, string email, IList<string> roles)
//            : this(login, login, email, null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue, DateTime.MinValue)
//        {
//            _ID = id;
//            _FirstName = firstName;
//            _LastName = lastName;
//            _Login = login;
//            PasswordHash = passwordHash;
//            Roles = new ReadOnlyCollection<string>(roles);
//            PasswordHash = passwordHash;
//        }
//
//        protected CustomUser(string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
//            : base(Membership.Provider.Name, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
//        {
//        }
//
//        public readonly ReadOnlyCollection<string> Roles;
//
//        public readonly string PasswordHash;
//
//        public override string UserName
//        {
//            get
//            {
//                return _FirstName + " " + _LastName;
//            }
//        }
//
//        public int ID { get { return _ID; } }
//
//        public string Login { get { return _Login; } }
//
//        public override string GetPassword()
//        {
//            throw new InvalidOperationException("GetPassword is not available");
//        }
//
//        public override string GetPassword(string passwordAnswer)
//        {
//            throw new InvalidOperationException("GetPassword is not available");
//        }
//
//        public override bool IsApproved
//        {
//            get
//            {
//                return true;
//            }
//            set
//            {
//            }
//        }
//
//        private readonly string _FirstName;
//        private readonly string _LastName;
//        private readonly string _Login;
//        private readonly int _ID;
//    }
}
