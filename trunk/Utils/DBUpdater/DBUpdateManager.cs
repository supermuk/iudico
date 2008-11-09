using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LEX.CONTROLS;

namespace DBUpdater
{
    public interface IDBUpdaterUserInteractionContext
    {
        void AsyncLog(string message);
        void AsyncErrorMessage(string error);
        void AsyncEnQueueOperation(Action<IDBUpdaterUserInteractionContext> action, string title);
        KeyValuePair<Action<IDBUpdaterUserInteractionContext>, string> AsyncDeQueueOperation();
        void AsyncOperationCompleted();
        void AsyncOperationBegins(string title);
        void AsyncSetScripts(IEnumerable<string> runnedScripts, IEnumerable<string> scriptsToRun);
        string DBName { get; }
        SqlConnection Connection { get; }
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

        public static void EnsureDBVersionExists(IDBUpdaterUserInteractionContext view)
        {
            if (!IsDBVersionExists(view))
            {
                view.AsyncLog("DB doesn't containe sysDBVersion table. Creating...");
                CreateSysDbVersionTable(view);
                view.AsyncLog("sysDBVersion table has been created");
            }
        }

        public static void ReCreateDataBase(IDBUpdaterUserInteractionContext view)
        {
            try
            {
                var dbname = view.DBName;

                view.Connection.Close();
                SqlConnection.ClearAllPools();

                using (var newConnection = new SqlConnection(new SqlConnectionStringBuilder
                {
                    DataSource = view.Connection.DataSource,
                    IntegratedSecurity = true
                }.ToString()))
                {
                    newConnection.Open();

                    using (var c = new SqlCommand(GetDBUsageSql(dbname), newConnection))
                    {
                        var ok = true;
                        var sb = new StringBuilder("Database is using by following clients:");
                        sb.AppendLine();
                        using (var r = c.ExecuteReader())
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
                                return;
                        }
                    }

                    using (var c = new SqlCommand(string.Format("DROP DATABASE [{0}]", dbname), newConnection))
                    {
                        c.LexExecuteNonQuery();

                        c.CommandText = string.Format("CREATE DATABASE [{0}]", dbname);
                        c.LexExecuteNonQuery();
                    }
                }

                view.Connection.Open();

                CreateSysDbVersionTable(view);
            }
            finally
            {
                if (view.Connection.State != ConnectionState.Open)
                {
                    view.Connection.Open();
                }
                view.AsyncEnQueueOperation(GetScriptsToRun, "Getting scripts to run...");
            }
        }

        public static Action<IDBUpdaterUserInteractionContext> UpgrateToNextVersion(int dbVersion, string sqlscript, string scriptFileName)
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
                            cmd.ExecuteScalar();

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
            };
        }

        public static void GetScriptsToRun(IDBUpdaterUserInteractionContext view)
        {
            var runned = new List<string>();
            var version = 0;
            var files = new List<string>(Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.sql"));
            using (var c = new SqlCommand("select * from sysDBVersion", view.Connection))
            {
                using (var r = c.ExecuteReader())
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
            view.AsyncSetScripts(runned, files);
        }

        private static bool IsDBVersionExists(IDBUpdaterUserInteractionContext f)
        {
            using (var c = new SqlCommand("select * from sys.tables where name = 'sysDBVersion'", f.Connection))
            {
                using (var r = c.ExecuteReader())
                {
                    return r.Read();
                }
            }
        }

        private static void CreateSysDbVersionTable(IDBUpdaterUserInteractionContext f)
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
