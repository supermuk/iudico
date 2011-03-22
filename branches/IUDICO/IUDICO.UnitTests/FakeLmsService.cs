using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using System.Data.Common;

namespace IUDICO.UnitTests
{
    class FakeLmsService : ILmsService
    {
        #region ILmsService Members

        public T FindService<T>() where T : IService
        {
            //must return pathes to fake or real services
            throw new NotImplementedException();
        }

        public DBDataContext GetDbDataContext()
        {
            return new DBDataContext();//"This must be path to fake repository");
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
