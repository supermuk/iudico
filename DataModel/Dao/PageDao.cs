using System;
using System.Data;
using System.Data.SqlClient;
using CourseImport.Dao.Entity;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.Dao
{
    public class PageDao : Dao
    {
        public int Insert(PageEntity pe)
        {
            SqlCommand sqlCommand = GetSqlCommand("spPagesInsert");
            int courseId;
            try
            {
                sqlCommand.Parameters.Add("@PageFile", SqlDbType.VarBinary).Value = pe.PageFile;
                sqlCommand.Parameters.Add("@PageName", SqlDbType.NVarChar, 50).Value = pe.PageName;
                if(pe.PageRank != 0)
                    sqlCommand.Parameters.Add("@PageRank", SqlDbType.Int).Value = pe.PageRank;
                sqlCommand.Parameters.Add("@PageTypeRef", SqlDbType.Int).Value = pe.PageType;
                sqlCommand.Parameters.Add("@ThemeRef", SqlDbType.Int).Value = pe.ThemeRef;
                
                courseId = GetCourseId(sqlCommand);
            }
            finally
            {
                CloseConnection();
            }

            return courseId;
        }

        public PageEntity Select(int pageId)
        {
            SqlCommand sqlCommand = GetSqlCommand("spPagesSelect");
            SqlDataReader sqlReader = null;
            try
            {
                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = pageId;
                sqlReader = sqlCommand.ExecuteReader();
                PageEntity pe = null;
                if (sqlReader != null)
                    while (sqlReader.Read())
                    {
                        var id = (int)sqlReader["ID"];
                        var pageFile = (byte[])sqlReader["PageFile"];
                        string pageName = sqlReader["PageName"].ToString();
                        var pageType = (PageTypeEnum)Enum.ToObject(typeof(PageTypeEnum), (int)sqlReader["PageTypeRef"]); 
                        var themeRef = (int) sqlReader["ThemeRef"];
                        var pageRank = 0;
                        if (!sqlReader["PageRank"].ToString().Equals(string.Empty))
                            pageRank = (int)sqlReader["PageRank"];


                        pe = new PageEntity(id, themeRef, pageName, pageFile, pageType, pageRank);

                    }
                return pe;
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
                CloseConnection();
            }
        }
    }
}