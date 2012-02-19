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
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        protected string _IudicoPath = Path.Combine(ConfigurationManager.AppSettings["PathToIUDICO.UnitTests"], "IUDICO.LMS");

        private class MemoryFile : HttpPostedFileBase
        {
            private Stream stream;
            private string contentType;
            private string fileName;

            public MemoryFile(Stream stream, string contentType, string fileName)
            {
                this.stream = stream;
                this.contentType = contentType;
                this.fileName = fileName;
            }

            public override int ContentLength
            {
                get { return (int) stream.Length; }
            }

            public override string ContentType
            {
                get { return contentType; }
            }

            public override string FileName
            {
                get { return fileName; }
            }

            public override Stream InputStream
            {
                get { return stream; }
            }

            public override void SaveAs(string filename)
            {
                using (var file = File.Open(filename, FileMode.CreateNew))
                {
                    stream.CopyTo(file);
                }
            }
        }

        // Upload the photo, that doesn't exist in the ~/Data/Avatars directory -> check if the file is being copied
        [Test]
        public void UploadNewPhoto()
        {
            Guid id = Guid.NewGuid();

            string filePath = Path.Combine(Path.Combine(_IudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                string fileType = "image/png";

                MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
                _Tests.Storage.UploadAvatar(id, postedFile);
            }

            filePath = Path.Combine(Path.Combine(_IudicoPath, @"Data\Avatars"), Path.GetFileName(id + ".png"));
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Exists);
        }

        // Upload photo with the same name -> check if the file is being replaced
        [Test]
        public void ReplaceExistingPhoto()
        {
            Guid id = Guid.NewGuid();
            string filePath = Path.Combine(Path.Combine(_IudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));
            string fileType = "image/png";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
                _Tests.Storage.UploadAvatar(id, postedFile);

                filePath = Path.Combine(Path.Combine(_IudicoPath, @"Data\Avatars"), Path.GetFileName("test2.png"));
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);

                Assert.IsTrue(_Tests.Storage.UploadAvatar(id, postedFile) == 2);
            }
        }

        // Upload null file
        [Test]
        public void UploadNullFile()
        {
            Guid id = Guid.NewGuid();

            Assert.IsTrue(_Tests.Storage.UploadAvatar(id, null) == -1);
        }

        // Delete existing photo -> check if the file is being deleted
        [Test]
        public void DeleteExixtingPhoto()
        {
            Guid id = Guid.NewGuid();
            string filePath = Path.Combine(Path.Combine(_IudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));
            string fileType = "image/png";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
                _Tests.Storage.UploadAvatar(id, postedFile);
                _Tests.Storage.DeleteAvatar(id);
            }

            filePath = Path.Combine(Path.Combine(_IudicoPath, @"Data\Avatars"), Path.GetFileName(id + ".png"));
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Exists == false);
        }

        // Delete not existing photo
        [Test]
        public void DeleteNotExistingPhoto()
        {
            Guid id = Guid.NewGuid();
            string filePath = Path.Combine(Path.Combine(_IudicoPath, @"Data\Avatars"), Path.GetFileName("test.png"));
            string fileType = "image/png";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
                _Tests.Storage.UploadAvatar(id, postedFile);
                _Tests.Storage.DeleteAvatar(id);
            }

            Assert.IsTrue(_Tests.Storage.DeleteAvatar(id) == -1);
        }
    }
}