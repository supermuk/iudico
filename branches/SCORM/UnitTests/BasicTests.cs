using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using IUDICO.UnitTest.Base;
using NUnit.Framework;
using System.Diagnostics;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class BasicTests: TestFixtureDB
    {
        [Test]
        public void CourseTest()
        {
            using (var c = new DataObjectCleaner())
            {
                //TblCourses course = new TblCourses { Description = "Test courses", };

                //ServerModel.DB.Insert<TblCourses>(course);
            }
        }

        [Test]
        public void UserTest()
        {
            using (var c = new DataObjectCleaner())
            {
                //TblUsers user = new TblUsers {  };
            }
        }
    }
}
