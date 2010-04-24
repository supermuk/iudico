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
using LEX.CONTROLS.DBUpdater;

namespace IUDICO.UnitTest.Base
{
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
        
        public TestFixtureDB()
        {
            /*
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
            */
            
            _Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IUDICO_TEST"].ConnectionString);
            _Connection.Open();
        }

        protected override void InitializeFixture()
        {
            ServerModel.Initialize(_Connection.ConnectionString, HttpRuntime.Cache);

            if (!ServerModel.DB.DatabaseExists())
                ServerModel.DB.CreateDatabase();
        }

        protected override void FinializeFixture()
        {
            ServerModel.UnInitialize();

            if (NeedToRecreateDB)
            {
                ServerModel.DB.DeleteDatabase();
            }
        }

        protected virtual bool NeedToRecreateDB
        {
            get
            {
                return false;
            }
        }

        private readonly SqlConnection _Connection;
        //private readonly string _DataBaseName;
    }
}