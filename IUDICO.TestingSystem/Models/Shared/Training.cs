using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.LearningComponents.Storage;
using Microsoft.LearningComponents;
using LearningComponentsHelper;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.TS.Models.Shared
{
    [DisplayName("Training")]
    public class Training
    {
        #region Public Properties

        [DisplayName("Package ID")]
        public long? PackageID { get; set; }

        [DisplayName("File Name")]
        public string PackageFileName { get; set; }

        [DisplayName("Organization ID")]
        public long? OrganizationID { get; set; }

        [DisplayName("Organization Title")]
        public string OrganizationTitle { get; set; }

        [DisplayName("Attempt ID")]
        public long? AttemptID { get; set; }
        
        [DisplayName("Upload Time")]
        public DateTime? UploadDateTime { get; set; }

        [DisplayName("Status")]
        [DisplayFormat(NullDisplayText="Not Started")]
        public AttemptStatus? AttemptStatusProp { get; set; }
        
        [DisplayName("Score")]
        public float? TotalPoints { get; set; }
        
        
        public long? PlayID
        {
            get
            {
                if (this.AttemptStatusProp != null)
                {
                    switch (this.AttemptStatusProp)
                    {
                        case AttemptStatus.Active:
                            return this.AttemptID;
                        case AttemptStatus.Suspended:
                            return this.AttemptID;
                        case AttemptStatus.Abandoned:
                            return this.AttemptID;
                        default:
                            return this.OrganizationID;
                    }
                }
                else
                {
                    return this.OrganizationID;
                }
            }
        }

        #endregion

        #region Constructors

        public Training(long? pID, string packageFileName, long? orgID, string orgTitle, long? attemptID, DateTime? uploadDateTime, AttemptStatus? attemptStatus, float? totalPoints)
        {
            this.PackageID = pID;
            this.PackageFileName = packageFileName;
            this.OrganizationID = orgID;
            this.OrganizationTitle = orgTitle;
            this.AttemptID = attemptID;
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
            long? pID;
            if (packageId == null)
            {
                pID = null;
            }
            else
            {
                pID = packageId.GetKey();
            }
            string packageFileName;
            LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.PackageFileName],
                out packageFileName);
            ActivityPackageItemIdentifier organizationId;
            LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationId],
                out organizationId);
            long? orgID;
            if (organizationId == null)
            {
                orgID = null;
            }
            else
            {
                orgID = organizationId.GetKey();
            }
            string organizationTitle;
            LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationTitle],
                out organizationTitle);
            AttemptItemIdentifier attemptId;
            LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.AttemptId],
                out attemptId);
            long? attID;
            if (attemptId == null)
            {
                attID = null;
            }
            else
            {
                attID = attemptId.GetKey();
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

            this.PackageID = pID;
            this.PackageFileName = packageFileName;
            this.OrganizationID = orgID;
            this.OrganizationTitle = organizationTitle;
            this.AttemptID = attID;
            this.UploadDateTime = uploadDateTime;
            this.AttemptStatusProp = attemptStatus;
            this.TotalPoints = score;
        }

        #endregion
    }
}