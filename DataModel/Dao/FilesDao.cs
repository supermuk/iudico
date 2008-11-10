using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Dao.Entity;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Dao
{
    public class FilesDao : Dao
    {
        public void Insert(FilesEntity fe)
        {
            SqlCommand sqlCommand = GetSqlCommand("spFilesInsert");
            try
            {
                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value =  fe.Id;
                if(fe.File != null)
                    sqlCommand.Parameters.Add("@File", SqlDbType.VarBinary).Value = fe.File;
                sqlCommand.Parameters.Add("@IsDirectory", SqlDbType.Bit).Value = fe.IsDirectory;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = fe.Name;
                if(fe.PageRef != 0)
                    sqlCommand.Parameters.Add("@PageRef", SqlDbType.Int).Value = fe.PageRef;
                if(fe.Pid != 0)
                    sqlCommand.Parameters.Add("@PID", SqlDbType.Int).Value = fe.Pid;

                sqlCommand.LexExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}