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
    public class CourseTeachersListController : BaseTeacherController
    {
        [ControllerParameter]
        public int CourseId;
        private TblCourses course;

        public IVariable<string> CourseOwner = string.Empty.AsVariable();

        public Table AlreadySharedTeachers { get; set; }
        public Table CanBeSharedTeachers { get; set; }

        //"magic words"
        private const string pageCaption = "Share of course: {0}.";
        private const string pageDescription = "Here you can share course: {0} to another teachers, or edit previously done share.";
        private const string noTeachers = "Nobody.";

        public override void Loaded()
        {
            base.Loaded();

            course = ServerModel.DB.Load<TblCourses>(CourseId);
            Caption.Value = pageCaption.Replace("{0}", course.Name);
            Description.Value = pageDescription.Replace("{0}", course.Name);
            Title.Value = Caption.Value;

            fillTeachersTable();
        }

        private void fillTeachersTable()
        {
            AlreadySharedTeachers.Rows.Clear();
            CanBeSharedTeachers.Rows.Clear();
            TblUsers currentUser = ServerModel.DB.Load<TblUsers>(ServerModel.User.Current.ID);
            foreach (TblUsers teacher in TeacherHelper.GetTeachers())
            {
                if (teacher.ID != currentUser.ID)
                {
                    HyperLink sharedHyperlink = new HyperLink();
                    sharedHyperlink.Text = teacher.DisplayName;
                    sharedHyperlink.NavigateUrl = ServerModel.Forms.BuildRedirectUrl<CourseShareController>
                        (new CourseShareController { BackUrl = RawUrl, CourseId = CourseId, TeacherId = teacher.ID });
                    TableCell sharedCell = new TableCell();
                    sharedCell.Controls.Add(sharedHyperlink);
                    TableRow sharedRow = new TableRow();
                    sharedRow.Cells.Add(sharedCell);
                    if (TeacherHelper.AreParentAndChildByCourse(currentUser, teacher, course))
                    {
                        AlreadySharedTeachers.Rows.Add(sharedRow);
                    }
                    else
                    {
                        CanBeSharedTeachers.Rows.Add(sharedRow);
                    }
                }
            }

            if (AlreadySharedTeachers.Rows.Count == 0)
            {
                HyperLink alreadySharedHyperlink = new HyperLink();
                alreadySharedHyperlink.Text = noTeachers;
                TableCell alreadySharedCell = new TableCell();
                alreadySharedCell.Controls.Add(alreadySharedHyperlink);
                TableRow alreadySharedRow = new TableRow();
                alreadySharedRow.Cells.Add(alreadySharedCell);
                AlreadySharedTeachers.Rows.Add(alreadySharedRow);
            }
            if (CanBeSharedTeachers.Rows.Count == 0)
            {
                HyperLink canBeSharedHyperlink = new HyperLink();
                canBeSharedHyperlink.Text = noTeachers;
                TableCell canBeSharedCell = new TableCell();
                canBeSharedCell.Controls.Add(canBeSharedHyperlink);
                TableRow canBeSharedRow = new TableRow();
                canBeSharedRow.Cells.Add(canBeSharedCell);
                CanBeSharedTeachers.Rows.Add(canBeSharedRow);
            }
        }


        //private void fillTeachersTable()
        //{
        //    TeachersTable.Rows.Clear();

        //    foreach (TblUsers teacher in TeacherHelper.GetTeachers())
        //    {
        //        if (teacher.ID != ServerModel.User.Current.ID)
        //        {
        //            TableRow teacherRow = new TableRow();
        //            TableCell teacherCell = new TableCell();

        //            Button teacherButton = new Button();
        //            teacherButton.Text = teacher.DisplayName;
        //            teacherButton.ID = teacher.ID.ToString();
        //            teacherButton.Click += new EventHandler(teacherButton_Click);
        //            teacherCell.Controls.Add(teacherButton);

        //            teacherRow.Cells.Add(teacherCell);
        //            TeachersTable.Rows.Add(teacherRow);
        //        }

        //    }
        //}

        //void teacherButton_Click(object sender, EventArgs e)
        //{
        //    int teacherId = int.Parse((sender as Button).ID);
        //    TblUsers teacher = ServerModel.DB.Load<TblUsers>(teacherId);

        //    foreach (TableRow operationRow in OperationsTable.Rows)
        //    {
        //        CheckBox operationCheckBox = operationRow.Cells[0].Controls[0] as CheckBox;
        //        if (operationCheckBox.Checked)
        //        {
        //            FxCourseOperations operation = ServerModel.DB.Load<FxCourseOperations>(int.Parse(operationCheckBox.ID));
        //            if (TeacherHelper.HavePermissionForCourse(teacherId, course, operation))
        //            {
        //                NotifyLabel.Text = "Teacher: " + teacher.DisplayName + " already have permission to: " + operation.Name;
        //            }
        //            else
        //            {
        //                TeacherHelper.Share(TeacherHelper.GetPermissionForCourse(course, operation), teacher.ID, false);
        //            }
        //        }
        //    }

        //}

        //private void fillOperationsTable()
        //{
        //    OperationsTable.Rows.Clear();

        //    foreach (FxCourseOperations operation in TeacherHelper.CourseOperations())
        //    {
        //        TableRow operationRow = new TableRow();
        //        TableCell operationCell = new TableCell();

        //        CheckBox operationCheckBox = new CheckBox();
        //        operationCheckBox.Text = operation.Name;
        //        operationCheckBox.ID = operation.ID.ToString();

        //        TblPermissions permission = TeacherHelper.GetPermissionForCourse(course, operation);
        //        if (permission == null || !permission.CanBeDelagated)
        //        {
        //            operationCheckBox.Enabled = false;
        //        }

        //        operationCell.Controls.Add(operationCheckBox);

        //        operationRow.Cells.Add(operationCell);
        //        OperationsTable.Rows.Add(operationRow);
        //    }
        //}

    }
}
