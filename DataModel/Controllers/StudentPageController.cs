using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Controllers
{
    public class StudentPageController : ControllerBase
    {
        public TreeView CurriculumnTreeView { get; set; }

        public HttpResponse Response { get; set; }

        public Table LastPagesResult { get; set; }

        [PersistantField]
        private bool isTreeBuilded;

        private const int countHowManyPagesToShow = 5;

        private const string isThemeFlag = "theme:";

        private const string themeResultRequest = "ThemeResult.aspx?themeId={0}";
        private const string openTestRequest = "OpenTest.aspx?openThema={0}";

        public void openTestButton_Click(object sender, EventArgs e)
        {
            if (isTheme())
                Response.Redirect(string.Format(openTestRequest, GetThemeId()));
        }

        private int GetThemeId()
        {
            return int.Parse(CurriculumnTreeView.SelectedNode.Value.Replace(isThemeFlag, string.Empty));
        }

        private bool isTheme()
        {
            return CurriculumnTreeView.SelectedNode.Value.Contains(isThemeFlag);
        }

        public void showResultButton_Click(object sender, EventArgs e)
        {
            if (isTheme())
                Response.Redirect(string.Format(themeResultRequest, GetThemeId()));
        }

        public void page_Load(object sender, EventArgs e)
        {
            if (!isTreeBuilded)
            {
                BuildTree();
                isTreeBuilded = true;
            }
            BuildLatestResultTable();
        }

        private void BuildLatestResultTable()
        {
            var listOfPages = GetLatestResults();

            int countOfShowedPages = listOfPages.Count > countHowManyPagesToShow
                                         ? countHowManyPagesToShow
                                         : listOfPages.Count;

            for (int i = 0; i < countOfShowedPages; i++)
            {
                var pageNameCell = new TableCell{Text = listOfPages[i].PageName};
                var themeNameCell = new TableCell{ Text = listOfPages[i].ThemeName};
                var pageStatusPageCell = new TableCell{ Text = listOfPages[i].GetStatus};
                var dateCell = new TableCell{ Text = listOfPages[i].Date.ToString() };

                var row = new TableRow();
                row.Cells.AddRange(new[] { pageNameCell, themeNameCell, pageStatusPageCell, dateCell});
                LastPagesResult.Rows.Add(row);

            }
        }

        private static List<SortPageBydateHelper> GetLatestResults()
        {
            var user = ServerModel.DB.Load<TblUsers>(((CustomUser) Membership.GetUser()).ID);
            var userAnswersIds = ServerModel.DB.LookupIds<TblUserAnswers>(user, null);

            List<SortPageBydateHelper> sortHelpers = new List<SortPageBydateHelper>();
            
            foreach (var answerId in userAnswersIds)
            {
                var currHelper = new SortPageBydateHelper(answerId);

                if (!sortHelpers.Contains(currHelper))
                {
                    sortHelpers.Add(currHelper);
                }
            }

            sortHelpers.Sort();

            return sortHelpers;
        }

        private void BuildTree()
        {
            var userCurriculumsIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
                ((CustomUser) Membership.GetUser()).ID, null, null);
           

            var userCurriculums = ServerModel.DB.Load<TblCurriculums>(userCurriculumsIds);
            
            foreach (var curriculum in userCurriculums)
            {
                var node = new TreeNode(curriculum.Name, curriculum.ID.ToString());
                CurriculumnTreeView.Nodes.Add(node);
                BuildStages(curriculum, node);
            }
        }

        private static void BuildStages(TblCurriculums curriculum, TreeNode node)
        {
            var stages = ServerModel.DB.Load<TblStages>(ServerModel.DB.LookupIds<TblStages>(curriculum, null));

            foreach (var stage in stages)
            {
                var child = new TreeNode(stage.Name, stage.ID.ToString());
                node.ChildNodes.Add(child);
                BuildThemes(stage, child);
            }
        }

        private static void BuildThemes(TblStages stage, TreeNode node)
        {
            var themesIds = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            var themes = ServerModel.DB.Load<TblThemes>(themesIds);


            foreach (var theme in themes)
            {
                node.ChildNodes.Add(new TreeNode(theme.Name, isThemeFlag + theme.ID));
            }
        }
    }

    class SortPageBydateHelper : IComparable<SortPageBydateHelper>
    {
        public string ThemeName { get; set; }

        public string PageName
        { 
            get
            {
                return page.PageName;
            }
        }

        private readonly TblPages page;

        public DateTime Date { get; set; }

        public SortPageBydateHelper(int userAnswerId)
        {
            var ans = ServerModel.DB.Load<TblUserAnswers>(userAnswerId);

            Date = (DateTime)ans.Date;

            var que = ServerModel.DB.Load<TblQuestions>((int)ans.QuestionRef);

            page = ServerModel.DB.Load<TblPages>((int)que.PageRef);

            var theme = ServerModel.DB.Load<TblThemes>((int)page.ThemeRef);

            ThemeName = theme.Name;

        }

        public int CompareTo(SortPageBydateHelper other)
        {
            return Date.CompareTo(other.Date);
        }

        public override bool Equals(object obj)
        {
            var obj2 = obj as SortPageBydateHelper;

            if (obj2 != null)
                return page.ID == obj2.page.ID && Date.Equals(obj2.Date);

            return false;
        }

        public override int GetHashCode()
        {
                int result = page.ID;
                result = (result * 397) ^ (ThemeName != null ? ThemeName.GetHashCode() : 0);
                result = (result * 397) ^ (PageName != null ? PageName.GetHashCode() : 0);
                result = (result * 397) ^ Date.GetHashCode();
                return result;
        }

        public string GetStatus
        {
            get
            {
                var userRank = GetUserRank(page, ((CustomUser) Membership.GetUser()).ID, Date);

                return userRank >= (int)page.PageRank ? "pass" : "fail";
            }
        }

        private static int CalculateUserRank(TblQuestions question, int userId, DateTime date)
        {
            int userRank = 0;

            var userAnswers = ServerModel.DB.Load<TblUserAnswers>(ServerModel.DB.LookupIds<TblUserAnswers>(question, null));

            if (userAnswers != null)
            {
                var currUserAnswers = FindUserAnswers(userAnswers, userId);
                if (currUserAnswers.Count != 0)
                {
                    var latestUserAnswer = FindUserAnswerWithNeededDate(currUserAnswers, date);

                    if (latestUserAnswer.UserAnswer == question.CorrectAnswer)
                        userRank += (int)question.Rank;
                }
                else
                {
                    userRank = -1;
                }
            }

            return userRank;
        }

        private static TblUserAnswers FindUserAnswerWithNeededDate(IList<TblUserAnswers> userAnswers, DateTime date)
        {
            foreach (var o in userAnswers)
            {
                if (o.Date.ToString().Equals(date.ToString()))
                    return o;
            }

            return null;
        }

        private static IList<TblUserAnswers> FindUserAnswers(IList<TblUserAnswers> userAnswers, int userId)
        {
            var currentUserAnswers = new List<TblUserAnswers>();

            foreach (var o in userAnswers)
            {
                if (o.UserRef == userId)
                    currentUserAnswers.Add(o);
            }

            return currentUserAnswers;
        }

        private static int GetUserRank(TblPages page, int userId, DateTime date)
        {
            var questionsIDs = ServerModel.DB.LookupIds<TblQuestions>(page, null);
            var questions = ServerModel.DB.Load<TblQuestions>(questionsIDs);

            int userRank = 0;

            foreach (var question in questions)
            {
                userRank += CalculateUserRank(question, userId, date);
            }
            return userRank;
        }
    }
}
