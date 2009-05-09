using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class TeachersListController : BaseTeacherController
    {
        [ControllerParameter]
        public int CourseId;
        private TblCourses course;

        [ControllerParameter]
        public int CurriculumId;
        private TblCurriculums curriculum;

        public IVariable<string> ObjectOwner = string.Empty.AsVariable();

        public Table AlreadySharedTeachers { get; set; }
        public Table CanBeSharedTeachers { get; set; }

        //"magic words"
        private const string pageCaption = "Share of {0}: {1}.";
        private const string courseStr = "course";
        private const string curriculumStr = "curriculum";
        private const string pageDescription = "Here you can share {0}: {1} to another teachers, or edit previously done share.";
        private const string noTeachers = "Nobody.";
        private const string grantedObject = "{0} granted you permission to {1} this {2}";
        private const string ownObject = "This {0} is your own so you can {1} it.";
        private const string breakLine = "<br>";
        
        public override void Loaded()
        {
            base.Loaded();

            string message = "";
            if (CourseId != -1)
            {
                course = ServerModel.DB.Load<TblCourses>(CourseId);
                Caption.Value = pageCaption.
                    Replace("{0}", courseStr).
                    Replace("{1}", course.Name);
                Description.Value = pageDescription.
                    Replace("{0}", courseStr).
                    Replace("{1}", course.Name);
                              
                foreach (TblPermissions permission in TeacherHelper.CurrentUserPermissionsForCourse(course))
                {
                    FxCourseOperations operation = ServerModel.DB.Load<FxCourseOperations>(permission.CourseOperationRef.Value);
                    if (permission.ParentPermitionRef.HasValue)
                    {
                        TblPermissions parentPermission = ServerModel.DB.Load<TblPermissions>(permission.ParentPermitionRef.Value);
                        TblUsers parent = ServerModel.DB.Load<TblUsers>(parentPermission.OwnerUserRef.Value);
                        message += grantedObject.
                            Replace("{0}", parent.DisplayName).
                            Replace("{1}", operation.Name).
                            Replace("{2}", courseStr) + breakLine;
                    }
                    else
                    {
                        message += ownObject.Replace("{0}", courseStr).Replace("{1}", operation.Name) + breakLine;
                    }
                }
            }

            if (CurriculumId != -1)
            {
                curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumId);
                Caption.Value = pageCaption.
                    Replace("{0}", curriculumStr).
                    Replace("{1}", curriculum.Name);
                Description.Value = pageDescription.
                    Replace("{0}", curriculumStr).
                    Replace("{1}", curriculum.Name);

                foreach (TblPermissions permission in TeacherHelper.CurrentUserPermissionsForCurriculum(curriculum))
                {
                    FxCurriculumOperations operation = ServerModel.DB.Load<FxCurriculumOperations>(permission.CurriculumOperationRef.Value);
                    if (permission.ParentPermitionRef.HasValue)
                    {
                        TblPermissions parentPermission = ServerModel.DB.Load<TblPermissions>(permission.ParentPermitionRef.Value);
                        TblUsers parent = ServerModel.DB.Load<TblUsers>(parentPermission.OwnerUserRef.Value);
                        message += grantedObject.
                            Replace("{0}", parent.DisplayName).
                            Replace("{1}", operation.Name).
                            Replace("{2}", curriculumStr) + breakLine;
                    }
                    else
                    {
                        message += ownObject.Replace("{0}", curriculumStr).Replace("{1}", operation.Name) + breakLine;
                    }
                }
            }

            Message.Value = message;
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
                    sharedHyperlink.NavigateUrl = ServerModel.Forms.BuildRedirectUrl<ShareController>
                        (new ShareController { BackUrl = RawUrl, CourseId = CourseId, CurriculumId = CurriculumId, TeacherId = teacher.ID });
                    TableCell sharedCell = new TableCell();
                    sharedCell.Controls.Add(sharedHyperlink);
                    TableRow sharedRow = new TableRow();
                    sharedRow.Cells.Add(sharedCell);
                    if (CourseId != -1)
                    {
                        if (TeacherHelper.AreParentAndChildByCourse(currentUser, teacher, course))
                        {
                            AlreadySharedTeachers.Rows.Add(sharedRow);
                        }
                        else
                        {
                            CanBeSharedTeachers.Rows.Add(sharedRow);
                        }
                    }
                    if (CurriculumId != -1)
                    {
                        if (TeacherHelper.AreParentAndChildByCurriculum(currentUser, teacher, curriculum))
                        {
                            AlreadySharedTeachers.Rows.Add(sharedRow);
                        }
                        else
                        {
                            CanBeSharedTeachers.Rows.Add(sharedRow);
                        }
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
    }
}
