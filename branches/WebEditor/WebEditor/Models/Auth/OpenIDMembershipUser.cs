using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebEditor.Models.Auth
{
    public class OpenIDMembershipUser : MembershipUser
    {
        public string OpenID { get; set; }

        public OpenIDMembershipUser(MembershipUser user, string openID)
            : base(
            user.ProviderName,
            user.UserName,
            user.ProviderUserKey,
            user.Email,
            user.PasswordQuestion,
            user.Comment,
            user.IsApproved,
            user.IsLockedOut,
            user.CreationDate,
            user.LastLoginDate,
            user.LastActivityDate,
            user.LastPasswordChangedDate,
            user.LastLockoutDate)
        {
            OpenID = openID;
        }

        public void Update(EditModel editModel)
        {
            OpenID = editModel.OpenID;
            Email = editModel.Email;
        }
    }
}