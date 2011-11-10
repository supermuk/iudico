namespace IUDICO.Common.Models.Interfaces
{
    public interface IDataContext : IMockableDataContext
    {
        IMockableTable<Course> Courses { get; }
        IMockableTable<User> Users { get; }
        IMockableTable<Group> Groups { get; }
        IMockableTable<GroupUser> GroupUsers { get; }
        IMockableTable<UserRole> UserRoles { get; }
        IMockableTable<Curriculum> Curriculums { get; }
        IMockableTable<Stage> Stages { get; }
        IMockableTable<Theme> Themes { get; }
        IMockableTable<CurriculumAssignment> CurriculumAssignments { get; }
        IMockableTable<Timeline> Timelines { get; }
        IMockableTable<ThemeAssignment> ThemeAssignments { get; }
        IMockableTable<ThemeType> ThemeTypes { get; }
    }
}
