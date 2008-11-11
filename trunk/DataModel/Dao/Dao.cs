using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Dao
{
    public abstract class Dao
    {
        private SqlConnection connection;
        protected string connectionString;

        private SqlCommand sqlCommand;

        protected SqlConnection GetConnection()
        {
            connection = ServerModel.AcquireConnection();
            connection.Open();
            return connection;
        }

        protected void CloseConnection()
        {
            sqlCommand.Dispose();
            connection.Close();
            connection.Dispose();
        }

        protected SqlCommand GetSqlCommand(string procedureName)
        {
            connection = GetConnection();
            return (sqlCommand = new SqlCommand(procedureName, connection) {CommandType = CommandType.StoredProcedure});
        }
    }
}