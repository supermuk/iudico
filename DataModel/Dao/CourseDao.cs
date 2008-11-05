using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.Dao
{
    public class CourseDao : Dao
    {
        public void Insert(CourseEntity ce)
        {
            SqlCommand sqlCommand = GetSqlCommand("spCoursesInsert");

            try
            {
                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ce.Id;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = ce.Name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ce.Description;
                sqlCommand.Parameters.Add("@UploadDate", SqlDbType.DateTime).Value = ce.UploadDate;
                sqlCommand.Parameters.Add("@Version", SqlDbType.Int).Value = ce.Version;
                sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}