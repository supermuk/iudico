using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Models.Auth
{
    public class OpenIdRoleProvider : RoleProvider
    {
        protected ILmsService _LmsService;

        public OpenIdRoleProvider(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        protected DBDataContext GetDbContext()
        {
            return _LmsService.GetDbDataContext();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var db = GetDbContext();
            var user = db.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var userRole = (Role) user.RoleId;
            var minRole = GetRole(roleName);

            if (minRole == Role.None)
            {
                return false;
            }

            return userRole >= minRole;
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = GetDbContext();

            var role = db.Users.Where(u => u.Username == username).Select(u => u.RoleId).First();

            return new [] { ((Role)role).ToString() };
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
                throw new ArgumentException();
            }

            var db = GetDbContext();
            var users = db.Users.Where(user => usernames.Contains(user.Username));

            foreach (var user in users)
            {
                user.RoleId = (int)GetRole(roleNames[0]);
            }

            db.SubmitChanges();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var db = GetDbContext();
            var role = (int)GetRole(roleName);

            return db.Users.Where(user => user.RoleId == role).Select(user => user.Username).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return Enum.GetNames(typeof (Role)).Skip(1).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var db = GetDbContext();
            var role = (int) GetRole(roleName);

            return db.Users.Where(u => u.RoleId == role && u.Username.Contains(usernameToMatch)).Select(u => u.Username).ToArray();
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