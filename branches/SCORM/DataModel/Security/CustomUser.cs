using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Security;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Security
{
    public class CustomUser : MembershipUser
    {
        public CustomUser(int id, string firstName, string lastName, string login, string passwordHash, string email, IList<string> roles)
            : this( login, login, email, null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue, DateTime.MinValue)
        {
            _ID = id;
            _FirstName = firstName;
            _LastName = lastName;
            _Login = login;
            PasswordHash = passwordHash;
            Roles = new ReadOnlyCollection<string>(roles);
        }

        protected CustomUser(string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
            : base(Membership.Provider.Name, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
        }

        public readonly ReadOnlyCollection<string> Roles;

        public readonly string PasswordHash;

        public override string UserName
        {
            get
            {
                if (_FirstName.IsNotNull() || _LastName.IsNotNull())
                {
                    return _FirstName + " " + _LastName;
                }
                else
                {
                    return Login;
                }
            }
        }

        public int ID { get { return _ID; } }

        public string Login { get { return _Login; } }

        public override string GetPassword()
        {
            throw new InvalidOperationException(Translations.CustomUser_GetPassword_GetPassword_is_not_available);
        }

        public override string GetPassword(string passwordAnswer)
        {
            throw new InvalidOperationException(Translations.CustomUser_GetPassword_GetPassword_is_not_available);
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

        private readonly string _FirstName;
        private readonly string _LastName;
        private readonly string _Login;
        private readonly int _ID;
    }
}
