using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using System.Reflection;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;

namespace IUDICO.UserManagement.Models.Storage
{
    public class CachedUserStorage: IUserStorage
    {
        // TODO: fix
        // GetUser(email)
        // RestorePassword(User)
        // methods User instead of username/id

        private readonly ICacheProvider _cacheProvider;
        private readonly IUserStorage _storage;
        private readonly object lockObject = new object();

        public CachedUserStorage(IUserStorage storage, ICacheProvider cachePrvoider)
        {
            _storage = storage;
            _cacheProvider = cachePrvoider;
        }

        /*
        private static readonly object CacheLockObject = new object();
        private static string keyPrefix
        {
            get { return typeof(CachedData_storageUserStorage).FullName; }
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
        
        public User GetCurrentUser()
        {
            var key = "guest";

            if (HttpContext.Current == null || HttpContext.Current.User == null ||
                !HttpContext.Current.User.Identity.IsAuthenticated)
                key = HttpContext.Current.User.Identity.Name;

            //var user = _cacheProvider.["user-" + key] as User;
            return _cacheProvider.Get<User>("user-name-" + key, @lockObject, () => _storage.GetCurrentUser(), DateTime.Now.AddHours(3), "user-name-" + key);
        }

        public User GetUser(Guid userId)
        {
            return _cacheProvider.Get<User>("user-id-" + userId, @lockObject, () => _storage.GetUser(userId), DateTime.Now.AddDays(1), "user-id-" + userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _cacheProvider.Get<IEnumerable<User>>("users", @lockObject, () => _storage.GetUsers(), DateTime.Now.AddDays(1), "users");
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return GetUsers().Where(predicate);
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize)
        {
            return GetUsers().Skip(pageIndex).Take(pageSize);
        }

        public User GetUser(Func<User, bool> predicate)
        {
            return _storage.GetUser(predicate);
        }

        public bool UsernameExists(string username)
        {
            return _storage.UsernameExists(username);
        }

        public bool UserUniqueIdAvailable(string userUniqueId, Guid userId)
        {
            return _storage.UserUniqueIdAvailable(userUniqueId, userId);
        }

        // TODO: rewrite interface method
        public void ActivateUser(Guid id)
        {
            _storage.ActivateUser(id);

            var user = GetUser(id);

            _cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");
        }

        // TODO: rewrite interface method
        public void DeactivateUser(Guid id)
        {
            _storage.DeactivateUser(id);

            var user = GetUser(id);

            _cacheProvider.Invalidate("user-id-" + user.Id, "user-name" + user.Username, "users");
        }

        public User RestorePassword(RestorePasswordModel restorePasswordModel)
        {
            var user = _storage.RestorePassword(restorePasswordModel);

            _cacheProvider.Invalidate("user-id-" + user.Id, "user-name" + user.Username, "users");

            return user;
        }

        public bool CreateUser(User user)
        {
            _cacheProvider.Invalidate("users");

            return _storage.CreateUser(user);
        }

        public Dictionary<string, string> CreateUsersFromCSV(string csvPath)
        {
            _cacheProvider.Invalidate("users");

            return _storage.CreateUsersFromCSV(csvPath);
        }

        public void EditUser(Guid id, User user)
        {
            _storage.EditUser(id, user);

            _cacheProvider.Invalidate("user-" + id, "users");
        }

        public void EditUser(Guid id, EditUserModel user)
        {
            _storage.EditUser(id, user);

            _cacheProvider.Invalidate("user-" + id, "users");
        }

        public User DeleteUser(Func<User, bool> predicate)
        {
            var user = _storage.DeleteUser(predicate);

            _cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username);

            return user;
        }

        public IEnumerable<User> GetUsersInGroup(Group group)
        {
            return _cacheProvider.Get<IEnumerable<User>>("users-group-" + group.Id, @lockObject, () => _storage.GetUsersInGroup(group), DateTime.Now.AddDays(1), "users");
        }

        public IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            return _cacheProvider.Get<IEnumerable<User>>("users-ngroup-" + group.Id, @lockObject, () => _storage.GetUsersInGroup(group), DateTime.Now.AddDays(1), "users");
        }

        public User RegisterUser(RegisterModel registerModel)
        {
            var user = _storage.RegisterUser(registerModel);

            _cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");

            return user;
        }

        public void EditAccount(EditModel editModel)
        {
            _storage.EditAccount(editModel);

            var user = GetUser(editModel.Id);

            _cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");
        }

        public void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = GetCurrentUser();
            
            _storage.ChangePassword(changePasswordModel);

            _cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");
        }

        public string EncryptPassword(string password)
        {
            return _storage.EncryptPassword(password);
        }

        #endregion

        #region Roles members

        public IEnumerable<User> AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var users = _storage.AddUsersToRoles(usernames, roles);

            _cacheProvider.Invalidate(users.Select(u => "user-id-" + u.Id).ToArray());
            _cacheProvider.Invalidate(users.Select(u => "user-name-" + u.Username).ToArray());
            _cacheProvider.Invalidate("users", "roles");

            return users;
        }

        public IEnumerable<User> RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var users = _storage.RemoveUsersFromRoles(usernames, roles);

            _cacheProvider.Invalidate(users.Select(u => "user-id-" + u.Id).ToArray());
            _cacheProvider.Invalidate(users.Select(u => "user-name-" + u.Username).ToArray());
            _cacheProvider.Invalidate("users", "roles");

            return users;
        }

        public IEnumerable<User> GetUsersInRole(Role role)
        {
            return _cacheProvider.Get<IEnumerable<User>>("users-role-" + role, @lockObject, () => _storage.GetUsersInRole(role), DateTime.Now.AddDays(1), "users");
        }

        public IEnumerable<Role> GetUserRoles(string username)
        {
            return _cacheProvider.Get<IEnumerable<Role>>("roles-user-" + username, @lockObject, () => _storage.GetUserRoles(username), DateTime.Now.AddDays(1), "roles");
        }

        public void RemoveUserFromRole(Role role, User user)
        {
            _storage.RemoveUserFromRole(role, user);

            _cacheProvider.Invalidate("role-" + role, "user-id-" + user.Id, "user-name-" + user.Username, "users", "roles");
        }

        public void AddUserToRole(Role role, User user)
        {
            _storage.AddUserToRole(role, user);

            _cacheProvider.Invalidate("role-" + role, "user-id-" + user.Id, "user-name-" + user.Username, "users", "roles");
        }

        public IEnumerable<Role> GetRolesAvailableToUser(User user)
        {
            return _cacheProvider.Get<IEnumerable<Role>>("roles-user-avail-" + user.Id, @lockObject, () => _storage.GetRolesAvailableToUser(user), DateTime.Now.AddDays(1), "roles");
        }

        public bool IsPromotedToAdmin()
        {
            return _storage.IsPromotedToAdmin();
        }

        public void RateTopic(int topicId, int rating)
        {
            _storage.RateTopic(topicId, rating);
        }

        #endregion

        #region Group memebers

        
        public IEnumerable<Group> GetGroups()
        {
            return _cacheProvider.Get<IEnumerable<Group>>("groups", @lockObject, () => _storage.GetGroups(), DateTime.Now.AddDays(1), "groups");
        }

        public Group GetGroup(int id)
        {
            return _cacheProvider.Get<Group>("group-" + id, @lockObject, () => _storage.GetGroup(id), DateTime.Now.AddDays(1), "group-" + id);
        }

        public void CreateGroup(Group group)
        {
            _storage.CreateGroup(group);

            _cacheProvider.Invalidate("groups");
        }

        public void EditGroup(int id, Group group)
        {
            _storage.EditGroup(id, group);

            _cacheProvider.Invalidate("group-" + id, "groups");
        }

        public void DeleteGroup(int id)
        {
            _storage.DeleteGroup(id);

            _cacheProvider.Invalidate("group-" + id, "groups");
        }

        public void AddUserToGroup(Group group, User user)
        {
            _storage.AddUserToGroup(group, user);

            _cacheProvider.Invalidate("groups", "users", "group-" + group.Id, "user-id-" + user.Id, "user-name-" + user.Username);
        }

        public void RemoveUserFromGroup(Group group, User user)
        {
            _storage.RemoveUserFromGroup(group, user);

            _cacheProvider.Invalidate("groups", "users", "group-" + group.Id, "user-id-" + user.Id, "user-name-" + user.Username);
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            return _cacheProvider.Get<IEnumerable<Group>>("groups-user-" + user.Id, @lockObject, () => _storage.GetGroupsByUser(user), DateTime.Now.AddDays(1), "groups", "user-" + user.Id);
        }

        public IEnumerable<Group> GetGroupsAvailableToUser(User user)
        {
            return _cacheProvider.Get<IEnumerable<Group>>("groups-nuser-" + user.Id, @lockObject, () => _storage.GetGroupsAvailableToUser(user), DateTime.Now.AddDays(1), "groups", "user-" + user.Id);
        }

        #endregion

        public int UploadAvatar(Guid id, HttpPostedFileBase file)
        {
            return _storage.UploadAvatar(id, file);
        }

        public int DeleteAvatar(Guid id)
        {
            return _storage.DeleteAvatar(id);
        }
    }
}