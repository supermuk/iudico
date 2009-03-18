using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Security;
using GrayMatterSoft;
using System.Drawing;
using IUDICO.DataModel.Controllers;

public partial class OperationsTable : UserControl, ITextControl
{
    // "magic" words
    readonly DateTime minDateTime = (new DateTime(2009, 1, 1, 0, 0, 0));
    readonly DateTime maxDateTime = (new DateTime(2010, 12, 31, 23, 59, 59));
    private const string curriculumChar = "c";
    private const string stageChar = "s";
    private const string applyChar = "a";
    private const string removeChar = "r";
    private const string since = "Since";
    private const string till = "Till";
    private const string apply = "Apply";
    private const string remove = "Remove";
    private const string noDateSelected = "Date is not set up";
    TblGroups group;
    TblCurriculums curriculum;
    TblStages stage;
    string valuePath;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (DropDownList_CurriculumOperations.Items.Count == 0)
        {
            fillCurriculumOperations();
        }
        if (DropDownList_StageOperations.Items.Count == 0)
        {
            fillStageOperations();
        }


    }

    public string Text
    {
        get
        {
            return "";
        }
        set
        {
            if (value == "-1")
            {
                Visible = false;
                return;
            }

            Visible = true;
            valuePath = value.Split()[0];
            group = ServerModel.DB.Load<TblGroups>(int.Parse(value.Split()[1]));

            if (value.Split()[3] == curriculumChar)
            {
                curriculum = ServerModel.DB.Load<TblCurriculums>(int.Parse(value.Split()[2]));
                buildCurriculumTable(curriculum);
                DropDownList_CurriculumOperations.Visible = true;
                DropDownList_StageOperations.Visible = false;
                Button_AddOperation.Click += new EventHandler(Button_AddCurriculumOperation);
            }
            if (value.Split()[3] == stageChar)
            {
                stage = ServerModel.DB.Load<TblStages>(int.Parse(value.Split()[2]));
                curriculum = ServerModel.DB.Load<TblCurriculums>(stage.CurriculumRef.Value);

                buildStageTable(stage);
                DropDownList_CurriculumOperations.Visible = false;
                DropDownList_StageOperations.Visible = true;
                Button_AddOperation.Click += new EventHandler(Button_AddStageOperation);
            }
        }
    }

    private void buildCurriculumTable(TblCurriculums curriculum)
    {
        foreach (TblPermissions permission in TeacherHelper.GroupPermissionsForCurriculum(group, curriculum))
        {
            Table_Operations.Rows.AddAt(Table_Operations.Rows.Count - 1, buildPermissionRow(permission));
        }
    }

    private void buildStageTable(TblStages stage)
    {
        foreach (TblPermissions permission in TeacherHelper.GroupPermissionsForStage(group, stage))
        {
            Table_Operations.Rows.AddAt(Table_Operations.Rows.Count - 1, buildPermissionRow(permission));
        }
    }

    private TableRow buildPermissionRow(TblPermissions permission)
    {
        TableRow operationRow = new TableRow();

        TableCell operationCell = new TableCell();
        Label dateSinceLabel = new Label();
        dateSinceLabel.Text = since + ": ";
        GMDatePicker dateSinceDatePicker = recreateDatePicker(since + permission.ID.ToString());
        Label dateTillLabel = new Label();
        dateTillLabel.Text = till + ": ";
        GMDatePicker dateTillDatePicker = recreateDatePicker(till + permission.ID.ToString());

        TableCell operationNameCell = new TableCell();
        if (permission.StageOperationRef.HasValue)
        {
            FxStageOperations stageOperation =
                            ServerModel.DB.Load<FxStageOperations>(permission.StageOperationRef.Value);
            operationNameCell.Text = stageOperation.Name;
        }
        if (permission.CurriculumOperationRef.HasValue)
        {
            FxCurriculumOperations curriculumOperation =
                            ServerModel.DB.Load<FxCurriculumOperations>(permission.CurriculumOperationRef.Value);
            operationNameCell.Text = curriculumOperation.Name;
        }
        operationRow.Cells.Add(operationNameCell);

        Table layoutTable = new Table();
        TableRow layoutRow = new TableRow();

        TableCell sinceLabelCell = new TableCell();
        sinceLabelCell.Controls.Add(dateSinceLabel);

        TableCell sincePickerCell = new TableCell();
        sincePickerCell.Width = 200;
        sincePickerCell.HorizontalAlign = HorizontalAlign.Right;
        sincePickerCell.Controls.Add(dateSinceDatePicker);

        TableCell tillLabelCell = new TableCell();
        tillLabelCell.Controls.Add(dateTillLabel);

        TableCell tillPickerCell = new TableCell();
        tillPickerCell.Width = 200;
        tillPickerCell.HorizontalAlign = HorizontalAlign.Right;
        tillPickerCell.Controls.Add(dateTillDatePicker);

        layoutRow.Cells.Add(sinceLabelCell);
        layoutRow.Cells.Add(sincePickerCell);
        layoutRow.Cells.Add(tillLabelCell);
        layoutRow.Cells.Add(tillPickerCell);
        layoutTable.Rows.Add(layoutRow);

        operationCell.Controls.Add(layoutTable);
        operationRow.Cells.Add(operationCell);

        if (permission.DateSince.HasValue)
        {
            dateSinceDatePicker.InitialText = permission.DateSince.Value.ToShortDateString();
            dateSinceDatePicker.InitialTimePickerText = permission.DateSince.Value.ToShortTimeString();
        }

        if (permission.DateTill.HasValue)
        {
            dateTillDatePicker.InitialText = permission.DateTill.Value.ToShortDateString();
            dateTillDatePicker.InitialTimePickerText = permission.DateTill.Value.ToShortTimeString();
        }

        operationCell = new TableCell();

        Button ApppyButton = new Button();
        ApppyButton.ID = applyChar + permission.ID.ToString();
        ApppyButton.Click += new EventHandler(ApppyButton_Click);
        ApppyButton.Text = apply;

        Button RemoveButton = new Button();
        RemoveButton.ID = removeChar + permission.ID.ToString();
        RemoveButton.Click += new EventHandler(RemoveButton_Click);
        RemoveButton.Text = remove;

        operationCell.Controls.Add(ApppyButton);
        operationCell.Controls.Add(RemoveButton);
        operationRow.Cells.Add(operationCell);

        return operationRow;
    }

    private void ApppyButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int permissionID = int.Parse(button.ID.Replace(applyChar, ""));

        GMDatePicker sinceDatePicker = (button.Parent.Parent as TableRow).Cells[1].FindControl(since + permissionID) as GMDatePicker;
        GMDatePicker tillDatePicker = (button.Parent.Parent as TableRow).Cells[1].FindControl(till + permissionID) as GMDatePicker;

        TblPermissions permission = ServerModel.DB.Load<TblPermissions>(permissionID);
        if (sinceDatePicker.IsNull)
        {
            permission.DateSince = null;
        }
        else
        {
            permission.DateSince = sinceDatePicker.Date;
            sinceDatePicker.Date = sinceDatePicker.Date;
        }

        if (tillDatePicker.IsNull)
        {
            permission.DateTill = null;
        }
        else
        {
            permission.DateTill = tillDatePicker.Date;
            tillDatePicker.Date = tillDatePicker.Date;
        }

        ServerModel.DB.Update<TblPermissions>(permission);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int permissionID = int.Parse(button.ID.Replace(removeChar, ""));
        ServerModel.DB.Delete<TblPermissions>(permissionID);
    }

    private void Button_AddCurriculumOperation(object sender, EventArgs e)
    {
        FxCurriculumOperations operation = ServerModel.DB.Load<FxCurriculumOperations>(int.Parse(DropDownList_CurriculumOperations.SelectedValue));
        PermissionsManager.Grand(curriculum, operation, null, group.ID, DateTimeInterval.Full);
    }

    private void Button_AddStageOperation(object sender, EventArgs e)
    {
        FxStageOperations operation = ServerModel.DB.Load<FxStageOperations>(int.Parse(DropDownList_StageOperations.SelectedValue));
        PermissionsManager.Grand(stage, operation, null, group.ID, DateTimeInterval.Full);
    }

    private void fillCurriculumOperations()
    {
        DropDownList_CurriculumOperations.Items.Add(new ListItem(FxCurriculumOperations.View.Name, FxCurriculumOperations.View.ID.ToString()));
        DropDownList_CurriculumOperations.Items.Add(new ListItem(FxCurriculumOperations.Pass.Name, FxCurriculumOperations.Pass.ID.ToString()));
    }

    private void fillStageOperations()
    {
        DropDownList_StageOperations.Items.Add(new ListItem(FxStageOperations.View.Name, FxStageOperations.View.ID.ToString()));
        DropDownList_StageOperations.Items.Add(new ListItem(FxStageOperations.Pass.Name, FxStageOperations.Pass.ID.ToString()));
    }

    protected override object SaveViewState()
    {

        object[] newState = new object[5] { base.SaveViewState(), 
            DropDownList_CurriculumOperations.SelectedIndex, DropDownList_CurriculumOperations.Visible,
            DropDownList_StageOperations.SelectedIndex, DropDownList_StageOperations.Visible        };
        return newState;
    }

    protected override void LoadViewState(object savedState)
    {
        object[] newState = savedState as object[];
        base.LoadViewState(newState[0]);

        fillCurriculumOperations();
        fillStageOperations();

        DropDownList_CurriculumOperations.SelectedIndex = int.Parse(newState[1].ToString());
        DropDownList_CurriculumOperations.Visible = bool.Parse(newState[2].ToString());
        DropDownList_StageOperations.SelectedIndex = int.Parse(newState[3].ToString());
        DropDownList_StageOperations.Visible = bool.Parse(newState[4].ToString());
    }

    private GMDatePicker recreateDatePicker(string ID)
    {
        GMDatePicker DatePicker = new GMDatePicker();
        DatePicker.ID = ID;
        DatePicker.EnableTimePicker = true;
        DatePicker.DisplayMode = DisplayMode.Label;
        DatePicker.InitialValueMode = InitialValueMode.Null;
        DatePicker.InitialText = noDateSelected;
        DatePicker.NoneText = noDateSelected;
        DatePicker.CalendarTheme = CalendarTheme.Silver;

        return DatePicker;
    }
}
