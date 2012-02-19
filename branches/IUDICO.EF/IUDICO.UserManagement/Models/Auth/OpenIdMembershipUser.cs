using System;
using System.Web.Security;

namespace IUDICO.UserManagement.Models.Auth
{
    public class OpenIdMembershipUser : MembershipUser
    {
        public string OpenId { get; set; }

        public OpenIdMembershipUser(string providerName,
                                    string name,
                                    object providerUserKey,
                                    string email,
                                    string openId,
                                    string passwordQuestion,
                                    string comment,
                                    bool isApproved,
                                    bool isLockedOut,
                                    DateTime creationDate,
                                    DateTime lastLoginDate,
                                    DateTime lastActivityDate,
                                    DateTime lastPasswordChangedDate,
                                    DateTime lastLockoutDate)
            : base(providerName,
                   name,
                   providerUserKey,
                   email,
                   passwordQuestion,
                   comment,
                   isApproved,
                   isLockedOut,
                   creationDate,
                   lastLoginDate,
                   lastActivityDate,
                   lastPasswordChangedDate,
                   lastLockoutDate
                )
        {
            OpenId = openId;
        }
    }
}