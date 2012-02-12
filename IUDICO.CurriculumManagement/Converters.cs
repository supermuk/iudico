using IUDICO.CurriculumManagement.Models.Enums;
using System;
namespace IUDICO.CurriculumManagement
{
    public static class Converters
    {
        public static TopicType ConvertToTopicType(IUDICO.Common.Models.Shared.TopicType topicType)
        {
            switch (topicType.Name)
            {
                case "Test":return TopicType.Test;
                case "Theory":return TopicType.Theory;
                case "TestWithoutCourse": return TopicType.TestWithoutCourse;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string ConvertToString(IUDICO.Common.Models.Shared.TopicType topicType)
        {
            TopicType enumTopicType = ConvertToTopicType(topicType);
            switch (enumTopicType)
            {
                case TopicType.Test: return Localization.getMessage("TopicType.Test");
                case TopicType.Theory: return Localization.getMessage("TopicType.Theory");
                case TopicType.TestWithoutCourse: return  Localization.getMessage("TopicType.TestWithoutCourse");
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}