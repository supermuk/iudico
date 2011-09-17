using System;
using System.Collections.Specialized;
using System.Web.Security;

namespace IUDICO.LMS.Models.Providers
{
    public class IoCRoleProvider : RoleProvider
    {
        private RoleProvider _Provider;

        public void Initialize(RoleProvider provider)
        {
            _Provider = provider;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            ApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        #region Overrides of RoleProvider

        public override bool IsUserInRole(string username, string roleName)
        {
            return _Provider.IsUserInRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return _Provider.GetRolesForUser(username);
        }

        public override void CreateRole(string roleName)
        {
            _Provider.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return _Provider.DeleteRole(roleName, throwOnPopulatedRole);
        }

        public override bool RoleExists(string roleName)
        {
            return _Provider.RoleExists(roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            _Provider.AddUsersToRoles(usernames, roleNames);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            _Provider.RemoveUsersFromRoles(usernames, roleNames);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return _Provider.GetUsersInRole(roleName);
        }

        public override string[] GetAllRoles()
        {
            return _Provider.GetAllRoles();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return _Provider.FindUsersInRole(roleName, usernameToMatch);
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}