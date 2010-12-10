using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        public Package(long ownerID, DateTime? uploadDateTime, string fileName)
        {
            this.Owner = ownerID;
            this.UploadDateTime = uploadDateTime;
            this.FileName = fileName;
        }

        #endregion

        #region Public Methods

        public abstract PackageReader GetPackageReader();

        #endregion
    }
}