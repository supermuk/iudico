using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public static class SqlUtils
    {
        public static string CreateSafeDropProcedureStatement([NotNull] string name)
        {
            return string.Format(
                    @"IF EXISTS (
	SELECT object_id FROM sys.procedures 
	WHERE name = '{0}'
) 
	DROP PROCEDURE {0}",
                    name);
        }

        public static void RecreateProc([NotNull] string name, [NotNull] string body, [NotNull] IDbCommand cmd)
        {
            Logger.WriteLine(name);

            cmd.CommandText = CreateSafeDropProcedureStatement(name);
            cmd.ExecuteNonQuery();

            cmd.CommandText = body;
            cmd.ExecuteNonQuery();
        }

        public static List<int> FullReadInts([NotNull] SqlCommand cmd)
        {
            using (var r = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                var result = new List<int>();

                while (r.Read())
                {
                    result.Add(r.GetInt32(0));
                }

                return result;
            }
        }

        public static SqlConnection AcquireConnection()
        {
            return new SqlConnection(WebConfigurationManager.ConnectionStrings["IUDICO"].ConnectionString);
        }

        public static SqlConnection AcruireOpenedConnection()
        {
            var res = AcquireConnection();
            res.Open();
            return res;
        }
    }
}
