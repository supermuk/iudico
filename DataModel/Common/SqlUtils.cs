using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public enum SINGLE_READ_RESULT
    {
        OK,
        NO_DATA,
        AMBIGUOUS_DATA
    }

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

        public static List<int> FullReadInts([NotNull] this IDbCommand cmd)
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

        public static List<string> FullReadStrings([NotNull] this IDbCommand cmd)
        {
            using (var r = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                var result = new List<string>();

                while (r.Read())
                {
                    result.Add(r.GetString(0));
                }

                return result;
            }
        }
    }
}
