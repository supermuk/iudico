using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common;

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
			ValidationStatus validationStatus = new ValidationStatus();

			DateTime minAllowedDate = Constants.MinAllowedDateTime;
			DateTime maxAllowedDate = Constants.MaxAllowedDateTime;
			if (timeline.StageRef <= 0)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseStage")));
			}
			if (timeline.StartDate > timeline.EndDate)
			{
				validationStatus.Errors.Add(Localization.getMessage("StartDateMustLessThanEndDate"));
			}
			if (timeline.StartDate < minAllowedDate || timeline.StartDate > maxAllowedDate)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("StartDateMustBeBetween"),
					 minAllowedDate.ToString(), maxAllowedDate.ToString()));
			}
			if (timeline.EndDate < minAllowedDate || timeline.EndDate > maxAllowedDate)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("EndDateMustBeBetween"),
					 minAllowedDate.ToString(), maxAllowedDate.ToString()));
			}

			var timelines = storage.GetCurriculumAssignmentTimelines(timeline.CurriculumAssignmentRef);

			bool errorCheck = true;
			List<string> errors = new List<string>();
			foreach (var item in timelines)
			{
				if (timeline.StartDate >= item.StartDate && item.EndDate >= timeline.EndDate)
				{
					errorCheck = false;
					break;
				}
				else
				{
					errors.Add(String.Format("{0} - {1}", String.Format("{0:g}", item.StartDate), String.Format("{0:g}", item.EndDate)));
				}
			}
			if (errorCheck == true)
			{
				validationStatus.Errors.Add(Localization.getMessage("StageTimelineBiggerThanCurriculumTimeline"));
				validationStatus.Errors.AddRange(errors);
			}

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
				validationStatus.Errors.Add(Localization.getMessage("StartDateMustLessThanEndDate"));
			}
			if (timeline.StartDate < minAllowedDate || timeline.StartDate > maxAllowedDate)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("StartDateMustBeBetween"),
					 minAllowedDate.ToString(), maxAllowedDate.ToString()));
			}
			if (timeline.EndDate < minAllowedDate || timeline.EndDate > maxAllowedDate)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("EndDateMustBeBetween"),
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

			if (curriculumAssignment.UserGroupRef <= 0)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseStage")));
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
				validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseCourse")));
			}
			if (theme.ThemeTypeRef <= 0)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseThemeType")));
			}
			if (theme.Name == null || theme.Name == "")
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("NameReqiured")));
			}
			if (theme.Name != null && theme.Name.Length > Constants.MaxStringFieldLength)
			{
				validationStatus.Errors.Add(String.Format(Localization.getMessage("NameCanNotBeLongerThan"), Constants.MaxStringFieldLength));
			}

			return validationStatus;
		}
	}
}