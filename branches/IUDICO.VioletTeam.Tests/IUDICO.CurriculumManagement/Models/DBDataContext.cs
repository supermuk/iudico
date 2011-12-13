using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models
{
    public partial class DBDataContext : System.Data.Linq.DataContext
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
    }
}