using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using IUDICO.Common.Models;

namespace IUDICO.UM.Models
{
    public class OpenIDRoleProvider : RoleProvider
    {
        protected DB db = DB.Instance;

        public override void Initialize(string name, NameValueCollection config)
        {
            ApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        protected string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            RoleUser role = db.RoleUsers.SingleOrDefault(r => r.Role.Name == roleName && r.User.Username == username);

            return role != null;
        }

        public override string[] GetRolesForUser(string username)
        {
            return db.RoleUsers.Where(r => r.User.Username == username).Select(r => r.Role.Name).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                Role role = new Role { Name = roleName };

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
                Role role = db.Roles.SingleOrDefault(r => r.Name == roleName);

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
            Role role = db.Roles.SingleOrDefault(r => r.Name == roleName);

            if (role != null)
            {
                return true;
            }

            return false;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            /*foreach (var username in usernames)
            {
                try
                {
                    User user = db.Users.SingleOrDefault(u => u.Username == username);
                    Role role = db.Roles.SingleOrDefault(r => r.Name == roleNames[0]);

                    user.Role = role;
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                }
            }*/
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            /*Role role = db.Roles.SingleOrDefault(r => r.Name == roleName);

            return (from user in db.Users
                    where user.RoleID == role.ID
                    select user.Username).ToArray();*/
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return db.Roles.Select(r => r.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            /*Role role = db.Roles.SingleOrDefault(r => r.Name == roleName);

            return (from user in db.Users
                    where user.RoleID == role.ID
                    where user.Username.Contains(usernameToMatch)
                    select user.Username).ToArray();*/
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}