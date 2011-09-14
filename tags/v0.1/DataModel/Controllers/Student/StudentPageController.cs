using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers.Student
{
    public class StudentPageController : ControllerBase
    {
        private const int CountHowManyPagesToShow = 5;
        private IList<DateTime?> _noteDates;

        private int _userId;
        
        [PersistantField] private bool _isViewOperations = true;

        [PersistantField] private bool _isControlInProgress;

        public OperationType GetOperationType
        {
            get
            {
                if (_isViewOperations)
                    return OperationType.View;

                return OperationType.Pass;
            }
        }

        public readonly IVariable<string> Description = string.Empty.AsVariable();

        public readonly IVariable<string> UserName = string.Empty.AsVariable();


        #region Page Controls
        
        public TreeView CurriculumnTreeView { get; set; }

        public Table LastPagesResultTable { get; set; }

        public Button ChangeModeButton { get; set; }

        public Button OpenTestButton { get; set; }

        public Button RebuildTreeButton { get; set; }

        public Calendar CurriculumnCalendar { get; set; }

        public ListBox PeriodDescription;

        public TextBox UserDescription;

        public Button DescriptionButton;

        public Button ShowDescription;
            
        
        #endregion


        public void OpenTestButtonClick(object sender, EventArgs e)
        {
            if (CurriculumnTreeView.SelectedNode != null)
            {
                var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;

                if (selectedNode.Type == NodeType.Theme)
                {
                    var shifter = new PageShifter(selectedNode.ID);
                    StatisticManager.MarkNotIncludedPages(shifter.NotUsedPages);
                    StatisticManager.MarkUsedPages(shifter.UsedPages);
                    RedirectToController(new OpenTestController
                                                 {
                                                     BackUrl = string.Empty,
                                                     ThemaId = selectedNode.ID,
                                                     CurriculumnId = ((IdendtityNode) selectedNode.Parent.Parent).ID,
                                                     StageId = ((IdendtityNode) selectedNode.Parent).ID,
                                                     PageIndex = 0,
                                                     PagesIds = shifter.GetRequestParameter()
                                                 });
                }
            }
        }

        public void ShowDescriptionButtonClick(object sender, EventArgs e)
        {
            CurriculumnCalendar.SelectedDates.Clear();
            CurriculumnCalendar.SelectedDayStyle.BackColor = Color.Orange;
            foreach (DateTime date in StudentRecordFinder.GetAllDates(_userId))
            {
                CurriculumnCalendar.SelectedDates.Add(date);
            }
        }
        public void SetDescriptionButtonClick(object sender, EventArgs e)
        {
            StudentRecordFinder.SetDataDescription(_userId, CurriculumnCalendar.SelectedDate, UserDescription.Text);
        }
        public void ShowResultButtonClick(object sender, EventArgs e)
        {
            if (CurriculumnTreeView.SelectedNode != null)
            {
                var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;

                if (selectedNode.Type == NodeType.Theme)
                    RedirectToController(new ThemeResultController
                                             {
                                                 BackUrl = string.Empty,
                                                 ThemeId = selectedNode.ID,
                                                 CurriculumnName = selectedNode.Parent.Parent.Text,
                                                 StageName = selectedNode.Parent.Text,
                                                 UserId = _userId
                                             });

                if (selectedNode.Type == NodeType.Stage)
                    RedirectToController(new StageResultController
                                             {
                                                 BackUrl = string.Empty,
                                                 StageId = selectedNode.ID,
                                                 CurriculumnName = selectedNode.Parent.Text,
                                                 UserId = _userId
                                             });

                if (selectedNode.Type == NodeType.Curriculum)
                    RedirectToController(new CurriculumnResultController
                                             {
                                                 BackUrl = string.Empty,
                                                 CurriculumnId = selectedNode.ID,
                                                 UserId = _userId
                                             });
            }
        }

        public void PageLoad(object sender, EventArgs e)
        {
            if (ServerModel.User.Current != null)
            {
                _userId = ServerModel.User.Current.ID;
                UserName.Value = ServerModel.User.Current.UserName;

                if (!((Page)sender).IsPostBack)
                {
                    ShowCommonDescription();
                    var controlInfo = BuildTree(null);

                    if (controlInfo != null && controlInfo.IsControlStartsNow)
                    {
                        DoPreControlPreparation();
                        BuildTreeForControl(controlInfo);
                        SelectDatesPeriods(controlInfo.DatePeriods);
                        UserDescription.Text = StudentRecordFinder.GetDateDescription(_userId, CurriculumnCalendar.SelectedDate);
                        _isControlInProgress = true;
                    }
                }
                BuildLatestResultTable();
            }
        }

        public void ModeButtonClick(object sender, EventArgs e)
        {
                ChangeModeButton.Text = string.Format("Show {0} Dates", GetOperationType);

                _isViewOperations = !_isViewOperations;

                SelectDatesForSelectedNode();

                if (CurriculumnTreeView.SelectedNode == null)
                    ShowDescriptionForDataSelection();
                else
                    ShowDescriptionForNodeSelection();
        }

        public void RebuildTreeButtonClick(object sender, EventArgs e)
        {
            BuildTree(null);
            CurriculumnCalendar.SelectedDates.Clear();
            PeriodDescription.Items.Clear();
        }

        public void SelectedDateChanged(object sender, EventArgs e)
        {
            CurriculumnCalendar.SelectedDayStyle.BackColor = Color.Gray;
            OpenTestButton.Enabled = false;
            ShowDescriptionForDataSelection();
            BuildTree(CurriculumnCalendar.SelectedDate);
            UserDescription.Text = StudentRecordFinder.GetDateDescription(_userId, CurriculumnCalendar.SelectedDate);
        }

        public void CurriculumnTreeSelectionChanged(object sender, EventArgs e)
        {
            if (!_isControlInProgress)
            {
                ShowDescriptionForNodeSelection();
                SelectDatesForSelectedNode();
            }
            SetButtonVisibility();
        }


        private void BuildTreeForControl(ControlInfo info)
        {
            CurriculumnTreeView.Nodes.Clear();

            var themeNode = new IdendtityNode(info.Theme);

            var stageNode = new IdendtityNode(info.Stage);
            stageNode.ChildNodes.Add(themeNode);

            var currNode = new IdendtityNode(info.Curriculumn);
            currNode.ChildNodes.Add(stageNode);

            CurriculumnTreeView.Nodes.Add(currNode);
            CurriculumnTreeView.ExpandAll();
        }

        private void SelectDatesForSelectedNode()
        {
            CurriculumnCalendar.SelectedDates.Clear();
            SetCalendarColor();

            var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;

            if (selectedNode != null)
                SelectDatesForObject(selectedNode);
        }

        private void SetCalendarColor()
        {
            CurriculumnCalendar.SelectedDayStyle.BackColor = _isViewOperations ? Color.Blue : Color.Green;
        }

        private void DoPreControlPreparation()
        {
            CurriculumnCalendar.SelectedDayStyle.BackColor = Color.Red;
            ChangeModeButton.Enabled = false;
            RebuildTreeButton.Enabled = false;
            CurriculumnCalendar.Enabled = false;
            ShowDescriptionForControl();
        }

        private void SelectDatesForObject(IdendtityNode selectedNode)
        {
            IList<DatePeriod> datePeriods;


            if(selectedNode.Type == NodeType.Theme)
                datePeriods = StudentPermissionsHelper.GetPermissionsDatePeriods(_userId, ((IdendtityNode)selectedNode.Parent).ID, ((IdendtityNode)selectedNode.Parent).Type, GetOperationType);
            else
                datePeriods = StudentPermissionsHelper.GetPermissionsDatePeriods(_userId, selectedNode.ID, selectedNode.Type, GetOperationType);


            if (datePeriods.Count == 0)
                SelectDates(DateTime.Today, DateTime.Today.AddYears(1));
            else
                SelectDatesPeriods(datePeriods);
        }

        private void SelectDatesPeriods(IList<DatePeriod> periods)
        {
            PeriodDescription.Items.Clear();
            
            foreach (var p in periods)
                SelectDates(p.Start, p.End);
        }

        private void SelectDates(DateTime? since, DateTime? till)
        {
            if (since == null)
                since = DateTime.Today;

            if (till == null)
                till = DateTime.Today.AddYears(1);

            if (till >= since)
            {
                var nextDay = (DateTime) since;

                while (nextDay <= till)
                {
                    CurriculumnCalendar.SelectedDates.Add(nextDay);
                    nextDay = nextDay.AddDays(1);
                }
            }
            PeriodDescription.Items.Add(string.Format("{0} - {1}", since, till));
        }

        private void BuildLatestResultTable()
        {
            var listOfPages = StatisticManager.GetUserLatestAnswers(_userId);

            int countOfShowedPages = Math.Min(CountHowManyPagesToShow, listOfPages.Count);

            LastPagesResultTable.Visible = (countOfShowedPages != 0);
                
            for (int i = 0; i < countOfShowedPages; i++)
            {
                var pageNameCell = new TableCell {Text = listOfPages[i].PageName};
                var themeNameCell = new TableCell {Text = listOfPages[i].ThemeName};
                var pageStatusPageCell = new TableCell {Text = listOfPages[i].GetStatus().ToString()};
                var dateCell = new TableCell {Text = listOfPages[i].Date.ToString()};

                var row = new TableRow();
                row.Cells.AddRange(new[] {pageNameCell, themeNameCell, pageStatusPageCell, dateCell});
                LastPagesResultTable.Rows.Add(row);        
            }
        }

        private void SetButtonVisibility()
        {
            var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Type == NodeType.Curriculum || selectedNode.Type == NodeType.Stage)
                    OpenTestButton.Enabled = false;
                else
                    OpenTestButton.Enabled = ConditionChecker.IsViewingAllowed(((IdendtityNode)selectedNode.Parent).ID);
            }
        }

        [CanBeNull]
        private ControlInfo BuildTree(DateTime? date)
        {
            CurriculumnTreeView.Nodes.Clear();
            var userCurriculums = StudentRecordFinder.GetCurriculumnsForUser(_userId);

            var controlInfo = new ControlInfo();

            foreach (TblCurriculums curriculum in userCurriculums)
            {
                var node = new IdendtityNode(curriculum);

                if (StudentPermissionsHelper.IsDateAllowed(date, _userId, node.ID, node.Type, GetOperationType))
                {
                    controlInfo = BuildStages(curriculum, node, date);

                    if (controlInfo != null && controlInfo.IsControlStartsNow)
                        return controlInfo;

                    if (node.ChildNodes.Count != 0)
                        CurriculumnTreeView.Nodes.Add(node);
                }
            }

            CurriculumnTreeView.ExpandAll();

            return controlInfo;
        }

        private ControlInfo BuildStages(TblCurriculums curriculum, TreeNode node, DateTime? date)
        {
            IList<TblStages> stages = StudentRecordFinder.GetStagesForCurriculum(curriculum);

            ControlInfo controlInfo = new ControlInfo();

            foreach (TblStages stage in stages)
            {
                var child = new IdendtityNode(stage);

                if (StudentPermissionsHelper.IsDateAllowed(date, _userId, stage.ID, NodeType.Stage, GetOperationType))
                {
                    controlInfo = BuildThemes(stage, child);

                    if (controlInfo.IsControlStartsNow)
                    {
                        controlInfo.AddCurriculumnToInfo(curriculum);
                        return controlInfo;
                    }
                    if (child.ChildNodes.Count != 0)
                        node.ChildNodes.Add(child);
                }
            }

            return controlInfo;
        }

        private ControlInfo BuildThemes(TblStages stage, TreeNode node)
        {
            var themes = StudentRecordFinder.GetThemesForStage(stage);

            ControlInfo controlInfo = new ControlInfo();

            foreach (TblThemes theme in themes)
            {
                controlInfo = StudentPermissionsHelper.IsTimeForControl(_userId, stage, theme);

                if (controlInfo.IsControlStartsNow)
                    return controlInfo;

                if(!theme.IsControl)
                    node.ChildNodes.Add(new IdendtityNode(theme));
            }

            return controlInfo;
        }


        private void ShowDescriptionForDataSelection()
        {
            Description.Value = string.Format("Display themes that you can {0} in selected date", GetOperationType);
        }

        private void ShowCommonDescription()
        {
            Description.Value =
                string.Format("From this page you can open test or see test results; And see you last {0} result",
                              CountHowManyPagesToShow);
        }

        private void ShowDescriptionForControl()
        {
            Description.Value = "You have control now. Do you best";
        }

        private void ShowDescriptionForNodeSelection()
        {
            Description.Value = string.Format("In calendar your see dates when you can {0} test", GetOperationType);
        }
    }
}