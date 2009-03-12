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

    TblGroups group;
    TblCurriculums curriculum;
    TblStages stage;

    private void buildCurriculumTable(TblCurriculums curriculum)
    {
        while (Table_Operations.Rows.Count != 2)
        {
            Table_Operations.Rows.RemoveAt(1);

        }
        foreach (TblPermissions permission in TeacherHelper.GroupPermissionsForCurriculum(group, curriculum))
        {
            Table_Operations.Rows.AddAt(Table_Operations.Rows.Count - 1, buildCurriculumPermissionRow(permission));
        }
    }

    private TableRow buildCurriculumPermissionRow(TblPermissions permission)
    {
        TableRow operationRow = new TableRow();

        TableCell operationCell = new TableCell();
        Label dateSinceLabel = new Label();
        dateSinceLabel.Text = since;
        TextBox dateSinceTextBox = new TextBox();
        dateSinceTextBox.ID = since + permission.ID.ToString();

        Label dateTillLabel = new Label();
        dateTillLabel.Text = till;
        TextBox dateTillTextBox = new TextBox();
        dateTillTextBox.ID = till + permission.ID.ToString();

        TableCell operationNameCell = new TableCell();
        FxCurriculumOperations curriculumOperation =
                        ServerModel.DB.Load<FxCurriculumOperations>(permission.CurriculumOperationRef.Value);
        operationNameCell.Text = curriculumOperation.Name;
        operationRow.Cells.Add(operationNameCell);

        if (permission.DateSince.HasValue)
        {
            dateSinceTextBox.Text = permission.DateSince.Value.ToString();
        }
        else
        {
            dateSinceTextBox.Text = minDateTime.ToString();
        }

        if (permission.DateTill.HasValue)
        {
            dateTillTextBox.Text = permission.DateTill.Value.ToString();
        }
        else
        {
            dateTillTextBox.Text = maxDateTime.ToString();
        }

        operationCell.Controls.Add(dateSinceLabel);
        operationCell.Controls.Add(dateSinceTextBox);
        operationCell.Controls.Add(dateTillLabel);
        operationCell.Controls.Add(dateTillTextBox);
        operationRow.Cells.Add(operationCell);

        operationCell = new TableCell();

        Button ApppyButton = new Button();
        ApppyButton.ID = applyChar + permission.ID.ToString();
        ApppyButton.Click += new EventHandler(ApppyCurriculumButton_Click);
        ApppyButton.Text = apply;

        Button RemoveButton = new Button();
        RemoveButton.ID = removeChar + permission.ID.ToString();
        RemoveButton.Click += new EventHandler(RemoveCurriculumButton_Click);
        RemoveButton.Text = remove;

        operationCell.Controls.Add(ApppyButton);
        operationCell.Controls.Add(RemoveButton);
        operationRow.Cells.Add(operationCell);

        return operationRow;
    }

    private void RemoveCurriculumButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int permissionID = int.Parse(button.ID.Replace(removeChar, ""));
        ServerModel.DB.Delete<TblPermissions>(permissionID);
        buildCurriculumTable(curriculum);
    }

    private void ApppyCurriculumButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int permissionID = int.Parse(button.ID.Replace(applyChar, ""));
        ApplyPermission(permissionID, button);
    }

    private void ApplyPermission(int permissionID, Button senderButton)
    {
        TextBox sinceDatePicker = (senderButton.Parent.Parent as TableRow).Cells[1].FindControl(since + permissionID) as TextBox;
        TextBox tillDatePicker = (senderButton.Parent.Parent as TableRow).Cells[1].FindControl(till + permissionID) as TextBox;

        TblPermissions permission = ServerModel.DB.Load<TblPermissions>(permissionID);
        permission.DateSince = DateTime.Parse(sinceDatePicker.Text);
        permission.DateTill = DateTime.Parse(tillDatePicker.Text);

        ServerModel.DB.Update<TblPermissions>(permission);
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


            group = ServerModel.DB.Load<TblGroups>(int.Parse(value.Split()[0]));
            if (value.Split()[2] == curriculumChar)
            {
                curriculum = ServerModel.DB.Load<TblCurriculums>(int.Parse(value.Split()[1]));
                if (DropDownList_Operations.Items.Count == 0)
                {
                    addCurriculumOperations();
                }
                buildCurriculumTable(curriculum);
                Button_AddOperation.Click += new EventHandler(Button_AddOperation_Click);
            }
            if (value.Split()[2] == stageChar)
            {
                stage = ServerModel.DB.Load<TblStages>(int.Parse(value.Split()[1]));
                addStageOperations();
            }
        }
    }



    void Button_AddOperation_Click(object sender, EventArgs e)
    {
        FxCurriculumOperations operation = ServerModel.DB.Load<FxCurriculumOperations>(int.Parse(DropDownList_Operations.SelectedValue));
        PermissionsManager.Grand(curriculum, operation, null, group.ID, DateTimeInterval.Full);

        buildCurriculumTable(curriculum);
    }

    private void addCurriculumOperations()
    {
        DropDownList_Operations.Items.Add(new ListItem(FxCurriculumOperations.View.Name, FxCurriculumOperations.View.ID.ToString()));
        DropDownList_Operations.Items.Add(new ListItem(FxCurriculumOperations.Pass.Name, FxCurriculumOperations.Pass.ID.ToString()));
    }

    private void addStageOperations()
    {
        DropDownList_Operations.Items.Add(new ListItem(FxStageOperations.View.Name, FxStageOperations.View.ID.ToString()));
        DropDownList_Operations.Items.Add(new ListItem(FxStageOperations.Pass.Name, FxStageOperations.Pass.ID.ToString()));
    }

    protected override object SaveViewState()
    {

        object[] newState = new object[2] { base.SaveViewState(), DropDownList_Operations.SelectedIndex };
        return newState;
    }

    protected override void LoadViewState(object savedState)
    {
        object[] newState = savedState as object[];
        base.LoadViewState(newState[0]);
        addCurriculumOperations();
        DropDownList_Operations.SelectedIndex = int.Parse(newState[1].ToString());
    }
}
