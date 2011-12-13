using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
namespace IUDICO.UserManagement.Models
{
    public interface IDataContext : IMockableDataContext
    {
        IMockableTable<User> Users { get; }
        IMockableTable<Group> Groups { get; }
        IMockableTable<GroupUser> GroupUsers { get; }
        IMockableTable<UserRole> UserRoles { get; }
    }
}
