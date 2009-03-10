using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.ImportManagers.RemoveManager;
using IUDICO.DataModel.Security;
using System.Data;

namespace IUDICO.DataModel.Controllers
{
    public class CourseShareController : ControllerBase
    {
        [ControllerParameter]
        public int CourseId;
        private TblCourses course;


        public Table TeachersTable { get; set; }
        public Table OperationsTable { get; set; }
        public Label NotifyLabel { get; set; }



        public void PageLoad(object sender, EventArgs e)
        {
            NotifyLabel.Text = "Select operation and teacher to share";
            course = ServerModel.DB.Load<TblCourses>(CourseId);

            fillOperationsTable();
            fillTeachersTable();
        }

        private void fillTeachersTable()
        {
            TeachersTable.Rows.Clear();

            foreach (TblUsers teacher in TeacherHelper.GetTeachers())
            {
                if (teacher.ID != ServerModel.User.Current.ID)
                {
                    TableRow teacherRow = new TableRow();
                    TableCell teacherCell = new TableCell();

                    Button teacherButton = new Button();
                    teacherButton.Text = teacher.DisplayName;
                    teacherButton.ID = teacher.ID.ToString();
                    teacherButton.Click += new EventHandler(teacherButton_Click);
                    teacherCell.Controls.Add(teacherButton);

                    teacherRow.Cells.Add(teacherCell);
                    TeachersTable.Rows.Add(teacherRow);
                }

            }
        }

        void teacherButton_Click(object sender, EventArgs e)
        {
            int teacherId = int.Parse((sender as Button).ID);
            TblUsers teacher = ServerModel.DB.Load<TblUsers>(teacherId);

            foreach (TableRow operationRow in OperationsTable.Rows)
            {
                CheckBox operationCheckBox = operationRow.Cells[0].Controls[0] as CheckBox;
                if (operationCheckBox.Checked)
                {
                    FxCourseOperations operation = ServerModel.DB.Load<FxCourseOperations>(int.Parse(operationCheckBox.ID));
                    if (TeacherHelper.HavePermissionForCourse(teacherId, course, operation))
                    {
                        NotifyLabel.Text = "Teacher: " + teacher.DisplayName + " already have permission to: " + operation.Name;
                    }
                    else
                    {
                        TeacherHelper.Share(TeacherHelper.GetPermissionForCourse(course, operation), teacher.ID, false);
                    }
                }
            }

        }

        private void fillOperationsTable()
        {
            OperationsTable.Rows.Clear();

            foreach (FxCourseOperations operation in TeacherHelper.CourseOperations())
            {
                TableRow operationRow = new TableRow();
                TableCell operationCell = new TableCell();

                CheckBox operationCheckBox = new CheckBox();
                operationCheckBox.Text = operation.Name;
                operationCheckBox.ID = operation.ID.ToString();

                TblPermissions permission = TeacherHelper.GetPermissionForCourse(course, operation);
                if (permission == null || !permission.CanBeDelagated)
                {
                    operationCheckBox.Enabled = false;
                }

                operationCell.Controls.Add(operationCheckBox);

                operationRow.Cells.Add(operationCell);
                OperationsTable.Rows.Add(operationRow);
            }
        }

    }
}
