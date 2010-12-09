using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using IUDICO.Common.Models;

namespace IUDICO.UserManagement.Models
{
    public class OpenIDMembershipUser : MembershipUser
    {
        public string OpenID { get; set; }

        public OpenIDMembershipUser(string providerName,
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
            OpenID = openId;
        }
    }
}