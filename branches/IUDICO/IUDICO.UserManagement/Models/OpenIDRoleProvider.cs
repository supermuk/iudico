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
            RoleUser role = GetDbContext().RoleUsers.SingleOrDefault(r => r.Role.Name == roleName && r.User.Username == username);

            return role != null;
        }

        public override string[] GetRolesForUser(string username)
        {
            return GetDbContext().RoleUsers.Where(r => r.User.Username == username).Select(r => r.Role.Name).ToArray();
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
            var db = GetDbContext();

            foreach (var username in usernames)
            {
                foreach (var role in roleNames.Where(role => !IsUserInRole(username, role)))
                {
                    try
                    {
                        var link = new RoleUser
                                       {
                                           Role = db.Roles.SingleOrDefault(r => r.Name == role),
                                           User = db.Users.SingleOrDefault(u => u.Username == username)
                                       };

                        db.RoleUsers.InsertOnSubmit(link);
                        db.SubmitChanges();
                    }
                    catch
                    {
                    }
                }
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var db = GetDbContext();
            var links = db.RoleUsers.Where(l => usernames.Contains(l.User.Username) && roleNames.Contains(l.Role.Name));
            
            foreach (var link in links)
            {
                db.RoleUsers.DeleteOnSubmit(link);
            }

            db.SubmitChanges();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return GetDbContext().RoleUsers.Where(r => r.Role.Name == roleName).Select(r => r.User.Name).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return GetDbContext().Roles.Select(r => r.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return GetDbContext().RoleUsers.Where(r => r.Role.Name == roleName).Where(r => r.User.Username.Contains(usernameToMatch)).Select(r => r.User.Name).ToArray();
        }

        public override string ApplicationName { get; set; }
    }
}