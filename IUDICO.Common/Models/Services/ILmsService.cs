using System.Data.Common;

namespace IUDICO.Common.Models.Services
{
    public interface ILmsService
    {
        T FindService<T>() where T : IService;

        //string GetDbConnectionString();
        DBDataContext GetDbDataContext();
        //DbConnection GetDbConnection();

        void Inform(string evt, params object[] data);
    }
}
