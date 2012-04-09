using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
namespace IUDICO.DisciplineManagement.Models
{
    public interface IDataContext : IMockableDataContext
    {
        IMockableTable<Discipline> Disciplines { get; }
        IMockableTable<Chapter> Chapters { get; }
        IMockableTable<Topic> Topics { get; }
        IMockableTable<TopicType> TopicTypes { get; }
    }
}
