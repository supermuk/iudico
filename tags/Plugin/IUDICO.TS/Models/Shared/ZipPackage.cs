using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Microsoft.LearningComponents;

namespace IUDICO.TS.Models.Shared
{
    public class ZipPackage : Package
    {
        #region Public Properties

        [DisplayName("Zip File Path")]
        public string ZipPath { get; set; }

        #endregion

        #region Constructors

        public ZipPackage(string zipPath)
            : base()
        {
            this.ZipPath = zipPath;
        }

        public ZipPackage(string zipPath, long ownerID, DateTime? uploadDateTime, string fileName)
            : base(ownerID, uploadDateTime, fileName)
        {
            this.ZipPath = zipPath;
        }

        #endregion

        #region Methods

        public override Microsoft.LearningComponents.PackageReader GetPackageReader()
        {
            ZipPackageReader reader = new ZipPackageReader(this.ZipPath);
            return reader;
        }

        #endregion
    }
}