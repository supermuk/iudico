using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers.Teacher
{
    public class ReCompilePageController : ControllerBase
    {
        public readonly IVariable<string> Description = string.Empty.AsVariable();


        public Button ReCompileButton { get; set;}

        public DropDownList GroupDropDownList { get; set; }

        public DropDownList CurriculumnDropDownList { get; set; }

        public DropDownList StageDropDownList { get; set; }

        public DropDownList ThemeDropDownList { get; set; }



        public void PageLoad(object sender, EventArgs e)
        {
            Description.Value = "Selct Group and Theme that your want to recompile";
            
            if (!((Page)sender).IsPostBack)
            {
                CreateGroupList();
                CreateCurriculumnList();
                CreateStageList();
                CreateThemeList();
            }
        }

        public void ReCompileButtonClick(object sender, EventArgs e)
        {
            var selectedGroup = ServerModel.DB.Load<TblGroups>(int.Parse(GroupDropDownList.SelectedItem.Value));
            var usersIds = ServerModel.DB.LookupMany2ManyIds<TblUsers>(selectedGroup, null);
            var users = ServerModel.DB.Load<TblUsers>(usersIds);

            if (ThemeDropDownList.SelectedItem != null)
            {

                var selectedTheme = ServerModel.DB.Load<TblThemes>(int.Parse(ThemeDropDownList.SelectedItem.Value));

                var pagesIds = ServerModel.DB.LookupIds<TblPages>(selectedTheme, null);
                var pages = ServerModel.DB.Load<TblPages>(pagesIds);

                var answersForReCompilation = new List<TblUserAnswers>();



                foreach (var page in pages)
                {
                    var questionsIds = ServerModel.DB.LookupIds<TblQuestions>(page, null);

                    foreach (var u in users)
                    {
                        var userAnswerIds = ServerModel.DB.LookupIds<TblUserAnswers>(u, null);
                        var userAnswers = ServerModel.DB.Load<TblUserAnswers>(userAnswerIds);

                        var compiledAnswers = new List<TblUserAnswers>();

                        foreach (var ua in userAnswers)
                        {
                            if (ua.IsCompiledAnswer)
                                compiledAnswers.Add(ua);
                        }

                        foreach (var q in questionsIds)
                        {
                            var answersForQuestion = new List<TblUserAnswers>();

                            foreach (var c in compiledAnswers)
                            {
                                if (c.QuestionRef == q)
                                    answersForQuestion.Add(c);
                            }

                            var lstUserAnswer = (new LatestUserAnswerFinder().FindUserAnswer(answersForQuestion));
                            
                            if(lstUserAnswer != null)
                                answersForReCompilation.Add(lstUserAnswer);
                        }

                    }
                }


                foreach (var n in answersForReCompilation)
                    CompilationTestManager.GetNewManager(n).ReCompile();

                Description.Value = "ReCompilation is started";
            }
            else
            {
                Description.Value = "Theme not selected !!!";
            }

        }

        public void GroupDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void CreateGroupList()
        {
            GroupDropDownList.Items.Clear();

            var groups = ServerModel.DB.Query<TblGroups>(null);

            foreach (var g in groups)
                GroupDropDownList.Items.Add(new ListItem(g.Name, g.ID.ToString()));
        }

        private void CreateCurriculumnList()
        {
            CurriculumnDropDownList.Items.Clear();
            StageDropDownList.Items.Clear();
            ThemeDropDownList.Items.Clear();

            var selectedGroup = ServerModel.DB.Load<TblGroups>(int.Parse(GroupDropDownList.SelectedItem.Value));

            var curriculumns = TeacherHelper.GetCurriculumsForGroup(selectedGroup);

            foreach (var c in curriculumns)
                CurriculumnDropDownList.Items.Add(new ListItem(c.Name, c.ID.ToString()));
        }

        private void CreateStageList()
        {
            StageDropDownList.Items.Clear();
            ThemeDropDownList.Items.Clear();

            var selectedCurriculumn = ServerModel.DB.Load<TblCurriculums>(int.Parse(CurriculumnDropDownList.SelectedItem.Value));
            var stagesIds = ServerModel.DB.LookupIds<TblStages>(selectedCurriculumn, null);
            var stages = ServerModel.DB.Load<TblStages>(stagesIds);

            foreach (var s in stages)
                StageDropDownList.Items.Add(new ListItem(s.Name, s.ID.ToString()));
        }

        private void CreateThemeList()
        {
            ThemeDropDownList.Items.Clear();

            var selectedStage = ServerModel.DB.Load<TblStages>(int.Parse(StageDropDownList.SelectedItem.Value));
            var themesIds = ServerModel.DB.LookupMany2ManyIds<TblThemes>(selectedStage, null);
            var themes = ServerModel.DB.Load<TblThemes>(themesIds);

            foreach (var s in themes)
                ThemeDropDownList.Items.Add(new ListItem(s.Name, s.ID.ToString()));
        }
    }
}
