using System;
using System.Collections.Generic;
using IUDICO.Common.Models;

namespace IUDICO.UM.Models.Storage
{
    public interface IUMStorage
    {
        #region Role methods

        IEnumerable<Role> GetRoles();
        bool CreateRole(Role role);
        Role GetRole(int id);
        bool EditRole(int id, Role role);
        bool Delete(int id);

        #endregion

        #region User methods



        #endregion

        #region Group Methods

        #endregion
    }
}