using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.Controllers.Teacher;

namespace App_Code
{
    public static class PagesReg
    {
        public static void RegisterPages(FormsModel f)
        {
            f.Register<HomeController>("~/Home.aspx");
            f.Register<LoginController>("~/Login.aspx");
            f.Register<Admin_EditGroupController>("~/Admin/EditGroup.aspx");
            f.Register<Admin_SelectGroupController>("~/Admin/SelectGroup.aspx");
            f.Register<Admin_RemoveUserFromGroupController>("~/Admin/RemoveUserFromGroup.aspx");
            f.Register<Admin_IncludeUserIntoGroupController>("~/Admin/IncludeUserIntoGroup.aspx");
            f.Register<Admin_EditUserController>("~/Admin/EditUser.aspx");
            f.Register<Admin_EditSettingController>("~/Admin/EditSetting.aspx");
            f.Register<Admin_GroupsController>("~/Admin/Groups.aspx");
            f.Register<CreateGroupController>("~/Admin/CreateGroup.aspx");
            f.Register<Admin_CreateUserController>("~/Admin/CreateUser.aspx");
            f.Register<Admin_RemoveGroupConfirmationController>("~/Admin/RemoveGroupConfirmation.aspx");
            f.Register<Admin_RemoveUserConfirmationController>("~/Admin/RemoveUserConfirmation.aspx");
            f.Register<Admin_RemoveSettingConfirmationController>("~/Admin/RemoveSettingConfirmation.aspx");
            f.Register<Admin_SettingsController>("~/Admin/Settings.aspx");
            f.Register<Admin_CreateSettingController>("~/Admin/CreateSetting.aspx");
            f.Register<Admin_UsersController>("~/Admin/Users.aspx");
            f.Register<Admin_CreateBulkUserController>("~/Admin/CreateUserBulk.aspx");
            f.Register<Admin_SearchPageSolrController>("~/Admin/SearchPageSolr.aspx");

            f.Register<CurriculumTimelineController>("~/Teacher/CurriculumTimeline.aspx");
            f.Register<CurriculumEditController>("~/Teacher/CurriculumEdit.aspx");
            f.Register<CurriculumDeleteConfirmationController>("~/Teacher/CurriculumDeleteConfirmation.aspx");
            f.Register<CourseEditController>("~/Teacher/CourseEdit.aspx");
            f.Register<CourseDeleteConfirmationController>("~/Teacher/CourseDeleteConfirmation.aspx");
            f.Register<CurriculumAssignmentController>("~/Teacher/CurriculumAssignment.aspx");
            f.Register<StatisticSelectController>("~/Teacher/StatisticSelect.aspx");
            f.Register<StatisticShowCurriculumsController>("~/Teacher/StatisticShowCurriculums.aspx");
            f.Register<StatisticShowController>("~/Teacher/StatisticShow.aspx");
            f.Register<StatisticShowGraphController>("~/Teacher/StatisticShowGraph.aspx");
            f.Register<TeachersListController>("~/Teacher/TeachersList.aspx");
            f.Register<TeacherObjectsController>("~/Teacher/TeacherObjects.aspx");
            f.Register<ShareController>("~/Teacher/Share.aspx");
            f.Register<ThemePagesController>("~/Teacher/ThemePages.aspx");
            f.Register<CourseBehaviorController>("~/Teacher/CourseBehavior.aspx");

            f.Register<StudentPageController>("~/Student/StudentPage.aspx");
            f.Register<TestDetailsController>("~/Student/TestDetails.aspx");
            f.Register<OpenTestController>("~/Student/OpenTest.aspx");
            f.Register<ThemeResultController>("~/Student/ThemeResult.aspx");
            f.Register<StageResultController>("~/Student/StageResult.aspx");
            f.Register<CurriculumnResultController>("~/Student/CurriculumnResult.aspx");
            f.Register<CompiledQuestionsDetailsController>("~/Student/CompiledQuestionsDetails.aspx");
        }
    }
}