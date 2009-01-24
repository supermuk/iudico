using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;
using IUDICO.DataModel.Common;
using System.Collections;
using System.Linq;

namespace IUDICO.DataModel
{
    public static class ServerModel
    {
        public static void Initialize(string connectionString, Cache cache)
        {
            using (Logger.Scope("Initializing ServerModel..."))
            {
                _ConnectionString = connectionString;
                DB = new DatabaseModel(AcruireOpenedConnection());
                DB.Initialize(cache);
                PermissionsManager.Initialize(DB.GetConnectionSafe());
                DBKeyValueAttributeInitialize();
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

        private static void DBKeyValueAttributeInitialize()
        {
            foreach(var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.GetInterface(typeof(IFxDataObject).Name) != null)
                {
                    var fields = new List<FieldInfo>(t.GetFields(BindingFlags.Static | BindingFlags.SetField | BindingFlags.Public).Where(f => f.HasAtr<TableRecordAttribute>()));
                    if (fields.Count > 0)
                    {
                        var items = (IEnumerable) DatabaseModel.FIXED_METHOD.MakeGenericMethod(new[] {t}).Invoke(DB, Type.EmptyTypes);
                        foreach (var f in fields)
                        {
                            bool found = false;
                            foreach (IFxDataObject i in items)
                            {
                                if (i.Name == f.Name)
                                {
                                    found = true;
                                    if (f.FieldType == typeof(int))
                                    {
                                        f.SetValue(null, i.ID);
                                    }
                                    else
                                    {
                                        f.SetValue(null, i);
                                    }
                                    break;
                                }
                            }
                            if (!found)
                            {
                                throw new DMError("Couldn't found DB value for {0}.{1} marked with {2}", t.Name, f.Name, typeof(TableRecordAttribute).Name);
                            }
                        }
                    }
                }
            }
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

        public CustomUser Current
        {
            get
            {
                return (CustomUser) Membership.GetUser();
            }
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
