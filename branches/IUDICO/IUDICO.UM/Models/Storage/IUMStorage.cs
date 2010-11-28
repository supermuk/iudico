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

        #endregion

        #region User methods



        #endregion

        #region Group Methods

        #endregion
    }
}