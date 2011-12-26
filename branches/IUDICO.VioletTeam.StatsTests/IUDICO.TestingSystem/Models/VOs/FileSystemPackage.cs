using System;
using System.ComponentModel;
using Microsoft.LearningComponents;

namespace IUDICO.TestingSystem.Models.VOs
{
    public class FileSystemPackage : Package
    {
        #region Properties

        [DisplayName("Folder Path")]
        public string FolderPath { get; set; }

        #endregion

        #region Constructors

        public FileSystemPackage(string folderPath)
            : base()
        {
            FolderPath = folderPath;
        }

        public FileSystemPackage(string folderPath, long ownerId, DateTime? uploadDateTime, string fileName, int courseId)
            : base(ownerId, uploadDateTime, fileName, courseId)
        {
            FolderPath = folderPath;
        }

        #endregion

        #region Public Methods

        public override PackageReader GetPackageReader()
        {
            var reader = new FileSystemPackageReader(FolderPath);

            return reader;
        }

        #endregion
    }
}