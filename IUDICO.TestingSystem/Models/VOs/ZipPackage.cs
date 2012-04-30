// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipPackage.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;

using Microsoft.LearningComponents;

namespace IUDICO.TestingSystem.Models.VOs
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

        public ZipPackage(string zipPath, long ownerId, DateTime? uploadDateTime, string fileName, int courseId)
            : base(ownerId, uploadDateTime, fileName, courseId)
        {
            this.ZipPath = zipPath;
        }

        #endregion

        #region Methods

        public override PackageReader GetPackageReader()
        {
            var reader = new ZipPackageReader(this.ZipPath);

            return reader;
        }

        #endregion
    }
}