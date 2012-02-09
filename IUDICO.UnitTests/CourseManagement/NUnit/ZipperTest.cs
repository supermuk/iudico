using System;
using System.Configuration;
using System.IO;
using System.Web;
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
			   string filename = "1.zip";
				string folder =  Path.Combine(ConfigurationManager.AppSettings.Get("PathToIUDICO.UnitTests"),@"IUDICO.UnitTests\bin\Debug\Site\Data\");


            Zipper.CreateZip(filename,folder);

			  Assert.IsTrue(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("PathToIUDICO.UnitTests"),@"IUDICO.UnitTests\bin\Debug\1.zip")));
        }

    }

}
