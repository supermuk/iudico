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
            var user = _UserStorage.GetUser(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            var minRole = GetRole(roleName);

            return user.Role >= minRole;
        }

        public override string[] GetRolesForUser(string username)
        {
            return new[] { _UserStorage.GetUser(u => u.Username == username).Role.ToString() };
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
            if (roleNames.Length != 1)
            {
                throw new NotSupportedException();
            }

            var users = _UserStorage.GetUsers(u => usernames.Contains(u.Username));

            foreach (var user in users)
            {
                user.RoleId = (int)GetRole(roleNames[0]);

                _UserStorage.EditUser(user.Id, user);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var role = GetRole(roleName);

            return _UserStorage.GetUsers(u => u.Role == role).Select(u => u.Username).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return Enum.GetNames(typeof (Role)).Skip(1).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var role = GetRole(roleName);

            return _UserStorage.GetUsers(u => u.Role == role && u.Username.Contains(usernameToMatch)).Select(u => u.Username).ToArray();
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