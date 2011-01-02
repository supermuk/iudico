using System.Collections.Generic;

namespace IUDICO.Common.Models.Services
{
    public interface IUserService : IService
    {
        #region Role methods

        IEnumerable<Role> GetRoles();
        Role GetRole(int id);

        #endregion

        #region User methods

        IEnumerable<User> GetUsersByGroup(Group group);
        IEnumerable<User> GetUsers();

        #endregion

        #region Group Methods

        IEnumerable<Group> GetGroups();
        Group GetGroup(int id);

        #endregion
    }
}
