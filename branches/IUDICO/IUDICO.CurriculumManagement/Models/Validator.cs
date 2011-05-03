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
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("ChooseStage")));
            }
            if (timeline.StartDate > timeline.EndDate)
            {
                validationStatus.Errors.Add(IUDICO.CurriculumManagement.Localization.getMessage("StartDateMustLessThanEndDate"));
            }
            if (timeline.StartDate < minAllowedDate || timeline.StartDate > maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("StartDateMustBeBetween"),
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }
            if (timeline.EndDate < minAllowedDate || timeline.EndDate > maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("EndDateMustBeBetween"),
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }
            
            var timelines = storage.GetCurriculumAssignmentTimelines(timeline.CurriculumAssignmentRef).Where(item => item.Id != timeline.Id);

            bool errorCheck = false;
            foreach (var item in timelines)
            {
                if (item.StageRef == null)
                {
                    if (timeline.StartDate < item.StartDate || timeline.EndDate > item.EndDate)
                    {
                        errorCheck = true;
                        break;
                    }
                }
            }
            if (errorCheck == true)
                validationStatus.Errors.Add(IUDICO.CurriculumManagement.Localization.getMessage("StageTimelineBiggerThanCurriculumTimeline"));

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
                validationStatus.Errors.Add(IUDICO.CurriculumManagement.Localization.getMessage("StartDateMustLessThanEndDate"));
            }
            if (timeline.StartDate < minAllowedDate || timeline.StartDate >maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("StartDateMustBeBetween"),
                    minAllowedDate.ToString(), maxAllowedDate.ToString()));
            }
            if (timeline.EndDate < minAllowedDate || timeline.EndDate > maxAllowedDate)
            {
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("EndDateMustBeBetween"),
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
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("ChooseStage")));
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
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("ChooseCourse")));
            }
            if (theme.ThemeTypeRef <= 0)
            {
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("ChooseThemeType")));
            }
            if (theme.Name == null || theme.Name == "")
            {
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("NameReqiured")));
            }
            if (theme.Name != null && theme.Name.Length > Constants.MaxStringFieldLength)
            {
                validationStatus.Errors.Add(String.Format(IUDICO.CurriculumManagement.Localization.getMessage("NameCanNotBeLongerThan"), Constants.MaxStringFieldLength));
            }

            return validationStatus;
        }
   }
}