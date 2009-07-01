using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using IUDICO.DBManager;
using LEX.CONTROLS;

namespace DBUpdater
{
    public partial class frmDBConsole : Form
    {
        public frmDBConsole()
        {
            InitializeComponent();
        }

        public string ConnectionString { get; set; }

        private void llRun_Click(object sender, EventArgs e)
        {
            rtbResult.Enabled = rtbSqlCommands.Enabled = llRun.Enabled = false;
            UseWaitCursor = true;
            try
            {
                var cmds = DBUpdateManager.SplitSqlCommands(rtbSqlCommands.Text);
                var result = new StringBuilder();
                using (var cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        foreach (var c in cmds)
                        {
                            cmd.CommandText = c;
                            using (var r = cmd.LexExecuteReader())
                            {
                                do
                                {
                                    ProcessReader(r, result);
                                    result.AppendLine();
                                    result.AppendLine();
                                } while (r.NextResult());
                            }
                        }
                    }
                }
                rtbResult.Text = result.ToString();
            }
            catch(Exception ex)
            {
                rtbResult.Text = ex.Message;
            }
            finally
            {
                rtbResult.Enabled = rtbSqlCommands.Enabled = llRun.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private static void ProcessReader(IDataReader r, StringBuilder result)
        {
            if (r.RecordsAffected >= 0)
            {
                result.AppendLine("Records affected: " + r.RecordsAffected);
                result.AppendLine();
            }
            else
            {
                for (var i = 0; i < r.FieldCount; ++i)
                {
                    result.Append('[');
                    result.Append(r.GetName(i));
                    result.Append("] ");
                }
                result.AppendLine();

                while (r.Read())
                {
                    for (var i = 0; i < r.FieldCount; ++i)
                    {
                        result.Append(r.GetValue(i));
                        result.Append(' ');
                    }
                    result.AppendLine();
                }
            }
            
        }
    }
}
