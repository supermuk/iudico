using IUDICO.Common.Models.Interfaces;

namespace IUDICO.Common.Models
{
    public partial class DBDataContext : IDataContext
    {
        IMockableTable<User> IDataContext.Users
        {
            get { return new MockableTable<User>(Users); }
        }
        IMockableTable<Course> IDataContext.Courses
        {
            get { return new MockableTable<Course>(Courses); }
        }
        IMockableTable<CourseUser> IDataContext.CourseUsers
        {
            get { return new MockableTable<CourseUser>(CourseUsers); }
        }
        IMockableTable<Node> IDataContext.Nodes
        {
            get { return new MockableTable<Node>(Nodes); }
        }
        IMockableTable<NodeResource> IDataContext.NodeResources
        {
            get { return new MockableTable<NodeResource>(NodeResources); }
        }
    }
}
