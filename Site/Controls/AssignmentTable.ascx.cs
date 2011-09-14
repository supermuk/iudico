using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

public partial class AssignmentTable : UserControl, ITextControl
{
    private const char assignChar = 'a';
    private const char modifyChar = 'm';
    private const char unsignChar = 'u';

    private const string modify = "Modify";
    private const string assign = "Assign";
    private const string unsign = "Unsign";

    public int VisibleGroupID = -1;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        buildAssignTable();
    }

    private void buildAssignTable()
    {
        List<TblGroups> groups = ServerModel.DB.Query<TblGroups>(null);
        IList<TblCurriculums> curriculums = TeacherHelper.CurrentUserCurriculums(FxCurriculumOperations.Use);

        Table_Assignments.Rows.Clear();

        //create header row
        TableRow headerRow = new TableRow();
        TableHeaderCell emptyHeaderCell = new TableHeaderCell();
        emptyHeaderCell.Text = "";
        headerRow.Cells.Add(emptyHeaderCell);
        foreach (TblCurriculums curriculum in curriculums)
        {
            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Text = curriculum.Name;
            headerRow.Cells.Add(headerCell);
        }
        Table_Assignments.Rows.Add(headerRow);

        //create row for each group
        foreach (TblGroups group in groups)
        {
            TableRow groupRow = new TableRow();
            TableHeaderCell groupHeaderCell = new TableHeaderCell();
            groupHeaderCell.Text = group.Name;
            groupRow.Cells.Add(groupHeaderCell);

            IList<TblCurriculums> assignedCurriculums = TeacherHelper.GetCurriculumsForGroup(group);

            foreach (TblCurriculums curriculum in curriculums)
            {
                bool isAssigned = false;
                foreach (TblCurriculums assignedCurriculum in assignedCurriculums)
                {
                    if (assignedCurriculum.ID == curriculum.ID)
                    {
                        isAssigned = true;
                        break;
                    }
                }
                TableCell groupCell = new TableCell();
                if (isAssigned)
                {
                    Button modifyButton = new Button();
                    modifyButton.ID = group.ID.ToString() + modifyChar + curriculum.ID;
                    modifyButton.Text = modify;
                    modifyButton.PostBackUrl = ServerModel.Forms.BuildRedirectUrl<CurriculumTimelineController>(
                        new CurriculumTimelineController()
                        {
                            BackUrl = Request.RawUrl,
                            GroupID = group.ID,
                            CurriculumID = curriculum.ID
                        });

                    Button unsignButton = new Button();
                    unsignButton.ID = group.ID.ToString() + unsignChar + curriculum.ID;
                    unsignButton.Click += new EventHandler(unsignButton_Click);
                    unsignButton.Text = unsign;

                    groupCell.Controls.Add(modifyButton);
                    groupCell.Controls.Add(unsignButton);
                }
                else
                {
                    Button assignButton = new Button();
                    assignButton.ID = group.ID.ToString() + assignChar + curriculum.ID;
                    assignButton.Click += new EventHandler(assignButton_Click);
                    assignButton.Text = assign;

                    groupCell.Controls.Add(assignButton);
                }

                groupRow.Cells.Add(groupCell);
            }
            if (assignedCurriculums.Count == 0 && group.ID != VisibleGroupID)
            {
                groupRow.Visible = false;
            }
            Table_Assignments.Rows.Add(groupRow);
        }
    }

    private void assignButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int groupID = int.Parse(button.ID.Split(assignChar)[0]);
        int curriculumID = int.Parse(button.ID.Split(assignChar)[1]);
        TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
        TblGroups group = ServerModel.DB.Load<TblGroups>(groupID);

        TeacherHelper.SignGroupForCurriculum(group, curriculum);
        string nextUrl = ServerModel.Forms.BuildRedirectUrl<CurriculumTimelineController>(
                        new CurriculumTimelineController()
                        {
                            BackUrl = Request.RawUrl,
                            GroupID = group.ID,
                            CurriculumID = curriculum.ID
                        });

        Response.Redirect(nextUrl);

    }

    private void unsignButton_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int groupID = int.Parse(button.ID.Split(unsignChar)[0]);
        int curriculumID = int.Parse(button.ID.Split(unsignChar)[1]);
        TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
        TblGroups group = ServerModel.DB.Load<TblGroups>(groupID);

        TeacherHelper.UnSignGroupFromCurriculum(group, curriculum);
        buildAssignTable();
    }

    public string Text
    {
        get
        {
            return VisibleGroupID.ToString();
        }
        set
        {
            VisibleGroupID = int.Parse(value);
            buildAssignTable();
        }
    }

}
