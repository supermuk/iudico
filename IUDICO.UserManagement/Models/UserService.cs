using System;
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
        private IUserStorage _userStorage;

        public UserService(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        #region Implementation of IUserService

        public IEnumerable<Role> GetRoles()
        {
            return _userStorage.GetRoles();
        }

        public Role GetRole(int id)
        {
            return _userStorage.GetRole(id);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _userStorage.GetGroups();
        }

        public Group GetGroup(int id)
        {
            return _userStorage.GetGroup(id);
        }

        #endregion
    }
}