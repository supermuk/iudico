using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UserManagement.Models.Storage
{
    using IUDICO.Common.Models.Shared.Statistics;

    public class CachedUserStorage : IUserStorage
    {
        // TODO: fix
        // GetUser(email)
        // RestorePassword(User)
        // methods User instead of username/id

        private readonly ICacheProvider cacheProvider;
        private readonly IUserStorage storage;
        private readonly object lockObject = new object();

        public CachedUserStorage(IUserStorage storage, ICacheProvider cachePrvoider)
        {
            this.storage = storage;
            this.cacheProvider = cachePrvoider;
        }

        #region User members

        public User GetCurrentUser()
        {
            var key = "guest";

            if (HttpContext.Current != null && HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity.IsAuthenticated)
            {
                key = HttpContext.Current.User.Identity.Name;
            }

            ////var user = _cacheProvider.["user-" + key] as User;
            return this.cacheProvider.Get(
                "user-name-" + key,
                this.@lockObject,
                () => this.storage.GetCurrentUser(),
                DateTime.Now.AddHours(3),
                "user-name-" + key);
        }

        public User GetUser(Guid userId)
        {
            return this.cacheProvider.Get(
                "user-id-" + userId,
                this.@lockObject,
                () => this.storage.GetUser(userId),
                DateTime.Now.AddDays(1),
                "user-id-" + userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.cacheProvider.Get(
                "users", this.@lockObject, () => this.storage.GetUsers(), DateTime.Now.AddDays(1), "users");
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return this.GetUsers().Where(predicate);
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize)
        {
            return this.GetUsers().Skip(pageIndex).Take(pageSize);
        }

        public User GetUser(Func<User, bool> predicate)
        {
            return this.storage.GetUser(predicate);
        }

        public User GetUser(string username)
        {
            return this.cacheProvider.Get(
                "user-name-" + username,
                this.@lockObject,
                () => this.storage.GetUser(username),
                DateTime.Now.AddDays(1),
                "user-name-" + username);
        }

        public bool UsernameExists(string username)
        {
            return this.storage.UsernameExists(username);
        }

        public bool UserUniqueIdAvailable(string userUniqueId, Guid userId)
        {
            return this.storage.UserUniqueIdAvailable(userUniqueId, userId);
        }

        // TODO: rewrite interface method
        public void ActivateUser(Guid id)
        {
            this.storage.ActivateUser(id);

            var user = this.GetUser(id);

            this.cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");
        }

        // TODO: rewrite interface method
        public void DeactivateUser(Guid id)
        {
            this.storage.DeactivateUser(id);

            var user = this.GetUser(id);

            this.cacheProvider.Invalidate("user-id-" + user.Id, "user-name" + user.Username, "users");
        }

        public User RestorePassword(RestorePasswordModel restorePasswordModel)
        {
            var user = this.storage.RestorePassword(restorePasswordModel);

            this.cacheProvider.Invalidate("user-id-" + user.Id, "user-name" + user.Username, "users");

            return user;
        }

        public bool CreateUser(User user)
        {
            this.cacheProvider.Invalidate("users");

            return this.storage.CreateUser(user);
        }

        public Dictionary<string, string> CreateUsersFromCSV(string csvPath)
        {
            this.cacheProvider.Invalidate("users");

            return this.storage.CreateUsersFromCSV(csvPath);
        }

        public void EditUser(Guid id, User user)
        {
            this.storage.EditUser(id, user);

            this.cacheProvider.Invalidate("user-" + id, "users");
        }

        public void EditUser(Guid id, EditUserModel user)
        {
            this.storage.EditUser(id, user);

            this.cacheProvider.Invalidate("user-" + id, "users");
        }

        public User DeleteUser(Func<User, bool> predicate)
        {
            var user = this.storage.DeleteUser(predicate);

            this.cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username);

            return user;
        }

        public IEnumerable<User> GetUsersInGroup(Group group)
        {
            return this.cacheProvider.Get(
                "users-group-" + group.Id,
                this.lockObject,
                () => this.storage.GetUsersInGroup(group),
                DateTime.Now.AddDays(1),
                "users");
        }

        public IEnumerable<User> GetUsersNotInGroup(Group group)
        {
            return this.cacheProvider.Get(
                "users-ngroup-" + group.Id,
                this.lockObject,
                () => this.storage.GetUsersInGroup(group),
                DateTime.Now.AddDays(1),
                "users");
        }

        public User RegisterUser(RegisterModel registerModel)
        {
            var user = this.storage.RegisterUser(registerModel);

            this.cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");

            return user;
        }

        public void EditAccount(EditModel editModel)
        {
            this.storage.EditAccount(editModel);

            var user = this.GetUser(editModel.Id);

            this.cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");
        }

        public void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = this.GetCurrentUser();

            this.storage.ChangePassword(changePasswordModel);

            this.cacheProvider.Invalidate("user-id-" + user.Id, "user-name-" + user.Username, "users");
        }

        public string EncryptPassword(string password)
        {
            return this.storage.EncryptPassword(password);
        }

        #endregion

        #region Roles members

        public IEnumerable<User> AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var users = this.storage.AddUsersToRoles(usernames, roles);

            this.cacheProvider.Invalidate(users.Select(u => "user-id-" + u.Id).ToArray());
            this.cacheProvider.Invalidate(users.Select(u => "user-name-" + u.Username).ToArray());
            this.cacheProvider.Invalidate("users", "roles");

            return users;
        }

        public IEnumerable<User> RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles)
        {
            var users = this.storage.RemoveUsersFromRoles(usernames, roles);

            this.cacheProvider.Invalidate(users.Select(u => "user-id-" + u.Id).ToArray());
            this.cacheProvider.Invalidate(users.Select(u => "user-name-" + u.Username).ToArray());
            this.cacheProvider.Invalidate("users", "roles");

            return users;
        }

        public IEnumerable<User> GetUsersInRole(Role role)
        {
            return this.cacheProvider.Get(
                "users-role-" + role,
                this.lockObject,
                () => this.storage.GetUsersInRole(role),
                DateTime.Now.AddDays(1),
                "users");
        }

        public IEnumerable<Role> GetUserRoles(string username)
        {
            return this.cacheProvider.Get(
                "roles-user-" + username,
                this.lockObject,
                () => this.storage.GetUserRoles(username),
                DateTime.Now.AddDays(1),
                "roles");
        }

        public void RemoveUserFromRole(Role role, User user)
        {
            this.storage.RemoveUserFromRole(role, user);

            this.cacheProvider.Invalidate(
                "role-" + role, "user-id-" + user.Id, "user-name-" + user.Username, "users", "roles");
        }

        public void AddUserToRole(Role role, User user)
        {
            this.storage.AddUserToRole(role, user);

            this.cacheProvider.Invalidate(
                "role-" + role, "user-id-" + user.Id, "user-name-" + user.Username, "users", "roles");
        }

        public IEnumerable<Role> GetRolesAvailableToUser(User user)
        {
            return this.cacheProvider.Get(
                "roles-user-avail-" + user.Id,
                this.lockObject,
                () => this.storage.GetRolesAvailableToUser(user),
                DateTime.Now.AddDays(1),
                "roles");
        }

        public bool IsPromotedToAdmin()
        {
            return this.storage.IsPromotedToAdmin();
        }

        public void RateTopic(int topicId, int rating)
        {
            this.storage.RateTopic(topicId, rating);
        }

        public void UpdateUserAverage(AttemptResult attemptResult)
        {
            this.storage.UpdateUserAverage(attemptResult);
        }

        #endregion

        #region Group memebers

        public IEnumerable<Group> GetGroups()
        {
            return this.cacheProvider.Get(
                "groups", this.lockObject, () => this.storage.GetGroups(), DateTime.Now.AddDays(1), "groups");
        }

        public Group GetGroup(int id)
        {
            return this.cacheProvider.Get(
                "group-" + id, this.lockObject, () => this.storage.GetGroup(id), DateTime.Now.AddDays(1), "group-" + id);
        }

        public void CreateGroup(Group group)
        {
            this.storage.CreateGroup(group);

            this.cacheProvider.Invalidate("groups");
        }

        public void EditGroup(int id, Group group)
        {
            this.storage.EditGroup(id, group);

            this.cacheProvider.Invalidate("group-" + id, "groups");
        }

        public void DeleteGroup(int id)
        {
            this.storage.DeleteGroup(id);

            this.cacheProvider.Invalidate("group-" + id, "groups");
        }

        public void AddUserToGroup(Group group, User user)
        {
            this.storage.AddUserToGroup(group, user);

            this.cacheProvider.Invalidate(
                "groups", "users", "group-" + group.Id, "user-id-" + user.Id, "user-name-" + user.Username, "groups-user-" + user.Id);
        }

        public void RemoveUserFromGroup(Group group, User user)
        {
            this.storage.RemoveUserFromGroup(group, user);

            this.cacheProvider.Invalidate(
                "groups", "users", "group-" + group.Id, "user-id-" + user.Id, "user-name-" + user.Username, "groups-user-" + user.Id);
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            return this.cacheProvider.Get(
                "groups-user-" + user.Id,
                this.lockObject,
                () => this.storage.GetGroupsByUser(user),
                DateTime.Now.AddDays(1),
                "groups",
                "user-" + user.Id);
        }

        public IEnumerable<Group> GetGroupsAvailableToUser(User user)
        {
            return this.cacheProvider.Get(
                "groups-nuser-" + user.Id,
                this.lockObject,
                () => this.storage.GetGroupsAvailableToUser(user),
                DateTime.Now.AddDays(1),
                "groups",
                "user-" + user.Id);
        }

        #endregion

        public int UploadAvatar(Guid id, HttpPostedFileBase file)
        {
            return this.storage.UploadAvatar(id, file);
        }

        public int DeleteAvatar(Guid id)
        {
            return this.storage.DeleteAvatar(id);
        }
    }
}