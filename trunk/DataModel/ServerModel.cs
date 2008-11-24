using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel
{
    public static class ServerModel
    {
        public static void Initialize(string connectionString)
        {
            using (Logger.Scope("Initializing ServerModel..."))
            {
                _ConnectionString = connectionString;
                DB = new DatabaseModel(AcruireOpenedConnection());
                DB.Initialize();
                PermissionsManager.Initialize(DB.GetConnectionSafe());
            }
        }

        public static void UnInitialize()
        {
        }

        public static DatabaseModel DB;

        public static UserModel User = new UserModel();

        public static SqlConnection AcquireConnection()
        {
            return new SqlConnection(_ConnectionString);
        }

        public static SqlConnection AcruireOpenedConnection()
        {
            var res = AcquireConnection();
            res.Open();
            return res;
        }

        private static string _ConnectionString;
    }

    public sealed class UserModel
    {
        public void Create(string login, string password, string email)
        {
            ServerModel.DB.Insert(new TblUsers
            {
                LastName = @login,
                Login = @login,
                PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"),
                Email = email
            });
        }

        public CustomUser ByLogin(string login)
        {
            CustomUser res;
            if (HttpRuntime.Cache.TryGet(login, out res))
                return res;

            int fID;
            string fName, lName, pHash, email;
            List<string> roles;
            using (var cn = ServerModel.AcruireOpenedConnection())
            {
                var cmd = new SqlCommand(@"SELECT ID, FirstName, LastName, PasswordHash, Email 
FROM tblUsers WHERE Login = @Login", cn);
                cmd.Parameters.Assign(new { Login = login });
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        fID = r.GetInt32(0);
                        fName = r.GetStringNull(1);
                        lName = r.GetStringNull(2);
                        pHash = r.GetStringNull(3);
                        email = r.GetStringNull(4);
                    }
                    else
                    {
                        return null;
                    }
                }
                cmd.CommandText = "GetUserRoles";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Assign(new { UserLogin = login });
                roles = cmd.FullReadStrings();
            }
            res = new CustomUser(fID, fName, lName, login, pHash, email, roles);
            DoCache(res);
            return res;
        }

        public CustomUser ByEmail(string email)
        {
            CustomUser res = CachedUsers.Find(u => u.Email == email);
            if (res != null)
            {
                return res;
            }

            int fID;
            string fName, lName, pHash, login;
            List<string> roles;
            using (var cn = ServerModel.AcruireOpenedConnection())
            {
                var cmd = new SqlCommand(@"SELECT ID, FirstName, LastName, PasswordHash, Login 
FROM tblUsers WHERE Email = @Email", cn);
                cmd.Parameters.Assign(new { Email = email });
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        fID = r.GetInt32(0);
                        fName = r.GetStringNull(1);
                        lName = r.GetStringNull(2);
                        pHash = r.GetStringNull(3);
                        login = r.GetStringNull(4);
                    }
                    else
                    {
                        return null;
                    }
                }
                cmd.CommandText = "GetUserRoles";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Assign(new { UserLogin = login });
                roles = cmd.FullReadStrings();
            }
            res = new CustomUser(fID, fName, lName, login, pHash, email, roles);
            DoCache(res);
            return res;
        }

        private void DoCache(CustomUser u)
        {
            lock (this)
            {
                HttpRuntime.Cache.Add(u, u.Login, CacheItemPriority.High, OnRemovedFromCache);
                CachedUsers.Add(u);
            }
        }

        private void OnRemovedFromCache(string key, object value, CacheItemRemovedReason reason)
        {
            string ck = CacheUtility.GetKey<CustomUser>(key);
            lock (this)
            {
                var ind = CachedUsers.FindIndex(u => u.Login == ck);
                if (ind >= 0)
                {
                    CachedUsers.RemoveAt(ind);
                }
            }
        }

        private readonly List<CustomUser> CachedUsers = new List<CustomUser>();
    }
}
