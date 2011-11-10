using IUDICO.Common.Models.Interfaces;

namespace IUDICO.Common.Models
{
    public partial class DBDataContext : IDataContext
    {
        IMockableTable<Course> IDataContext.Courses
        {
            get { return new MockableTable<Course>(Courses); }
        }

        IMockableTable<User> IDataContext.Users
        {
            get { return new MockableTable<User>(Users); }
        }

        IMockableTable<Group> IDataContext.Groups
        {
            get { return new MockableTable<Group>(Groups); }
        }

        IMockableTable<GroupUser> IDataContext.GroupUsers
        {
            get { return new MockableTable<GroupUser>(GroupUsers); }
        }

        IMockableTable<UserRole> IDataContext.UserRoles
        {
            get { return new MockableTable<UserRole>(UserRoles); }
        }

        IMockableTable<Curriculum> IDataContext.Curriculums
        {
            get { return new MockableTable<Curriculum>(Curriculums); }
        }

        IMockableTable<Stage> IDataContext.Stages
        {
            get { return new MockableTable<Stage>(Stages); }
        }

        IMockableTable<Theme> IDataContext.Themes
        {
            get { return new MockableTable<Theme>(Themes); }
        }

        IMockableTable<CurriculumAssignment> IDataContext.CurriculumAssignments
        {
            get { return new MockableTable<CurriculumAssignment>(CurriculumAssignments); }
        }

        IMockableTable<Timeline> IDataContext.Timelines
        {
            get { return new MockableTable<Timeline>(Timelines); }
        }

        IMockableTable<ThemeAssignment> IDataContext.ThemeAssignments
        {
            get { return new MockableTable<ThemeAssignment>(ThemeAssignments); }
        }

        IMockableTable<ThemeType> IDataContext.ThemeTypes
        {
            get { return new MockableTable<ThemeType>(ThemeTypes); }
        }
    }
}
