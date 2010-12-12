using System;
using System.ComponentModel;
using Microsoft.LearningComponents;

namespace IUDICO.TestingSystem.Models.Shared
{
    public abstract class Package
    {
        #region Public Properties

        public long Owner { get; set; }

        [DisplayName("Upload Time")]
        public DateTime? UploadDateTime { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; }

        #endregion

        #region Constructors

        public Package()
        {

        }

        public Package(long ownerId, DateTime? uploadDateTime, string fileName)
        {
            Owner = ownerId;
            UploadDateTime = uploadDateTime;
            FileName = fileName;
        }

        #endregion

        #region Public Methods

        public abstract PackageReader GetPackageReader();

        #endregion
    }
}