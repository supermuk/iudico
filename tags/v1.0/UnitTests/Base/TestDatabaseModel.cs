using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.DataModel.DB;
using System.Data;

namespace IUDICO.UnitTest.Base
{
    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "IUDICO_TEST")]
    public class TestDatabaseModel: DatabaseModel
    {
        public TestDatabaseModel(string connection)
            : base(connection)
        { }

        public TestDatabaseModel(IDbConnection connection)
            : base(connection)
        { }
    }
}
