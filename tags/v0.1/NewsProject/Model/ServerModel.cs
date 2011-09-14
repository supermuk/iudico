using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Caching;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace LEX.CONTROLS
{
    public static class ServerModel
    {
        public static DatabaseModel DB;

        public static void Initialize(string connectionString, Cache cache)
        {
            using(Logger.Scope("Initializing ServerModel"))
            {
                if (__ConnectionString != null)
                    throw new DMError("ServerModel is alredy initialized");

                __ConnectionString = connectionString;
                __Connection = new SqlConnection(connectionString);

                (DB = new DatabaseModel(__Connection)).Initialize(cache);
            }
        }

        public static void UnInitialize()
        {
            using (Logger.Scope("UnInitializing DatabaseModel"))
            {
                __Connection.Close();
            }
        }        

        internal static DbConnection AcquireOpenedConnection()
        {
            if (__Connection.State == ConnectionState.Closed)
                __Connection.Open();
            return __Connection;
        }

        private static string __ConnectionString;
        private static SqlConnection __Connection;
    }
}
