using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.Dao
{
    public class ThemeDao : Dao
    {
        public void Insert(ThemeEntity ce)
        {
            SqlCommand sqlCommand = GetSqlCommand("spThemesInsert");

            try
            {
                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ce.Id;
                sqlCommand.Parameters.Add("@CourseRef", SqlDbType.Int).Value = ce.CourseRef;
                sqlCommand.Parameters.Add("@IsControl", SqlDbType.Bit).Value = ce.IsControl;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = ce.Name;
                sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}