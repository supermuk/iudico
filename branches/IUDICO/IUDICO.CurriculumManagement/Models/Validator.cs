using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Models
{
    /// <summary>
    /// Validator class.
    /// </summary>
    /// <remarks></remarks>
    public class Validator
    {
        private ICurriculumStorage storage { get; set; }

        public Validator(ICurriculumStorage storage)
        {
            this.storage = storage;
        }

        /// <summary>
        /// Validates the timeline.
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public ValidationStatus ValidateTimeline(Timeline timeline)
        {
            DateTime minAllowedDate = Constants.MinAllowedDateTime;
            DateTime maxAllowedDate = Constants.MaxAllowedDateTime;
            if (timeline.StartDate > timeline.EndDate || timeline.StartDate < minAllowedDate || timeline.EndDate > maxAllowedDate)
            {
                return new ValidationStatus(false, String.Format("Date must be between {0} and {1}",
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }
            else
            {
                return new ValidationStatus(true, "");
            }
        }

        /// <summary>
        /// Validates the group index in CurriculumAssignment
        /// </summary>
        /// <param name="groupId">The group index</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumAssignment(int groupId)
        {
            if(groupId <= 0)
            {
                return new ValidationStatus(false,String.Format("Index must be bigger than 0"));
            }
            else
            {
                return new ValidationStatus(true, "");
            }
        }
    }
}