using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using System.Reflection;
using IUDICO.Common.Models.Cache;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Models.Storage
{
    public class CachedDatabaseUserStorage: DatabaseUserStorage
    {
        protected ICacheProvider _cacheProvider;

        public CachedDatabaseUserStorage(ILmsService lmsService, ICacheProvider cachePrvoider): base(lmsService)
        {
            _cacheProvider = cachePrvoider;
        }

        /*
        private static readonly object CacheLockObject = new object();
        private static string keyPrefix
        {
            get { return typeof(CachedDatabaseUserStorage).FullName; }
        }

        protected object GetCacheObject(string key)
        {
            return HttpRuntime.Cache[key];
        }

        protected void SaveCacheObject(string key, object obj)
        {
            lock (CacheLockObject)
            {
                //HttpRuntime.Cache.Remove(method + "-" + key);
                if (GetCacheObject(key) == null)
                    HttpRuntime.Cache.Insert(key, obj, null, DateTime.Now.AddSeconds(60), TimeSpan.Zero);
            }
        }

        protected void ExpireCacheObject(string key)
        {
            lock (CacheLockObject)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        public void Update(string evt, params object[] args)
        {

        }
        */
        public User GetCurrentUser()
        {
            var key = "guest";

            if (HttpContext.Current == null || HttpContext.Current.User == null ||
                !HttpContext.Current.User.Identity.IsAuthenticated)
                key = HttpContext.Current.User.Identity.Name;

            var user = _cacheProvider["user-" + key] as User;

            if (user == null)
            {
                user = base.GetCurrentUser();
                _cacheProvider["user-" + key] = user;
            }

            return user;
        }
        
        // Is this needed?
        public override IEnumerable<User> GetUsers()
        {
            return base.GetUsers();
            /*
            var users = GetCacheObject("users-all") as IEnumerable<User>;

            if (users == null)
            {
                users = base.GetUsers();
                SaveCacheObject("users-all", users);
            }

            return users;
            */
        }

        public override void EditUser(Guid id, EditUserModel user)
        {
            _cacheProvider.Expire("user-" + user.Username);

            base.EditUser(id, user);
        }
    }
}