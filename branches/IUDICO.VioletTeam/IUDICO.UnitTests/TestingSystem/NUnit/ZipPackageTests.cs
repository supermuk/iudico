using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.TestingSystem.Models.VO;
using Microsoft.LearningComponents;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    class ZipPackageTests
    {
        private ZipPackage zipPackage;

        [SetUp]
        public void ZipPackageTestsSetUp()
        {
            zipPackage = new ZipPackage("IUDICO/TestingSystem/Models/VOs/", 12345,
                                                      new DateTime(2011, 11, 11), "package.zip", 1);
        }

        [Test]
        public void ZipPackagePropertiesTest1()
        {
            ZipPackageTestsSetUp();
            Assert.AreEqual(zipPackage.CourseID, 1);
            Assert.AreEqual(zipPackage.FileName, "package.zip");
            Assert.AreEqual(zipPackage.ZipPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(zipPackage.Owner, 12345);
            Assert.AreEqual(zipPackage.UploadDateTime, new DateTime(2011, 11, 11));
        }

        [Test]
        public void ZipPackagePropertiesTest2()
        {
            ZipPackageTestsSetUp();
            zipPackage = new ZipPackage("IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(zipPackage.CourseID, default(int));
            Assert.AreEqual(zipPackage.FileName, default(string));
            Assert.AreEqual(zipPackage.ZipPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(zipPackage.Owner, default(long));
            Assert.AreEqual(zipPackage.UploadDateTime, null);
        }

        [Test]
        public void ZipPackageGetPackageReaderTest()
        {
            ZipPackageTestsSetUp();
            Assert.That(zipPackage.GetPackageReader(), Is.TypeOf(typeof(ZipPackageReader)));
            Assert.That(zipPackage.GetPackageReader(), Is.Not.Null);
        }
    }
}
