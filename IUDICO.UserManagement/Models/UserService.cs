using System;
using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Models
{
    public class UserService : IUserService
    {
        private readonly IUserStorage userStorage;

        public UserService(IUserStorage userStorage)
        {
            this.userStorage = userStorage;
        }

        #region Implementation of IUserService

        public User GetCurrentUser()
        {
            return this.userStorage.GetCurrentUser();
        }

        public IEnumerable<Role> GetCurrentUserRoles()
        {
            var user = this.GetCurrentUser();

            if (string.IsNullOrEmpty(user.Username))
            {
                return user.Roles;
            }

            return this.userStorage.GetUserRoles(user.Username);
        }

        public IEnumerable<Group> GetGroups()
        {
            return this.userStorage.GetGroups();
        }

        public Group GetGroup(int id)
        {
            return this.userStorage.GetGroup(id);
        }

        public IEnumerable<UserTopicRating> GetRatings(User user, IEnumerable<int> topicId)
        {
            return this.userStorage.GetRatings(user, topicId);
        }

        public IEnumerable<User> GetUsersByGroup(Group group)
        {
            return this.userStorage.GetUsersInGroup(group);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.userStorage.GetUsers();
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return this.userStorage.GetUsers(predicate);
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            return this.userStorage.GetGroupsByUser(user);
        }

        #endregion
    }
}