using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.WebControl;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers.Student
{
    public class TestDetailsController : ControllerBase
    {
        public Panel PageContent { get; set; }

        public IVariable<string> PageRank = string.Empty.AsVariable();

        public IVariable<string> MaxPageRank = string.Empty.AsVariable();

        public IVariable<string> QuestionCount = string.Empty.AsVariable();

        public IVariable<string> PageTitle = string.Empty.AsVariable();

        public IVariable<string> PageHeader = string.Empty.AsVariable();

        [ControllerParameter]
        public int TestType;

        [ControllerParameter]
        public int PageId;

        [ControllerParameter]
        public int UserId;


        public void PageLoad(object sender, EventArgs e)
        {
            ShowPage();
        }

        private void ShowPage()
        {
            if(PageId != 0)
            {
                TblPages page = GetPage(PageId);

                var questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));

                SetHeader(page);

                AddControl(page);

                MaxPageRank.Value = GetMaxRank(questions).ToString();
                PageRank.Value = ((int)page.PageRank).ToString();
                QuestionCount.Value = (questions.Count).ToString();
            }
            else
            {
                throw new Exception("Page ID is not specified");
            }
        }

        private void AddControl(TblPages page)
        {
            Control control;

            if ((FX_PAGETYPE)page.PageTypeRef == FX_PAGETYPE.Practice)
            {
                control = TestControlHelper.GetPracticeControl(page, PageContent);
                
            }
            else
            {
                throw new Exception("You can't see details for theory page");
            }

            PageContent.Controls.Clear();
            PageContent.Controls.Add(control);
            FillAnswer(control);
        }

        private void FillAnswer(Control control)
        {
            foreach (var c in control.Controls)
            {
                if (c is ITestControl)
                {
                    if (TestType == (int) TestSessionType.UserAnswer)
                    {
                        ((ITestControl) c).FillUserAnswer(UserId);
                    }
                    if (TestType == (int) TestSessionType.CorrectAnswer)
                    {
                        ((ITestControl) c).FillCorrectAnswer();
                    }
                }
                if(c is Button)
                {
                    ((Button) c).Enabled = false;
                }
            }
        }

        private void SetHeader(TblPages page)
        {
            PageTitle.Value = GetPageTitleType();
            PageHeader.Value = string.Format("{0} for Page: {1}", GetPageTitleType(), page.PageName); 
        }


        private static int GetMaxRank(IList<TblQuestions> questions)
        {
            var maxRank = 0;
            foreach (var question in questions)
            {
                maxRank += (int)question.Rank;
            }
            return maxRank;
        }

        private static TblPages GetPage(int pageId)
        {
            return ServerModel.DB.Load<TblPages>(pageId);
        }

        public string GetPageTitleType()
        {
            if (TestType == (int) TestSessionType.UserAnswer)
            {
                return "User Answers";
            }

            if (TestType == (int)TestSessionType.CorrectAnswer)
            {
                return "Correct Answers";
            }
            return "";
        }
    }

    public enum TestSessionType
    {
        UserAnswer,
        CorrectAnswer
    }
}