using System;
using System.ComponentModel;
using Microsoft.LearningComponents;

namespace IUDICO.TestingSystem.Models.VO
{
    public abstract class Package
    {
        #region Public Properties

        public long Owner { get; set; }

        public DateTime? UploadDateTime { get; set; }

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