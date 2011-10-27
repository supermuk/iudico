using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using System.Data.Common;
using System.Configuration;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UnitTests.Fakes
{
    class FakeLmsService : ILmsService
    {
        #region ILmsService Members

        public T FindService<T>() where T : IService
        {
            if (typeof(T) == typeof(IUserService))
            {
                return (T)(new FakeUserService() as IService);
            }
            else if (typeof(T) == typeof(ICourseService))
            {
                return (T)(new FakeCourseService() as IService);
            }

            //must return pathes to fake or real services
            throw new NotImplementedException();
        }

        public DBDataContext GetDbDataContext()
        {
            return new DBDataContext(ConfigurationManager.ConnectionStrings["IUDICO-TEST"].ConnectionString);
        }

        public IDataContext GetIDataContext()
        {
            return GetDbDataContext();
        }

        public DbConnection GetDbConnection()
        {
            throw new NotImplementedException();
        }

        public void Inform(string evt, params object[] data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
