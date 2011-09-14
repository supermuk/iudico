using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for TeachersList.aspx page
    /// </summary>
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
        private readonly string pageCaption = Translations.TeachersListController_pageCaption_Share_of__0____1__;
        private readonly string courseStr = Translations.TeachersListController_courseStr_course;
        private readonly string curriculumStr = Translations.TeachersListController_curriculumStr_curriculum;
        private readonly string pageDescription = Translations.TeachersListController_pageDescription_Here_you_can_share__0____1__to_another_teachers__or_edit_previously_done_share_;
        private readonly string noTeachers = Translations.TeachersListController_noTeachers_Nobody_;
        private readonly string grantedObject = Translations.TeachersListController_grantedObject__0__granted_you_permission_to__1__this__2_;
        private readonly string ownObject = Translations.TeachersListController_ownObject_This__0__is_your_own_so_you_can__1__it_;
        private readonly string breakLine = "<br>";
        
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
