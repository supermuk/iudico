﻿using System.Collections.Generic;

namespace IUDICO.Common.Models.Services
{
    public interface IUserService : IService
    {
        #region User methods

        IEnumerable<User> GetUsersByGroup(Group group);
        IEnumerable<User> GetUsers();
        User GetCurrentUser();
        IEnumerable<Group> GetGroupsByUser(User user);

        #endregion

        #region Group Methods

        IEnumerable<Group> GetGroups();
        Group GetGroup(int id);

        #endregion
    }
}