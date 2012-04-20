using System;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.DisciplineManagement.Models.Storage;
using System.Collections.Generic;
using System.Linq;

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
        /// Validates users for which we want to share discipline.
        /// </summary>
        /// <param name="disciplineId">The discipline id.</param>
        /// <param name="sharewith">Users share to.</param>
        /// <returns></returns>
        public ValidationStatus ValidateDisciplineSharing(int disciplineId, IList<Guid> sharewith)
        {
            var validationStatus = new ValidationStatus();
            var actualSharedUsers = _storage.GetDisciplineSharedUsers(disciplineId);
            foreach (var user in actualSharedUsers)
            {
                //If we want to unshare user
                if (!sharewith.Contains(user.Id))
                {
                    //if it already contains curriculums
                    if (_storage.GetCurriculums(c => c.DisciplineRef == disciplineId).Any())
                    {
                        validationStatus.AddLocalizedError("CanNotUnshareUser", user.Name);
                    }
                }
            }

            return validationStatus;
        }

        /// <summary>
        /// Validates the topic.
        /// </summary>
        /// <param name="data">The topic.</param>
        /// <returns></returns>
        public ValidationStatus ValidateTopic(Topic data)
        {
            var validationStatus = new ValidationStatus();
            //if (String.IsNullOrEmpty(data.Name))
            //{
            //    validationStatus.AddLocalizedError("NameReqiured");
            //}
            /*else*/
            if (!String.IsNullOrEmpty(data.Name) && data.Name.Length > Constants.MaxStringFieldLength)
            {
                validationStatus.AddLocalizedError("NameCanNotBeLongerThan", Constants.MaxStringFieldLength);
            }

            if (!data.TestCourseRef.HasValue && !data.TheoryCourseRef.HasValue)
            {
                validationStatus.AddLocalizedError("ChooseAtLeastOneCourse");
            }
            //else
            //{
            //    if (data.TestCourseRef.HasValue)
            //    {
            //        if (data.TestTopicTypeRef <= 0 || !data.TestTopicTypeRef.HasValue)
            //        {
            //            validationStatus.AddLocalizedError("ChooseTopicType");
            //        }
            //        else
            //        {
            //            var testTopicType = Converter.ToTopicType(_storage.GetTopicType(data.TestTopicTypeRef.Value));
            //            if (testTopicType == TopicTypeEnum.TestWithoutCourse && data.TestCourseRef != Constants.TestFromPaperCourseId)
            //            {
            //                validationStatus.AddLocalizedError("TestWithoutCourse");
            //            }
            //            if (testTopicType != TopicTypeEnum.TestWithoutCourse && data.TestCourseRef <= 0)
            //            {
            //                validationStatus.AddLocalizedError("ChooseTestCourse");
            //            }
            //        }
            //    }

            //    if (data.TheoryCourseRef.HasValue)
            //    {
            //        if (data.TheoryTopicTypeRef <= 0 || !data.TheoryTopicTypeRef.HasValue)
            //        {
            //            validationStatus.AddLocalizedError("ChooseTopicType");
            //        }
            //        else
            //        {
            //            if (data.TheoryCourseRef <= 0)
            //            {
            //                validationStatus.AddLocalizedError("ChooseTheoryCourse");
            //            }
            //        }
            //    }
            //}

            return validationStatus;
        }
    }
}