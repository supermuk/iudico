using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common;
using IUDICO.Common.Models.Shared.CurriculumManagement;

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
        /// Validates the curriculum chapter.
        /// </summary>
        /// <param name="data">The curriculum chapter.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumChapter(CurriculumChapter data)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            ValidateDate(data.StartDate, data.EndDate, validationStatus);

            //TODO: check topic timelines and curriculum timelines!
            return validationStatus;
        }

        /// <summary>
        /// Validates the curriculum chapter topic.
        /// </summary>
        /// <param name="curriculumChapter">The curriculum chapter topic.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumChapterTopic(CurriculumChapterTopic data)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            ValidateDate(data.TestStartDate, data.TestEndDate, validationStatus);
            ValidateDate(data.TheoryStartDate, data.TheoryEndDate, validationStatus);

            //TODO: check topic timelines and curriculum timelines!

            return validationStatus;
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        /// <param name="curriculum">The curriculum.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculum(Curriculum curriculum)
        {
            ValidationStatus validationStatus = new ValidationStatus();

            if (curriculum.UserGroupRef <= 0)
            {
                validationStatus.AddLocalizedError("ChooseGroup");
            }
            ValidateDate(curriculum.StartDate, curriculum.EndDate, validationStatus);

            return validationStatus;
        }

        /// <summary>
        /// Validates the topic.
        /// </summary>
        /// <param name="data">The topic.</param>
        /// <returns></returns>
        public ValidationStatus ValidateTopic(Topic data)
        {
            ValidationStatus validationStatus = new ValidationStatus();
            if (String.IsNullOrEmpty(data.Name))
            {
                validationStatus.AddLocalizedError("NameReqiured");
            }
            else if (data.Name.Length > Constants.MaxStringFieldLength)
            {
                validationStatus.AddLocalizedError("NameCanNotBeLongerThan", Constants.MaxStringFieldLength);
            }

            if (!data.TestCourseRef.HasValue && !data.TheoryCourseRef.HasValue)
            {
                validationStatus.AddLocalizedError("ChooseAtLeastOneCourse");
            }
            else
            {
                if (data.TestCourseRef.HasValue)
                {
                    if (data.TestTopicTypeRef == 0)
                    {
                        validationStatus.AddLocalizedError("ChooseTopicType");
                    }
                    else
                    {
                        var testTopicType = Converter.ToTopicType(storage.GetTopicType(data.TestTopicTypeRef.Value));
                        if (testTopicType == TopicTypeEnum.TestWithoutCourse && data.TestCourseRef != Constants.NoCourseId)
                        {
                            validationStatus.AddLocalizedError("TestWithoutCourse");
                        }
                        if (testTopicType != TopicTypeEnum.TestWithoutCourse && (!data.TestCourseRef.HasValue || data.TestCourseRef <= 0))
                        {
                            validationStatus.AddLocalizedError("ChooseTestCourse");
                        }
                    }
                }

                if (data.TheoryCourseRef.HasValue)
                {
                    if (data.TheoryTopicTypeRef == 0)
                    {
                        validationStatus.AddLocalizedError("ChooseTopicType");
                    }
                    else
                    {
                        if (!data.TheoryCourseRef.HasValue || data.TheoryCourseRef <= 0)
                        {
                            validationStatus.AddLocalizedError("ChooseTheoryCourse");
                        }
                    }
                }
            }

            return validationStatus;
        }

        private void ValidateDate(DateTime? startDate, DateTime? endDate, ValidationStatus validationStatus)
        {
            DateTime minAllowedDate = Constants.MinAllowedDateTime;
            DateTime maxAllowedDate = Constants.MaxAllowedDateTime;
            if (endDate.HasValue ^ startDate.HasValue)
            {
                validationStatus.AddLocalizedError("ChooseStartAndEndDate");
            }
            else if (endDate.HasValue && startDate.HasValue)
            {
                if (startDate > endDate)
                {
                    validationStatus.AddLocalizedError("StartDateMustLessThanEndDate");
                }
                if (startDate < minAllowedDate || startDate > maxAllowedDate)
                {
                    validationStatus.AddLocalizedError("StartDateMustBeBetween", minAllowedDate.ToString(), maxAllowedDate.ToString());
                }
                if (endDate < minAllowedDate || endDate > maxAllowedDate)
                {
                    validationStatus.AddLocalizedError("EndDateMustBeBetween", minAllowedDate.ToString(), maxAllowedDate.ToString());
                }
            }
        }
    }
}