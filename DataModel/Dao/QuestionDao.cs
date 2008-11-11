using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao;
using LEX.CONTROLS;

/// <summary>
/// Summary description for CorrectAnswerDao
/// </summary>
public class QuestionDao : Dao
{
    public void Insert(QuestionEntity cae)
    {
        SqlCommand sqlCommand = GetSqlCommand("spQuestionsInsert");

        try
        {
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = cae.Id;
            if(cae.CompiledQuestionRef != 0)
                sqlCommand.Parameters.Add("@CompiledQuestionRef", SqlDbType.Int).Value = cae.CompiledQuestionRef;
            if(cae.CorrectAnswer != null)
                sqlCommand.Parameters.Add("@CorrectAnswer", SqlDbType.NVarChar).Value = cae.CorrectAnswer;
            sqlCommand.Parameters.Add("@IsCompiled", SqlDbType.Bit).Value = cae.IsCompiled;
            sqlCommand.Parameters.Add("@PageRef", SqlDbType.Int).Value = cae.PageRef;
            sqlCommand.Parameters.Add("@Rank", SqlDbType.Int).Value = cae.Rank;
            sqlCommand.Parameters.Add("@TestName", SqlDbType.NVarChar, 50).Value = cae.TestName;

            sqlCommand.LexExecuteNonQuery();
        }
        finally
        {
            CloseConnection();
        }
    }
}