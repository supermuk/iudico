using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
namespace IUDICO.CurriculumManagement.Models
{
    public interface IDataContext : IMockableDataContext
    {
        IMockableTable<Discipline> Disciplines { get; }
        IMockableTable<Chapter> Chapters { get; }
        IMockableTable<Topic> Topics { get; }
        IMockableTable<Curriculum> Curriculums { get; }
        IMockableTable<CurriculumChapter> CurriculumChapters { get; }
        IMockableTable<CurriculumChapterTopic> CurriculumChapterTopics { get; }
        IMockableTable<TopicType> TopicTypes { get; }
        IMockableTable<UserTopicScore> UserTopicScores { get; }
    }
}
