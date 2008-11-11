using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.Dao
{
    public class ThemeDao : Dao
    {
        public int Insert(ThemeEntity ce)
        {
            SqlCommand sqlCommand = GetSqlCommand("spThemesInsert");
            int courseId;

            try
            {
                sqlCommand.Parameters.Add("@CourseRef", SqlDbType.Int).Value = ce.CourseRef;
                sqlCommand.Parameters.Add("@IsControl", SqlDbType.Bit).Value = ce.IsControl;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = ce.Name;

                courseId = GetCourseId(sqlCommand);
            }
            finally
            {
                CloseConnection();
            }

            return courseId;
        }
    }
}