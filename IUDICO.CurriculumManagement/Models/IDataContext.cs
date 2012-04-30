using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
namespace IUDICO.CurriculumManagement.Models
{
    public interface IDataContext : IMockableDataContext
    {
        IMockableTable<Curriculum> Curriculums { get; }
        IMockableTable<CurriculumChapter> CurriculumChapters { get; }
        IMockableTable<CurriculumChapterTopic> CurriculumChapterTopics { get; }
    }
}
