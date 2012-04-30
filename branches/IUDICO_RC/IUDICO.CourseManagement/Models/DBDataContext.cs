using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CourseManagement.Models
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
            this.OnCreated();
        }

        public DBDataContext(string connection) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public DBDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public DBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public DBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            this.OnCreated();
        }

        public System.Data.Linq.Table<CourseUser> CourseUsers
        {
            get
            {
                return this.GetTable<CourseUser>();
            }
        }

        public System.Data.Linq.Table<Node> Nodes
        {
            get
            {
                return this.GetTable<Node>();
            }
        }

        public System.Data.Linq.Table<Course> Courses
        {
            get
            {
                return this.GetTable<Course>();
            }
        }

        public System.Data.Linq.Table<NodeResource> NodeResources
        {
            get
            {
                return this.GetTable<NodeResource>();
            }
        }

        IMockableTable<Course> IDataContext.Courses
        {
            get { return new MockableTable<Course>(this.Courses); }
        }

        IMockableTable<CourseUser> IDataContext.CourseUsers
        {
            get { return new MockableTable<CourseUser>(this.CourseUsers); }
        }

        IMockableTable<Node> IDataContext.Nodes
        {
            get { return new MockableTable<Node>(this.Nodes); }
        }

        IMockableTable<NodeResource> IDataContext.NodeResources
        {
            get { return new MockableTable<NodeResource>(this.NodeResources); }
        }
    }
}