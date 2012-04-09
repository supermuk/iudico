using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models
{
    public partial class DBDataContext : DataContext
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
            OnCreated();
        }

        public DBDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public Table<ForecastingTree> ForecastingTrees
        {
            get { return this.GetTable<ForecastingTree>(); }
        }

        public Table<TopicScore> TopicScores
        {
            get { return this.GetTable<TopicScore>(); }
        }

        public Table<UserScore> UserScores
        {
            get { return this.GetTable<UserScore>(); }
        }

        public Table<Tag> Tags
        {
            get { return this.GetTable<Tag>(); }
        }

        public Table<TopicTag> TopicTags
        {
            get { return this.GetTable<TopicTag>(); }
        }
    }
}