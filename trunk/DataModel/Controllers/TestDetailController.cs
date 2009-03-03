using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class TestDetailsController : ControllerBase
    {
        public HttpRequest Request { get; set; }

        private HtmlControl iframe;

        public ContentPlaceHolder PageContent { get; set; }

        public Label PageRank { get; set; }

        public Label MaxPageRank { get; set; }

        public Label QuestionCount { get; set; }

        public string pageIDRequestParameter = "pageId";

        private const string testPageRequestPattern = "DisplayIudicoTestPage.ipp?pageId={0}&submit=false&answers=true&themeId=0&pageIndex=0";


        public void PageLoad(object sender, EventArgs e)
        {
            LoadIFrame();

            ShowPage();
        }

        private void ShowPage()
        {
            if(Request[pageIDRequestParameter] != null)
            {
                TblPages page = GetPage(Request[pageIDRequestParameter]);

                var questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));

                SetMaxRankLabel(GetMaxRank(questions));
                SetRankLabel((int) page.PageRank);
                SetCountLabel(questions.Count);
                SetUrl(page.ID);
            }
            else
            {
                throw new Exception("Page ID is not specified");
            }
        }

        private void SetMaxRankLabel(int rank)
        {
            MaxPageRank.Text = "Maximal Posible Rank: " + rank;
        }

        private void SetRankLabel(int rank)
        {
            PageRank.Text = "Page Rank: " + rank;
        }

        private void SetCountLabel(int count)
        {
            QuestionCount.Text = "Questions on Page: " + count;
        }

        private void SetUrl(int pageId)
        {
            iframe.Attributes["src"] = string.Format(testPageRequestPattern, pageId);
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

        private static TblPages GetPage(string pageId)
        {
            return ServerModel.DB.Load<TblPages>(int.Parse(pageId));
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
    }
}
