using System;
using IUDICO.Common;
using IUDICO.Common.Models.Shared;
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
            this._storage = storage;
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
            var actualSharedUsers = this._storage.GetDisciplineSharedUsers(disciplineId);
            foreach (var user in actualSharedUsers)
            {
                // If we want to unshare user
                if (!sharewith.Contains(user.Id))
                {
                    // if it already contains curriculums
                    if (this._storage.GetCurriculums(c => c.DisciplineRef == disciplineId).Any())
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
            if (!string.IsNullOrEmpty(data.Name) && data.Name.Length > Constants.MaxStringFieldLength)
            {
                validationStatus.AddLocalizedError("NameCanNotBeLongerThan", Constants.MaxStringFieldLength);
            }

            if (!data.TestCourseRef.HasValue && !data.TheoryCourseRef.HasValue)
            {
                validationStatus.AddLocalizedError("ChooseAtLeastOneCourse");
            }

            var chapterId = data.Id == 0 ? data.ChapterRef : _storage.GetTopic(data.Id).ChapterRef;
            var topics = _storage.GetTopics(item => item.ChapterRef == chapterId);
            var theoryCourseRefs =
                topics.Select(item => item.TheoryCourseRef).Where(item => item.HasValue && item.Value >= 0);
            var testCourseRefs =
                topics.Select(item => item.TestCourseRef).Where(item => item.HasValue && item.Value >= 0);
            var union = theoryCourseRefs.Union(testCourseRefs);
            if (union.Contains(data.TestCourseRef) || union.Contains(data.TheoryCourseRef))
            {
                validationStatus.AddLocalizedError("TopicWithSuchCourseIsPresent");
            }
            return validationStatus;
        }

        public string GetValidationError(Discipline discipline)
        {
            string error = Localization.GetMessage("CorrectTopics");
            var chapters = this._storage.GetDiscipline(discipline.Id).Chapters.Where(item => !item.IsDeleted);
            var count = 0;
            foreach (var chapter in chapters)
            {
                var topics = chapter.Topics.Where(item => !item.IsDeleted);
                
                foreach (var topic in topics)
                {
                    if (topic.TheoryCourseRef == null && topic.TestCourseRef == null)
                    {
                        if (count == 0)
                        {
                            error    += " " + topic.Name;
                        }
                        else
                        {
                            error += ", " + topic.Name;
                        }
                        count++;
                    }
                    else
                    {
                        if (topic.TheoryCourseRef != null)
                        {
                            if (this._storage.GetCourse((int)topic.TheoryCourseRef) == null)
                            {
                                if (count == 0)
                                {
                                    error += " " + topic.Name;
                                }
                                else
                                {
                                    error += ", " + topic.Name;
                                }
                                count++;
                            }
                        }
                        else if ((int)topic.TestCourseRef != -1)
                        {
                            if (this._storage.GetCourse((int)topic.TestCourseRef) == null)
                            {
                                if (count == 0)
                                {
                                    error += " " + topic.Name;
                                }
                                else
                                {
                                    error += ", " + topic.Name;
                                }
                                count++;
                            }
                        }
                    }
                }
            }
            return error;
        }
    }
}