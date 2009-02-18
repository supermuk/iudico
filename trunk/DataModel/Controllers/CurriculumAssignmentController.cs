using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.DB;
using System.Web.UI;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumAssignmentController : ControllerBase
    {
        public Table AssigmentsTable { get; set; }
        private string rawUrl;

        public void PageLoad(object sender, EventArgs e)
        {
            rawUrl = (sender as Page).Request.RawUrl;
            fillAssigmentsTable();
        }

        private void fillAssigmentsTable()
        {
            AssigmentsTable.Rows.Clear();
            TableRow headerRow = new TableRow();

            TableCell emptyHeaderCell = new TableCell();
            emptyHeaderCell.Text = "";
            headerRow.Cells.Add(emptyHeaderCell);
            foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
            {
                TableCell headerCell = new TableCell();
                headerCell.Text = group.Name;
                headerRow.Cells.Add(headerCell);
            }
            AssigmentsTable.Rows.Add(headerRow);

            foreach (TblCurriculums curriculum in TeacherHelper.MyCurriculums(FxCurriculumOperations.Use))
            {
                TableRow curriculumRow = new TableRow();
                TableCell curriculumHeaderCell = new TableCell();
                curriculumHeaderCell.Text = curriculum.Name;

                curriculumRow.Cells.Add(curriculumHeaderCell);
                IList<TblGroups> assignedGroups = TeacherHelper.GetGroupsForCurriculum(curriculum);
                foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
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
                        modifyButton.Click += new EventHandler(modifyButton_Click);
                        modifyButton.Text = "Modify";

                        Button unsignButton = new Button();
                        unsignButton.ID = group.ID.ToString() + unsignChar + curriculum.ID;
                        unsignButton.Click += new EventHandler(unsignButton_Click);
                        unsignButton.Text = "Unsign";

                        curriculumCell.Controls.Add(modifyButton);
                        curriculumCell.Controls.Add(unsignButton);
                    }
                    else
                    {
                        Button assignButton = new Button();
                        assignButton.ID = group.ID.ToString() + assignChar + curriculum.ID;
                        assignButton.Click += new EventHandler(assignButton_Click);
                        assignButton.Text = "Assign";

                        curriculumCell.Controls.Add(assignButton);
                    }

                    curriculumRow.Cells.Add(curriculumCell);
                }
                AssigmentsTable.Rows.Add(curriculumRow);
            }
        }

        private const char assignChar = 'a';
        private const char modifyChar = 'm';
        private const char unsignChar = 'u';

        private void assignButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int groupID = int.Parse(button.ID.Split(assignChar)[0]);
            int curriculumID = int.Parse(button.ID.Split(assignChar)[1]);
            TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
            TblGroups group = ServerModel.DB.Load<TblGroups>(groupID);

            TeacherHelper.SignGroupForCurriculum(group, curriculum);
            Redirect(ServerModel.Forms.BuildRedirectUrl<CurriculumTimelineController>(
                new CurriculumTimelineController()
                {
                    BackUrl = rawUrl,
                    GroupID = groupID,
                    CurriculumID = curriculumID
                }));
        }

        private void unsignButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int groupID = int.Parse(button.ID.Split(unsignChar)[0]);
            int curriculumID = int.Parse(button.ID.Split(unsignChar)[1]);
            TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
            TblGroups group = ServerModel.DB.Load<TblGroups>(groupID);

            TeacherHelper.UnSignGroupFromCurriculum(group, curriculum);
            fillAssigmentsTable();
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int groupID = int.Parse(button.ID.Split(modifyChar)[0]);
            int curriculumID = int.Parse(button.ID.Split(modifyChar)[1]);

            Redirect(ServerModel.Forms.BuildRedirectUrl<CurriculumTimelineController>(
                new CurriculumTimelineController()
                {
                    BackUrl = rawUrl,
                    GroupID = groupID,
                    CurriculumID = curriculumID
                }));
        }
    }


}
