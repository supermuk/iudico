using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IUDICO.Common.Models;

namespace IUDICO.UserManagement.Models.Database
{
    public class UserManagementDBContext : DbContext
    {
        public UserManagementDBContext() : base(IUDICO.UserManagement.Properties.Settings.Default.ConnectionString) { }

        public DbSet<User> Users { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}