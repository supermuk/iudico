using System;
using IUDICO.TestingSystem.Models.VOs;
using Microsoft.LearningComponents;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    class FileSystemPackageTests
    {
        private FileSystemPackage fileSystemPackage;

        [SetUp]
        public void FileSystemPackageTestsSetUp()
        {
            fileSystemPackage = new FileSystemPackage("IUDICO/TestingSystem/Models/VOs/", 12345,
                                                      new DateTime(2011, 11, 11), "package.zip", 1);
        }

        [Test]
        public void FileSystemPackagePropertiesTest1()
        {
            FileSystemPackageTestsSetUp();
            Assert.AreEqual(fileSystemPackage.CourseID, 1);
            Assert.AreEqual(fileSystemPackage.FileName, "package.zip");
            Assert.AreEqual(fileSystemPackage.FolderPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(fileSystemPackage.Owner, 12345);
            Assert.AreEqual(fileSystemPackage.UploadDateTime, new DateTime(2011, 11, 11));
        }

        [Test]
        public void FileSystemPackagePropertiesTest2()
        {
            fileSystemPackage = new FileSystemPackage("IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(fileSystemPackage.CourseID, default(int));
            Assert.AreEqual(fileSystemPackage.FileName, default(string));
            Assert.AreEqual(fileSystemPackage.FolderPath, "IUDICO/TestingSystem/Models/VOs/");
            Assert.AreEqual(fileSystemPackage.Owner, default(long));
            Assert.AreEqual(fileSystemPackage.UploadDateTime, null);
        }

        [Test]
        public void FileSystemPackageGetPackageReaderTest()
        {
            FileSystemPackageTestsSetUp();
            Assert.That(fileSystemPackage.GetPackageReader(), Is.TypeOf(typeof(FileSystemPackageReader)));
            Assert.That(fileSystemPackage.GetPackageReader(), Is.Not.Null);
        }
    }
}
