using System.Data;
using System.Data.SqlClient;

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

        protected int GetCourseId(SqlCommand sqlCommand)
        {
            int courseId = 0;
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            if (sqlReader != null)
            {
                if (sqlReader.Read())
                    courseId = (int) sqlReader["ID"];
                
                sqlReader.Close();
            }
            return courseId;
        }
    }
}