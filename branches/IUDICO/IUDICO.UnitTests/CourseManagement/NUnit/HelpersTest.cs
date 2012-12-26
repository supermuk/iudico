using System.IO;
using System.Linq;
using System.Net.Mime;
using IUDICO.CourseManagement.Helpers;

using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    using System;

    [TestFixture]
    public class HelpersTest : BaseCourseManagementTest
    {
        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void CreateZipTest()
        {
            // creating the folder 
            var folder = Path.Combine(this.root, @"New Folder");
            // creating zip file
            var zip = Path.Combine(this.root, @"1.zip");

            // push folder into zip
            Zipper.CreateZip(zip, folder);

            Assert.IsTrue(File.Exists(zip));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void ExtractZipFileTest()
        {
            // creating the folder 
            var folder = Path.Combine(this.root, @"Zipped Folder");
            // creating zip file from which we extract
            var zip = Path.Combine(this.root, @"Zipped Folder.zip");

            //extracting into folder
            Zipper.ExtractZipFile(zip, folder);

            Assert.IsTrue(Directory.Exists(folder));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void PackageValidatorValidPackageTest()
        {
            //path to file which we want to validate
            var path = Path.Combine(this.root, @"Valid package.zip");
            bool valid;
            var res = PackageValidator.Validate(path, out valid);

            Assert.IsTrue(res.Count == 1);
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void PackageValidatorInvalidPackageTest()
        {
            //path to file which we want to validate
            var path = Path.Combine(this.root, @"Invalid package.zip");
            bool valid;
            var res = PackageValidator.Validate(path, out valid);

            Assert.IsTrue(res.Count == 6);
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void FileHelperDirectoryCopyTest()
        {
            //copy from
            var from = Path.Combine(this.root, @"New Folder");
            //copy to
            var to = Path.Combine(this.root, @"New Folder Copy" + Guid.NewGuid());

            Directory.CreateDirectory(to);

            FileHelper.DirectoryCopy(from, to);

            Assert.IsTrue(Directory.Exists(to));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void FileHelperDirectoryCreateTest()
        {
            //path for directory
            var dir = Path.Combine(this.root, @"New Folder Create");

            //creating directory
            FileHelper.DirectoryCreate(dir);

            Assert.IsTrue(Directory.Exists(dir));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void FileHelperDirectoryExistsTest()
        {
            var dir = Path.Combine(this.root, @"New Folder");

            Assert.IsTrue(FileHelper.DirectoryExists(dir));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        public void FileHelperDirectoryNotExistsTest()
        {
            var dir = Path.Combine(this.root, @"Not Existing New Folder");

            Assert.IsFalse(FileHelper.DirectoryExists(dir));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        // Test gets files from directory 
        public void FileHelperDirectoryGetFilesTest()
        {
            var dir = Path.Combine(this.root, @"New Folder");
            var file = Path.Combine(dir, "New Document.txt");

            var res = FileHelper.DirectoryGetFiles(dir).ToArray();

            Assert.IsTrue(res.Count() == 1 && res[0] == file);
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        // Test writes into file
        public void FileHelperFileWriteTest()
        {
            var file = Path.Combine(this.root, @"New Document Write.txt");

            FileHelper.FileWrite(file, "test");

            Assert.IsTrue(File.Exists(file));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        // Test reads from file
        public void FileHelperFileReadTest()
        {
            var file = Path.Combine(this.root, @"New Folder\New Document.txt");

            Assert.IsTrue(FileHelper.FileRead(file) == "test");
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        // Test copies from "from" to "to"file
        public void FileHelperFileCopyTest()
        {
            var from = Path.Combine(this.root, @"New Folder\New Document.txt");
            var to = Path.Combine(this.root, @"New Document Copy.txt");

            FileHelper.FileCopy(from, to, true);

            Assert.IsTrue(File.Exists(to));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        // Test deletes the file
        public void FileHelperFileDeleteTest()
        {
            var file = Path.Combine(this.root, @"New Document Delete.txt");

            FileHelper.FileDelete(file);

            Assert.IsFalse(File.Exists(file));
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        // Test watches is file existing
        public void FileHelperFileExistsTest()
        {
            var file = Path.Combine(this.root, @"New Folder\New Document.txt");

            Assert.IsTrue(FileHelper.FileExists(file));
        }
    }
}