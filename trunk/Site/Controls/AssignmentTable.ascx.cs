using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Common;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class AssignmentTable : System.Web.UI.UserControl
{
    private const char assignChar = 'a';
    private const char modifyChar = 'm';
    private const char unsignChar = 'u';

    private const string modify = "Modify";
    private const string assign = "Assign";
    private const string unsign = "Unsign";

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
        foreach (TblGroups group in groups)
        {
            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Text = group.Name;
            headerRow.Cells.Add(headerCell);
        }
        Table_Assignments.Rows.Add(headerRow);

        //create row for each curriculum
        foreach (TblCurriculums curriculum in curriculums)
        {
            TableRow curriculumRow = new TableRow();
            TableHeaderCell curriculumHeaderCell = new TableHeaderCell();
            curriculumHeaderCell.Text = curriculum.Name;
            curriculumRow.Cells.Add(curriculumHeaderCell);

            IList<TblGroups> assignedGroups = TeacherHelper.GetGroupsForCurriculum(curriculum);
            foreach (TblGroups group in groups)
            {
                bool isAssigned = false;
                foreach (TblGroups assignedGroup in assignedGroups)
                {
                    if (assignedGroup.ID == group.ID)
                    {
                        isAssigned = true;
                        break;
                    }
                }
                TableCell curriculumCell = new TableCell();
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

                    curriculumCell.Controls.Add(modifyButton);
                    curriculumCell.Controls.Add(unsignButton);
                }
                else
                {
                    Button assignButton = new Button();
                    assignButton.ID = group.ID.ToString() + assignChar + curriculum.ID;
                    assignButton.Click += new EventHandler(assignButton_Click);
                    assignButton.Text = assign;
                    
                    curriculumCell.Controls.Add(assignButton);
                }

                curriculumRow.Cells.Add(curriculumCell);
            }
            Table_Assignments.Rows.Add(curriculumRow);
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

}
