using System.Data.Linq.Mapping;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models;

namespace IUDICO.DisciplineManagement.Models
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
        partial void InsertUserTopicScore(UserTopicScore instance);
        partial void UpdateUserTopicScore(UserTopicScore instance);
        partial void DeleteUserTopicScore(UserTopicScore instance);
        partial void InsertTopicFeature(TopicFeature instance);
        partial void UpdateTopicFeature(TopicFeature instance);
        partial void DeleteTopicFeature(TopicFeature instance);
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

        public System.Data.Linq.Table<Topic> Topics
        {
            get
            {
                return this.GetTable<Topic>();
            }
        }

        public System.Data.Linq.Table<Chapter> Chapters
        {
            get
            {
                return this.GetTable<Chapter>();
            }
        }

        public System.Data.Linq.Table<TopicType> TopicTypes
        {
            get
            {
                return this.GetTable<TopicType>();
            }
        }

        public System.Data.Linq.Table<Discipline> Disciplines
        {
            get
            {
                return this.GetTable<Discipline>();
            }
        }

        public System.Data.Linq.Table<UserTopicScore> UserTopicScores
        {
            get
            {
                return this.GetTable<UserTopicScore>();
            }
        }
        IMockableTable<Discipline> IDataContext.Disciplines
        {
            get { return new MockableTable<Discipline>(Disciplines); }
        }

        IMockableTable<Chapter> IDataContext.Chapters
        {
            get { return new MockableTable<Chapter>(Chapters); }
        }

        IMockableTable<Topic> IDataContext.Topics
        {
            get { return new MockableTable<Topic>(Topics); }
        }

        IMockableTable<TopicType> IDataContext.TopicTypes
        {
            get { return new MockableTable<TopicType>(TopicTypes); }
        }

        IMockableTable<UserTopicScore> IDataContext.UserTopicScores
        {
            get { return new MockableTable<UserTopicScore>(UserTopicScores); }
        }
    }
}