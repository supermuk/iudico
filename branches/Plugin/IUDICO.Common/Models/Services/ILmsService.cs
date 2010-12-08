using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Services
{
    public interface ILmsService
    {
        T FindService<T>() where T : IService;
        string GetDBConnectionString();
        DBDataContext GetDBDataContext();
        void Inform(string evt, params object[] data);
    }
}
