using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.WebTest;

namespace IUDICO.DataModel.Controllers.Teacher
{
    public class ReCompilePageController : ControllerBase
    {
        public Button ReCompileButton { get; set;}

        public DropDownList GroupDropDownList { get; set; }

        public DropDownList CurriculumnDropDownList { get; set; }

        public DropDownList StageDropDownList { get; set; }

        public DropDownList ThemeDropDownList { get; set; }

        public void PageLoad(object sender, EventArgs e)
        {
            CreateGroupList();
            CreateCurriculumnList();
            CreateStageList();
            CreateThemeList();
        }



        public void ReCompileButtonClick(object sender, EventArgs e)
        {
            var selectedGroup = ServerModel.DB.Load<TblGroups>(int.Parse(GroupDropDownList.SelectedItem.Value));
            var usersIds = ServerModel.DB.LookupMany2ManyIds<TblUsers>(selectedGroup, null);
            var users = ServerModel.DB.Load<TblUsers>(usersIds);

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

                        answersForReCompilation.Add((new LatestUserAnswerFinder().FindUserAnswer(answersForQuestion)));
                    }

                }
            }


            foreach (var n in answersForReCompilation)
            {
                CompilationManager.GetNewManager(n).ReCompile();
            }

        }

        public void GroupDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            CreateCurriculumnList();
        }

        public void StageDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            CreateThemeList();
        }

        public void CurriculumnDropDownListSelectedIndexChanged(object sender, EventArgs e)
        {
            CreateStageList();
        }


        private void CreateGroupList()
        {
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
