using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UserManagement.Models
{
    public partial class DBDataContext : DataContext, IDataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

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
            base(ConfigurationManager.ConnectionStrings["IUDICOConnectionString"].ConnectionString, mappingSource)
        {
            this.OnCreated();
        }

        public DBDataContext(string connection) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public DBDataContext(IDbConnection connection) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public DBDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public DBDataContext(IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public Table<GroupUser> GroupUsers
        {
            get { return this.GetTable<GroupUser>(); }
        }

        public Table<Group> Groups
        {
            get { return this.GetTable<Group>(); }
        }

        public Table<UserRole> UserRoles
        {
            get { return this.GetTable<UserRole>(); }
        }

        public Table<User> Users
        {
            get { return this.GetTable<User>(); }
        }

        public Table<UserTopicRating> UserTopicRatings
        {
            get { return this.GetTable<UserTopicRating>(); }
        }

        IMockableTable<GroupUser> IDataContext.GroupUsers
        {
            get { return new MockableTable<GroupUser>(this.GroupUsers); }
        }

        IMockableTable<Group> IDataContext.Groups
        {
            get { return new MockableTable<Group>(this.Groups); }
        }

        IMockableTable<UserRole> IDataContext.UserRoles
        {
            get { return new MockableTable<UserRole>(this.UserRoles); }
        }

        IMockableTable<User> IDataContext.Users
        {
            get { return new MockableTable<User>(this.Users); }
        }

        IMockableTable<UserTopicRating> IDataContext.UserTopicRatings
        {
            get { return new MockableTable<UserTopicRating>(this.UserTopicRatings); }
        }
    }
}