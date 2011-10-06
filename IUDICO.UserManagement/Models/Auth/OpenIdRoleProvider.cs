using System;
using System.Linq;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Models.Auth
{
    public class OpenIdRoleProvider : RoleProvider
    {
        protected IUserStorage _UserStorage;

        public OpenIdRoleProvider(IUserStorage userStorage)
        {
            _UserStorage = userStorage;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = GetRolesForUser(username);

            return roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new[] { "None" };
            }

            return _UserStorage.GetUser(u => u.Username == username).UserRoles.Select(ur => ((Role)ur.RoleRef).ToString()).ToArray();
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
            return GetRole(roleName) != Role.None;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var roles = roleNames.Select(GetRole).Where(r => r != Role.None);

            _UserStorage.AddUsersToRoles(usernames, roles);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var roles = roleNames.Select(GetRole).Where(r => r != Role.None);

            _UserStorage.RemoveUsersFromRoles(usernames, roles);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var role = GetRole(roleName);

            return _UserStorage.GetUsersInRole(role).Select(u => u.Username).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return _UserStorage.GetRoles().Select(r => r.ToString()).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            /*var role = GetRole(roleName);

            return _UserStorage.GetUsers(u => u.Role == role && u.Username.Contains(usernameToMatch)).Select(u => u.Username).ToArray();*/
            throw new NotImplementedException();
        }

        protected Role GetRole(string roleName)
        {
            try
            {
                var role = (Role)Enum.Parse(typeof(Role), roleName);

                return role;
            }
            catch (Exception)
            {
                return Role.None;
            }
        }

        public override string ApplicationName { get; set; }
    }
}