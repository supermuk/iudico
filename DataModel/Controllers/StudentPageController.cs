using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    public class StudentPageController : ControllerBase
    {
        public TreeView CurriculumnTreeView { get; set; }

        public Table LastPagesResultTable { get; set; }

        public Button ChangeModeButton { get; set; }

        public Button OpenTestButton { get; set; }

        public Calendar CurriculumnCalendar { get; set; }

        private const int countHowManyPagesToShow = 5;

        public IVariable<string> UserName = string.Empty.AsVariable();

        public IVariable<string> Description = string.Empty.AsVariable();

        [PersistantField] private bool isViewMode = true;

        [PersistantField] private bool isControlNow;


        public void OpenTestButton_Click(object sender, EventArgs e)
        {
            var selectedNode = (IdendtityNode)CurriculumnTreeView.SelectedNode;

            var permissions = StudentHelper.GetPermissionForNode(selectedNode, SECURED_OBJECT_TYPE.THEME, false);

            bool showSubmit = StudentHelper.IsDateAllowed(DateTime.Today, permissions);

            if (selectedNode.Type == NodeType.Theme)
                RedirectToController(new OpenTestController { BackUrl = string.Empty, OpenThema = selectedNode.ID, Submit = showSubmit.ToString(), PageIndex = 0 });
        }

        public void ShowResultButton_Click(object sender, EventArgs e)
        {
            var selectedNode = (IdendtityNode)CurriculumnTreeView.SelectedNode;
            if (selectedNode.Type == NodeType.Theme)
                RedirectToController(new ThemeResultController{ BackUrl = string.Empty, ThemeId = selectedNode.ID });
        }

        public void Page_Load(object sender, EventArgs e)
        {
            SetCalendarColor();
            if (!((Page) sender).IsPostBack)
            {
                BuildTree(null);
            }
            BuildLatestResultTable();

            UserName.Value = ServerModel.User.Current.UserName;
            if (isControlNow)
            {
                Description.Value = "You have control now. Do you best";
            }
            else
            {
                Description.Value = string.Format("From this page you can open test or see test results; And see you last {0} result", countHowManyPagesToShow);
            }
        }

        public void ModeButton_Click(object sender, EventArgs e)
        {
            if (isControlNow)
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
                SelectDatesForSelectedNode();
            }
        }

        public void RebuildTreeButton_Click(object sender, EventArgs e)
        {
            if (!isControlNow)
            {
                BuildTree(null);
                CurriculumnCalendar.SelectedDates.Clear();
            }
        }

        public void SelectedDateChanged(object sender, EventArgs e)
        {

            if (!isControlNow)
            {
                Description.Value = string.Format("Display themes that you can {0} in selected date",
                                                  isViewMode ? "view" : "pass");
                BuildTree(CurriculumnCalendar.SelectedDate);
            }
        }

        public void CurriculumnTree_SelectionChanged(object sender, EventArgs e)
        {
            Description.Value = string.Format("In calendar your see dates when you can {0} test", isViewMode ? "view" : "pass");
            SelectDatesForSelectedNode();
        }

        private void SelectDatesForSelectedNode()
        {
            CurriculumnCalendar.SelectedDates.Clear();
            SetCalendarColor();

            var selectedNode = (IdendtityNode)CurriculumnTreeView.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Type == NodeType.Curriculum)
                {
                    SelectDatesForObject(selectedNode, SECURED_OBJECT_TYPE.CURRICULUM);
                    SetOpenTestButtonVisibility(selectedNode, SECURED_OBJECT_TYPE.CURRICULUM);
                }
                if (selectedNode.Type == NodeType.Stage)
                {
                    SelectDatesForObject(selectedNode, SECURED_OBJECT_TYPE.STAGE);
                    SetOpenTestButtonVisibility(selectedNode, SECURED_OBJECT_TYPE.STAGE);
                }
                if (selectedNode.Type == NodeType.Theme)
                {
                    SelectDatesForObject((IdendtityNode) selectedNode.Parent, SECURED_OBJECT_TYPE.STAGE);
                    SetOpenTestButtonVisibility((IdendtityNode) selectedNode.Parent, SECURED_OBJECT_TYPE.STAGE);
                }
            }
        }

        private void SetCalendarColor()
        {
            CurriculumnCalendar.SelectedDayStyle.BackColor = isViewMode ? Color.Blue : Color.Green;

            if (isControlNow)
                CurriculumnCalendar.SelectedDayStyle.BackColor = Color.Red;
        }

        private void SelectDatesForObject(IdendtityNode selectedNode, SECURED_OBJECT_TYPE type)
        {
            var permissions = StudentHelper.GetPermissionForNode(selectedNode, type, isViewMode);

            if(permissions.Count == 0)
            {
                SelectDates(DateTime.Today, DateTime.Today.AddYears(1));
            }
            else
            {
                foreach (var permission in permissions)
                {
                    SelectDates(permission.DateSince, permission.DateTill);
                }
            }
 
        }

        private void SelectDates(DateTime? since, DateTime? till)
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
            var listOfPages = UserResultCalculator.GetLatestResults();

            int countOfShowedPages = Math.Min(countHowManyPagesToShow, listOfPages.Count);

            if (countOfShowedPages != 0)
            {
                for (int i = 0; i < countOfShowedPages; i++)
                {
                    var pageNameCell = new TableCell {Text = listOfPages[i].PageName};
                    var themeNameCell = new TableCell {Text = listOfPages[i].ThemeName};
                    var pageStatusPageCell = new TableCell {Text = listOfPages[i].GetStatus()};
                    var dateCell = new TableCell {Text = listOfPages[i].Date.ToString()};

                    var row = new TableRow();
                    row.Cells.AddRange(new[] {pageNameCell, themeNameCell, pageStatusPageCell, dateCell});
                    LastPagesResultTable.Rows.Add(row);
                }
            }
            else
            {
                LastPagesResultTable.Visible = false;
            }
        }

        private void SetOpenTestButtonVisibility(IdendtityNode selectedNode, SECURED_OBJECT_TYPE type)
        {
            var permissions = StudentHelper.GetPermissionForNode(selectedNode, type, true);

            OpenTestButton.Enabled = StudentHelper.IsDateAllowed(DateTime.Now, permissions);
            
        }

        private void BuildTree(DateTime? date)
        {
            CurriculumnTreeView.Nodes.Clear();

            var userCurriculumsIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
                ServerModel.User.Current.ID, FxCurriculumOperations.View.ID, null);
           

            var userCurriculums = ServerModel.DB.Load<TblCurriculums>(userCurriculumsIds);
            
            foreach (var curriculum in userCurriculums)
            {
                var node = new IdendtityNode(curriculum);

                var permissions = StudentHelper.GetPermission(curriculum.ID, SECURED_OBJECT_TYPE.CURRICULUM, isViewMode);

                    if (StudentHelper.IsDateAllowed(date, permissions))
                    {
                        BuildStages(curriculum, node, date);

                        if (isControlNow)
                        {
                            CurriculumnTreeView.Nodes.Clear();
                            CurriculumnTreeView.Nodes.Add(node);
                            return;
                        }

                        if (node.ChildNodes.Count != 0)
                            CurriculumnTreeView.Nodes.Add(node);
                    }
            }

            CurriculumnTreeView.ExpandAll();
        }

        private void BuildStages(TblCurriculums curriculum, TreeNode node, DateTime? date)
        {
            var stages = ServerModel.DB.Load<TblStages>(ServerModel.DB.LookupIds<TblStages>(curriculum, null));

            foreach (var stage in stages)
            {
                var child = new IdendtityNode(stage);
                
                var permissions = StudentHelper.GetPermission(stage.ID, SECURED_OBJECT_TYPE.STAGE, isViewMode);

                if (StudentHelper.IsDateAllowed(date, permissions))
                {
                    BuildThemes(stage, child);

                    if(isControlNow)
                    {
                        node.ChildNodes.Clear();
                        node.ChildNodes.Add(child);
                        return;
                    }

                    if (child.ChildNodes.Count != 0)
                        node.ChildNodes.Add(child);
                }
            }
        }

        private void BuildThemes(TblStages stage, TreeNode node)
        {
            var themesIds = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            var themes = ServerModel.DB.Load<TblThemes>(themesIds);

            foreach (var theme in themes)
            {
                CheckForControlChapterTime(stage.ID, theme);

                if(isControlNow)
                {
                    node.ChildNodes.Clear();
                    node.ChildNodes.Add(new IdendtityNode(theme));
                    return;
                }

                if(!theme.IsControl)
                    node.ChildNodes.Add(new IdendtityNode(theme));
            }
        }

        private void CheckForControlChapterTime(int stageId, TblThemes theme)
        {
            var permissions = StudentHelper.GetPermission(stageId, SECURED_OBJECT_TYPE.STAGE, false);

            if (theme.IsControl)
                if(StudentHelper.IsDateAllowed(DateTime.Now, permissions))


            isControlNow = false;
        }
    }


}
