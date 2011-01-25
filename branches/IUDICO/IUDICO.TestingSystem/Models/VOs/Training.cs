using System;
using Microsoft.LearningComponents.Storage;
using Microsoft.LearningComponents;
using LearningComponentsHelper;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.TestingSystem.Models.VO
{
    [DisplayName("Training")]
    public class Training
    {
        #region Public Properties

        [DisplayName("Package ID")]
        public long? PackageId { get; set; }

        [DisplayName("File Name")]
        public string PackageFileName { get; set; }

        [DisplayName("Organization ID")]
        public long? OrganizationId { get; set; }

        [DisplayName("Organization Title")]
        public string OrganizationTitle { get; set; }

        [DisplayName("Attempt ID")]
        public long? AttemptId { get; set; }
        
        [DisplayName("Upload Time")]
        public DateTime? UploadDateTime { get; set; }

        [DisplayName("Status")]
        [DisplayFormat(NullDisplayText="Not Started")]
        public AttemptStatus? AttemptStatusProp { get; set; }
        
        [DisplayName("Score")]
        public float? TotalPoints { get; set; }
        
        
        public long? PlayId
        {
            get
            {
                if (AttemptStatusProp == null)
                {
                    return OrganizationId;
                }

                switch (AttemptStatusProp)
                {
                    case AttemptStatus.Active:
                        return AttemptId;
                    case AttemptStatus.Suspended:
                        return AttemptId;
                    case AttemptStatus.Abandoned:
                        return AttemptId;
                    default:
                        return OrganizationId;
                }
            }
        }

        #endregion

        #region Constructors

        public Training(long? pId, string packageFileName, long? orgId, string orgTitle, long? attemptId, DateTime? uploadDateTime, AttemptStatus? attemptStatus, float? totalPoints)
        {
            this.PackageId = pId;
            this.PackageFileName = packageFileName;
            this.OrganizationId = orgId;
            this.OrganizationTitle = orgTitle;
            this.AttemptId = attemptId;
            this.UploadDateTime = uploadDateTime;
            this.AttemptStatusProp = attemptStatus;
            this.TotalPoints = totalPoints;
        }

        public Training(DataRow dataRow)
        {
            // extract information from <dataRow> into local variables
            PackageItemIdentifier packageId;
            LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.PackageId],
                out packageId);

            long? pId;

            if (packageId == null)
            {
                pId = null;
            }
            else
            {
                pId = packageId.GetKey();
            }

            string packageFileName;

            LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.PackageFileName],
                out packageFileName);

            ActivityPackageItemIdentifier organizationId;
            LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationId],
                out organizationId);

            long? orgId;

            if (organizationId == null)
            {
                orgId = null;
            }
            else
            {
                orgId = organizationId.GetKey();
            }

            string organizationTitle;

            LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationTitle],
                out organizationTitle);

            AttemptItemIdentifier attemptId;

            LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.AttemptId],
                out attemptId);

            long? attId;

            if (attemptId == null)
            {
                attId = null;
            }
            else
            {
                attId = attemptId.GetKey();
            }

            DateTime? uploadDateTime;

            LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.UploadDateTime],
                out uploadDateTime);

            AttemptStatus? attemptStatus;

            LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.AttemptStatus],
                out attemptStatus);

            float? score;

            LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.TotalPoints],
                out score);

            PackageId = pId;
            PackageFileName = packageFileName;
            OrganizationId = orgId;
            OrganizationTitle = organizationTitle;
            AttemptId = attId;
            UploadDateTime = uploadDateTime;
            AttemptStatusProp = attemptStatus;
            TotalPoints = score;
        }

        #endregion
    }
}