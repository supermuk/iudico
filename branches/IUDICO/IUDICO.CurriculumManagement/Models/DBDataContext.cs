using System.Data.Linq.Mapping;
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

        public System.Data.Linq.Table<TopicType> TopicTypes
        {
            get
            {
                return this.GetTable<TopicType>();
            }
        }

        public System.Data.Linq.Table<TopicAssignment> TopicAssignments
        {
            get
            {
                return this.GetTable<TopicAssignment>();
            }
        }

        public System.Data.Linq.Table<Timeline> Timelines
        {
            get
            {
                return this.GetTable<Timeline>();
            }
        }

        public System.Data.Linq.Table<Discipline> Disciplines
        {
            get
            {
                return this.GetTable<Discipline>();
            }
        }

        public System.Data.Linq.Table<Chapter> Chapters
        {
            get
            {
                return this.GetTable<Chapter>();
            }
        }

        //IMockableTable<Course> IDataContext.Courses
        //{
        //    get { return new MockableTable<Course>(Courses); }
        //}

        //IMockableTable<User> IDataContext.Users
        //{
        //    get { return new MockableTable<User>(Users); }
        //}

        //IMockableTable<Group> IDataContext.Groups
        //{
        //    get { return new MockableTable<Group>(Groups); }
        //}

        //IMockableTable<GroupUser> IDataContext.GroupUsers
        //{
        //    get { return new MockableTable<GroupUser>(GroupUsers); }
        //}

        //IMockableTable<UserRole> IDataContext.UserRoles
        //{
        //    get { return new MockableTable<UserRole>(UserRoles); }
        //}

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

        IMockableTable<Curriculum> IDataContext.Curriculums
        {
            get { return new MockableTable<Curriculum>(Curriculums); }
        }

        IMockableTable<Timeline> IDataContext.Timelines
        {
            get { return new MockableTable<Timeline>(Timelines); }
        }

        IMockableTable<TopicAssignment> IDataContext.TopicAssignments
        {
            get { return new MockableTable<TopicAssignment>(TopicAssignments); }
        }

        IMockableTable<TopicType> IDataContext.TopicTypes
        {
            get { return new MockableTable<TopicType>(TopicTypes); }
        }
    }
}