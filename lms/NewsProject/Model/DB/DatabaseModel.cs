using System;
using System.Data.Common;
using System.Web.Caching;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB
{
    public partial class DatabaseModel
    {
        public DbConnection GetConnectionSafe()
        {
            return ServerModel.AcquireOpenedConnection();
        }
    }
}
