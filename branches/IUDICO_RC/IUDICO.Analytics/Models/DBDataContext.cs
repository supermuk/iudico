using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models
{
    using IUDICO.Common.Models;
    using IUDICO.Common.Models.Interfaces;

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
        }

        public DBDataContext(string connection) :
            base(connection, mappingSource)
        {
        }

        public DBDataContext(IDbConnection connection) :
            base(connection, mappingSource)
        {
        }

        public DBDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
        }

        public DBDataContext(IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
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

        IMockableTable<ForecastingTree> IDataContext.ForecastingTrees
        {
            get { return new MockableTable<ForecastingTree>(this.ForecastingTrees); }
        }

        IMockableTable<TopicScore> IDataContext.TopicScores
        {
            get { return new MockableTable<TopicScore>(this.TopicScores); }
        }

        IMockableTable<UserScore> IDataContext.UserScores
        {
            get { return new MockableTable<UserScore>(this.UserScores); }
        }

        IMockableTable<Tag> IDataContext.Tags
        {
            get { return new MockableTable<Tag>(this.Tags); }
        }

        IMockableTable<TopicTag> IDataContext.TopicTags
        {
            get { return new MockableTable<TopicTag>(this.TopicTags); }
        }
    }
}