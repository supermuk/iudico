using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LEX.CONTROLS;

namespace IUDICO.DBManager
{
    public interface IDBUpdaterInteractionContext : IAsyncExecuterContext
    {
        string DBName { get; }
        SqlConnection Connection { get; }
        string DBScriptsPath { get; }
    }

    public static class DBUpdateManager
    {
        public static string GetDBUsageSql(string dbName)
        {
            return string.Format(@"select [status], [hostname], [PROGRAM_NAME] from master.dbo.sysprocesses where dbid = (SELECT database_id from sys.databases where name = '{0}')", dbName);
        }

        public static string GetDBVersionProcSql(int version, bool alter)
        {
            return string.Format(@"{1} FUNCTION GetDBVersion()
RETURNS INT
AS
BEGIN
	RETURN {0};
END", version, alter ? "ALTER" : "CREATE");
        }

        public static object EnsureDBVersionExists(IDBUpdaterInteractionContext context)
        {
            if (!IsDBVersionExists(context))
            {
                CreateSysDbVersionTable(context);
            }
            return true;
        }

        public static Func<IDBUpdaterInteractionContext, object> IsDatabaseExists(string dbName)
        {
            return c =>
               {
                   using (var cmd = c.Connection.CreateCommand())
                   {
                       cmd.CommandText = string.Format("select count(*) from sys.databases where name = '{0}'", dbName);
                       return cmd.LexExecuteScalar<int>() == 1;
                   }
               };
        }

        public static object DropDataBase(IDBUpdaterInteractionContext context)
        {
            var dbname = context.DBName;

            context.Connection.Close();
            SqlConnection.ClearAllPools();

            using (var newConnection = GetNewConnectionWithoutDBRefference(context.Connection))
            {
                newConnection.Open();

                using (var c = new SqlCommand(GetDBUsageSql(dbname), newConnection))
                {
                    var ok = true;
                    var sb = new StringBuilder("Database is using by following clients:");
                    sb.AppendLine();
                    using (var r = c.LexExecuteReader())
                    {
                        while (r.Read())
                        {
                            string status = r.GetString(0).Trim();
                            string host = r.GetString(1).Trim();
                            string prg = r.GetString(2).Trim();

                            if ((host.IsNotNull() || prg.IsNotNull()))
                            {
                                ok = false;
                                sb.AppendLine(string.Format("{0} | {1} | {2}", status, host, prg));
                            }
                        }
                    }
                    if (!ok)
                    {
                        sb.AppendLine();
                        sb.Append("Close ALL connection?");
                        if (
                            MessageBox.Show(sb.ToString(), "Close confirmation", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            c.CommandText =
                                string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                                              dbname);
                            c.CommandType = CommandType.Text;
                            c.LexExecuteNonQuery();
                        }
                        else
                            return false;
                    }
                }

                using (var c = new SqlCommand(string.Format("DROP DATABASE [{0}]", dbname), newConnection))
                {
                    c.LexExecuteNonQuery();
                }
            }
            return true;
        }

        public static object CreateDataBase(IDBUpdaterInteractionContext context)
        {
            var dbname = context.DBName;
            using (var newConnection = GetNewConnectionWithoutDBRefference(context.Connection))
            {
                context.Connection.Close();

                newConnection.Open();
                using (var c = new SqlCommand(string.Format("CREATE DATABASE [{0}]", dbname), newConnection))
                {
                    c.LexExecuteNonQuery();
                }
            }

            context.Connection.ConnectionString = new SqlConnectionStringBuilder(context.Connection.ConnectionString) { InitialCatalog = dbname }.ToString();

            context.Connection.Open();

            CreateSysDbVersionTable(context);
            return true;
        }

        public static Func<IDBUpdaterInteractionContext, object> UpgrateToNextVersion(int dbVersion, string sqlscript, string scriptFileName)
        {
            return frmDBStatus =>
               {
                   using (var transaction = frmDBStatus.Connection.BeginTransaction(IsolationLevel.ReadUncommitted))
                   {
                       using (var cmd = new SqlCommand("select dbo.GetDBVersion()", transaction.Connection)
                                            {
                                                Transaction = transaction
                                            })
                       {
                           try
                           {
                               var v = cmd.LexExecuteScalar<int>();
                               if (v != dbVersion)
                               {
                                   throw new InvalidDBVersionException(v);
                               }

                               foreach (var cmdText in SplitSqlCommands(sqlscript))
                               {
                                   cmd.CommandText = cmdText;
                                   cmd.LexExecuteNonQuery();
                               }

                               cmd.CommandText = GetDBVersionProcSql(dbVersion + 1, true);
                               cmd.LexExecuteNonQuery();

                               cmd.CommandText = "INSERT INTO sysDBVersion (VersionNumber, ScriptFileName, Date) VALUES (@version, @filename, @date)";
                               cmd.Parameters.Assign(new
                                                         {
                                                             version = dbVersion,
                                                             filename = scriptFileName,
                                                             date = DateTime.Now
                                                         });
                               cmd.LexExecuteNonQuery();

                               transaction.Commit();
                           }
                           catch
                           {
                               transaction.Rollback();
                               throw;
                           }
                       }
                   }
                   return null;
               };
        }

        public static object GetScriptsToRun(IDBUpdaterInteractionContext context)
        {
            var runned = new List<string>();
            var version = 0;
            var files = new List<string>(Directory.GetFiles(context.DBScriptsPath, "*.sql"));
            using (var c = new SqlCommand("select * from sysDBVersion", context.Connection))
            {
                using (var r = c.LexExecuteReader())
                {
                    while (r.Read())
                    {
                        version = Math.Max(r.GetInt32(0), version);
                        runned.Add(r.GetString(1));
                    }
                }
            }

            for (int i = 0; i < files.Count; i++)
            {
                files[i] = Path.GetFileNameWithoutExtension(files[i]);
            }

            foreach (var r in runned)
            {
                var ind = files.FindIndex(sc => sc.EndsWith(r));
                if (ind >= 0)
                {
                    files.RemoveAt(ind);
                }
            }
            return new KeyValuePair<IList<string>, IList<string>>(runned, files);
        }

        public static int ExtractScriptVersion(string fileName)
        {
            var m = _UpdateScriptVersion.Match(fileName);
            if (m.Success)
            {
                return int.Parse(m.Groups["number"].Value);
            }
            else
            {
                throw new InvalidOperationException("Invalid update script file name: " + fileName);
            }
        }

        private static SqlConnection GetNewConnectionWithoutDBRefference(DbConnection basedOn)
        {
            return new SqlConnection(new SqlConnectionStringBuilder
                 {
                     DataSource = basedOn.DataSource,
                     IntegratedSecurity = true
                 }.ToString());
        }

        private static bool IsDBVersionExists(IDBUpdaterInteractionContext f)
        {
            using (var c = new SqlCommand("select * from sys.tables where name = 'sysDBVersion'", f.Connection))
            {
                using (var r = c.LexExecuteReader())
                {
                    return r.Read();
                }
            }
        }

        private static object CreateSysDbVersionTable(IDBUpdaterInteractionContext f)
        {
            using (var c = new SqlCommand(CreateSysDbVersionTableScript, f.Connection))
            {
                c.LexExecuteNonQuery();

                c.CommandText = CreateSysDbVersionTriggerScript;
                c.LexExecuteNonQuery();

                c.CommandText = GetDBVersionProcSql(0, false);
                c.LexExecuteNonQuery();

                c.CommandText = CreateUpgradeDBProcedure;
                c.LexExecuteNonQuery();
            }
            return true;
        }

        private static IEnumerable<string> SplitSqlCommands(string cmds)
        {
            var result = new List<string>();

            var gos = _SplitSqlCommands.Matches(cmds);
            var prevIndex = 0;
            foreach (Match m in gos)
            {
                var cmd = cmds.Substring(prevIndex, m.Index - prevIndex);
                result.Add(cmd);
                prevIndex = m.Index + 4;
            }
            var lastCmd = cmds.Substring(prevIndex);
            if (lastCmd.EndsWith("GO", StringComparison.CurrentCultureIgnoreCase))
            {
                lastCmd = lastCmd.Remove(lastCmd.Length - 2);
            }
            result.Add(lastCmd);

            return result;
        }

        private static readonly Regex _SplitSqlCommands = new Regex(@"\sGO\s", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

        private static readonly Regex _UpdateScriptVersion = new Regex(@"(?<number>\d+)\.\W*", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        private const string CreateSysDbVersionTableScript = @"CREATE TABLE sysDBVersion
(
	VersionNumber INT NOT NULL UNIQUE,
	ScriptFileName nvarchar(255) NOT NULL,
	[Date] datetime NOT NULL
)";
        private const string CreateSysDbVersionTriggerScript = @"CREATE TRIGGER sysDBVersion_ADD_ONLY
ON sysDBVersion 
INSTEAD OF DELETE, UPDATE
AS
	RAISERROR('sysDBVersion table is add-only table', 16, 16)";

        private const string CreateUpgradeDBProcedure = @"CREATE PROCEDURE UpgradeDB
	@version INT,
	@script NVARCHAR(MAX)
AS
BEGIN
	IF dbo.GetDBVersion() <> @version
		RAISERROR ('Incorrect version of database', 1, 1);
    
	EXEC sp_sqlexec @script;
END";
    }

    public class InvalidDBVersionException : Exception
    {
        public int Version { get; private set; }

        public InvalidDBVersionException(int version)
        {
            Version = version;
        }
    }
}