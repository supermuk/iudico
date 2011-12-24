using System.Data.Linq.Mapping;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models
{
    public partial class DBDataContext : System.Data.Linq.DataContext, IDataContext
    {
        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions

        partial void OnCreated();
        partial void InsertManualResult(ManualResult instance);
        partial void UpdateManualResult(ManualResult instance);
        partial void DeleteManualResult(ManualResult instance);

        #endregion

        #region Constructors

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

        #endregion

        #region Public Properties

        public System.Data.Linq.Table<ManualResult> ManualResults
        {
            get
            {
                return this.GetTable<ManualResult>();
            }
        }

        #endregion

        #region IDataContext Members

        IMockableTable<ManualResult> IDataContext.ManualResults
        {
            get { return new MockableTable<ManualResult>(ManualResults); }
        }

        #endregion
    }
}