using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBUpdater
{
    public partial class frmConnectionString : Form
    {
        public string ConnectionString
        {
            get
            {
                return _builder.ConnectionString;
            }
        }

        public frmConnectionString()
        {
            InitializeComponent();

            _builder = new SqlConnectionStringBuilder();
            pgConnection.SelectedObject = _builder;
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                _builder.ConnectionString = textBox1.Text;
                pgConnection.Refresh();
            }
            catch
            {
            }
        }

        private void pgConnection_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            textBox1.Text = _builder.ConnectionString;
        }

        private readonly SqlConnectionStringBuilder _builder;
    }
}
