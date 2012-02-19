using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
namespace IUDICO.CurriculumManagement.Models
{
    public interface IDataContext : IMockableDataContext
    {
        //IMockableTable<Course> Courses { get; }
        //IMockableTable<User> Users { get; }
        //IMockableTable<Group> Groups { get; }
        //IMockableTable<GroupUser> GroupUsers { get; }
        //IMockableTable<UserRole> UserRoles { get; }
        IMockableTable<Discipline> Disciplines { get; }
        IMockableTable<Chapter> Chapters { get; }
        IMockableTable<Topic> Topics { get; }
        IMockableTable<Curriculum> Curriculums { get; }
        IMockableTable<Timeline> Timelines { get; }
        IMockableTable<TopicAssignment> TopicAssignments { get; }
        IMockableTable<TopicType> TopicTypes { get; }
        IMockableTable<UserTopicScore> UserTopicScores { get; }
    }
}
