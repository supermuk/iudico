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
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    public class CourseShareController : BaseTeacherController
    {
        [ControllerParameter]
        public int CourseId;
        [ControllerParameter]
        public int TeacherId;

        public Table Operations { get; set; }

        private TblCourses course;
        private TblUsers teacher;

        //"magic words"
        private const string pageCaption = "Share of course: {0} to {1}.";
        private const string pageDescription = "Here you can share course: {0} to {1}, select operations to be shared.";

        public override void Loaded()
        {
            base.Loaded();

            course = ServerModel.DB.Load<TblCourses>(CourseId);
            teacher = ServerModel.DB.Load<TblUsers>(TeacherId);

            Caption.Value = pageCaption.
                Replace("{0}", course.Name).
                Replace("{1}", teacher.DisplayName);
            Description.Value = pageDescription.
                Replace("{0}", course.Name).
                Replace("{1}", teacher.DisplayName);
            Title.Value = Caption.Value;

            fillOperationsTable();
        }

        public void UpdateButton_Click()
        {
            foreach (TableRow operationRow in Operations.Rows)
            {
                CheckBox operationCheckBox = operationRow.Cells[0].Controls[0] as CheckBox;
                if (operationCheckBox.Enabled)
                {
                    FxCourseOperations operation = ServerModel.DB.Load<FxCourseOperations>(int.Parse(operationCheckBox.ID));
                    if (operationCheckBox.Checked)
                    {
                        if (!TeacherHelper.HavePermissionForCourse(teacher.ID, course, operation))
                        {
                            TeacherHelper.Share(TeacherHelper.GetPermissionForCourse(course, operation), teacher.ID, false);
                        }
                    }
                    else
                    {
                        if (TeacherHelper.HavePermissionForCourse(teacher.ID, course, operation))
                        {
                            TeacherHelper.RemovePermissionForCourse(course, operation, teacher);
                        }
                    }
                }
            }
        }

        private void fillOperationsTable()
        {
            Operations.Rows.Clear();

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
                else
                {
                    operationCheckBox.Checked = TeacherHelper.AreParentAndChildByCourse(permission, teacher, course);
                }
                operationCell.Controls.Add(operationCheckBox);

                operationRow.Cells.Add(operationCell);
                Operations.Rows.Add(operationRow);
            }
        }
    }
}
