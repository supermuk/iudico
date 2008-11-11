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
                if(fe.File != null)
                    sqlCommand.Parameters.Add("@File", SqlDbType.VarBinary).Value = fe.File;
                sqlCommand.Parameters.Add("@IsDirectory", SqlDbType.Bit).Value = fe.IsDirectory;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = fe.Name;
                if(fe.PageRef != 0)
                    sqlCommand.Parameters.Add("@PageRef", SqlDbType.Int).Value = fe.PageRef;

                sqlCommand.LexExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        public FilesEntity Select(int fileId)
        {
            SqlCommand sqlCommand = GetSqlCommand("spFilesSelect");
            SqlDataReader sqlReader = null;
            try
            {
                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = fileId;
                sqlReader = sqlCommand.ExecuteReader();
                FilesEntity fe = null;
                if (sqlReader != null)
                    while (sqlReader.Read())
                    {
                        var id = (int)sqlReader["ID"];
                        var file = (byte[])sqlReader["File"];
                        var isDirectory = (bool)sqlReader["IsDirectory"];
                        
                        string name = sqlReader["Name"].ToString();
                        
                        var pid = 0;
                        if (!sqlReader["PID"].ToString().Equals(string.Empty))
                            pid = (int)sqlReader["PID"];
                        
                        var pageRef = 0;
                        if (!sqlReader["PageRef"].ToString().Equals(string.Empty))
                            pageRef = (int)sqlReader["PageRef"];

                        if(isDirectory)
                        {
                            fe = FilesEntity.newDirectory(id, pageRef, name);
                        }
                        else
                        {
                            fe = FilesEntity.newFile(id, pid, file, name, pageRef);
                        }

                    }
                return fe;
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
                CloseConnection();
            }
        }

        public int SelectId(int pageRef, string name)
        {
            SqlCommand sqlCommand = GetSqlCommand("spFilesSelectId");
            SqlDataReader sqlReader = null;
            try
            {
                sqlCommand.Parameters.Add("@PageRef", SqlDbType.Int).Value = pageRef;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                sqlReader = sqlCommand.ExecuteReader();
                int fileId = 0;
                if (sqlReader != null)
                    while (sqlReader.Read())
                    {
                        fileId = (int)sqlReader["ID"];
                    }
                return fileId;
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
                CloseConnection();
            }
        }
    }
}