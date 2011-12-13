using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
namespace IUDICO.CourseManagement.Models
{
    public interface IDataContext : IMockableDataContext
    {
        //IMockableTable<User> Users { get; }
        IMockableTable<Course> Courses { get; }
        IMockableTable<CourseUser> CourseUsers { get; }
        IMockableTable<Node> Nodes { get; }
        IMockableTable<NodeResource> NodeResources { get; }
    }
}
