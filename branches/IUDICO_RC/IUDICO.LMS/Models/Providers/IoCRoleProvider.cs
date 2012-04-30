using System;
using System.Collections.Specialized;
using System.Web.Security;

namespace IUDICO.LMS.Models.Providers
{
    public class IoCRoleProvider : RoleProvider
    {
        private RoleProvider provider;

        public void Initialize(RoleProvider provider)
        {
            this.provider = provider;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            this.ApplicationName = this.GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            return string.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        #region Overrides of RoleProvider

        public override bool IsUserInRole(string username, string roleName)
        {
            return this.provider.IsUserInRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return this.provider.GetRolesForUser(username);
        }

        public override void CreateRole(string roleName)
        {
            this.provider.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return this.provider.DeleteRole(roleName, throwOnPopulatedRole);
        }

        public override bool RoleExists(string roleName)
        {
            return this.provider.RoleExists(roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            this.provider.AddUsersToRoles(usernames, roleNames);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            this.provider.RemoveUsersFromRoles(usernames, roleNames);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return this.provider.GetUsersInRole(roleName);
        }

        public override string[] GetAllRoles()
        {
            return this.provider.GetAllRoles();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return this.provider.FindUsersInRole(roleName, usernameToMatch);
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}