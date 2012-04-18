using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using System.Reflection;
using IUDICO.Common.Models.Cache;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;

namespace IUDICO.UserManagement.Models.Storage
{
    public class CachedDatabaseUserStorage: DatabaseUserStorage
    {
        // TODO: fix
        // GetUser(email)
        // RestorePassword(User)

        // TODO: !!
        // Add expire group, nongroup

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

        #region User members

        protected void CacheUser(User user)
        {
            _cacheProvider["user-" + user.Username] = user;
            _cacheProvider["user-id-" + user.Id] = user;

            _cacheProvider.AddTag("user-" + user.Id, "user-id-" + user.Id);
            _cacheProvider.AddTag("user-" + user.Id, "user-" + user.Username);
        }

        public override User GetCurrentUser()
        {
            var key = "guest";

            if (HttpContext.Current == null || HttpContext.Current.User == null ||
                !HttpContext.Current.User.Identity.IsAuthenticated)
                key = HttpContext.Current.User.Identity.Name;

            var user = _cacheProvider["user-" + key] as User;

            if (user == null)
            {
                user = base.GetCurrentUser();

                CacheUser(user);
            }

            return user;
        }

        public override User GetUser(Guid userId)
        {
            var user = _cacheProvider["user-id-" + userId] as User;

            if (user == null)
            {
                user = base.GetUser(userId);

                CacheUser(user);
            }

            return user;
        }

        public override IEnumerable<User> GetUsers()
        {
            var users = _cacheProvider["users-all"] as IEnumerable<User>;

            if (users == null)
            {
                users = base.GetUsers();
                _cacheProvider["users-all"] = users;

                foreach (var user in users)
                {
                    CacheUser(user);

                    _cacheProvider.AddTag("user-" + user.Id, "users-all");
                }
            }

            return users;
        }

        public override void ActivateUser(Guid id)
        {
            base.ActivateUser(id);

            _cacheProvider.ExpireTag("user-" + id);
        }

        public override void DeactivateUser(Guid id)
        {
            base.DeactivateUser(id);

            _cacheProvider.ExpireTag("user-" + id);
        }

        public override User RestorePassword(RestorePasswordModel restorePasswordModel)
        {
            var user = base.RestorePassword(restorePasswordModel);

            _cacheProvider.ExpireTag("user-" + user.Id);

            return user;
        }

        public override bool CreateUser(User user)
        {
            _cacheProvider.Expire("users-all");

            return base.CreateUser(user);
        }

        public override Dictionary<string, string> CreateUsersFromCSV(string csvPath)
        {
            _cacheProvider.Expire("users-all");

            return base.CreateUsersFromCSV(csvPath);
        }

        public override void EditUser(Guid id, User user)
        {
            base.EditUser(id, user);

            _cacheProvider.ExpireTag("user-" + id);
        }

        public override void EditUser(Guid id, EditUserModel user)
        {
            base.EditUser(id, user);

            _cacheProvider.ExpireTag("user-" + id);
        }

        public override User DeleteUser(Func<User, bool> predicate)
        {
            var user = base.DeleteUser(predicate);

            _cacheProvider.ExpireTag("user-" + user.Id);

            return user;
        }

        public override IEnumerable<User> GetUsersInGroup(Group group)
        {
            var users = _cacheProvider["users-group-" + group.Id] as IEnumerable<User>;

            if (users == null)
            {
                users = base.GetUsersInGroup(group);

                _cacheProvider["users-group-" + group.Id] = users;

                foreach (var user in users)
                {
                    CacheUser(user);

                    _cacheProvider.AddTag("user-" + user.Id, "users-group-" + group.Id);
                }
            }

            return users;
        }

        public override IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            var users = _cacheProvider["users-ngroup-" + group.Id] as IEnumerable<User>;

            if (users == null)
            {
                users = base.GetUsersNotInGroup(group);

                _cacheProvider["users-ngroup-" + group.Id] = users;

                foreach (var user in users)
                {
                    CacheUser(user);

                    _cacheProvider.AddTag("user-" + user.Id, "users-ngroup-" + group.Id);
                }
            }

            return users;
        }

        public override User RegisterUser(RegisterModel registerModel)
        {
            var user = base.RegisterUser(registerModel);

            _cacheProvider.ExpireTag("user-" + user.Id);

            return user;
        }

        public override void EditAccount(EditModel editModel)
        {
            base.EditAccount(editModel);

            _cacheProvider.ExpireTag("user-" + editModel.Id);
        }

        public override void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = GetCurrentUser();
            
            base.ChangePassword(changePasswordModel);

            _cacheProvider.ExpireTag("user-" + user.Id);
        }

        #endregion

        #region Roles members

        public override void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            base.AddUsersToRoles(usernames, roles);

            foreach (var username in usernames)
            {

            }

            foreach (var role in roles)
            {

            }
        }

        public override void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            base.RemoveUsersFromRoles(usernames, roles);

            foreach (var username in usernames)
            {

            }

            foreach (var role in roles)
            {

            }
        }

        public override IEnumerable<User> GetUsersInRole(Role role)
        {
            var users = _cacheProvider["users-role-" + role] as IEnumerable<User>;

            if (users == null)
            {
                users = base.GetUsersInRole(role);

                _cacheProvider["users-role-" + role] = users;

                foreach (var user in users)
                {
                    CacheUser(user);
                    _cacheProvider.AddTag("user-" + user.Id, "users-role-" + role);
                }
            }

            return users;
        }

        public override IEnumerable<Role> GetUserRoles(string username)
        {
            return base.GetUserRoles(username);
        }

        public override void RemoveUserFromRole(Role role, User user)
        {
            base.RemoveUserFromRole(role, user);
        }

        public override void AddUserToRole(Role role, User user)
        {
            base.AddUserToRole(role, user);
        }

        public override IEnumerable<Role> GetRolesAvailableToUser(User user)
        {
            return base.GetRolesAvailableToUser(user);
        }

        public override bool IsPromotedToAdmin()
        {
            return base.IsPromotedToAdmin();
        }

        public override void RateTopic(int topicId, int rating)
        {
            base.RateTopic(topicId, rating);
        }

        #endregion
    }
}