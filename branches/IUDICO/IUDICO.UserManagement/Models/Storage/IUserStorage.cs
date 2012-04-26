using System;
using System.Collections.Generic;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UserManagement.Models.Storage
{
    public interface IUserStorage
    {
        #region User members

        User GetCurrentUser();
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        IEnumerable<User> GetUsers(int pageIndex, int pageSize);
        User GetUser(Guid userId);
        User GetUser(string username);
        User GetUser(Func<User, bool> predicate);
        bool CreateUser(User user);
        Dictionary<string, string> CreateUsersFromCSV(string csvPath);
        void EditUser(Guid id, EditUserModel editor);
        void EditUser(Guid id, User editor);
        void EditAccount(EditModel editModel);
        void ChangePassword(ChangePasswordModel changePasswordModel);
        User DeleteUser(Func<User, bool> predicate);
        IEnumerable<User> GetUsersInGroup(Group group);
        IEnumerable<User> GetUsersNotInGroup(Group group);
        bool UsernameExists(string username);
        bool UserUniqueIdAvailable(string userUniqueId, Guid userId);
        void ActivateUser(Guid id);
        void DeactivateUser(Guid id);
        User RegisterUser(RegisterModel registerModel);
        string EncryptPassword(string password);
        User RestorePassword(RestorePasswordModel restorePasswordModel);

        int UploadAvatar(Guid id, HttpPostedFileBase file);
        int DeleteAvatar(Guid id);

        #endregion

        #region Role members

        IEnumerable<User> GetUsersInRole(Role role);
        IEnumerable<User> AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<Role> roles);
        IEnumerable<User> RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<Role> roles);
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

        bool IsPromotedToAdmin();
        void RateTopic(int topicId, int score);
    }
}