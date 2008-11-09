using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using LEX.CONTROLS;
using System.IO;

namespace DBUpdater
{
    public partial class frmDBStatus : Form, IDBUpdaterUserInteractionContext
    {
        public frmDBStatus()
        {
            InitializeComponent();

            ListViewScripts_Resize(lvScripts, null);
            ListViewScripts_Resize(lvRunned, null);

            EnableUserActions.AssignAsEnabledValue(btnOk, btnRecreateDataBase, lvScripts, tabControl1);

            _Thread.Start(this);
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

        public void AsyncLog(string message)
        {
            Invoke((Action<string>)(a =>
            {

            }), message);
        }

        public void AsyncErrorMessage(string error)
        {
            Func<string, string, MessageBoxButtons, MessageBoxIcon, DialogResult> v = MessageBox.Show;
            Invoke(v, error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AsyncEnQueueOperation(Action<IDBUpdaterUserInteractionContext> action, string title)
        {
            lock (this)
            {
                EnableUserActions.Value = false;

                _OperationQueue.Enqueue(new KeyValuePair<Action<IDBUpdaterUserInteractionContext>, string>(action, title));
                _OperationSemaphore.Release();
            }
        }

        public KeyValuePair<Action<IDBUpdaterUserInteractionContext>, string> AsyncDeQueueOperation()
        {
            lock (this)
            {
                return _OperationQueue.Dequeue();
            }
        }

        public void AsyncOperationCompleted()
        {
            if (_OperationQueue.Count == 0)
            {
                Invoke((Action)(() =>
                {
                    EnableUserActions.Value = true;
                    pbOperation.Visible = false;
                    tbOperationLabel.Text = string.Empty;
                }));
            }
        }

        public void AsyncOperationBegins(string title)
        {
            Invoke((Action<string>)(t =>
            {
                EnableUserActions.Value = false;
                tbOperationLabel.Text = t;
                pbOperation.Visible = true;
            }), title);
        }

        public void AsyncSetScripts(IEnumerable<string> runnedScripts, IEnumerable<string> scriptsToRun)
        {
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            AsyncEnQueueOperation(DBUpdateManager.EnsureDBVersionExists, "Checking DBVersion table...");
            AsyncEnQueueOperation(DBUpdateManager.GetScriptsToRun, "Calculating scripts to run...");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _Thread.Abort();
            _Connection.Dispose();
            Application.Exit();
        }

        private static void ProcessAsyncOperations(object ownerForm)
        {
            var f = (frmDBStatus)ownerForm;
            var w = f._OperationSemaphore;
            while (true)
            {
                w.WaitOne();

                Thread.BeginCriticalRegion();
                try
                {
                    if (f._OperationQueue.Count > 0)
                    {
                        var op = f.AsyncDeQueueOperation();
                        try
                        {
                            f.AsyncOperationBegins(op.Value);
                            op.Key(f);
                        }
                        finally
                        {
                            f.AsyncOperationCompleted();
                        }
                    }
                }
                catch (SqlCommandException e)
                {
                    f._OperationQueue.Clear();
                    SqlCommandExceptionDetails.Show(e);
                }
                catch (InvalidDBVersionException e)
                {
                    f._OperationQueue.Clear();
                    f.AsyncErrorMessage(string.Format("Invalid DB Version: {0}. Cannot run script!!!", e.Version));
                }
                catch (Exception e)
                {
                    f._OperationQueue.Clear();
                    f.AsyncErrorMessage(e.Message);
                }
                finally
                {
                    Thread.EndCriticalRegion();
                    f.AsyncOperationCompleted();
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var path = Path.GetDirectoryName(Application.ExecutablePath);
            var rx = new Regex(@"(?<number>\d+)\.\W*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (ListViewItem li in lvScripts.CheckedItems)
            {
                var t = li.Text;
                var m = rx.Match(t);
                if (m.Success)
                {
                    var version = int.Parse(m.Groups["number"].Value);
                    AsyncEnQueueOperation(
                        DBUpdateManager.UpgrateToNextVersion(version,
                            File.ReadAllText(Path.Combine(path, t + ".sql")), t),
                        string.Format("Updating to version {0}...", version));
                }
                else
                {
                    MessageBox.Show(this, string.Format("Invalid script name '{0}'. Script MUST by in the <number>.comment>.sql format!", t), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            AsyncEnQueueOperation(DBUpdateManager.GetScriptsToRun, "Updating DB State...");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRecreateDataBase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "All data will be lost. Are you realy want to recreate database " + _DBName + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AsyncEnQueueOperation(DBUpdateManager.ReCreateDataBase, "Recreating database...");
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

        private readonly IVariable<bool> EnableUserActions = true.AsVariable();
        private readonly SqlConnection _Connection;
        private readonly frmSelectDB _OwnerForm;
        private readonly string _DBName;
        private readonly Thread _Thread = new Thread(ProcessAsyncOperations);
        private readonly Semaphore _OperationSemaphore = new Semaphore(0, int.MaxValue);
        private readonly Queue<KeyValuePair<Action<IDBUpdaterUserInteractionContext>, string>> _OperationQueue = new Queue<KeyValuePair<Action<IDBUpdaterUserInteractionContext>, string>>();

    }
}