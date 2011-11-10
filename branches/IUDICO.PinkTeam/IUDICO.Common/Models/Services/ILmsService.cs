using System;
using System.Data.Common;
using IUDICO.Common.Models.Interfaces;

namespace IUDICO.Common.Models.Services
{
    public interface ILmsService
    {
        T FindService<T>() where T : IService;

        [Obsolete("Use GetDbConnection() instead.")]
        DBDataContext GetDbDataContext();
        IDataContext GetIDataContext();
        DbConnection GetDbConnection();

        void Inform(string evt, params object[] data);
    }
}
