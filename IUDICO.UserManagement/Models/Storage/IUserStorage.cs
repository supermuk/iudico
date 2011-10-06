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
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        IEnumerable<User> GetUsers(int pageIndex, int pageSize);
        User GetUser(Func<User, bool> predicate);
        void CreateUser(User user);
        Dictionary<string, string> CreateUsersFromCSV(string csvPath);
        void EditUser(Guid id, EditUserModel editor);
        void EditUser(Guid id, User editor);
        void EditAccount(EditModel editModel);
        void ChangePassword(ChangePasswordModel changePasswordModel);
        void DeleteUser(Func<User, bool> predicate);
        IEnumerable<User> GetUsersInGroup(Group group);
        IEnumerable<User> GetUsersNotInGroup(Group group);
        bool UsernameExists(string username);
        void ActivateUser(Guid id);
        void DeactivateUser(Guid id);
        void RegisterUser(RegisterModel registerModel);
        string EncryptPassword(string password);
        void RestorePassword(RestorePasswordModel restorePasswordModel);

        #endregion

        #region Role members

        IEnumerable<User> GetUsersInRole(Role role);
        void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles);
        void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles);
        IEnumerable<Role> GetUserRoles(string username);
        void RemoveUserFromRole(Role role, User user);
        void AddUserToRole(Role role, User user);
        IEnumerable<Role> GetRolesAvailableToUser(User user);

        #endregion

        #region Group members

        IEnumerable<Group> GetGroups();
        Group GetGroup(int id);
        void CreateGroup(Group group);
        void EditGroup(int id, Group group);
        void DeleteGroup(int id);
        void AddUserToGroup(Group group, User user);
        void RemoveUserFromGroup(Group group, User user);
        IEnumerable<Group> GetGroupsByUser(User user);
        IEnumerable<Group> GetGroupsAvailableToUser(User user);

        #endregion
    }
}
