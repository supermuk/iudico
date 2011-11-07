using IUDICO.Common.Models.Interfaces;

namespace IUDICO.Common.Models
{
    public partial class DBDataContext : IDataContext
    {
        IMockableTable<User> IDataContext.Users
        {
            get
            {
                return new MockableTable<User>(Users);
            }
        }

        IMockableTable<Group> IDataContext.Groups
        {
            get { return new MockableTable<Group>(Groups); }
        }

        IMockableTable<GroupUser> IDataContext.GroupUsers
        {
            get { return new MockableTable<GroupUser>(GroupUsers); }
        }

        IMockableTable<UserRole> IDataContext.UserRoles
        {
            get { return new MockableTable<UserRole>(UserRoles); }
        }
    }
}
