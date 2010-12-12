using IUDICO.Common.Models.Services;
using Castle.Windsor;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;

namespace IUDICO.LMS.Models
{
    public class LmsService: ILmsService
    {
        protected readonly IWindsorContainer _Container;

        public LmsService(IWindsorContainer container)
        {
            _Container = container;
        }

        #region ILmsService Members
        public T FindService<T>() where T : IService
        {
            return _Container.Resolve<T>();
        }

        public string GetDbConnectionString()
        {
            return Common.Properties.Settings.Default.IUDICOConnectionString;
        }

        public DBDataContext GetDbDataContext()
        {
            return new DBDataContext();
        }

        public void Inform(string evt, params object[] data)
        {
            var plugins = _Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                plugin.Update(evt, data);
            }
        }
        #endregion
    }
}