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
using IUDICO.DBManager;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using NUnit.Framework;

namespace IUDICO.UnitTest.Base
{
    public class TestFixture
    {
        [TestFixtureSetUp]
        protected virtual void InitializeFixture()
        {
        }

        [TestFixtureTearDown]
        protected virtual void FinializeFixture()
        {
        }

        protected static void AreEqual(DateTime a, DateTime b)
        {
            Assert.AreEqual(a.AddTicks(-a.Ticks), b.AddTicks(-b.Ticks));
        }

        protected static void AreEqual(DateTime? a, DateTime? b)
        {
            if (a != null && b != null)
            {
                AreEqual(a.Value, b.Value);
            }
            else
            {
                Assert.AreEqual(a, b);
            }
        }

    }

    public class TestFixtureDB : TestFixture
    {
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

        #region NUnitDBInteractionContext

        private class NUnitDBInteractionContext : IDBUpdaterInteractionContext
        {
            static NUnitDBInteractionContext()
            {
                _ScriptsPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))), "DBScripts");
            }

            public NUnitDBInteractionContext(TestFixtureDB fixture)
            {
                _Fixture = fixture;
            }

            #region Implementation of IAsyncExecuterContext

            public IVariable<bool> EnableUserActions
            {
                get { return _EnableUserActions; }
            }

            public void AsyncOperationsCompleted()
            {
            }

            public void AsyncOperationBegins(string title)
            {
                Debug.WriteLine("Star operation: " + title);
            }

            public void AsyncError(string error)
            {
                throw new InvalidOperationException(error);
            }

            #endregion

            #region Implementation of IDBUpdaterInteractionContext

            public string DBName
            {
                get { return _Fixture._DataBaseName; }
            }

            public SqlConnection Connection
            {
                get { return _Fixture._Connection; }
            }

            public string DBScriptsPath
            {
                get { return _ScriptsPath; }
            }

            #endregion

            private readonly IVariable<bool> _EnableUserActions = true.AsVariable();
            private readonly TestFixtureDB _Fixture;
            private static readonly string _ScriptsPath;
        }

        #endregion

        public TestFixtureDB()
        {
            var cB = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["IUDICO_TEST"].ConnectionString);
            _DataBaseName = cB.InitialCatalog;
            var cB2 = new SqlConnectionStringBuilder
                 {
                     DataSource = cB.DataSource,
                     IntegratedSecurity = cB.IntegratedSecurity
                 };
            if (cB.UserID.IsNotNull() && cB.Password.IsNotNull())
            {
                cB2.UserID = cB.UserID;
                cB2.Password = cB.Password;
            }

            _Connection = new SqlConnection(cB2.ToString());
            _Connection.Open();
        }

        protected void CreateTestDataBase()
        {
            DBUpdateManager.CreateDataBase(new NUnitDBInteractionContext(this));
        }

        protected void DropTestDataBase()
        {
            DBUpdateManager.DropDataBase(new NUnitDBInteractionContext(this));
        }

        protected override void InitializeFixture()
        {
            var context = new NUnitDBInteractionContext(this);
            var dbExists = (bool) DBUpdateManager.IsDatabaseExists(_DataBaseName)(context);
            if (NeedToRecreateDB || !dbExists)
            {
                if (dbExists)
                {
                    Debug.WriteLine(string.Format("Deleting database '{0}'...", context.DBName));
                    DropTestDataBase();
                }
                Debug.WriteLine(string.Format("Creating database '{0}'...", context.DBName));
                CreateTestDataBase();
            }
            else
            {
                _Connection.Close();
                _Connection.ConnectionString = new SqlConnectionStringBuilder(_Connection.ConnectionString)
                   {
                       InitialCatalog = _DataBaseName
                   }.ToString();
                _Connection.Open();
            }

            var scriptsToRun = ((KeyValuePair<IList<string>, IList<string>>)DBUpdateManager.GetScriptsToRun(context)).Value;
            Debug.Write("Scripts to run:" + scriptsToRun.ConcatComma());
            foreach (var s in scriptsToRun)
            {
                var updateAction = DBUpdateManager.UpgrateToNextVersion(
                    DBUpdateManager.ExtractScriptVersion(s),
                    File.ReadAllText(Path.Combine(context.DBScriptsPath,
                    s + ".sql")), s);
                Debug.WriteLine(string.Format("Running script '{0}'...", s));
                updateAction(context);
            }
            ServerModel.Initialize(Connection.ConnectionString, HttpRuntime.Cache);
        }

        protected override void FinializeFixture()
        {
            ServerModel.UnInitialize();

            if (NeedToRecreateDB)
            {
                DropTestDataBase();
            }
        }

        protected virtual bool NeedToRecreateDB { get { return true; } }

        private readonly SqlConnection _Connection;
        private readonly string _DataBaseName;
    }
}