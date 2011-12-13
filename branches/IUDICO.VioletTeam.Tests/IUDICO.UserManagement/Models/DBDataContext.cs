using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;
using System.Data.Linq.Mapping;

namespace IUDICO.UserManagement.Models
{
    public partial class DBDataContext : System.Data.Linq.DataContext, IDataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void InsertGroupUser(GroupUser instance);
        partial void UpdateGroupUser(GroupUser instance);
        partial void DeleteGroupUser(GroupUser instance);
        partial void InsertGroup(Group instance);
        partial void UpdateGroup(Group instance);
        partial void DeleteGroup(Group instance);
        partial void InsertUserRole(UserRole instance);
        partial void UpdateUserRole(UserRole instance);
        partial void DeleteUserRole(UserRole instance);
        partial void InsertUser(User instance);
        partial void UpdateUser(User instance);
        partial void DeleteUser(User instance);
        #endregion

        public DBDataContext() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["IUDICOConnectionString"].ConnectionString, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public System.Data.Linq.Table<GroupUser> GroupUsers
        {
            get
            {
                return this.GetTable<GroupUser>();
            }
        }

        public System.Data.Linq.Table<Group> Groups
        {
            get
            {
                return this.GetTable<Group>();
            }
        }

        public System.Data.Linq.Table<UserRole> UserRoles
        {
            get
            {
                return this.GetTable<UserRole>();
            }
        }

        public System.Data.Linq.Table<User> Users
        {
            get
            {
                return this.GetTable<User>();
            }
        }

        IMockableTable<GroupUser> IDataContext.GroupUsers
        {
            get { return new MockableTable<GroupUser>(GroupUsers); }
        }

        IMockableTable<Group> IDataContext.Groups
        {
            get { return new MockableTable<Group>(Groups); }
        }

        IMockableTable<UserRole> IDataContext.UserRoles
        {
            get { return new MockableTable<UserRole>(UserRoles); }
        }

        IMockableTable<User> IDataContext.Users
        {
            get { return new MockableTable<User>(Users); }
        }
    }
}