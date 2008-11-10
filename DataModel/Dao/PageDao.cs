using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Dao
{
    public class PageDao : Dao
    {
        public void Insert(PageEntity pe)
        {
            SqlCommand sqlCommand = GetSqlCommand("spPagesInsert");
            try
            {
                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = pe.Id;
                sqlCommand.Parameters.Add("@PageFile", SqlDbType.VarBinary).Value = pe.PageFile;
                sqlCommand.Parameters.Add("@PageName", SqlDbType.NVarChar, 50).Value = pe.PageName;
                if(pe.PageRank != 0)
                    sqlCommand.Parameters.Add("@PageRank", SqlDbType.Int).Value = pe.PageRank;
                sqlCommand.Parameters.Add("@PageTypeRef", SqlDbType.Int).Value = pe.PageType;
                sqlCommand.Parameters.Add("@ThemeRef", SqlDbType.Int).Value = pe.ThemeRef;
                sqlCommand.LexExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}