namespace IUDICO.Common.Models.Services
{
    public interface ILmsService
    {
        T FindService<T>() where T : IService;

        string GetDbConnectionString();
        DBDataContext GetDbDataContext();

        void Inform(string evt, params object[] data);
    }
}
