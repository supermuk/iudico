using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers.Student
{
    public class StudentPageController : ControllerBase
    {
        private const int CountHowManyPagesToShow = 5;

        private int _userId;

        [PersistantField] private bool _isControlNow;
        
        [PersistantField] private bool _isViewMode = true;

        public readonly IVariable<string> Description = string.Empty.AsVariable();

        public readonly IVariable<string> UserName = string.Empty.AsVariable();


        #region Page Controls
        
        public TreeView CurriculumnTreeView { get; set; }

        public Table LastPagesResultTable { get; set; }

        public Button ChangeModeButton { get; set; }

        public Button OpenTestButton { get; set; }

        public Calendar CurriculumnCalendar { get; set; }

        public ListBox PeriodDescription;
        
        #endregion


        public void OpenTestButtonClick(object sender, EventArgs e)
        {
            if (CurriculumnTreeView.SelectedNode != null)
            {
                var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;

                if (selectedNode.Type == NodeType.Theme)
                {
                    IList<TblPermissions> permissions = StudentHelper.GetPermissionForNode(_userId, selectedNode, false);

                    bool showSubmit = StudentHelper.IsDateAllowed(DateTime.Now, permissions);

                    if (selectedNode.Type == NodeType.Theme)
                        RedirectToController(new OpenTestController
                                                 {
                                                     BackUrl = string.Empty,
                                                     OpenThema = selectedNode.ID,
                                                     Submit = showSubmit.ToString(),
                                                     CurriculumnName = selectedNode.Parent.Parent.Text,
                                                     StageName = selectedNode.Parent.Text,
                                                     PageIndex = 0,
                                                     ShiftedPagesIds = StudentHelper.GetShiftedPagesIds(selectedNode.ID)
                                                 });
                }
            }
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
                                                 StageName = selectedNode.Parent.Text
                                             });

                if (selectedNode.Type == NodeType.Stage)
                    RedirectToController(new StageResultController
                                             {
                                                 BackUrl = string.Empty,
                                                 StageId = selectedNode.ID,
                                                 CurriculumnName = selectedNode.Parent.Text,
                                             });

                if (selectedNode.Type == NodeType.Curriculum)
                    RedirectToController(new CurriculumnResultController
                                             {
                                                 BackUrl = string.Empty,
                                                 CurriculumnId = selectedNode.ID
                                             });
            }
        }

        public void PageLoad(object sender, EventArgs e)
        {
            if (ServerModel.User.Current != null)
                _userId = ServerModel.User.Current.ID;

            SetCalendarColor();

            if (!((Page) sender).IsPostBack)
                BuildTree(null);

            BuildLatestResultTable();

            if (ServerModel.User.Current != null)
                UserName.Value = ServerModel.User.Current.UserName;

            if (_isControlNow)
            {
                CurriculumnCalendar.Enabled = false;
                ShowDescriptionForControl();
            }
            else
            {
                ShowCommonDescription();
            }
        }

        public void ModeButtonClick(object sender, EventArgs e)
        {
            if (!_isControlNow)
            {
                ChangeModeButton.Text = _isViewMode ? "Show View Dates" : "Show Pass Dates";

                _isViewMode = !_isViewMode;
                SelectDatesForSelectedNode();

                if (CurriculumnTreeView.SelectedNode == null)
                    ShowDescriptionForDataSelection();
                else
                    ShowDescriptionForNodeSelection();
            }
        }

        public void RebuildTreeButtonClick(object sender, EventArgs e)
        {
            if (!_isControlNow)
            {
                BuildTree(null);
                CurriculumnCalendar.SelectedDates.Clear();
                PeriodDescription.Items.Clear();
            }
        }

        public void SelectedDateChanged(object sender, EventArgs e)
        {
            PeriodDescription.Items.Clear();
            OpenTestButton.Enabled = false;
            if (!_isControlNow)
            {
                ShowDescriptionForDataSelection();
                BuildTree(CurriculumnCalendar.SelectedDate);
            }
        }

        public void CurriculumnTreeSelectionChanged(object sender, EventArgs e)
        {
            if (!_isControlNow)
            {
                ShowDescriptionForNodeSelection();
                SelectDatesForSelectedNode();
            }
            SetButtonVisibility();
        }


        private void SelectDatesForSelectedNode()
        {
            CurriculumnCalendar.SelectedDates.Clear();
            SetCalendarColor();

            var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;

            if (selectedNode != null)
            {
                SelectDatesForObject(selectedNode);
            }
        }

        private void SetCalendarColor()
        {
            CurriculumnCalendar.SelectedDayStyle.BackColor = _isViewMode ? Color.Blue : Color.Green;

            if (_isControlNow)
                CurriculumnCalendar.SelectedDayStyle.BackColor = Color.Red;
        }

        private void SelectDatesForObject(IdendtityNode selectedNode)
        {
            IList<TblPermissions> permissions = StudentHelper.GetPermissionForNode(_userId, selectedNode, _isViewMode);

            if (permissions.Count == 0)
            {
                SelectDates(DateTime.Today, DateTime.Today.AddYears(1));
            }
            else
            {
                SelectDatesForPermissions(permissions);
            }
        }

        private void SelectDatesForPermissions(IList<TblPermissions> permissions)
        {
            PeriodDescription.Items.Clear();
            foreach (TblPermissions permission in permissions)
            {
                SelectDates(permission.DateSince, permission.DateTill);
            }
        }

        private void SelectDates(DateTime? since, DateTime? till)
        {
            if (since == null)
            {
                since = DateTime.Today;
            }

            if (till == null)
            {
                till = DateTime.Today.AddYears(1);
            }

            if (till > since)
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
            IList<UserAnswer> listOfPages = UserResultCalculator.GetLatestResultsByQuestions();

            int countOfShowedPages = Math.Min(CountHowManyPagesToShow, listOfPages.Count);

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

        private void SetButtonVisibility()
        {
            var selectedNode = (IdendtityNode) CurriculumnTreeView.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Type == NodeType.Curriculum || selectedNode.Type == NodeType.Stage)
                {
                    OpenTestButton.Enabled = false;
                }
                else
                {
                    IList<TblPermissions> permissions = StudentHelper.GetPermissionForNode(_userId, selectedNode, true);

                    OpenTestButton.Enabled = StudentHelper.IsDateAllowed(DateTime.Now, permissions);
                }
            }
        }

        private void BuildTree(DateTime? date)
        {
            CurriculumnTreeView.Nodes.Clear();

            if (ServerModel.User.Current != null)
            {
                IList<int> userCurriculumsIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
                                                                                     ServerModel.User.Current.ID,
                                                                                     FxCurriculumOperations.View.ID,
                                                                                     null);


                IList<TblCurriculums> userCurriculums = ServerModel.DB.Load<TblCurriculums>(userCurriculumsIds);

                foreach (TblCurriculums curriculum in userCurriculums)
                {
                    var node = new IdendtityNode(curriculum);

                    IList<TblPermissions> permissions = StudentHelper.GetPermission(curriculum.ID, _userId,
                                                                                    NodeType.Curriculum, _isViewMode);

                    if (StudentHelper.IsDateAllowed(date, permissions))
                    {
                        BuildStages(curriculum, node, date);

                        if (_isControlNow)
                        {
                            CurriculumnTreeView.Nodes.Clear();
                            CurriculumnTreeView.Nodes.Add(node);
                            return;
                        }

                        if (node.ChildNodes.Count != 0)
                            CurriculumnTreeView.Nodes.Add(node);
                    }
                }
            }

            CurriculumnTreeView.ExpandAll();
        }

        private void BuildStages(TblCurriculums curriculum, TreeNode node, DateTime? date)
        {
            IList<TblStages> stages =
                ServerModel.DB.Load<TblStages>(ServerModel.DB.LookupIds<TblStages>(curriculum, null));

            foreach (TblStages stage in stages)
            {
                var child = new IdendtityNode(stage);

                IList<TblPermissions> permissions = StudentHelper.GetPermission(stage.ID, _userId, NodeType.Stage,
                                                                                _isViewMode);

                if (StudentHelper.IsDateAllowed(date, permissions))
                {
                    BuildThemes(stage, child);

                    if (_isControlNow)
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
            List<int> themesIds = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            IList<TblThemes> themes = ServerModel.DB.Load<TblThemes>(themesIds);

            foreach (TblThemes theme in themes)
            {
                IsControlChapterTime(stage.ID, theme);

                if (_isControlNow)
                {
                    node.ChildNodes.Clear();
                    node.ChildNodes.Add(new IdendtityNode(theme));
                    return;
                }

                if (!theme.IsControl)
                    node.ChildNodes.Add(new IdendtityNode(theme));
            }
        }

        private void IsControlChapterTime(int stageId, TblThemes theme)
        {
            IList<TblPermissions> permissions = StudentHelper.GetPermission(stageId, _userId, NodeType.Stage, false);

            if (theme.IsControl)
                if (!StudentHelper.IsAllDatasAreNull(permissions))
                    if (StudentHelper.IsDateAllowed(DateTime.Now, permissions))
                    {
                        _isControlNow = true;
                        SelectDatesForPermissions(permissions);
                        SetCalendarColor();
                        return;
                    }


            _isControlNow = false;
        }

        private void ShowDescriptionForDataSelection()
        {
            Description.Value = string.Format("Display themes that you can {0} in selected date",
                                              _isViewMode ? "view" : "pass");
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
            Description.Value = string.Format("In calendar your see dates when you can {0} test",
                                              _isViewMode ? "view" : "pass");
        }
    }
}