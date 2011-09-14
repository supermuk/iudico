using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using Castle.Windsor;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using System.ComponentModel.Composition;

namespace IUDICO.LMS.Models
{
    public class LmsService: ILmsService
    {
        protected readonly IWindsorContainer container;

        public LmsService(IWindsorContainer container)
        {
            this.container = container;
        }

        #region ILmsService Members
        public T FindService<T>() where T : IService
        {
            return container.Resolve<T>();
        }

        public string GetDBConnectionString()
        {
            return IUDICO.Common.Properties.Settings.Default.ButterflyConnectionString;
        }

        public DBDataContext GetDBDataContext()
        {
            return new DBDataContext();
        }

        public void Inform(string evt, params object[] data)
        {
            IPlugin[] plugins = container.ResolveAll<IPlugin>();

            foreach (IPlugin plugin in plugins)
            {
                plugin.Update(evt, data);
            }
        }
        #endregion
    }
}