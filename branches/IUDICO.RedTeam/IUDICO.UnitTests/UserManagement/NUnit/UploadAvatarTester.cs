using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Notifications;
using IUDICO.UserManagement.Models;
using Moq;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    class UploadAvatarTester
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        class MemoryFile : HttpPostedFileBase
        {
            Stream stream;
            string contentType;
            string fileName; 

            public MemoryFile(Stream stream, string contentType, string fileName)
            {
                this.stream = stream;
                this.contentType = contentType;
                this.fileName = fileName;
            }

            public override int ContentLength
            { 
                get { return (int)stream.Length; }
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
                    stream.CopyTo(file);
            }
        }

        // Upload the photo, that doesn't exist in the ~/Data/Avatars directory -> check if the file is being copied
        [Test]
        public void UploadNewPhoto()
        {
            Guid id = Guid.NewGuid();
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/Avatars"), Path.GetFileName("test.png"));
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            string fileType = "image/png";

            MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
            _Tests.Storage.UploadAvatar(id, postedFile);

            filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/Avatars"), Path.GetFileName(id.ToString() + ".png"));
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Exists == true);
        }

        // Upload photo with the same name -> check if the file is being replaced
        [Test]
        public void ReplaceExistingPhoto()
        {
            Guid id = Guid.NewGuid();
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/Avatars"), Path.GetFileName("test.png"));
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            string fileType = "image/png";

            MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
            _Tests.Storage.UploadAvatar(id, postedFile);

            filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/Avatars"), Path.GetFileName("test2.png"));
            fileStream = new FileStream(filePath, FileMode.Open);
            postedFile = new MemoryFile(fileStream, fileType, filePath);

            Assert.IsTrue(_Tests.Storage.UploadAvatar(id, postedFile) == 2);
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
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/Avatars"), Path.GetFileName("test.png"));
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            string fileType = "image/png";

            MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
            _Tests.Storage.UploadAvatar(id, postedFile);
            _Tests.Storage.DeleteAvatar(id);

            filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/Avatars"), Path.GetFileName(id.ToString() + ".png"));
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.IsTrue(fileInfo.Exists == false);
        }

        // Delete not existing photo
        [Test]
        public void DeleteNotExistingPhoto()
        {
            Guid id = Guid.NewGuid();
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/Avatars"), Path.GetFileName("test.png"));
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            string fileType = "image/png";

            MemoryFile postedFile = new MemoryFile(fileStream, fileType, filePath);
            _Tests.Storage.UploadAvatar(id, postedFile);
            _Tests.Storage.DeleteAvatar(id);

            Assert.IsTrue(_Tests.Storage.DeleteAvatar(id) == -1);
        }
    }
}
