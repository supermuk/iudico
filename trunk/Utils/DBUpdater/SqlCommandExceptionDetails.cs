using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DBUpdater
{
    public partial class SqlCommandExceptionDetails : Form
    {
        public static void Show(SqlCommandException e)
        {
            var f = new SqlCommandExceptionDetails();
            Utils.RegisterAutoDisposableForm(f);
            f.Initialize(e);
            f.ShowDialog();
        }

        public SqlCommandExceptionDetails()
        {
            InitializeComponent();
        }

        public void Initialize(SqlCommandException e)
        {
            tbSql.Text = e.Sql;
            // rander errors
            tbErrors.Clear();
            var b = new StringBuilder();
            foreach (SqlError er in e.SqlException.Errors)
            {
                b.AppendFormat("{0}: {1}", er.LineNumber, er.Message);
                b.AppendLine();
            }
            tbErrors.Text = b.ToString();
        }
    }
}
