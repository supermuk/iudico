using System;
using Microsoft.LearningComponents.Storage;
using Microsoft.LearningComponents;
using LearningComponentsHelper;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.TestingSystem.Models.VO
{
    public class Training
    {
        #region Public Properties

        public long? PackageId { get; set; }
        
        public long? OrganizationId { get; set; }
        
        public long? AttemptId { get; set; }
        
        public DateTime? UploadDateTime { get; set; }

        public AttemptStatus? AttemptStatusProp { get; set; }
        
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

        public Training(long? pId, long? orgId, long? attemptId, AttemptStatus? attemptStatus, float? totalPoints)
        {
            this.PackageId = pId;
            this.OrganizationId = orgId;
            this.AttemptId = attemptId;
            this.AttemptStatusProp = attemptStatus;
            this.TotalPoints = totalPoints;
        }

        public Training(DataRow dataRow)
        {
            // extract information from <dataRow> into local variables
            PackageItemIdentifier packageId;
            LStoreHelper.CastNonNull(dataRow[Schema.MyAttempts.PackageId],
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


            ActivityPackageItemIdentifier organizationId;
            LStoreHelper.CastNonNull(dataRow[Schema.MyAttempts.OrganizationId],
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


            AttemptItemIdentifier attemptId;

            LStoreHelper.Cast(dataRow[Schema.MyAttempts.AttemptId],
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
            
            AttemptStatus? attemptStatus;

            LStoreHelper.Cast(dataRow[Schema.MyAttempts.AttemptStatus],
                out attemptStatus);

            float? score;

            LStoreHelper.Cast(dataRow[Schema.MyAttempts.TotalPoints],
                out score);

            PackageId = pId;
            OrganizationId = orgId;
            AttemptId = attId;
            AttemptStatusProp = attemptStatus;
            TotalPoints = score;
        }

        #endregion
    }
}