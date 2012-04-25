// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemPackage.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
            this.FolderPath = folderPath;
        }

        public FileSystemPackage(
            string folderPath, long ownerId, DateTime? uploadDateTime, string fileName, int courseId)
            : base(ownerId, uploadDateTime, fileName, courseId)
        {
            this.FolderPath = folderPath;
        }

        #endregion

        #region Public Methods

        public override PackageReader GetPackageReader()
        {
            var reader = new FileSystemPackageReader(this.FolderPath);

            return reader;
        }

        #endregion
    }
}