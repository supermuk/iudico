using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class ShareController : BaseTeacherController
    {
        [ControllerParameter]
        public int CourseId;
        [ControllerParameter]
        public int CurriculumId;
        [ControllerParameter]
        public int TeacherId;

        public Table Operations { get; set; }

        private TblCurriculums curriculum;
        private TblCourses course;
        private TblUsers teacher;

        //"magic words"
        private const string pageCaption = "Share of {0}: {1} to {2}.";
        private const string pageDescription = "Here you can share {0}: {1} to {2}, select operations to be shared.";
        private const string courseStr = "course";
        private const string curriculumStr = "curriculum";
        private const string delegateStr = "Allow to delegate this operation.";


        public override void Loaded()
        {
            base.Loaded();
            teacher = ServerModel.DB.Load<TblUsers>(TeacherId);
            if (CourseId != -1)
            {
                course = ServerModel.DB.Load<TblCourses>(CourseId);
                Caption.Value = pageCaption.
                    Replace("{0}", courseStr).
                    Replace("{1}", course.Name).
                    Replace("{2}", teacher.DisplayName);
                Description.Value = pageDescription.
                    Replace("{0}", courseStr).
                    Replace("{1}", course.Name).
                    Replace("{2}", teacher.DisplayName);

                fillCourseOperationsTable();
            }
            if (CurriculumId != -1)
            {
                curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumId);
                Caption.Value = pageCaption.
                    Replace("{0}", curriculumStr).
                    Replace("{1}", curriculum.Name).
                    Replace("{2}", teacher.DisplayName);
                Description.Value = pageDescription.
                    Replace("{0}", curriculumStr).
                    Replace("{1}", curriculum.Name).
                    Replace("{2}", teacher.DisplayName);

                fillCurriculumOperationsTable();
            }

            Title.Value = Caption.Value;

        }

        public void UpdateButton_Click()
        {
            if (CourseId != -1)
            {
                UpdateCourseButton_Click();
            }
            if (CurriculumId != -1)
            {
                UpdateCurriculumButton_Click();
            }
        }

        private void fillCourseOperationsTable()
        {
            Operations.Rows.Clear();

            foreach (FxCourseOperations operation in TeacherHelper.CourseOperations())
            {
                TableRow operationRow = new TableRow();
                TableCell operationCell = new TableCell();

                CheckBox operationCheckBox = new CheckBox();
                operationCheckBox.Text = operation.Name;

                operationCheckBox.AutoPostBack = true;
                operationCheckBox.CheckedChanged += new EventHandler(operationCheckBox_CheckedChanged);

                CheckBox delegateCheckBox = new CheckBox();
                delegateCheckBox.Text = delegateStr;

                TblPermissions permission = TeacherHelper.CurrentUserPermissionForCourse(course, operation);
                if (permission == null || !permission.CanBeDelagated)
                {
                    operationCheckBox.Enabled = false;
                    delegateCheckBox.Enabled = false;
                }
                else
                {
                    operationCheckBox.ID = permission.ID.ToString();
                    operationCheckBox.Checked = TeacherHelper.AreParentAndChildByCourse(permission, teacher, course);
                    if (operationCheckBox.Checked)
                    {
                        delegateCheckBox.Checked = TeacherHelper.CanChildDelegateCourse(permission, teacher, course);
                    }
                    else
                    {
                        delegateCheckBox.Enabled = false;
                    }
                }

                TableCell leftCell = new TableCell();
                leftCell.HorizontalAlign = HorizontalAlign.Left;
                leftCell.Controls.Add(operationCheckBox);

                TableCell rightCell = new TableCell();
                rightCell.HorizontalAlign = HorizontalAlign.Right;
                rightCell.Controls.Add(delegateCheckBox);

                operationRow.Cells.Add(leftCell);
                operationRow.Cells.Add(rightCell);
                Operations.Rows.Add(operationRow);
            }
        }

        private void UpdateCourseButton_Click()
        {
            foreach (TableRow operationRow in Operations.Rows)
            {
                CheckBox operationCheckBox = operationRow.Cells[0].Controls[0] as CheckBox;
                CheckBox delegateCheckBox = operationRow.Cells[1].Controls[0] as CheckBox;

                if (operationCheckBox.Enabled)
                {
                    TblPermissions parentPermission = ServerModel.DB.Load<TblPermissions>(int.Parse(operationCheckBox.ID));
                    FxCourseOperations operation = ServerModel.DB.Load<FxCourseOperations>(parentPermission.CourseOperationRef.Value);

                    TblPermissions childPermission = TeacherHelper.GetPermissionForCourse(parentPermission, teacher, course, operation);
                    if (operationCheckBox.Checked)
                    {
                        if (childPermission == null)
                        {
                            TeacherHelper.Share(TeacherHelper.CurrentUserPermissionForCourse(course, operation), teacher.ID, delegateCheckBox.Checked);
                        }
                        else
                        {
                            if (childPermission.CanBeDelagated && !delegateCheckBox.Checked)
                            {
                                TeacherHelper.RemoveChildPermissions(childPermission);
                            }

                            childPermission.CanBeDelagated = delegateCheckBox.Checked;
                            ServerModel.DB.Update<TblPermissions>(childPermission);
                        }
                    }
                    else
                    {
                        if (childPermission != null)
                        {
                            TeacherHelper.RemoveChildPermissions(childPermission);
                            ServerModel.DB.Delete<TblPermissions>(childPermission.ID);
                        }
                    }
                }
            }

            Redirect(BackUrl);
        }

        private void operationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox operationCheckBox = sender as CheckBox;
            CheckBox delegateCheckBox = operationCheckBox.Parent.Parent.Controls[1].Controls[0] as CheckBox;
            delegateCheckBox.Enabled = operationCheckBox.Checked;
        }

        private void fillCurriculumOperationsTable()
        {
            Operations.Rows.Clear();

            foreach (FxCurriculumOperations operation in TeacherHelper.CurriculumsOperations())
            {
                TableRow operationRow = new TableRow();
                TableCell operationCell = new TableCell();

                CheckBox operationCheckBox = new CheckBox();
                operationCheckBox.Text = operation.Name;

                operationCheckBox.AutoPostBack = true;
                operationCheckBox.CheckedChanged += new EventHandler(operationCheckBox_CheckedChanged);

                CheckBox delegateCheckBox = new CheckBox();
                delegateCheckBox.Text = delegateStr;

                TblPermissions permission = TeacherHelper.CurrentUserPermissionForCurriculum(curriculum, operation);
                if (permission == null || !permission.CanBeDelagated)
                {
                    operationCheckBox.Enabled = false;
                    delegateCheckBox.Enabled = false;
                }
                else
                {
                    operationCheckBox.ID = permission.ID.ToString();
                    operationCheckBox.Checked = TeacherHelper.AreParentAndChildByCurriculum(permission, teacher, curriculum);
                    if (operationCheckBox.Checked)
                    {
                        delegateCheckBox.Checked = TeacherHelper.CanChildDelegateCurriculum(permission, teacher, curriculum);
                    }
                    else
                    {
                        delegateCheckBox.Enabled = false;
                    }
                }

                TableCell leftCell = new TableCell();
                leftCell.HorizontalAlign = HorizontalAlign.Left;
                leftCell.Controls.Add(operationCheckBox);

                TableCell rightCell = new TableCell();
                rightCell.HorizontalAlign = HorizontalAlign.Right;
                rightCell.Controls.Add(delegateCheckBox);

                operationRow.Cells.Add(leftCell);
                operationRow.Cells.Add(rightCell);
                Operations.Rows.Add(operationRow);
            }
        }

        private void UpdateCurriculumButton_Click()
        {
            foreach (TableRow operationRow in Operations.Rows)
            {
                CheckBox operationCheckBox = operationRow.Cells[0].Controls[0] as CheckBox;
                CheckBox delegateCheckBox = operationRow.Cells[1].Controls[0] as CheckBox;

                if (operationCheckBox.Enabled)
                {
                    TblPermissions parentPermission = ServerModel.DB.Load<TblPermissions>(int.Parse(operationCheckBox.ID));
                    FxCurriculumOperations operation = ServerModel.DB.Load<FxCurriculumOperations>(parentPermission.CurriculumOperationRef.Value);

                    TblPermissions childPermission = TeacherHelper.GetPermissionForCurriculum(parentPermission, teacher, curriculum, operation);
                    if (operationCheckBox.Checked)
                    {
                        if (childPermission == null)
                        {
                            TeacherHelper.Share(TeacherHelper.CurrentUserPermissionForCurriculum(curriculum, operation), teacher.ID, delegateCheckBox.Checked);
                        }
                        else
                        {
                            if (childPermission.CanBeDelagated && !delegateCheckBox.Checked)
                            {
                                TeacherHelper.RemoveChildPermissions(childPermission);
                            }

                            childPermission.CanBeDelagated = delegateCheckBox.Checked;
                            ServerModel.DB.Update<TblPermissions>(childPermission);
                        }
                    }
                    else
                    {
                        if (childPermission != null)
                        {
                            TeacherHelper.RemoveChildPermissions(childPermission);
                            ServerModel.DB.Delete<TblPermissions>(childPermission.ID);
                        }
                    }
                }
            }

            Redirect(BackUrl);
        }
    }
}
