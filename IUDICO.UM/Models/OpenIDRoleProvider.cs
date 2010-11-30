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
            foreach (var username in usernames)
            {
                foreach (var role in roleNames)
                {
                    if (!IsUserInRole(username, role))
                    {
                        try
                        {
                            RoleUser link = new RoleUser();
                            link.Role = db.Roles.SingleOrDefault(r => r.Name == role);
                            link.User = db.Users.SingleOrDefault(u => u.Username == username);
                            db.RoleUsers.InsertOnSubmit(link);
                            db.SubmitChanges();
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var links = db.RoleUsers.Where(l => usernames.Contains(l.User.Username) && roleNames.Contains(l.Role.Name));
            foreach (var link in links)
            {
                db.RoleUsers.DeleteOnSubmit(link);
            }
            db.SubmitChanges();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return db.RoleUsers.Where(r => r.Role.Name == roleName).Select(r => r.User.Name).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return db.Roles.Select(r => r.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return db.RoleUsers.Where(r => r.Role.Name == roleName).Where(r => r.User.Username.Contains(usernameToMatch)).Select(r => r.User.Name).ToArray();
        }

        public override string ApplicationName { get; set; }
    }
}