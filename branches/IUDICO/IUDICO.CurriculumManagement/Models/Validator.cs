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
        /// Validates the stage timeline.
        /// </summary>
        /// <param name="timeline">The stage timeline.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public ValidationStatus ValidateStageTimeline(Timeline timeline)
        {
            ValidationStatus validationStatus=new ValidationStatus();

            DateTime minAllowedDate = Constants.MinAllowedDateTime;
            DateTime maxAllowedDate = Constants.MaxAllowedDateTime;
            if (timeline.StageRef <= 0)
            {
                validationStatus.Errors.Add(String.Format("Choose a stage."));
            }
            if (timeline.StartDate > timeline.EndDate)
            {
                validationStatus.Errors.Add("Start date must be less than end date.");
            }
            if (timeline.StartDate < minAllowedDate || timeline.StartDate > maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format("Start date must be between {0} and {1}.",
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }
            if (timeline.EndDate < minAllowedDate || timeline.EndDate > maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format("End date must be between {0} and {1}.",
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }
            
            var timelines = storage.GetCurriculumAssignmentTimelines(timeline.CurriculumAssignmentRef).Where(item => item.Id != timeline.Id);

            bool errorCheck = false;
            foreach (var item in timelines)
            {
                if (item.StageRef == null)
                {
                    if (timeline.StartDate >= item.StartDate && timeline.EndDate <= item.EndDate)
                    {
                        errorCheck = true;
                        break;
                    }
                }
            }
            if (errorCheck == false)
                validationStatus.Errors.Add("Stage timeline is bigger than curriculum timeline");

            return validationStatus;
        }

        /// <summary>
        /// Validates the curriculum assignment timeline.
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumAssignmentTimeline(Timeline timeline)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            DateTime minAllowedDate = Constants.MinAllowedDateTime;
            DateTime maxAllowedDate = Constants.MaxAllowedDateTime;
            if (timeline.StartDate > timeline.EndDate)
            {
                validationStatus.Errors.Add("Start date must be less than end date.");
            }
            if (timeline.StartDate < minAllowedDate || timeline.StartDate >maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format("Start date must be between {0} and {1}.",
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }
            if (timeline.EndDate < minAllowedDate || timeline.EndDate > maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format("End date must be between {0} and {1}.",
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }

            return validationStatus;
        }

        /// <summary>
        /// Validates the curriculum assignment.
        /// </summary>
        /// <param name="curriculumAssignment">The curriculum assignment.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumAssignment(CurriculumAssignment curriculumAssignment)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            if(curriculumAssignment.UserGroupRef <= 0)
            {
                validationStatus.Errors.Add(String.Format("Choose a group."));
            }

            return validationStatus;
        }

        /// <summary>
        /// Validates the theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <returns></returns>
        public ValidationStatus ValidateTheme(Theme theme)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            if (theme.CourseRef <= 0)
            {
                validationStatus.Errors.Add(String.Format("Choose a course."));
            }
            if (theme.ThemeTypeRef <= 0)
            {
                validationStatus.Errors.Add(String.Format("Choose a theme type."));
            }

            return validationStatus;
        }
   }
}