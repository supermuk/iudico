using System.Data.SqlClient;
using System.Web.Configuration;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel
{
    public static class ServerModel
    {
        public static DatabaseModel DB = new DatabaseModel(AcquireConnection());

        public static SqlConnection AcquireConnection()
        {
            return new SqlConnection(WebConfigurationManager.ConnectionStrings["IUDICO"].ConnectionString);
        }

        public static SqlConnection AcruireOpenedConnection()
        {
            var res = AcquireConnection();
            res.Open();
            return res;
        }
    }
}
