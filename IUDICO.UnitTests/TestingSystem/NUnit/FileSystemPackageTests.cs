using System;

using IUDICO.TestingSystem.Models.VOs;

using Microsoft.LearningComponents;

using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    internal class FileSystemPackageTests
    {
        private FileSystemPackage fileSystemPackage;

        [SetUp]
        public void FileSystemPackageTestsSetUp()
        {
            this.fileSystemPackage = new FileSystemPackage(
                "IUDICO/TestingSystem/Models/VOs/", 12345, new DateTime(2011, 11, 11), "package.zip", 1);
        }

        [Test]
        public void FileSystemPackagePropertiesTest1()
        {
            this.FileSystemPackageTestsSetUp();
            Assert.AreEqual(this.fileSystemPackage.CourseID, 1);
            Assert.AreEqual(this.fileSystemPackage.FileName, "package.zip");
            Assert.AreEqual(this.fileSystemPackage.FolderPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(this.fileSystemPackage.Owner, 12345);
            Assert.AreEqual(this.fileSystemPackage.UploadDateTime, new DateTime(2011, 11, 11));
        }

        [Test]
        public void FileSystemPackagePropertiesTest2()
        {
            this.fileSystemPackage = new FileSystemPackage("IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(this.fileSystemPackage.CourseID, default(int));
            Assert.AreEqual(this.fileSystemPackage.FileName, default(string));
            Assert.AreEqual(this.fileSystemPackage.FolderPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(this.fileSystemPackage.Owner, default(long));
            Assert.AreEqual(this.fileSystemPackage.UploadDateTime, null);
        }

        [Test]
        public void FileSystemPackageGetPackageReaderTest()
        {
            this.FileSystemPackageTestsSetUp();
            Assert.That(this.fileSystemPackage.GetPackageReader(), Is.TypeOf(typeof(FileSystemPackageReader)));
            Assert.That(this.fileSystemPackage.GetPackageReader(), Is.Not.Null);
        }
    }
}