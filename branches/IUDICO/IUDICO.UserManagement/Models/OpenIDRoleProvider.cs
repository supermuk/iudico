using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Models
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

        public override void Initialize(string name, NameValueCollection config)
        {
            ApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var db = GetDbContext();
            return db.Users.Any(user => user.Username == username && user.Role.Name == roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = GetDbContext();
            return db.Users.Where(user => user.Username == username).Select(user => user.Role.Name).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                var db = GetDbContext();
                var role = new Role { Name = roleName };

                db.Roles.InsertOnSubmit(role);
                db.SubmitChanges();
            }
            catch (Exception)
            {
                
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            try
            {
                var db = GetDbContext();
                var role = db.Roles.SingleOrDefault(r => r.Name == roleName);

                db.Roles.InsertOnSubmit(role);
                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {

            }

            return false;
        }

        public override bool RoleExists(string roleName)
        {
            var db = GetDbContext();
            Role role = db.Roles.SingleOrDefault(r => r.Name == roleName);

            return role != null;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (roleNames.Length != 1)
                throw new ArgumentException();

            var db = GetDbContext();
            Role role = db.Roles.SingleOrDefault(r => r.Name == roleNames[0]);
            IEnumerable<User> users = db.Users.Where(user => usernames.Contains(user.Username));
            foreach (User user in users)
                user.Role = role;
            db.SubmitChanges();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var db = GetDbContext();
            return db.Users.Where(user => user.Role.Name == roleName).Select(user => user.Name).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return GetDbContext().Roles.Select(r => r.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var db = GetDbContext();
            return db.Users.Where(user => user.Role.Name == roleName && user.Username.Contains(usernameToMatch)).Select(user => user.Username).ToArray();
        }

        public override string ApplicationName { get; set; }
    }
}