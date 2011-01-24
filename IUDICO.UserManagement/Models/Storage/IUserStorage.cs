using System;
using System.Collections.Generic;
using IUDICO.Common.Models;

namespace IUDICO.UserManagement.Models.Storage
{
    public interface IUserStorage
    {
        #region Role members

        IEnumerable<Role> GetRoles();
        Role GetRole(int id);

        #endregion

        #region User members

        User GetCurrentUser();
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        User GetUser(string openId);
        void CreateUser(User user);
        void EditUser(Guid id, EditUserModel editor);
        void DeleteUser(Guid id);
        IEnumerable<User> GetUsersByGroup(Group group);

        #endregion

        #region Group members

        IEnumerable<Group> GetGroups();
        Group GetGroup(int id);
        void CreateGroup(Group group);
        void EditGroup(int id, Group group);
        void DeleteGroup(int id);
        void AddUserToGroup(Group group, User user);
        void RemoveUserFromGroup(Group group, User user);

        #endregion
    }
}
