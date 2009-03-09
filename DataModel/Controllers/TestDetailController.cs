using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.HttpHandlers;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    public class TestDetailsController : ControllerBase
    {
        private HtmlControl iframe;

        public ContentPlaceHolder PageContent { get; set; }

        public IVariable<string> PageRank = string.Empty.AsVariable();

        public IVariable<string> MaxPageRank = string.Empty.AsVariable();

        public IVariable<string> QuestionCount = string.Empty.AsVariable();

        public IVariable<string> PageTitle = string.Empty.AsVariable();

        public IVariable<string> PageHeader = string.Empty.AsVariable();

        [ControllerParameter]
        public string AnswerFlag;

        [ControllerParameter]
        public int PageId;


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
            var request =
                RequestBuilder.newRequest("DisplayIudicoTestPage.ipp").AddPageId(PageId.ToString()).AddAnswers(AnswerFlag);

            iframe.Attributes["src"] = request.BuildRequestForHandler();
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
                iframe = (HtmlControl)PageContent.FindControl("testDetailsFrame");
                if (iframe == null)
                    throw new Exception("Can't load iframe");
            }
            else
            {
                throw new Exception("Can't load page Content");
            }
        }
    
        public string GetPageTitleType()
        {
            if (AnswerFlag.ToLower().Equals("user"))
            {
                return "Last Answers";
            }

            if (AnswerFlag.ToLower().Equals("correct"))
            {
                return "Correct Answers";
            }
            return "";
        }
    }
}
