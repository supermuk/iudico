using System;
using System.Linq;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Security
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            return ServerModel.User.ByLogin(username).Roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return ServerModel.User.ByLogin(username).Roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new InvalidOperationException(Translations.CustomRoleProvider_CreateRole_CreateRole_is_not_allowed);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new InvalidOperationException(Translations.CustomRoleProvider_DeleteRole_DeleteRole_is_not_allowed);
        }

        public override bool RoleExists(string roleName)
        {
            return DBEnum<FX_ROLE>.Values.Contains(roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return DBEnum<FX_ROLE>.Values.ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { return "IUDICO"; }
            set { throw new InvalidOperationException(Translations.CustomRoleProvider_ApplicationName_Changing_ApplicationName_is_not_allowed); }
        }
    }
}
