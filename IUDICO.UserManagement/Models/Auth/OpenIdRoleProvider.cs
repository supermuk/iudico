using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Models.Auth
{
    public class OpenIdRoleProvider : RoleProvider
    {
        protected Dictionary<string, List<string>> _Roles;
        protected ILmsService _LmsService;

        public OpenIdRoleProvider(ILmsService lmsService)
        {
            _LmsService = lmsService;
            
            GetRoles();
        }

        protected DBDataContext GetDbContext()
        {
            return _LmsService.GetDbDataContext();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var db = GetDbContext();

            return db.Users.Any(user => user.Username == username && _Roles[user.Role.Name].Contains(roleName));
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = GetDbContext();

            var role = db.Users.Where(user => user.Username == username).Select(user => user.Role.Name).First();

            return _Roles[role].ToArray();
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                var db = GetDbContext();
                var role = new Role { Name = roleName };

                db.Roles.InsertOnSubmit(role);
                db.SubmitChanges();

                GetRoles();
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
                
                GetRoles();

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
            var role = db.Roles.SingleOrDefault(r => r.Name == roleName);

            return role != null;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (roleNames.Length != 1)
            {
                throw new ArgumentException();
            }

            var db = GetDbContext();
            var role = db.Roles.SingleOrDefault(r => r.Name == roleNames[0]);
            var users = db.Users.Where(user => usernames.Contains(user.Username));

            foreach (var user in users)
            {
                user.Role = role;
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

        protected void GetRoles()
        {
            _Roles = new Dictionary<string, List<string>>();

            var db = GetDbContext();

            var roles = db.Roles.OrderBy(r => r.ParentId);

            foreach (var role in roles)
            {
                _Roles.Add(role.Name, new List<string>{role.Name});

                var parent = role.Role1;
                
                while (parent != null)
                {
                    _Roles[role.Name].Add(parent.Name);
                    
                    parent = parent.Role1;
                }
            }
        }

        public override string ApplicationName { get; set; }
    }
}