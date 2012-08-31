using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
namespace IUDICO.CourseManagement.Models
{
    public interface IDataContext : IMockableDataContext
    {
        // IMockableTable<User> Users { get; }
        IMockableTable<Course> Courses { get; }
        IMockableTable<CourseUser> CourseUsers { get; }
        IMockableTable<CoursesInfo> CoursesInfo { get; }
        IMockableTable<NodesInfo> NodesInfo { get; }
        IMockableTable<QuestionsInfo> QuestionsInfo { get; }
        IMockableTable<SimpleQuestion> SimpleQuestions { get; }
        IMockableTable<ChoiceQuestionsCorrectChoice> ChoiceQuestionsCorrectChoices { get; }
        IMockableTable<ChoiceQuestionsOption> ChoiceQuestionsOptions { get; }
        IMockableTable<CompiledTestQuestion> CompiledTestQuestions { get; }
        IMockableTable<Node> Nodes { get; }
        IMockableTable<NodeResource> NodeResources { get; }
    }
}
