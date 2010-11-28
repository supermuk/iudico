using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.LearningComponents.Storage;
using Microsoft.LearningComponents;

namespace IUDICO.TS.Models.Shared
{
    public class Training
    {
        #region Public Properties

        public long? PackageID { get; set; }

        public string PackageFileName { get; set; }

        public long? OrganizationID { get; set; }

        public string OrganizationTitle { get; set; }

        public long? AttemptID { get; set; }
        
        public DateTime? UploadDateTime { get; set; }

        public AttemptStatus? AttemptStatusProp { get; set; }
               
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

        #endregion
    }
}