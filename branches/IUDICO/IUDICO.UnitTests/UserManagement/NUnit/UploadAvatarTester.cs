using System;
using System.Configuration;
using System.IO;
using System.Web;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    internal class UploadAvatarTester
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        protected string iudicoPath = Path.Combine(ConfigurationManager.AppSettings["PathToIUDICO.UnitTests"], "IUDICO.LMS");

        private class MemoryFile : HttpPostedFileBase
        {
            private readonly Stream stream;
            private readonly string contentType;
            private readonly string fileName;

            public MemoryFile(Stream stream, string contentType, string fileName)
            {
                this.stream = stream;
                this.contentType = contentType;
                this.fileName = fileName;
            }

            public override int ContentLength
            {
                get
                {
                    return (int)this.stream.Length;
                }
            }

            public override string ContentType
            {
                get { return this.contentType; }
            }

            public override string FileName
            {
                get { return this.fileName; }
            }

            public override Stream InputStream
            {
                get { return this.stream; }
            }

            public override void SaveAs(string filename)
            {
                using (var file = File.Open(filename, FileMode.CreateNew))
                {
                    this.stream.CopyTo(file);
                }
            }
        }

        // Upload the photo, that doesn't exist in the ~/Data/Avatars directory -> check if the file is being copied
        [Test]
        public void UploadNewPhoto()
        {
            var id = Guid.NewGuid();

            var filePath = Path.Combine(Path.Combine(this.iudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                const string FileType = "image/png";
                var postedFile = new MemoryFile(fileStream, FileType, filePath);

                this.tests.Storage.UploadAvatar(id, postedFile);
            }

            filePath = Path.Combine(Path.Combine(this.iudicoPath, @"Data\Avatars"), Path.GetFileName(id + ".png"));
            
            var fileInfo = new FileInfo(filePath);

            Assert.IsTrue(fileInfo.Exists);
        }

        // Upload photo with the same name -> check if the file is being replaced
        [Test]
        public void ReplaceExistingPhoto()
        {
            var id = Guid.NewGuid();
            var filePath = Path.Combine(Path.Combine(this.iudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));
            const string FileType = "image/png";

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var postedFile = new MemoryFile(fileStream, FileType, filePath);

                this.tests.Storage.UploadAvatar(id, postedFile);

                filePath = Path.Combine(Path.Combine(this.iudicoPath, @"Data\Avatars"), Path.GetFileName("test2.png"));
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var postedFile = new MemoryFile(fileStream, FileType, filePath);

                Assert.IsTrue(this.tests.Storage.UploadAvatar(id, postedFile) == 2);
            }
        }

        // Upload null file
        [Test]
        public void UploadNullFile()
        {
            var id = Guid.NewGuid();

            Assert.IsTrue(this.tests.Storage.UploadAvatar(id, null) == -1);
        }

        // Delete existing photo -> check if the file is being deleted
        [Test]
        public void DeleteExixtingPhoto()
        {
            var id = Guid.NewGuid();
            var filePath = Path.Combine(Path.Combine(this.iudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));
            const string FileType = "image/png";

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var postedFile = new MemoryFile(fileStream, FileType, filePath);

                this.tests.Storage.UploadAvatar(id, postedFile);
                this.tests.Storage.DeleteAvatar(id);
            }

            filePath = Path.Combine(Path.Combine(this.iudicoPath, @"Data\Avatars"), Path.GetFileName(id + ".png"));

            var fileInfo = new FileInfo(filePath);

            Assert.IsTrue(fileInfo.Exists == false);
        }

        // Delete not existing photo
        [Test]
        public void DeleteNotExistingPhoto()
        {
            var id = Guid.NewGuid();
            var filePath = Path.Combine(Path.Combine(this.iudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));
            const string FileType = "image/png";

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var postedFile = new MemoryFile(fileStream, FileType, filePath);

                this.tests.Storage.UploadAvatar(id, postedFile);
                this.tests.Storage.DeleteAvatar(id);
            }

            Assert.IsTrue(this.tests.Storage.DeleteAvatar(id) == -1);
        }
    }
}