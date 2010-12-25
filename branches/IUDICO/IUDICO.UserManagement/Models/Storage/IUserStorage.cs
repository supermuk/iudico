﻿using System;
using System.Collections.Generic;
using IUDICO.Common.Models;

namespace IUDICO.UserManagement.Models.Storage
{
    public interface IUserStorage
    {
        #region Role members

        IEnumerable<Role> GetRoles();
        Role GetRole(int id);
        void CreateRole(Role role);
        void EditRole(int id, Role role);
        void DeleteRole(int id);

        #endregion

        #region User members

        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        User GetUser(string openId);
        void CreateUser(User user);
        void EditUser(Guid id, EditUserModel editor);
        void DeleteUser(Guid id);

        #endregion

        #region Group members

        IEnumerable<Group> GetGroups();
        Group GetGroup(int id);
        void CreateGroup(Group group);
        void EditGroup(int id, Group group);
        void DeleteGroup(int id);

        #endregion
    }
}
