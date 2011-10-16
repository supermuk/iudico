using System.Data.Entity;
using IUDICO.Common.Models;

class CourseManagementDbConext : DbContext
{
    public CourseManagementDbConext() : base(IUDICO.CourseManagement.Properties.Settings.Default.ConnectionString) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseUser> CourseUsers { get; set; }
    public DbSet<Node> Nodes { get; set; }
}