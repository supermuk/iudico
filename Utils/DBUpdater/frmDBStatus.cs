using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using IUDICO.DBManager;

namespace DBUpdater
{
    public partial class frmDBStatus : Form, IDBUpdaterInteractionContext
    {
        static frmDBStatus()
        {
            _DBScriptsPath = Path.GetDirectoryName(Application.ExecutablePath);
        }

        public frmDBStatus()
        {
            InitializeComponent();

            _Executer = new AsyncSingleThreadExecuter<IDBUpdaterInteractionContext>(this);

            ListViewScripts_Resize(lvScripts, null);
            ListViewScripts_Resize(lvRunned, null);

            _EnableUserActions.AssignAsEnabledValue(btnOk, btnRecreateDataBase, lvScripts, tabControl1);
        }

        public frmDBStatus([NotNull]SqlConnection connection, [NotNull] frmSelectDB ownerForm, [NotNull] string dbName)
            : this()
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            _Connection = connection;
            _OwnerForm = ownerForm;
            _DBName = dbName;
        }

        public frmSelectDB OwnerForm
        {
            get { return _OwnerForm; }
        }

        public void AsyncMessage(string message)
        {
            Invoke(MESSAGE_BOX, message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void AsyncError(string error)
        {
            Invoke(MESSAGE_BOX, error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AsyncOperationsCompleted()
        {
            Invoke((Action)(() =>
            {
                _EnableUserActions.Value = true;
                pbOperation.Visible = false;
                tbOperationLabel.Text = string.Empty;
            }));
        }

        public void AsyncOperationBegins(string title)
        {
            Invoke((Action<string>)(t =>
            {
                _EnableUserActions.Value = false;
                tbOperationLabel.Text = t;
                pbOperation.Visible = true;
            }), title);
        }

        public void AsyncSetScripts(IDBUpdaterInteractionContext context, object result)
        {
            var r = (KeyValuePair<IList<string>, IList<string>>) result;
            var runnedScripts = r.Key;
            var scriptsToRun = r.Value;

            Invoke((Action<frmDBStatus, List<string>, List<string>>)((v, rn, fs) =>
            {
                FillListView(v.lvScripts, fs, true);
                FillListView(v.lvRunned, rn, false);

            }), this, runnedScripts, scriptsToRun);
        }

        public string DBName
        {
            get { return _DBName; }
        }

        public SqlConnection Connection
        {
            get { return _Connection; }
        }

        public string DBScriptsPath
        {
            get { return _DBScriptsPath; }
        }

        public IVariable<bool> EnableUserActions { get { return _EnableUserActions; } }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _Executer.AsyncEnQueueOperation(DBUpdateManager.EnsureDBVersionExists, "Checking DBVersion table...");
            _Executer.AsyncEnQueueOperation(DBUpdateManager.GetScriptsToRun, "Calculating scripts to run...", AsyncSetScripts);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _Executer.Dispose();
            _Connection.Dispose();
            Application.Exit();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var path = Path.GetDirectoryName(Application.ExecutablePath);
            
            foreach (ListViewItem li in lvScripts.CheckedItems)
            {
                var t = li.Text;
                var version = DBUpdateManager.ExtractScriptVersion(t);
                _Executer.AsyncEnQueueOperation(
                    DBUpdateManager.UpgrateToNextVersion(version,
                        File.ReadAllText(Path.Combine(path, t + ".sql")), t),
                    string.Format("Updating to version {0}...", version));
            }
            _Executer.AsyncEnQueueOperation(DBUpdateManager.GetScriptsToRun, "Updating DB State...", AsyncSetScripts);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRecreateDataBase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "All data will be lost. Are you realy want to recreate database " + _DBName + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _Executer.AsyncEnQueueOperation(DBUpdateManager.DropDataBase, "Droping database...");
                _Executer.AsyncEnQueueOperation(DBUpdateManager.CreateDataBase, "Creating database...");
                _Executer.AsyncEnQueueOperation(DBUpdateManager.GetScriptsToRun, "Getting scripts to run...", AsyncSetScripts);
            }
        }

        private static void FillListView(ListView lv, IEnumerable<string> items, bool checkThem)
        {
            lv.BeginUpdate();
            try
            {
                lv.Items.Clear();

                foreach (var i in items)
                {
                    ListViewItem item = lv.Items.Add(i, i);
                    if (checkThem)
                    {
                        item.Checked = true;
                    }
                }
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        private void ListViewScripts_Resize(object sender, EventArgs e)
        {
            var lv = (ListView)sender;
            lv.Columns[0].Width = lv.ClientSize.Width;
        }

        private readonly IVariable<bool> _EnableUserActions = true.AsVariable();
        private readonly SqlConnection _Connection;
        private readonly frmSelectDB _OwnerForm;
        private readonly string _DBName;
        private readonly AsyncSingleThreadExecuter<IDBUpdaterInteractionContext> _Executer;
        private static readonly Func<string, string, MessageBoxButtons, MessageBoxIcon, DialogResult> MESSAGE_BOX = MessageBox.Show;
        private static readonly string _DBScriptsPath;
    }
}