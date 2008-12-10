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

        public void Initialize(Cache c)
        {
            Initialize();
            if (c == null)
                throw new ArgumentNullException("c");
            _Cache = c;
        }

        public Cache Cache { get { return _Cache; } }

        private Cache _Cache;
    }
}
