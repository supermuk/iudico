using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao;
using LEX.CONTROLS;

/// <summary>
/// Summary description for UserAnswerDao
/// </summary>
public class UserAnswerDao : Dao
{
    public void Insert(UserAnswerEntity uae)
    {
        SqlCommand sqlCommand = GetSqlCommand("spUserAnswersInsert");

        try
        {
            if(uae.CompiledAnswerRef != 0)
                sqlCommand.Parameters.Add("@CompiledAnswerRef", SqlDbType.Int).Value = uae.CompiledAnswerRef;
            sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = uae.Date;
            sqlCommand.Parameters.Add("@IsCompiledAnswer", SqlDbType.Bit).Value = uae.IsCompiledAnswer;
            sqlCommand.Parameters.Add("@QuestionRef", SqlDbType.Int).Value = uae.QuestionRef;
            sqlCommand.Parameters.Add("@UserAnswer", SqlDbType.NVarChar).Value = uae.UserAnswer;
            sqlCommand.Parameters.Add("@UserRef", SqlDbType.Int).Value = uae.UserRef;
            sqlCommand.LexExecuteNonQuery();
        }
        finally
        {
            CloseConnection();
        }
    }
}