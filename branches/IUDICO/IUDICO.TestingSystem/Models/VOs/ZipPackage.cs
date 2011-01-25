using System;
using System.ComponentModel;
using Microsoft.LearningComponents;

namespace IUDICO.TestingSystem.Models.VO
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
            ZipPath = zipPath;
        }

        public ZipPackage(string zipPath, long ownerId, DateTime? uploadDateTime, string fileName)
            : base(ownerId, uploadDateTime, fileName)
        {
            ZipPath = zipPath;
        }

        #endregion

        #region Methods

        public override PackageReader GetPackageReader()
        {
            var reader = new ZipPackageReader(ZipPath);

            return reader;
        }

        #endregion
    }
}