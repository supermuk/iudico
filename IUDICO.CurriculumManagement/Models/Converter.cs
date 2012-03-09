using IUDICO.Common.Models.Shared.CurriculumManagement;
using System;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.CurriculumManagement.Models
{
    public static class Converter
    {
        public static TopicTypeEnum ToTopicType(IUDICO.Common.Models.Shared.TopicType topicType)
        {
            switch (topicType.Name)
            {
                case "Test": return TopicTypeEnum.Test;
                case "Theory": return TopicTypeEnum.Theory;
                case "TestWithoutCourse": return TopicTypeEnum.TestWithoutCourse;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string ToString(IUDICO.Common.Models.Shared.TopicType topicType)
        {
            TopicTypeEnum enumTopicType = ToTopicType(topicType);
            switch (enumTopicType)
            {
                case TopicTypeEnum.Test: return Localization.getMessage("TopicType.Test");
                case TopicTypeEnum.Theory: return Localization.getMessage("TopicType.Theory");
                case TopicTypeEnum.TestWithoutCourse: return Localization.getMessage("TopicType.TestWithoutCourse");
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string ToString(DateTime? value)
        {
            return value.HasValue ?
                String.Format("{0:g}", value.Value) :
                Localization.getMessage("DateNotSpecified");
        }
    }
}