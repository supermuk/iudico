using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Dao
{
    public class CompiledQuestionDao : Dao
    {
        public void Insert(CompiledQuestionEntity cqe)
        {
            SqlCommand sqlCommand = GetSqlCommand("spCompiledQuestionsInsert");

            try
            {
                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = cqe.Id;
                sqlCommand.Parameters.Add("@LanguageRef", SqlDbType.Int).Value = cqe.LanguageRef;
                sqlCommand.Parameters.Add("@MemoryLimit", SqlDbType.Int).Value = cqe.MemoryLimit;
                sqlCommand.Parameters.Add("@OutputLimit", SqlDbType.Int).Value = cqe.OutputLimit;
                sqlCommand.Parameters.Add("@TimeLimit", SqlDbType.Int).Value = cqe.TimeLimit;
                sqlCommand.LexExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}