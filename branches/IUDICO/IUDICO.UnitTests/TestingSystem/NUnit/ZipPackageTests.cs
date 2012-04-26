using System;

using IUDICO.TestingSystem.Models.VOs;

using Microsoft.LearningComponents;

using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    internal class ZipPackageTests
    {
        private ZipPackage zipPackage;

        [SetUp]
        public void ZipPackageTestsSetUp()
        {
            this.zipPackage = new ZipPackage(
                "IUDICO/TestingSystem/Models/VOs/", 12345, new DateTime(2011, 11, 11), "package.zip", 1);
        }

        [Test]
        public void ZipPackagePropertiesTest1()
        {
            this.ZipPackageTestsSetUp();
            Assert.AreEqual(this.zipPackage.CourseID, 1);
            Assert.AreEqual(this.zipPackage.FileName, "package.zip");
            Assert.AreEqual(this.zipPackage.ZipPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(this.zipPackage.Owner, 12345);
            Assert.AreEqual(this.zipPackage.UploadDateTime, new DateTime(2011, 11, 11));
        }

        [Test]
        public void ZipPackagePropertiesTest2()
        {
            this.ZipPackageTestsSetUp();
            this.zipPackage = new ZipPackage("IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(this.zipPackage.CourseID, default(int));
            Assert.AreEqual(this.zipPackage.FileName, default(string));
            Assert.AreEqual(this.zipPackage.ZipPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(this.zipPackage.Owner, default(long));
            Assert.AreEqual(this.zipPackage.UploadDateTime, null);
        }

        [Test]
        public void ZipPackageGetPackageReaderTest()
        {
            this.ZipPackageTestsSetUp();
            Assert.That(this.zipPackage.GetPackageReader(), Is.TypeOf(typeof(ZipPackageReader)));
            Assert.That(this.zipPackage.GetPackageReader(), Is.Not.Null);
        }
    }
}