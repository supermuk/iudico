using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Services
{
    public interface IUserManagement : IService
    {
        #region Role methods

        IEnumerable<Role> GetRoles();
        bool CreateRole(Role role);
        Role GetRole(int id);
        bool EditRole(int id, Role role);
        bool DeleteRole(int id);

        #endregion

        #region User methods



        #endregion

        #region Group Methods

        IEnumerable<Group> GetGroups();
        bool CreateGroup(Group group);
        Group GetGroup(int id);
        bool EditGroup(int id, Group group);
        bool DeleteGroup(int id);

        #endregion
    }
}
