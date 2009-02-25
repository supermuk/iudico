using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using TestingSystem;

namespace IUDICO.DataModel.Controllers
{
    public class StudentPageController : ControllerBase
    {
        public TreeView CurriculumnTreeView { get; set; }

        public HttpResponse Response { get; set; }

        public Table LastPagesResult { get; set; }

        public Button ChangeModeButton { get; set; }

        public Button OpenTestButton { get; set; }

        public Calendar CurriculumnCalendar { get; set; }

        private const int countHowManyPagesToShow = 5;

        [PersistantField] private bool isViewMode = true;

        private const string themeResultRequest = "ThemeResult.aspx?themeId={0}";
        private const string openTestRequest = "OpenTest.aspx?openThema={0}";


        public void openTestButton_Click(object sender, EventArgs e)
        {
            openCorrespondingPage(openTestRequest);
        }

        public void showResultButton_Click(object sender, EventArgs e)
        {
            openCorrespondingPage(themeResultRequest);
        }

        private void openCorrespondingPage(string pageUrlFormat)
        {
            var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;
            
            if (selectedNode.Type == NodeType.Theme)
                Response.Redirect(string.Format(pageUrlFormat, selectedNode.ID));
        }

        public void page_Load(object sender, EventArgs e)
        {
            setCalendarColor();
            if (!((Page) sender).IsPostBack)
            {
                BuildTree(null);
            }
            BuildLatestResultTable();
        }

        public void modeButton_Click(object sender, EventArgs e)
        {
            if (isViewMode)
            {
                ChangeModeButton.Text = "Show View Dates";
                isViewMode = false;
            }
            else
            {
                ChangeModeButton.Text = "Show Pass Dates";
                isViewMode = true;
            }
            selectDatesForSelectedNode();
        }

        public void rebuildTreeButton_Click(object sender, EventArgs e)
        {
            BuildTree(null);
            CurriculumnCalendar.SelectedDates.Clear();
        }

        public void selectedDateChanged(object sender, EventArgs e)
        {
            BuildTree(CurriculumnCalendar.SelectedDate);
        }

        public void curriculumnTree_SelectionChanged(object sender, EventArgs e)
        {
            selectDatesForSelectedNode();
        }

        private void selectDatesForSelectedNode()
        {
            CurriculumnCalendar.SelectedDates.Clear();
            setCalendarColor();

            var selectedNode = (IdendtityNode)CurriculumnTreeView.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Type == NodeType.Curriculum)
                {
                    selectDatesForObject(selectedNode, SECURED_OBJECT_TYPE.CURRICULUM);
                    setOpenTestButtonVisibility(selectedNode, SECURED_OBJECT_TYPE.CURRICULUM);
                }
                if (selectedNode.Type == NodeType.Stage)
                {
                    selectDatesForObject(selectedNode, SECURED_OBJECT_TYPE.STAGE);
                    setOpenTestButtonVisibility(selectedNode, SECURED_OBJECT_TYPE.STAGE);
                }
                if (selectedNode.Type == NodeType.Theme)
                {
                    selectDatesForObject(selectedNode, SECURED_OBJECT_TYPE.THEME);
                    setOpenTestButtonVisibility(selectedNode, SECURED_OBJECT_TYPE.THEME);
                }
            }
        }

        private void setCalendarColor()
        {
            CurriculumnCalendar.SelectedDayStyle.BackColor = isViewMode ? Color.Blue : Color.Green;
        }

        private void selectDatesForObject(IdendtityNode selectedNode, SECURED_OBJECT_TYPE type)
        {
            var permission = StudentHelper.getPermissionForNode(selectedNode, type, isViewMode);

            if (StudentHelper.isBothDatesAreNull(permission.DateSince, permission.DateTill))
            {
                selectDates(DateTime.Today, DateTime.Today.AddYears(1));
            }
            else
            {
                selectDates(permission.DateSince, permission.DateTill);
            }
            
        }

        private void selectDates(DateTime? since, DateTime? till)
        {
            if(since == null)
            {
                since = DateTime.Today;
            }

            if(till == null)
            {
                till = DateTime.Today.AddYears(1);
            }

            if(till > since)
            {
                var nextDay = (DateTime)since;

                while(nextDay <= till)
                {
                    CurriculumnCalendar.SelectedDates.Add(nextDay);
                    nextDay = nextDay.AddDays(1);
                }
            }
        }

        private void BuildLatestResultTable()
        {
            var listOfPages = StudentHelper.GetLatestResults();

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

        private void setOpenTestButtonVisibility(IdendtityNode selectedNode, SECURED_OBJECT_TYPE type)
        {
            var permission = StudentHelper.getPermissionForNode(selectedNode, type, true);

            if (StudentHelper.isBothDatesAreNull(permission.DateSince, permission.DateTill))
            {
                OpenTestButton.Enabled = true;
            }
            else
            {
                OpenTestButton.Enabled = StudentHelper.isDateInPeriod(DateTime.Now, permission.DateSince, permission.DateTill);
            }
            
        }


        private void BuildTree(DateTime? date)
        {
            CurriculumnTreeView.Nodes.Clear();

            var userCurriculumsIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
                ((CustomUser) Membership.GetUser()).ID, FxCurriculumOperations.View.ID, null);
           

            var userCurriculums = ServerModel.DB.Load<TblCurriculums>(userCurriculumsIds);
            
            foreach (var curriculum in userCurriculums)
            {
                var node = new IdendtityNode(curriculum);

                var permission = StudentHelper.getPermission(curriculum.ID, SECURED_OBJECT_TYPE.CURRICULUM, isViewMode);

                if(StudentHelper.isDateInPeriod(date, permission.DateSince, permission.DateTill))
                {
                    BuildStages(curriculum, node, date, isViewMode);

                    if(node.ChildNodes.Count != 0)
                        CurriculumnTreeView.Nodes.Add(node);
                }
            }

            CurriculumnTreeView.ExpandAll();
        }

        private static void BuildStages(TblCurriculums curriculum, TreeNode node, DateTime? date, bool isViewMode)
        {
            var stages = ServerModel.DB.Load<TblStages>(ServerModel.DB.LookupIds<TblStages>(curriculum, null));

            foreach (var stage in stages)
            {
                var child = new IdendtityNode(stage);
                
                var permission = StudentHelper.getPermission(stage.ID, SECURED_OBJECT_TYPE.STAGE, isViewMode);

                if ( (permission == null) || (StudentHelper.isDateInPeriod(date, permission.DateSince, permission.DateTill) ) )
                {
                    BuildThemes(stage, child, date, isViewMode);

                    if (child.ChildNodes.Count != 0)
                        node.ChildNodes.Add(child);
                }
            }
        }

        private static void BuildThemes(TblStages stage, TreeNode node, DateTime? date, bool isViewMode)
        {
            var themesIds = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            var themes = ServerModel.DB.Load<TblThemes>(themesIds);


            foreach (var theme in themes)
            {
                var permission = StudentHelper.getPermission(theme.ID, SECURED_OBJECT_TYPE.THEME, isViewMode);
                
                if ( (permission == null) || (StudentHelper.isDateInPeriod(date, permission.DateSince, permission.DateTill) ) )
                    node.ChildNodes.Add(new IdendtityNode(theme));
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
            return other.Date.CompareTo(Date);
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
                    var userAnswerWithNeededDate = FindUserAnswerWithNeededDate(currUserAnswers, date);

                    if (userAnswerWithNeededDate.UserAnswer == question.CorrectAnswer)
                    {
                        userRank += (int) question.Rank;
                    }
                    else if (userAnswerWithNeededDate.IsCompiledAnswer)
                    {
                        var userCompiledAnswers = ServerModel.DB.Load<TblCompiledAnswers>(ServerModel.DB.LookupIds<TblCompiledAnswers>(userAnswerWithNeededDate, null));

                        bool allAcepted = true;

                        foreach (var compiledAnswer in userCompiledAnswers)
                        {
                            allAcepted &= (compiledAnswer.StatusRef == (int)Status.Accepted);
                        }
                        if(allAcepted)
                        {
                            userRank += (int)question.Rank;
                        }
                    }
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

    class StudentHelper
    {
        public static bool isBothDatesAreNull(DateTime? firstDate, DateTime? secondDate)
        {
            return (firstDate == null) && (secondDate == null);
        }

        public static List<SortPageBydateHelper> GetLatestResults()
        {
            var user = ServerModel.DB.Load<TblUsers>(((CustomUser)Membership.GetUser()).ID);
            var userAnswersIds = ServerModel.DB.LookupIds<TblUserAnswers>(user, null);

            var sortHelpers = new List<SortPageBydateHelper>();

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

        public static TblPermissions getPermissionForNode(IdendtityNode node, SECURED_OBJECT_TYPE type, bool isViewMode)
        {
            var permission = getPermission((node).ID, type, isViewMode);

            if(permission == null)
                return getPermissionForNode(((IdendtityNode)node.Parent), getParentForObject(type), isViewMode);

            return permission;
        }

        public static TblPermissions getPermission(int id, SECURED_OBJECT_TYPE type, bool isViewMode)
        {
            var permissionsIds = PermissionsManager.GetPermissions(type, ((CustomUser)Membership.GetUser()).ID, null, getOperationId(type, isViewMode));
            
            var permisions = ServerModel.DB.Load<TblPermissions>(permissionsIds);

            var permisionsForNode = findObjectPermission(type, id, permisions);

            if (permisionsForNode.Count != 0)
                return permisionsForNode[0];

                return null;
        }

        public static bool isDateInPeriod(DateTime? date, DateTime? startPeriod, DateTime? endPeriod)
        {
            if (date == null)
            {
                return true;
            }

            if (isBothDatesAreNull(startPeriod, endPeriod))
            {
                return true;
            }

            if(startPeriod == null)
            {
                return date <= endPeriod;
            }

            if (endPeriod == null)
            {
                return date >= startPeriod;
            }
            return ((startPeriod <= date) && (date <= endPeriod));
        }

        private static IList<TblPermissions> findObjectPermission(SECURED_OBJECT_TYPE type, int objectId, IList<TblPermissions> allPermissions)
        {
            switch (type)
            {
                case (SECURED_OBJECT_TYPE.CURRICULUM):
                    {
                        return findPerrmisionForCurriculumn(objectId, allPermissions);
                    }
                case (SECURED_OBJECT_TYPE.STAGE):
                    {
                        return findPerrmisionForStage(objectId, allPermissions);
                    }
                case (SECURED_OBJECT_TYPE.THEME):
                    {
                        return findPerrmisionForTheme(objectId, allPermissions);
                    }
            }
            return new List<TblPermissions>();
        }

        private static IList<TblPermissions> findPerrmisionForStage(int stageId, IList<TblPermissions> allPermisions)
        {
            var list = new List<TblPermissions>();

            foreach (var permission in allPermisions)
            {
                if(permission.StageRef == stageId)
                {
                    list.Add(permission);
                }
            }

            return list;
        }

        private static IList<TblPermissions> findPerrmisionForCurriculumn(int curriculumnId, IList<TblPermissions> allPermisions)
        {
            var list = new List<TblPermissions>();

            foreach (var permission in allPermisions)
            {
                if (permission.CurriculumRef == curriculumnId)
                {
                    list.Add(permission);
                }
            }

            return list;
        }

        private static IList<TblPermissions> findPerrmisionForTheme(int themeId, IList<TblPermissions> allPermisions)
        {
            var list = new List<TblPermissions>();

            foreach (var permission in allPermisions)
            {
                if (permission.ThemeRef == themeId)
                {
                    list.Add(permission);
                }
            }

            return list;
        }

        private static FxCurriculumOperations getFxOperationForCurriculumn(bool isViewMode)
        {
            return isViewMode ? FxCurriculumOperations.View : FxCurriculumOperations.Pass; 
        }

        private static FxStageOperations getFxOperationForStage(bool isViewMode)
        {
            return isViewMode ? FxStageOperations.View : FxStageOperations.Pass;
        }

        private static FxThemeOperations getFxOperationForTheme(bool isViewMode)
        {
            return isViewMode ? FxThemeOperations.View : FxThemeOperations.Pass;
        }

        private static int getOperationId(SECURED_OBJECT_TYPE type, bool isViewMode)
        {
            switch (type)
            {
                case(SECURED_OBJECT_TYPE.CURRICULUM):
                    {
                        return getFxOperationForCurriculumn(isViewMode).ID;
                    }
                case (SECURED_OBJECT_TYPE.STAGE):
                    {
                        return getFxOperationForStage(isViewMode).ID;
                    }
                case (SECURED_OBJECT_TYPE.THEME):
                    {
                        return getFxOperationForTheme(isViewMode).ID;
                    }
                default:
                    return 0;
            }
        }

        private static SECURED_OBJECT_TYPE getParentForObject(SECURED_OBJECT_TYPE type)
        {
            switch (type)
            {
                case (SECURED_OBJECT_TYPE.STAGE):
                    {
                        return SECURED_OBJECT_TYPE.CURRICULUM;
                    }
                case (SECURED_OBJECT_TYPE.THEME):
                    {
                        return SECURED_OBJECT_TYPE.STAGE;
                    }
                default:
                    return SECURED_OBJECT_TYPE.CURRICULUM;
            }
        }
    }
}
