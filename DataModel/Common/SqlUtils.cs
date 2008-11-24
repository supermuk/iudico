using System.Collections.Generic;
using System.Data;
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
        [NotNull]
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
            cmd.LexExecuteNonQuery();

            cmd.CommandText = body;
            cmd.LexExecuteNonQuery();
        }

        [NotNull]
        public static List<int> FullReadInts([NotNull] this IDbCommand cmd)
        {
            using (var r = cmd.LexExecuteReader())
            {
                var result = new List<int>();

                while (r.Read())
                {
                    result.Add(r.GetInt32(0));
                }

                return result;
            }
        }

        public static List<int> FullReadInts([NotNull] this IDbCommand cmd, string columnName)
        {
            using (var r = cmd.LexExecuteReader())
            {
                var result = new List<int>();
                var id = -1;

                while (r.Read())
                {
                    if (id < 0)
                    {
                        id = r.GetOrdinal(columnName);
                    }
                    result.Add(r.GetInt32(id));
                }

                // remove duplicates
                result.Sort(); 
                for (int i = result.Count - 1; i > 0; --i)
                {
                    if (result[i] == result[i - 1])
                    {
                        result.RemoveAt(i);
                    }
                }

                return result;
            }
        }

        [NotNull]
        public static List<string> FullReadStrings([NotNull] this IDbCommand cmd)
        {
            using (var r = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                var result = new List<string>();

                while (r.Read())
                {
                    result.Add(r.GetStringNull(0));
                }

                return result;
            }
        }

        [CanBeNull]
        public static string GetStringNull([NotNull] this IDataReader r, int ind)
        {
            if (r.IsDBNull(ind))
                return null;
            return r.GetString(ind);
        }

        [NotNull]
        public static string WrapDbId([NotNull] string id)
        {
            return string.Concat("[", id, "]");

        }

        public static string WrapArc([NotNull] string id)
        {
            return string.Concat("(", id, ")");            
        }
    }
}
