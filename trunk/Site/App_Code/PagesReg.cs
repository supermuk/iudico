using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public static class PagesReg
{
    public static void RegisterPages(FormsModel f)
    {
        f.Register<Admin_EditGroupController>("~/Admin/EditGroup.aspx");
        f.Register<Admin_SelectGroupController>("~/Admin/SelectGroup.aspx");
        f.Register<Admin_RemoveUserFromGroupController>("~/Admin/RemoveUserFromGroup.aspx");
        f.Register<Admin_IncludeUserIntoGroupController>("~/Admin/IncludeUserIntoGroup.aspx");
        f.Register<Admin_EditUserController>("~/Admin/EditUser.aspx");
        f.Register<Admin_GroupsController>("~/Admin/Groups.aspx");
        f.Register<CreateGroupController>("~/Admin/CreateGroup.aspx");
        f.Register<Admin_CreateUserController>("~/Admin/CreateUser.aspx");
        f.Register<Admin_RemoveGroupConfirmationController>("~/Admin/RemoveGroupConfirmation.aspx");
        f.Register<Admin_RemoveUserConfirmationController>("~/Admin/RemoveUserConfirmation.aspx");
        f.Register<Admin_UsersController>("~/Admin/Users.aspx");
        f.Register<Admin_CreateBulkUserController>("~/Admin/CreateUserBulk.aspx");

        f.Register<CurriculumTimelineController>("~/Teacher/CurriculumTimeline.aspx");
        f.Register<CurriculumEditController>("~/Teacher/CurriculumEdit.aspx");
        f.Register<CurriculumDeleteConfirmationController>("~/Teacher/CurriculumDeleteConfirmation.aspx");
        f.Register<CourseEditController>("~/Teacher/CourseEdit.aspx");
        f.Register<CourseDeleteConfirmationController>("~/Teacher/CourseDeleteConfirmation.aspx");
        f.Register<CurriculumAssignmentController>("~/Teacher/CurriculumAssignment.aspx");
        f.Register<StatisticSelectController>("~/Teacher/StatisticSelect.aspx");
        f.Register<StatisticShowController>("~/Teacher/StatisticShow.aspx");
        f.Register<CourseShareController>("~/Teacher/CourseShare.aspx");
        f.Register<TeacherObjectsController>("~/Teacher/TeacherObjects.aspx");

        f.Register<StudentPageController>("~/Student/StudentPage.aspx");
        f.Register<TestDetailsController>("~/Student/TestDetails.aspx");
        f.Register<OpenTestController>("~/Student/OpenTest.aspx");
        f.Register<ThemeResultController>("~/Student/ThemeResult.aspx");
        f.Register<CompiledQuestionsDetailsController>("~/Student/CompiledQuestionsDetails.aspx");
    }
}
