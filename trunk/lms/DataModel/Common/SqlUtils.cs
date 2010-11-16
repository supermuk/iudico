using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public enum SINGLE_READ_RESULT
    {
        OK,
        NO_DATA,
        AMBIGUOUS_DATA
    }

    /// <summary>
    /// Some sql queries
    /// </summary>
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
                    var v = r.GetValue(0);
                    if (!(v is DBNull))
                        result.Add((int)v);
                }

                return result;
            }
        }

        [NotNull]
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
                    var v = r.GetValue(id);
                    if (!(v is DBNull))
                        result.Add((int)v);
                }

                return result;
            }
        }

        [NotNull]
        public static List<string> FullReadStrings([NotNull] this IDbCommand cmd)
        {
            using (var r = cmd.ExecuteReader())
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

        public static string SqlQueryToString([NotNull]IDbCommand cmd)
        {
            var b = new StringBuilder();
            b.AppendLine(cmd.CommandText);
            var prms = cmd.Parameters;
            if (prms != null && prms.Count > 0)
            {
                foreach (IDbDataParameter p in prms)
                {
                    b.Append(p.ParameterName);
                    b.Append(" = ");
                    b.Append(p.Value);
               }   
            }
            return b.ToString();
        }
    }
}
