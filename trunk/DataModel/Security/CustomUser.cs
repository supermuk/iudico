using System;
using System.Web.Security;

namespace IUDICO.DataModel.Security
{
    public class CustomUser : MembershipUser
    {
        public CustomUser(string firstName, string lastName, string login, string passwordHash, string email)
            : this(null, login, email, null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue, DateTime.MinValue)
        {
            _FirstName = firstName;
            _LastName = lastName;
            _PasswordHash = passwordHash;
        }

        protected CustomUser(string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
            : base(typeof(CustomMembershipProvider).FullName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
        }

        public override string UserName
        {
            get
            {
                return _FirstName + " " + _LastName;
            }
        }

        public override string GetPassword()
        {
            throw new InvalidOperationException("GetPassword is not available");
        }

        public override string GetPassword(string passwordAnswer)
        {
            throw new InvalidOperationException("GetPassword is not available");
        }

        public override bool IsApproved
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        private string _PasswordHash;
        private string _FirstName;
        private string _LastName;
    }
}
