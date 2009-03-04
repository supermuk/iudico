using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.DB.Base;
using EO.Web;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumTimelineController : ControllerBase
    {
        public System.Web.UI.WebControls.TreeView CurriculumTree { get; set; }
        public Table TimeTable { get; set; }
        public Label NotifyLabel { get; set; }
        public Button AddOperationButton { get; set; }
        public DropDownList OperationDropDownList { get; set; }

        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;

        private TblGroups group;
        private TblCurriculums curriculum;

        private const string noNodeSelected = "Select node to modify";

        public void PageLoad(object sender, EventArgs e)
        {
            group = ServerModel.DB.Load<TblGroups>(GroupID);
            curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);

            CurriculumTree.SelectedNodeChanged += new EventHandler(CurriculumTree_SelectedNodeChanged);
            OperationDropDownList.SelectedIndexChanged += new EventHandler(OperationDropDownList_SelectedIndexChanged);
            AddOperationButton.Click += new EventHandler(AddOperationButton_Click);
            if (!(sender as Page).IsPostBack)
            {

                NotifyLabel.Text = "Detailed timeline for curriculum: " + curriculum.Name + " for group: " + group.Name;
                fillCurriculumTree();
            }

            if (CurriculumTree.SelectedNode == null)
            {
                CurriculumTree.Nodes[0].Select();
            }
            CurriculumTree_SelectedNodeChanged(CurriculumTree, EventArgs.Empty);

        }

        void OperationDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = OperationDropDownList.SelectedIndex;
        }

        public void PagePreRender(object sender, EventArgs e)
        {
            CurriculumTree_SelectedNodeChanged(CurriculumTree, EventArgs.Empty);
        }

        void AddOperationButton_Click(object sender, EventArgs e)
        {
            IdendtityNode selectedNode = CurriculumTree.SelectedNode as IdendtityNode;
            if (selectedNode == null)
            {
                NotifyLabel.Text = noNodeSelected;
                return;
            }
            switch (selectedNode.Type)
            {
                case NodeType.Curriculum:
                    {
                        TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(selectedNode.ID);
                        FxCurriculumOperations operation = ServerModel.DB.Load<FxCurriculumOperations>(int.Parse(OperationDropDownList.SelectedValue));
                        TblPermissions permission = TeacherHelper.GroupPermissionsForCurriculum(group, curriculum, operation);
                        if (permission != null)
                        {
                            NotifyLabel.Text = "Group aready have permission to " + operation.Name;
                        }
                        else
                        {
                            PermissionsManager.Grand(curriculum, operation, null, group.ID, DateTimeInterval.Full);
                        }
                        break;
                    }
                case NodeType.Stage:
                    {
                        TblStages stage = ServerModel.DB.Load<TblStages>(selectedNode.ID);
                        FxStageOperations operation = ServerModel.DB.Load<FxStageOperations>(int.Parse(OperationDropDownList.SelectedValue));
                        TblPermissions permission = TeacherHelper.GroupPermissionsForStage(group, stage, operation);
                        if (permission != null)
                        {
                            NotifyLabel.Text = "Group aready have permission to " + operation.Name;
                        }
                        else
                        {
                            PermissionsManager.Grand(stage, operation, null, group.ID, DateTimeInterval.Full);
                        }
                        break;
                    }
                case NodeType.Theme:
                    {
                        buildStageTable(ServerModel.DB.Load<TblStages>((selectedNode.Parent as IdendtityNode).ID));
                        OperationDropDownList.Items.Clear();
                        OperationDropDownList.Items.Add(new ListItem(FxStageOperations.Pass.Name, FxStageOperations.Pass.ID.ToString()));
                        OperationDropDownList.Items.Add(new ListItem(FxStageOperations.View.Name, FxStageOperations.View.ID.ToString()));
                        selectedNode.Parent.Select();
                        break;
                    }
            }
            CurriculumTree_SelectedNodeChanged(CurriculumTree, EventArgs.Empty);

        }

        [PersistantField]
        bool curriculumOperations;
        [PersistantField]
        int selectedIndex;

        private void CurriculumTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            IdendtityNode selectedNode = CurriculumTree.SelectedNode as IdendtityNode;
            switch (selectedNode.Type)
            {
                case NodeType.Curriculum:
                    {
                        buildCurriculumTable(ServerModel.DB.Load<TblCurriculums>(selectedNode.ID));
                        if (!curriculumOperations)
                        {
                            OperationDropDownList.Items.Clear();
                            OperationDropDownList.Items.Add(new ListItem(FxCurriculumOperations.Pass.Name, FxCurriculumOperations.Pass.ID.ToString()));
                            OperationDropDownList.Items.Add(new ListItem(FxCurriculumOperations.View.Name, FxCurriculumOperations.View.ID.ToString()));
                            curriculumOperations = true;
                        }
                        break;
                    }
                case NodeType.Stage:
                    {
                        buildStageTable(ServerModel.DB.Load<TblStages>(selectedNode.ID));
                        if (curriculumOperations)
                        {
                            OperationDropDownList.Items.Clear();
                            OperationDropDownList.Items.Add(new ListItem(FxStageOperations.Pass.Name, FxStageOperations.Pass.ID.ToString()));
                            OperationDropDownList.Items.Add(new ListItem(FxStageOperations.View.Name, FxStageOperations.View.ID.ToString()));
                            curriculumOperations = false;
                        }
                        break;
                    }
                case NodeType.Theme:
                    {
                        buildStageTable(ServerModel.DB.Load<TblStages>((selectedNode.Parent as IdendtityNode).ID));
                        OperationDropDownList.Items.Clear();
                        OperationDropDownList.Items.Add(new ListItem(FxStageOperations.Pass.Name, FxStageOperations.Pass.ID.ToString()));
                        OperationDropDownList.Items.Add(new ListItem(FxStageOperations.View.Name, FxStageOperations.View.ID.ToString()));
                        selectedNode.Parent.Select();
                        break;
                    }
            }
        }

        private void buildCurriculumTable(TblCurriculums curriculum)
        {
            buildHeaderRow();

            foreach (FxCurriculumOperations operation in TeacherHelper.GroupOperationsForCurriculum(group, curriculum))
            {
                TimeTable.Rows.Add(buildOperationRow(curriculum.ID, operation.ID, SECURED_OBJECT_TYPE.CURRICULUM));
            }
        }

        private void buildStageTable(TblStages stage)
        {
            buildHeaderRow();

            foreach (FxStageOperations operation in TeacherHelper.GroupOperationsForStage(group, stage))
            {
                TimeTable.Rows.Add(buildOperationRow(stage.ID, operation.ID, SECURED_OBJECT_TYPE.STAGE));
            }
        }

        private void buildThemeTable(TblThemes theme)
        {
            buildHeaderRow();
            FxThemeOperations[] operations =
                new FxThemeOperations[] { FxThemeOperations.View, FxThemeOperations.Pass };

            foreach (FxThemeOperations operation in operations)
            {
                TimeTable.Rows.Add(buildOperationRow(theme.ID, operation.ID, SECURED_OBJECT_TYPE.THEME));
            }
        }

        private void buildHeaderRow()
        {
            TimeTable.Rows.Clear();
            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Text = "Operation";
            headerRow.Cells.Add(headerCell);

            headerCell = new TableHeaderCell();
            headerCell.Text = "Time";
            headerRow.Cells.Add(headerCell);

            headerCell = new TableHeaderCell();
            headerCell.Text = "ZZZzzzz";
            headerRow.Cells.Add(headerCell);

            TimeTable.Rows.Add(headerRow);
        }

        private TableRow buildOperationRow(int dataObjectId, int operationId, SECURED_OBJECT_TYPE objectType)
        {
            TableRow operationRow = new TableRow();

            TableCell operationCell = new TableCell();
            Label dataSinceLabel = new Label();
            dataSinceLabel.Text = "Since:";
            DatePicker dateSincePicker = new DatePicker();
            dateSincePicker.ID = dataObjectId.ToString() + since + operationId.ToString();
            //  TextBox dataSinceTextBox = new TextBox();
            // dataSinceTextBox.ID = dataObjectId.ToString() + since + operationId.ToString();
            Label dataTillLabel = new Label();
            dataTillLabel.Text = "Till:";
            DatePicker dateTillPicker = new DatePicker();
            dateTillPicker.ID = dataObjectId.ToString() + till + operationId.ToString();
            // TextBox dataTillTextBox = new TextBox();
            //  dataTillTextBox.ID = dataObjectId.ToString() + till + operationId.ToString();

            TblPermissions permission = null;
            TableCell operationNameCell = new TableCell();
            switch (objectType)
            {
                case SECURED_OBJECT_TYPE.CURRICULUM:
                    {
                        TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(dataObjectId);
                        FxCurriculumOperations curriculumOperation =
                            ServerModel.DB.Load<FxCurriculumOperations>(operationId);
                        permission = TeacherHelper.GroupPermissionsForCurriculum(group, curriculum, curriculumOperation);

                        operationNameCell.Text = curriculumOperation.Name;
                        break;
                    }
                case SECURED_OBJECT_TYPE.STAGE:
                    {
                        TblStages stage = ServerModel.DB.Load<TblStages>(dataObjectId);
                        FxStageOperations stageOperation =
                            ServerModel.DB.Load<FxStageOperations>(operationId);
                        permission = TeacherHelper.GroupPermissionsForStage(group, stage, stageOperation);

                        operationNameCell.Text = stageOperation.Name;
                        break;
                    }
                case SECURED_OBJECT_TYPE.THEME:
                    {
                        TblThemes theme = ServerModel.DB.Load<TblThemes>(dataObjectId);
                        FxThemeOperations themeOperation =
                            ServerModel.DB.Load<FxThemeOperations>(operationId);
                        permission = TeacherHelper.GroupPermissionsForTheme(group, theme, themeOperation);

                        operationNameCell.Text = themeOperation.Name;
                        break;
                    }
            }
            operationRow.Cells.Add(operationNameCell);

            if (permission.DateSince.HasValue)
            {
                dateSincePicker.SelectedDate = permission.DateSince.Value;
                //dataSinceTextBox.Text = permission.DateSince.Value.ToString();
            }
            else
            {
                dateSincePicker.SelectedDate = minDateTime;
                //dataSinceTextBox.Text = minDateTime;
            }

            if (permission.DateTill.HasValue)
            {
                dateTillPicker.SelectedDate = permission.DateTill.Value;
                //dataTillTextBox.Text = permission.DateTill.Value.ToString();
            }
            else
            {
                dateTillPicker.SelectedDate = maxDateTime;
                //dataTillTextBox.Text = DateTime.MaxValue.ToString();
            }


            operationCell.Controls.Add(dataSinceLabel);
            operationCell.Controls.Add(dateSincePicker);
            //operationCell.Controls.Add(dataSinceTextBox);
            operationCell.Controls.Add(dataTillLabel);
            operationCell.Controls.Add(dateTillPicker);
            //operationCell.Controls.Add(dataTillTextBox);
            operationRow.Cells.Add(operationCell);

            operationCell = new TableCell();
            Button ApppyButton = new Button();
            Button RemoveButton = new Button();

            switch (objectType)
            {
                case SECURED_OBJECT_TYPE.CURRICULUM:
                    {
                        ApppyButton.ID = dataObjectId.ToString() + applyChar + curriculumChar + operationId.ToString();
                        ApppyButton.Click += new EventHandler(ApppyCurriculumButton_Click);

                        RemoveButton.ID = dataObjectId.ToString() + removeChar + curriculumChar + operationId.ToString();
                        RemoveButton.Click += new EventHandler(RemoveCurriculumButton_Click);
                        break;
                    }
                case SECURED_OBJECT_TYPE.STAGE:
                    {
                        ApppyButton.ID = dataObjectId.ToString() + applyChar + stageChar + operationId.ToString();
                        ApppyButton.Click += new EventHandler(ApppyStageButton_Click);

                        RemoveButton.ID = dataObjectId.ToString() + removeChar + stageChar + operationId.ToString();
                        RemoveButton.Click += new EventHandler(RemoveStageButton_Click);
                        break;
                    }
                case SECURED_OBJECT_TYPE.THEME:
                    {
                        ApppyButton.ID = dataObjectId.ToString() + applyChar + themeChar + operationId.ToString();
                        ApppyButton.Click += new EventHandler(ApppyStageButton_Click);

                        RemoveButton.ID = dataObjectId.ToString() + removeChar + themeChar + operationId.ToString();
                        RemoveButton.Click += new EventHandler(RemoveStageButton_Click);
                        break;
                    }
            }
            ApppyButton.Text = "Apply";
            RemoveButton.Text = "Remove";


            operationCell.Controls.Add(ApppyButton);
            operationCell.Controls.Add(RemoveButton);
            operationRow.Cells.Add(operationCell);

            return operationRow;
        }

        // "magic" words
        readonly DateTime minDateTime = (new DateTime(2009, 1, 1, 0, 0, 0));
        readonly DateTime maxDateTime = (new DateTime(2010, 12, 31, 23, 59, 59));
        private const string curriculumChar = "c";
        private const string stageChar = "s";
        private const string themeChar = "t";
        private const string applyChar = "a";
        private const string removeChar = "r";
        private const string since = "since";
        private const string till = "till";

        private void RemoveCurriculumButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int curriculumID = int.Parse(button.ID.Split
                (new string[] { removeChar, curriculumChar }, StringSplitOptions.RemoveEmptyEntries)[0]);
            int operationID = int.Parse(button.ID.Split
                (new string[] { removeChar, curriculumChar }, StringSplitOptions.RemoveEmptyEntries)[1]);

            TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
            FxCurriculumOperations operation = ServerModel.DB.Load<FxCurriculumOperations>(operationID);
            TblPermissions permission = TeacherHelper.GroupPermissionsForCurriculum(group, curriculum, operation);

            ServerModel.DB.Delete<TblPermissions>(permission.ID);
            //RemovePermission(permission, curriculumID, operationID, button);
        }

        private void RemoveStageButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int stageID = int.Parse(button.ID.Split
                (new string[] { removeChar, stageChar }, StringSplitOptions.RemoveEmptyEntries)[0]);
            int operationID = int.Parse(button.ID.Split
                (new string[] { removeChar, stageChar }, StringSplitOptions.RemoveEmptyEntries)[1]);

            TblStages stage = ServerModel.DB.Load<TblStages>(stageID);
            FxStageOperations operation = ServerModel.DB.Load<FxStageOperations>(operationID);
            TblPermissions permission = TeacherHelper.GroupPermissionsForStage(group, stage, operation);

            ServerModel.DB.Delete<TblPermissions>(permission.ID);
            //RemovePermission(permission, stageID, operationID, button);
        }

        private void RemovePermission(TblPermissions permission, int dataObjectId, int operationId, Button senderButton)
        {
            //permission.DateSince = null;
            //permission.DateTill = null;

            //TextBox sinceTextBox = (senderButton.Parent.Parent as TableRow).Cells[1].FindControl(dataObjectId.ToString() + since + operationId.ToString()) as TextBox;
            //TextBox tillTextBox = (senderButton.Parent.Parent as TableRow).Cells[1].FindControl(dataObjectId.ToString() + till + operationId.ToString()) as TextBox;
            //sinceTextBox.Text = minDateTime;
            //tillTextBox.Text = maxDateTime;
            //tillTextBox.Style["color"] = "black";
            //sinceTextBox.Style["color"] = "black";

            //ServerModel.DB.Update<TblPermissions>(permission);
        }

        private void ApppyCurriculumButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int curriculumID = int.Parse(button.ID.Split
                (new string[] { applyChar, curriculumChar }, StringSplitOptions.RemoveEmptyEntries)[0]);
            int operationID = int.Parse(button.ID.Split
                (new string[] { applyChar, curriculumChar }, StringSplitOptions.RemoveEmptyEntries)[1]);

            TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
            FxCurriculumOperations operation = ServerModel.DB.Load<FxCurriculumOperations>(operationID);
            TblPermissions permission = TeacherHelper.GroupPermissionsForCurriculum(group, curriculum, operation);

            ApplyPermission(permission, curriculum.ID, operation.ID, button);
        }

        private void ApppyStageButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int stageID = int.Parse(button.ID.Split
                (new string[] { applyChar, stageChar }, StringSplitOptions.RemoveEmptyEntries)[0]);
            int operationID = int.Parse(button.ID.Split
                (new string[] { applyChar, stageChar }, StringSplitOptions.RemoveEmptyEntries)[1]);

            TblStages stage = ServerModel.DB.Load<TblStages>(stageID);
            FxStageOperations operation = ServerModel.DB.Load<FxStageOperations>(operationID);
            TblPermissions permission = TeacherHelper.GroupPermissionsForStage(group, stage, operation);

            ApplyPermission(permission, stage.ID, operation.ID, button);
        }

        private void ApplyPermission(TblPermissions permission, int dataObjectId, int operationId, Button senderButton)
        {
            DatePicker sinceDatePicker = (senderButton.Parent.Parent as TableRow).Cells[1].FindControl(dataObjectId.ToString() + since + operationId.ToString()) as DatePicker;
            DatePicker tillDatePicker = (senderButton.Parent.Parent as TableRow).Cells[1].FindControl(dataObjectId.ToString() + till + operationId.ToString()) as DatePicker;

            permission.DateSince = sinceDatePicker.SelectedDate;
            permission.DateTill = tillDatePicker.SelectedDate;

            ServerModel.DB.Update<TblPermissions>(permission);
        }

        private void fillCurriculumTree()
        {
            CurriculumTree.Nodes.Clear();
            TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);
            IdendtityNode curriculumNode = new IdendtityNode(curriculum);

            foreach (TblStages stage in TeacherHelper.StagesForCurriculum(curriculum))
            {
                IdendtityNode stageNode = new IdendtityNode(stage);
                foreach (TblThemes theme in TeacherHelper.ThemesForStage(stage))
                {
                    IdendtityNode themeNode = new IdendtityNode(theme);
                    stageNode.ChildNodes.Add(themeNode);
                }
                curriculumNode.ChildNodes.Add(stageNode);
            }
            CurriculumTree.Nodes.Add(curriculumNode);
            CurriculumTree.ExpandAll();
        }

    }


}
