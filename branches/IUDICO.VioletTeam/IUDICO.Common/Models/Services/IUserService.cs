﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Common.Models.Services
{
    public interface IUserService : IService
    {
        #region User methods

        IEnumerable<User> GetUsersByGroup(Group group);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
         
        User GetCurrentUser();
        IEnumerable<Group> GetGroupsByUser(User user);

        #endregion

        #region Group Methods

        IEnumerable<Group> GetGroups();
        Group GetGroup(int id);

        #endregion
    }
}