using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models.ViewDataClasses;

namespace IUDICO.DisciplineManagement.Models
{
    using System.ComponentModel;

    using IUDICO.Common;
    using IUDICO.Common.Models;

    public static class Converter
    {
        public static TopicTypeEnum ToTopicTypeEnum(this TopicType topicType)
        {
            switch (topicType.Name)
            {
                case "Test": return TopicTypeEnum.Test;
                case "Theory": return TopicTypeEnum.Theory;
                case "TestWithoutCourse": return TopicTypeEnum.TestWithoutCourse;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string ToString(TopicType topicType)
        {
            return ToString(ToTopicTypeEnum(topicType));
        }

        public static string ToString(TopicTypeEnum topicType)
        {
            switch (topicType)
            {
                case TopicTypeEnum.Test: return Localization.GetMessage("TopicType.Test");
                case TopicTypeEnum.Theory: return Localization.GetMessage("TopicType.Theory");
                case TopicTypeEnum.TestWithoutCourse: return Localization.GetMessage("TopicType.TestWithoutCourse");
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string ToString(DateTime? value)
        {
            return value.HasValue ?
                DateFormatConverter.DataConvert(value.Value) :
                Localization.GetMessage("DateNotSpecified");
        }

        public static IList<ShareUser> ToShareUsers(this IEnumerable<User> users, bool isShared)
        {
            return users.Select(user => new ShareUser
            {
                Id = user.Id,
                Name = user.Name,
                Roles = user.Roles.Select(i => i.ToString()).ToArray(),
                Shared = isShared
            })
            .ToList();
        }

        public static void UpdateFromModel(this Topic topic, CreateTopicModel model)
        {
            var testTopicType = model.TestCourseId == Constants.TestWithoutCourseId
                                    ? TopicTypeEnum.TestWithoutCourse
                                    : model.TestCourseId == Constants.NoCourseId
                                          ? (TopicTypeEnum?)null
                                          : TopicTypeEnum.Test;
            var theoryTopicType = model.TheoryCourseId == Constants.NoCourseId
                                      ? (TopicTypeEnum?)null
                                      : TopicTypeEnum.Theory;

            topic.ChapterRef = model.ChapterId;
            topic.Name = model.TopicName;
            topic.TestCourseRef = model.TestCourseId != Constants.NoCourseId
                                      ? model.TestCourseId
                                      : (int?)null;
            topic.TestTopicTypeRef = (int?)testTopicType;
            topic.TheoryCourseRef = model.TheoryCourseId != Constants.NoCourseId
                                        ? model.TheoryCourseId
                                        : (int?)null;
            topic.TheoryTopicTypeRef = (int?)theoryTopicType;
        }

        public static ViewTopicModel ToViewTopicModel(this Topic topic, IDisciplineStorage storage)
        {
            return new ViewTopicModel
            {
                Id = topic.Id,
                ChapterId = topic.ChapterRef,
                Created = ToString(topic.Created),
                Updated = ToString(topic.Updated),
                TestCourseName =
                    topic.TestCourseRef.HasValue && topic.TestCourseRef != Constants.TestWithoutCourseId
                        ? storage.GetCourse(topic.TestCourseRef.Value).Name
                        : string.Empty,
                TestTopicType = topic.TestTopicTypeRef.HasValue
                                    ? ToString(
                                        storage.GetTopicType(topic.TestTopicTypeRef.Value))
                                    : string.Empty,
                TheoryCourseName = topic.TheoryCourseRef.HasValue
                                       ? storage.GetCourse(topic.TheoryCourseRef.Value).Name
                                       : string.Empty,
                TheoryTopicType = topic.TheoryTopicTypeRef.HasValue
                                      ? ToString(
                                          storage.GetTopicType(topic.TheoryTopicTypeRef.Value))
                                      : string.Empty,
                TopicName = topic.Name
            };
        }
    }
}