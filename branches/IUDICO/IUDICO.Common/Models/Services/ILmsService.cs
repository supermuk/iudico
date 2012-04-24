using System;
using System.Data.Common;
using IUDICO.Common.Models.Interfaces;
using System.Collections.Generic;
using IUDICO.Common.Models.Plugin;

namespace IUDICO.Common.Models.Services
{
    public interface ILmsService
    {
        T FindService<T>() where T : IService;

        Menu GetMenu();
        Dictionary<IPlugin, IEnumerable<Action>> GetActions();

        void Inform(string evt, params object[] data);
    }
}
