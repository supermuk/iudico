using System;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;

namespace IUDICO.CurriculumManagement.Models
{
    public static class Converter
    {
        public static TopicTypeEnum ToTopicType(TopicType topicType)
        {
            switch (topicType.Name)
            {
                case "Test": return TopicTypeEnum.Test;
                case "Theory": return TopicTypeEnum.Theory;
                case "TestWithoutCourse": return TopicTypeEnum.TestWithoutCourse;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string ToString(DateTime? value)
        {
            return value.HasValue ?
                String.Format("{0:g}", value.Value) :
                Localization.getMessage("DateNotSpecified");
        }

        public static void UpdateFromModel(this Curriculum curriculum, CreateCurriculumModel model)
        {
            curriculum.UserGroupRef = model.GroupId;
            curriculum.DisciplineRef = model.DisciplineId;
            curriculum.StartDate = model.SetTimeline ? model.StartDate : (DateTime?) null;
            curriculum.EndDate = model.SetTimeline ? model.EndDate : (DateTime?) null;
        }

        public static CreateCurriculumModel ToCreateCurriculumModel(this Curriculum curriculum, ICurriculumStorage storage,
            bool isCreateModel)
        {
            var groups = storage.GetGroups();
            var disciplines = storage.GetDisciplines(storage.GetCurrentUser());
            return new CreateCurriculumModel(groups, curriculum.UserGroupRef, disciplines, curriculum.DisciplineRef,
                curriculum.StartDate, curriculum.EndDate, isCreateModel);
        }

        //public static ViewTopicModel ToViewTopicModel(this Topic topic, ICurriculumStorage storage)
        //{
        //    return new ViewTopicModel
        //    {
        //        Id = topic.Id,
        //        ChapterId = topic.ChapterRef,
        //        Created = ToString(topic.Created),
        //        Updated = ToString(topic.Updated),
        //        TestCourseName =
        //            topic.TestCourseRef.HasValue && topic.TestCourseRef != Constants.TestWithoutCourseId
        //                ? storage.GetCourse(topic.TestCourseRef.Value).Name
        //                : String.Empty,
        //        TestTopicType = topic.TestTopicTypeRef.HasValue
        //                            ? ToString(
        //                                storage.GetTopicType(topic.TestTopicTypeRef.Value))
        //                            : String.Empty,
        //        TheoryCourseName = topic.TheoryCourseRef.HasValue
        //                               ? storage.GetCourse(topic.TheoryCourseRef.Value).Name
        //                               : String.Empty,
        //        TheoryTopicType = topic.TheoryTopicTypeRef.HasValue
        //                              ? ToString(
        //                                  storage.GetTopicType(topic.TheoryTopicTypeRef.Value))
        //                              : String.Empty,
        //        TopicName = topic.Name
        //    };
        //}
    }
}