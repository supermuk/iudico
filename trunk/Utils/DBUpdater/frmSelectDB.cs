using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace DBUpdater
{
    public partial class frmSelectDB : Form
    {
        public frmSelectDB()
        {
            InitializeComponent();

            btnOk.RegisterRememberEditControl(tbServerInstance);
            btnOk.RegisterRememberEditControl(tbDBName);
            btnOk.RegisterRememberEditControl(tbUserName);
            tbServerInstance.Validate = RememberTextBox.RequiredTextValidator;
            tbDBName.Validate = RememberTextBox.RequiredTextValidator;

            cbWindowsAutentication.Not().AssignAsEnabledValue(tbUserName, tbPassword);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (PerformSelectDataBase())
                Hide();
        }

        #region Logic

        private bool PerformSelectDataBase()
        {
            string serverInstance = tbServerInstance.Text.Trim();
            string dbName = tbDBName.Text.Trim();

            UseWaitCursor = true;
            bool disposeConnetion = true;
            var s = new SqlConnection(new SqlConnectionStringBuilder
            {
                DataSource = serverInstance,
                IntegratedSecurity = true,
            }.ToString());
            var Dbs = new List<string>();
            try
            {
                s.Open();
                var c = new SqlCommand("EXEC sp_databases", s);
                using (var r = c.LexExecuteReader())
                {
                    while (r.Read())
                    {
                        Dbs.Add(r.GetString(0));
                    }
                }

                if (Dbs.IndexOf(dbName) < 0)
                {
                    if (MessageBox.Show(this, string.Format("DataBase '{0}' was not found in {1}. Do you want to create it?", dbName, serverInstance), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        c.CommandText = @"CREATE DATABASE [" + dbName + "]";
                        c.LexExecuteNonQuery();
                    }
                    else
                    {
                        return false;
                    }
                }

                s.Close();
                s.ConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = serverInstance,
                    IntegratedSecurity = true,
                    InitialCatalog = dbName
                }.ToString();
                s.Open();

                disposeConnetion = false;
                var f = new frmDBStatus(s, this, dbName);
                f.Show(this);
                return true;
            }
            catch (SqlException se)
            {
                MessageBox.Show(this, se.Message, string.Format("Error on openning '{0}'", serverInstance), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                UseWaitCursor = false;
                if (disposeConnetion)
                {
                    s.Dispose();
                }
            }
        }

        #endregion

    }
}
