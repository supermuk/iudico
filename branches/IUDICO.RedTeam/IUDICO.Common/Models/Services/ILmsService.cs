using System;
using System.Data.Common;

namespace IUDICO.Common.Models.Services
{
    public interface ILmsService
    {
        T FindService<T>() where T : IService;

        [Obsolete("Use GetDbConnection() instead.")]
        DBDataContext GetDbDataContext();
        DbConnection GetDbConnection();

        void Inform(string evt, params object[] data);
    }
}
