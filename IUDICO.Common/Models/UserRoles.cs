﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IUDICO.Common.Models
{
    [Flags]
    public enum Role
    {
        None = 0,
        Student = 1,
        Teacher = 2,
        Admin = 4,
        CourseCreator = 8
    }

    public static class UserRoles
    {
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

        public static Role GetRole(int id)
        {
            return (Role)id;
        }

        public static IEnumerable<Role> GetRoles()
        {
            return Enum.GetValues(typeof(Role)).Cast<Role>().Skip(1);
        }
    }
}
