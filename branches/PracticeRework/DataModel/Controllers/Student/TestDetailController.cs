using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.Common.TestRequestUtils;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers.Student
{
    public class TestDetailsController : ControllerBase
    {
        private HtmlControl _iframe;

        public ContentPlaceHolder PageContent { get; set; }

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
            LoadIFrame();

            ShowPage();
        }

        private void ShowPage()
        {
            if(PageId != 0)
            {
                TblPages page = GetPage(PageId);

                var questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));

                SetHeader(page);

                MaxPageRank.Value = GetMaxRank(questions).ToString();
                PageRank.Value = ((int)page.PageRank).ToString();
                QuestionCount.Value = (questions.Count).ToString();
                SetUrl();
            }
            else
            {
                throw new Exception("Page ID is not specified");
            }
        }

        private void SetHeader(TblPages page)
        {
            PageTitle.Value = GetPageTitleType();
            PageHeader.Value = string.Format("{0} for Page: {1}", GetPageTitleType(), page.PageName); 
        }

        private void SetUrl()
        {
            _iframe.Attributes["src"] = TestRequestBuilder.NewRequestForHandler(PageId, FileExtentions.IudicoPracticePage)
                .AddTestSessionType((TestSessionType)TestType).AddUserId(UserId).Build();
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

        private void LoadIFrame()
        {
            if (PageContent != null)
            {
                _iframe = (HtmlControl)PageContent.FindControl("_testDetailsFrame");
                if (_iframe == null)
                    throw new Exception("Can't load iframe");
            }
            else
            {
                throw new Exception("Can't load page Content");
            }
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
}