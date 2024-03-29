﻿using System;
using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Models
{
    public class UserService : IUserService
    {
        private readonly IUserStorage _UserStorage;

        public UserService(IUserStorage userStorage)
        {
            _UserStorage = userStorage;
        }

        #region Implementation of IUserService

        public User GetCurrentUser()
        {
            return _UserStorage.GetCurrentUser();
        }

        public IEnumerable<Role> GetCurrentUserRoles()
        {
            var user = GetCurrentUser();

            if (string.IsNullOrEmpty(user.Username))
            {
                return user.Roles;
            }

            return _UserStorage.GetUserRoles(user.Username);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _UserStorage.GetGroups();
        }

        public Group GetGroup(int id)
        {
            return _UserStorage.GetGroup(id);
        }

        public IEnumerable<User> GetUsersByGroup(Group group)
        {
            return _UserStorage.GetUsersInGroup(group);
        }

        public IEnumerable<User> GetUsers()
        {
            return _UserStorage.GetUsers();
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return _UserStorage.GetUsers(predicate);
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            return _UserStorage.GetGroupsByUser(user);
        }

        #endregion
    }
}