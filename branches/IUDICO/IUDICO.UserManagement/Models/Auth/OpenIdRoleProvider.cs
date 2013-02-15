using System;
using System.Linq;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Models.Auth
{
    public class OpenIdRoleProvider : RoleProvider
    {
        protected IUserStorage userStorage;

        public OpenIdRoleProvider(IUserStorage userStorage)
        {
            this.userStorage = userStorage;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = this.GetRolesForUser(username);

            return roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return string.IsNullOrEmpty(username) ? new[] { "None" } : this.userStorage.GetUserRoles(username).Select(ur => ur.ToString()).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException();
        }

        public override bool RoleExists(string roleName)
        {
            return UserRoles.GetRole(roleName) != Role.None;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var roles = roleNames.Select(UserRoles.GetRole).Where(r => r != Role.None);

            this.userStorage.AddUsersToRoles(usernames, roles);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var roles = roleNames.Select(UserRoles.GetRole).Where(r => r != Role.None);

            this.userStorage.RemoveUsersFromRoles(usernames, roles);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var role = UserRoles.GetRole(roleName);

            return this.userStorage.GetUsersInRole(role).Select(u => u.Username).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return UserRoles.GetRoles().Select(r => r.ToString()).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}