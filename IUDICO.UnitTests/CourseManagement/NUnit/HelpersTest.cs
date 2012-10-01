using System.IO;
using System.Linq;

using IUDICO.CourseManagement.Helpers;

using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    using System;

    [TestFixture]
    public class HelpersTest : BaseCourseManagementTest
    {
        [Test]
        [Category("CreateZipTest")]
        public void CreateZipTest()
        {
            var folder = Path.Combine(this.root, @"New Folder");

            var zip = Path.Combine(this.root, "1.zip");

            Zipper.CreateZip(zip, folder);

            Assert.IsTrue(File.Exists(zip));
        }

        [Test]
        [Category("CreateZipTest")]
        public void ExtractZipFileTest()
        {
            var folder = Path.Combine(this.root, "Zipped Folder");

            var zip = Path.Combine(this.root, @"Zipped Folder.zip");

            Zipper.ExtractZipFile(zip, folder);

            Assert.IsTrue(Directory.Exists(folder));
        }

        [Test]
        public void PackageValidatorValidPackageTest()
        {
            var path = Path.Combine(this.root, "Valid package.zip");
            bool valid;
            var res = PackageValidator.Validate(path, out valid);

            Assert.IsTrue(res.Count == 1);
        }

        [Test]
        public void PackageValidatorInvalidPackageTest()
        {
            var path = Path.Combine(this.root, "Invalid package.zip");
            bool valid;
            var res = PackageValidator.Validate(path, out valid);

            Assert.IsTrue(res.Count == 6);
        }

        [Test]
        public void FileHelperDirectoryCopyTest()
        {
            var from = Path.Combine(this.root, "New Folder");
            var to = Path.Combine(this.root, "New Folder Copy" + Guid.NewGuid());

            Directory.CreateDirectory(to);

            FileHelper.DirectoryCopy(from, to);

            Assert.IsTrue(Directory.Exists(to));
        }

        [Test]
        public void FileHelperDirectoryCreateTest()
        {
            var dir = Path.Combine(this.root, "New Folder Create");

            FileHelper.DirectoryCreate(dir);

            Assert.IsTrue(Directory.Exists(dir));
        }

        [Test]
        public void FileHelperDirectoryExistsTest()
        {
            var dir = Path.Combine(this.root, "New Folder");

            Assert.IsTrue(FileHelper.DirectoryExists(dir));
        }

        [Test]
        public void FileHelperDirectoryNotExistsTest()
        {
            var dir = Path.Combine(this.root, "Not Existing New Folder");

            Assert.IsFalse(FileHelper.DirectoryExists(dir));
        }

        [Test]
        public void FileHelperDirectoryGetFilesTest()
        {
            var dir = Path.Combine(this.root, "New Folder");
            var file = Path.Combine(dir, "New Document.txt");

            var res = FileHelper.DirectoryGetFiles(dir).ToArray();

            Assert.IsTrue(res.Count() == 1 && res[0] == file);
        }

        [Test]
        public void FileHelperFileWriteTest()
        {
            var file = Path.Combine(this.root, "New Document Write.txt");

            FileHelper.FileWrite(file, "test");

            Assert.IsTrue(File.Exists(file));
        }

        [Test]
        public void FileHelperFileReadTest()
        {
            var file = Path.Combine(this.root, @"New Folder\New Document.txt");

            Assert.IsTrue(FileHelper.FileRead(file) == "test");
        }

        [Test]
        public void FileHelperFileCopyTest()
        {
            var from = Path.Combine(this.root, @"New Folder\New Document.txt");
            var to = Path.Combine(this.root, @"New Document Copy.txt");

            FileHelper.FileCopy(from, to, true);

            Assert.IsTrue(File.Exists(to));
        }

        [Test]
        public void FileHelperFileDeleteTest()
        {
            var file = Path.Combine(this.root, @"New Document Delete.txt");

            FileHelper.FileDelete(file);

            Assert.IsFalse(File.Exists(file));
        }

        [Test]
        public void FileHelperFileExistsTest()
        {
            var file = Path.Combine(this.root, @"New Folder\New Document.txt");

            Assert.IsTrue(FileHelper.FileExists(file));
        }
    }
}