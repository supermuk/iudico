using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers.Teacher
{
    /// <summary>
    /// Controller for ReCompilationPage.aspx page
    /// </summary>
    public class ReCompilePageController : ControllerBase
    {
        public readonly IVariable<string> Description = string.Empty.AsVariable();


        public Button ReCompileButton { get; set;}

        public DropDownList UserDropDownList { get; set; }

        public DropDownList GroupDropDownList { get; set; }

        public DropDownList CurriculumnDropDownList { get; set; }

        public DropDownList StageDropDownList { get; set; }

        public DropDownList ThemeDropDownList { get; set; }



        public void PageLoad(object sender, EventArgs e)
        {
            Description.Value = Translations.ReCompilePageController_PageLoad_Select_Group_and_Theme_that_your_want_to_recompile;
            
            if (!((Page)sender).IsPostBack)
            {
                CreateGroupList();
                CreateCurriculumnList();
                CreateStageList();
                CreateThemeList();
            }

            ReCompileButton.Enabled = ((ServerModel.User.Current.Islector())
                                        && (ThemeDropDownList.SelectedItem != null));
        }

        public void ReCompileButtonClick(object sender, EventArgs e)
        {
            IList<TblUsers> users = GetUsersForReCompilation();
            throw new NotImplementedException();

            if (ThemeDropDownList.SelectedItem != null)
            {
                var items = StudentRecordFinder.GetItemsForTheme(int.Parse(ThemeDropDownList.SelectedItem.Value));

                var answersForReCompilation = new List<TblUserAnswers>();

                foreach (var item in items)
                {
                    //AddAnswersFromPageToReCompilationList(item, users, answersForReCompilation);
                }

                ReCompile(answersForReCompilation);

                Description.Value = Translations.ReCompilePageController_ReCompileButtonClick_ReCompilation_is_started;
            }
            else
            {
                Description.Value = Translations.ReCompilePageController_ReCompileButtonClick_Theme_not_selected____;
            }
        }

        public void GroupDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            CreateUserList();
            CreateCurriculumnList();
            CreateStageList();
            CreateThemeList();
        }

        public void CurriculumnDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            CreateStageList();
            CreateThemeList();
        }

        public void StageDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            CreateThemeList();
        }

/*
        private static void AddAnswersFromPageToReCompilationList(TblItems item, IList<TblUsers> users, List<TblUserAnswers> answersForReCompilation)
        {
            throw new NotImplementedException();
            var questions = StudentRecordFinder.GetQuestionsForPage(item);

            foreach (var u in users)
            {
                foreach (var q in questions)
                {
                    TblUserAnswers lstUserAnswer = GetLastUserAnswerForCompiledQuestion(q, u);

                    AddAnswerToReCompilationList(lstUserAnswer, answersForReCompilation);
                }
            }
        }
*/
        private static void AddAnswerToReCompilationList(TblUserAnswers lstUserAnswer, List<TblUserAnswers> answersForReCompilation)
        {
            if(lstUserAnswer != null)
                answersForReCompilation.Add(lstUserAnswer);
        }

        private static void ReCompile(List<TblUserAnswers> answersForReCompilation)
        {
            foreach (var n in answersForReCompilation)
                CompilationTestManager.GetNewManager(n).ReCompile();
        }

        private static TblUserAnswers GetLastUserAnswerForCompiledQuestion(TblQuestions q, TblUsers u)
        {
            var userAnswers = StudentRecordFinder.GetUserAnswersForQuestion(q, u.ID);

            var compiledAnswers = StudentRecordFinder.ExtractCompiledAnswers(userAnswers);

            return StatisticManager.FindLatestUserAnswer(compiledAnswers);
        }

        private IList<TblUsers> GetUsersForReCompilation()
        {
            var users = new List<TblUsers>();

            int selectedUserId = int.Parse(UserDropDownList.SelectedItem.Value);
            int selectedGroupId = int.Parse(GroupDropDownList.SelectedItem.Value);

            if(selectedUserId == 0)
                users.AddRange(StudentRecordFinder.GetUsersFromGroup(selectedGroupId));
            else
                users.Add(ServerModel.DB.Load<TblUsers>(selectedUserId));

            return users;
        }

        private void CreateGroupList()
        {
            GroupDropDownList.Items.Clear();

            var groups = ServerModel.DB.Query<TblGroups>(null);

            if (groups.Count != 0)
            {
                foreach (var g in groups)
                    GroupDropDownList.Items.Add(new ListItem(g.Name, g.ID.ToString()));

                CreateUserList();
            }
            else
            {
                Description.Value = Translations.ReCompilePageController_CreateGroupList_No_groups_avalible;
            }
        }

        private void CreateUserList()
        {
            UserDropDownList.Items.Clear();

            if (GroupDropDownList.SelectedItem != null)
            {
                var users = StudentRecordFinder.GetUsersFromGroup(int.Parse(GroupDropDownList.SelectedItem.Value));

                UserDropDownList.Items.Add(new ListItem(Translations.ReCompilePageController_CreateUserList_All_Users_From_Group, "0"));

                foreach (var u in users)
                    UserDropDownList.Items.Add(new ListItem(u.Login, u.ID.ToString()));
            }
        }

        private void CreateCurriculumnList()
        {
            CurriculumnDropDownList.Items.Clear();
            StageDropDownList.Items.Clear();
            ThemeDropDownList.Items.Clear();

            if (GroupDropDownList.SelectedItem != null)
            {
                var selectedGroup = ServerModel.DB.Load<TblGroups>(int.Parse(GroupDropDownList.SelectedItem.Value));

                var curriculumns = TeacherHelper.GetCurriculumsForGroup(selectedGroup);

                foreach (var c in curriculumns)
                    CurriculumnDropDownList.Items.Add(new ListItem(c.Name, c.ID.ToString()));
            }
        }

        private void CreateStageList()
        {
            StageDropDownList.Items.Clear();
            ThemeDropDownList.Items.Clear();

            if (CurriculumnDropDownList.SelectedItem != null)
            {
                var selectedCurriculumn = ServerModel.DB.Load<TblCurriculums>(int.Parse(CurriculumnDropDownList.SelectedItem.Value));
                var stages = selectedCurriculumn.TblStages;

                foreach (var s in stages)
                    StageDropDownList.Items.Add(new ListItem(s.Name, s.ID.ToString()));
            }
        }

        private void CreateThemeList()
        {
            ThemeDropDownList.Items.Clear();

            if (StageDropDownList.SelectedItem != null)
            {
                var selectedStage = ServerModel.DB.Load<TblStages>(int.Parse(StageDropDownList.SelectedItem.Value));
                var themes = TeacherHelper.ThemesOfStage(selectedStage);

                foreach (var s in themes)
                    ThemeDropDownList.Items.Add(new ListItem(s.Name, s.ID.ToString()));
            }
        }
    }
}
