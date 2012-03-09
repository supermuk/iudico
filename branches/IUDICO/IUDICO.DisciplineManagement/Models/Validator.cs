using System;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.DisciplineManagement.Models.Storage;

namespace IUDICO.DisciplineManagement.Models
{
    /// <summary>
    /// Validator class.
    /// </summary>
    /// <remarks></remarks>
    public class Validator
    {
        private IDisciplineStorage _storage { get; set; }

        public Validator(IDisciplineStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Validates the topic.
        /// </summary>
        /// <param name="data">The topic.</param>
        /// <returns></returns>
        public ValidationStatus ValidateTopic(Topic data)
        {
            //TODO: FatTony; check method!
            var validationStatus = new ValidationStatus();
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
                        var testTopicType = Converter.ToTopicType(_storage.GetTopicType(data.TestTopicTypeRef.Value));
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
    }
}