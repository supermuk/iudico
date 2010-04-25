using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using NUnit.Framework;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace IUDICO.UnitTest.Base
{
    public class TestFixtureDB : TestFixture
    {
        private readonly SqlConnection _Connection;
        private readonly string _DataBaseName;
        private readonly DatabaseModel dm;
        private readonly string _ScriptsPath;

        protected virtual bool NeedToRecreateDB { get { return false; } }
        protected SqlConnection Connection
        {
            get
            {
                if (_Connection == null || _Connection.State != ConnectionState.Open)
                {
                    throw new InvalidOperationException("Connection is not ready");
                }
                return _Connection;
            }
        }

        protected void CreateTestDataBase()
        {
            dm.CreateDatabase();
            Server server = new Server(new ServerConnection(_Connection));
            
            List<string> scriptsToRun = new List<string>(Directory.GetFiles(_ScriptsPath, "*.sql"));
            foreach (var s in scriptsToRun)
            {
                string sqlText = File.ReadAllText(Path.Combine(_ScriptsPath, s));
                server.ConnectionContext.ExecuteNonQuery(sqlText);

                Debug.WriteLine(string.Format("Running script '{0}'...", s));
            }
        }

        protected void DropTestDataBase()
        {
            dm.DeleteDatabase();
        }

        protected bool DatabaseExists()
        {
            return dm.DatabaseExists();
        }

        public TestFixtureDB()
        {
            var cB = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["IUDICO_TEST"].ConnectionString);
            _DataBaseName = cB.InitialCatalog;
            var cB2 = new SqlConnectionStringBuilder
            {
                DataSource = cB.DataSource,
                IntegratedSecurity = cB.IntegratedSecurity
            };
            if (cB.UserID != null && cB.Password != null)
            {
                cB2.UserID = cB.UserID;
                cB2.Password = cB.Password;
            }

            _ScriptsPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))), "DBScripts");
            _Connection = new SqlConnection(cB2.ToString());

            dm = new TestDatabaseModel(_Connection);
        }

        protected override void InitializeFixture()
        {
            if (!DatabaseExists())
            {
                Debug.WriteLine(string.Format("Creating database '{0}'...", _DataBaseName));
                CreateTestDataBase();
            }

            _Connection.Close();
            _Connection.ConnectionString = new SqlConnectionStringBuilder(_Connection.ConnectionString)
            {
                InitialCatalog = _DataBaseName
            }.ToString();
            _Connection.Open();

            ServerModel.Initialize(Connection.ConnectionString, HttpRuntime.Cache);
        }

        protected override void FinializeFixture()
        {
            ServerModel.UnInitialize();
            _Connection.Close();

            if (NeedToRecreateDB)
            {
                DropTestDataBase();
            }
        }
    }
}