using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.LearningComponents;
using System.IO;

namespace IUDICO.TS.Models.Shared
{
    public class FileSystemPackage : Package
    {
        #region Properties

        public string FolderPath { get; set; }

        #endregion

        #region Constructors

        public FileSystemPackage(string folderPath)
            : base()
        {
            this.FolderPath = folderPath;
        }

        public FileSystemPackage(string folderPath, long? ownerID, DateTime? uploadDateTime, string fileName)
            : base(ownerID, uploadDateTime, fileName)
        {
            this.FolderPath = folderPath;
        }

        #endregion

        #region Public Methods

        public override PackageReader GetPackageReader()
        {
            FileSystemPackageReader reader = new FileSystemPackageReader(this.FolderPath);
            return reader;
        }

        #endregion
    }
}