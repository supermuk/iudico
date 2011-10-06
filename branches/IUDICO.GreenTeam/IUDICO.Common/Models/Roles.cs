using System;
using System.Collections.Generic;
using System.Linq;

namespace IUDICO.Common.Models
{
    public enum Role
    {
        None = 0,
        Student = 1,
        Teacher = 2,
        Admin = 3
    }

    public static class Roles
    {
        public static IEnumerable<Role> GetRoles()
        {
            return Enum.GetValues(typeof(Role)).Cast<Role>();
        }

        public static IEnumerable<string> GetStringRoles()
        {
            return GetRoles().Select(r => r.ToString());
        }

        public static Role GetRole(string roleName)
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
    }
}
