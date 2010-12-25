﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
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

        public IEnumerable<Role> GetRoles()
        {
            return _UserStorage.GetRoles();
        }

        public Role GetRole(int id)
        {
            return _UserStorage.GetRole(id);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _UserStorage.GetGroups();
        }

        public Group GetGroup(int id)
        {
            return _UserStorage.GetGroup(id);
        }

        #endregion
    }
}