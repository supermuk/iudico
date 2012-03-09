﻿using System.Data.Linq.Mapping;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
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

        public System.Data.Linq.Table<Curriculum> Curriculums
        {
            get
            {
                return this.GetTable<Curriculum>();
            }
        }

        public System.Data.Linq.Table<Topic> Topics
        {
            get
            {
                return this.GetTable<Topic>();
            }
        }

        public System.Data.Linq.Table<CurriculumChapterTopic> CurriculumChapterTopics
        {
            get
            {
                return this.GetTable<CurriculumChapterTopic>();
            }
        }

        public System.Data.Linq.Table<CurriculumChapter> CurriculumChapters
        {
            get
            {
                return this.GetTable<CurriculumChapter>();
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

        IMockableTable<Curriculum> IDataContext.Curriculums
        {
            get { return new MockableTable<Curriculum>(Curriculums); }
        }

        IMockableTable<CurriculumChapter> IDataContext.CurriculumChapters
        {
            get { return new MockableTable<CurriculumChapter>(CurriculumChapters); }
        }

        IMockableTable<CurriculumChapterTopic> IDataContext.CurriculumChapterTopics
        {
            get { return new MockableTable<CurriculumChapterTopic>(CurriculumChapterTopics); }
        }
    }
}