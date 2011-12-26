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

        public System.Data.Linq.Table<CurriculumAssignment> CurriculumAssignments
        {
            get
            {
                return this.GetTable<CurriculumAssignment>();
            }
        }

        public System.Data.Linq.Table<Theme> Themes
        {
            get
            {
                return this.GetTable<Theme>();
            }
        }

        public System.Data.Linq.Table<ThemeType> ThemeTypes
        {
            get
            {
                return this.GetTable<ThemeType>();
            }
        }

        public System.Data.Linq.Table<ThemeAssignment> ThemeAssignments
        {
            get
            {
                return this.GetTable<ThemeAssignment>();
            }
        }

        public System.Data.Linq.Table<Timeline> Timelines
        {
            get
            {
                return this.GetTable<Timeline>();
            }
        }

        public System.Data.Linq.Table<Curriculum> Curriculums
        {
            get
            {
                return this.GetTable<Curriculum>();
            }
        }

        public System.Data.Linq.Table<Stage> Stages
        {
            get
            {
                return this.GetTable<Stage>();
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

        IMockableTable<Curriculum> IDataContext.Curriculums
        {
            get { return new MockableTable<Curriculum>(Curriculums); }
        }

        IMockableTable<Stage> IDataContext.Stages
        {
            get { return new MockableTable<Stage>(Stages); }
        }

        IMockableTable<Theme> IDataContext.Themes
        {
            get { return new MockableTable<Theme>(Themes); }
        }

        IMockableTable<CurriculumAssignment> IDataContext.CurriculumAssignments
        {
            get { return new MockableTable<CurriculumAssignment>(CurriculumAssignments); }
        }

        IMockableTable<Timeline> IDataContext.Timelines
        {
            get { return new MockableTable<Timeline>(Timelines); }
        }

        IMockableTable<ThemeAssignment> IDataContext.ThemeAssignments
        {
            get { return new MockableTable<ThemeAssignment>(ThemeAssignments); }
        }

        IMockableTable<ThemeType> IDataContext.ThemeTypes
        {
            get { return new MockableTable<ThemeType>(ThemeTypes); }
        }
    }
}