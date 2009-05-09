using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;
using Extenders=LEX.CONTROLS.Extenders;
using Utils=IUDICO.DataModel.Common.Utils;

namespace IUDICO.DataModel
{
    public static class ServerModel
    {
        public static void Initialize(string connectionString, Cache cache)
        {
            using (Logger.Scope("Initializing ServerModel..."))
            {
                _ConnectionString = connectionString;
                (DB = new DatabaseModel(AcruireOpenedConnection())).Initialize(cache);
                PermissionsManager.Initialize();
                TableRecordAttributeInitialize();
            }
        }

        public static void UnInitialize()
        {
            if (DB != null)
            {
                DB.Dispose();
                DB = null;
            }
        }

        public static DatabaseModel DB;

        public static UserModel User = new UserModel();

        public static FormsModel Forms = new FormsModel();

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

        private static void TableRecordAttributeInitialize()
        {
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.GetInterface(typeof(IFxDataObject).Name) != null)
                {
                    var fields = new List<FieldInfo>(t.GetFields(BindingFlags.Static | BindingFlags.SetField | BindingFlags.Public).Where(Utils.HasAtr<TableRecordAttribute>));
                    if (fields.Count > 0)
                    {
                        var items = (IEnumerable)DatabaseModel.FIXED_METHOD.MakeGenericMethod(new[] { t }).Invoke(DB, Type.EmptyTypes);
                        foreach (var f in fields)
                        {
                            var atr = f.GetAtr<TableRecordAttribute>();
                            bool found = false;
                            foreach (IFxDataObject i in items)
                            {
                                if (i.Name == Extenders.IsNull(atr.Name, f.Name))
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
                PasswordHash = ServerModel.User.GetPasswordHash(password),
                Email = email
            });
        }
        
        [CanBeNull]
        public CustomUser Current
        {
            get
            {
                var mu = (CustomUser) Membership.GetUser();
                return mu != null ? ByLogin(mu.Login) : null; // To get the latest version

            }
        }

        public CustomUser ByLogin(string login)
        {
            //            CustomUser res;
            // TODO: Fix User caching
            //            if (HttpRuntime.Cache.TryGet(login, out res))
            //                return res;

            var users = ServerModel.DB.Query<TblUsers>(
                new CompareCondition<string>(
                    new PropertyCondition<string>("Login"),
                    new ValueCondition<string>(login),
                    COMPARE_KIND.EQUAL));

            if (users.Count == 0)
                return null;

            return CreateUser(users[0]);
            //            DoCache(res);
        }

        public CustomUser ByEmail(string email)
        {
            // TODO: Fix User caching
            //            CustomUser res = CachedUsers.Find(u => u.Email == email);
            //            if (res != null)
            //            {
            //                return res;
            //            }

            var users = ServerModel.DB.Query<TblUsers>(
                new CompareCondition<string>(
                    new PropertyCondition<string>("Email"),
                    new ValueCondition<string>(email),
                    COMPARE_KIND.EQUAL));
            if (users.Count == 0)
                return null;

            return CreateUser(users[0]);
            //            DoCache(res);
        }

        private static CustomUser CreateUser(TblUsers user)
        {
            var roleIDs = ServerModel.DB.LookupMany2ManyIds<FxRoles>(user, null);
            var roles = new List<string>(ServerModel.DB.Load<FxRoles>(roleIDs).Select(r => r.Name));
            return new CustomUser(user.ID, user.FirstName, user.LastName, user.Login, user.PasswordHash, user.Email, roles);
        }

        public void NotifyUpdated(TblUsers u)
        {
            HttpRuntime.Cache.Remove(ByLogin(u.Login));
        }

        public string GetPasswordHash(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
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

    public sealed class FormsModel
    {
        public void Initialize()
        {
        }

        public void Register<TController>([NotNull] string url)
            where TController : ControllerBase
        {
            _pages.Add(typeof(TController), url);
        }

        [NotNull]
        public string BuildRedirectUrl<TController>(TController c)
            where TController : ControllerBase
        {
            if (c.BackUrl == null)
            {
                throw new DMError("BackUrl is not specified");
            }
            var @params = ControllerParametersUtility<TController>.BuildUrlParams(c);
            var res = _pages[typeof (TController)];
            return string.IsNullOrEmpty(@params) ? res : res + '?' + @params;
        }

        private readonly Dictionary<Type, string> _pages = new Dictionary<Type, string>();
    }

    public static class CustomUserExtenders
    {
        public static bool IsStudent(this CustomUser u)
        {
            return u.Roles.Contains(FxRoles.STUDENT.Name);
        }

        public static bool IsTrainer(this CustomUser u)
        {
            return u.Roles.Contains(FxRoles.TRAINER.Name);
        }

        public static bool Islector(this CustomUser u)
        {
            return u.Roles.Contains(FxRoles.LECTOR.Name);
        }

        public static bool IsAdmin(this CustomUser u)
        {
            return u.Roles.Contains(FxRoles.ADMIN.Name) || u.IsSuperAdmin();
        }

        public static bool IsSuperAdmin(this CustomUser u)
        {
            return u.Roles.Contains(FxRoles.SUPER_ADMIN.Name);
        }

        public static bool IsUpperStudent(this CustomUser u)
        {
            return u.IsStudent() ? u.Roles.Count > 1 : u.Roles.Count > 0;
        }
    }
}
