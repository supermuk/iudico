using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.CourseManagement.Helpers;
using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class ZipperTest
    {
        [Test]
        [Category("CreateZipTest")]
        public void CreateZipTest()
        {
            string filename = "D:\\Tests";
            string folder = "D:\\Tests\\1";

            Zipper.CreateZip(filename,folder);


        }

    }
}
