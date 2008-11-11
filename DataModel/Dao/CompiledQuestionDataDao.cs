using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.Dao
{
    public class CompiledQuestionDataDao : Dao
    {
        public void Insert(CompiledQuestionDataEntity cqde)
        {
            SqlCommand sqlCommand = GetSqlCommand("spCompiledQuestionsDataInsert");

            try
            {
                sqlCommand.Parameters.Add("@CompiledQuestionRef", SqlDbType.Int).Value = cqde.CompiledQuestionRef;
                sqlCommand.Parameters.Add("@Input", SqlDbType.NVarChar).Value = cqde.Input;
                sqlCommand.Parameters.Add("@Output", SqlDbType.NVarChar).Value = cqde.Output;
                sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}