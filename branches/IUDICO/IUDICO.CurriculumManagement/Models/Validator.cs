using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
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
        /// Validates the chapter timeline.
        /// </summary>
        /// <param name="timeline">The chapter timeline.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public ValidationStatus ValidateChapterTimeline(Timeline timeline)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            DateTime minAllowedDate = Constants.MinAllowedDateTime;
            DateTime maxAllowedDate = Constants.MaxAllowedDateTime;
            if (timeline.ChapterRef <= 0)
            {
                validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseChapter")));
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

            var timelines = storage.GetCurriculumTimelines(timeline.CurriculumRef);

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
                validationStatus.Errors.Add(Localization.getMessage("ChapterTimelineBiggerThanDisciplineTimeline"));
                validationStatus.Errors.AddRange(errors);
            }

            return validationStatus;
        }

        /// <summary>
        /// Validates the discipline assignment timeline.
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumTimeline(Timeline timeline)
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
        /// Validates the discipline assignment.
        /// </summary>
        /// <param name="curriculum">The discipline assignment.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculum(Curriculum curriculum)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            if (curriculum.UserGroupRef <= 0)
            {
                validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseChapter")));
            }

            return validationStatus;
        }

        /// <summary>
        /// Validates the topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <returns></returns>
        public ValidationStatus ValidateTopic(Topic topic)
        {
            ValidationStatus validationStatus = new ValidationStatus();
            var topicType = Converters.ConvertToTopicType(storage.GetTopicType(topic.TopicTypeRef));

            if (topic.CourseRef <= 0 || (!topic.CourseRef.HasValue && topicType != Enums.TopicType.TestWithoutCourse))
            {
                validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseCourse")));
            }
            if (topicType == Enums.TopicType.TestWithoutCourse && topic.CourseRef > 0)
            {
                validationStatus.Errors.Add(String.Format(Localization.getMessage("TestWithoutCourse")));
            }
            if (topic.TopicTypeRef <= 0)
            {
                validationStatus.Errors.Add(String.Format(Localization.getMessage("ChooseTopicType")));
            }
            if (topic.Name == null || topic.Name == "")
            {
                validationStatus.Errors.Add(String.Format(Localization.getMessage("NameReqiured")));
            }
            if (topic.Name != null && topic.Name.Length > Constants.MaxStringFieldLength)
            {
                validationStatus.Errors.Add(String.Format(Localization.getMessage("NameCanNotBeLongerThan"), Constants.MaxStringFieldLength));
            }

            return validationStatus;
        }
    }
}