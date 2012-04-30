using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models
{
    public partial class DBDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
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

        public System.Data.Linq.Table<Room> Rooms
        {
            get
            {
                return this.GetTable<Room>();
            }
        }

        public System.Data.Linq.Table<Computer> Computers
        {
            get
            {
                return this.GetTable<Computer>();
            }
        }

        public System.Data.Linq.Table<UserActivity> UserActivities
        {
            get
            {
                return this.GetTable<UserActivity>();
            }
        }
    }
}