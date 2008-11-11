using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.Dao
{
    public class CompiledQuestionDao : Dao
    {
        public int Insert(CompiledQuestionEntity cqe)
        {
            SqlCommand sqlCommand = GetSqlCommand("spCompiledQuestionsInsert");
            int courseId;
            try
            {
                sqlCommand.Parameters.Add("@LanguageRef", SqlDbType.Int).Value = cqe.LanguageRef;
                sqlCommand.Parameters.Add("@MemoryLimit", SqlDbType.Int).Value = cqe.MemoryLimit;
                sqlCommand.Parameters.Add("@OutputLimit", SqlDbType.Int).Value = cqe.OutputLimit;
                sqlCommand.Parameters.Add("@TimeLimit", SqlDbType.Int).Value = cqe.TimeLimit;

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