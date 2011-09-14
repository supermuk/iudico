using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for TeacherObjects.aspx page
    /// </summary>
    public class TeacherObjectsController : BaseTeacherController
    {
        public Table CoursesTable { get; set; }
        public Table CurriculumsTable { get; set; }

        //"magic words"
        private readonly string pageCaption = Translations.TeacherObjectsController_pageCaption_Teacher_objects_;
        private readonly string pageDescription = Translations.TeacherObjectsController_pageDescription_Here_are_listed_all_your_courses_and_curriculums__Select_object_to_proceed_;
        private readonly string noCourses = Translations.TeacherObjectsController_noCourses_None__Click_to_upload_some_;
        private readonly string noCurriculums = Translations.TeacherObjectsController_noCurriculums_None__Click_to_create_some_;

        public override void Loaded()
        {
            base.Loaded();

            Caption.Value = pageCaption;
            Description.Value = pageDescription;
            Title.Value = Caption.Value;

            fillCoursesList();
            fillCurriculumsList();
        }

        private void fillCoursesList()
        {
            CoursesTable.Rows.Clear();
            foreach (TblCourses course in TeacherHelper.CurrentUserCourses())
            {
                TableRow courseRow = new TableRow();
                TableCell courseCell = new TableCell();
                HyperLink courseHyperLink = new HyperLink();
                courseHyperLink.Text = course.Name;
                courseHyperLink.NavigateUrl = ServerModel.Forms.BuildRedirectUrl<TeachersListController>(
                    new TeachersListController { CourseId = course.ID, CurriculumId = -1, BackUrl = RawUrl });
                courseCell.Controls.Add(courseHyperLink);
                courseRow.Cells.Add(courseCell);
                CoursesTable.Rows.Add(courseRow);
            }

            if (CoursesTable.Rows.Count == 0)
            {
                TableRow courseRow = new TableRow();
                TableCell courseCell = new TableCell();
                HyperLink courseLink = new HyperLink();
                courseLink.Text = noCourses;
                courseLink.NavigateUrl = ServerModel.Forms.BuildRedirectUrl<CourseEditController>
                    (new CourseEditController { BackUrl = RawUrl });
                courseCell.Controls.Add(courseLink);
                courseRow.Cells.Add(courseCell);
                CoursesTable.Rows.Add(courseRow);
            }
        }

        private void fillCurriculumsList()
        {
            CurriculumsTable.Rows.Clear();
            foreach (TblCurriculums curriculum in TeacherHelper.CurrentUserCurriculums())
            {
                TableRow curriculumRow = new TableRow();
                TableCell curriculumCell = new TableCell();
                HyperLink curriculumHyperLink = new HyperLink();
                curriculumHyperLink.Text = curriculum.Name;
                curriculumHyperLink.NavigateUrl = ServerModel.Forms.BuildRedirectUrl<TeachersListController>(
                    new TeachersListController { CourseId = -1, CurriculumId = curriculum.ID, BackUrl = RawUrl });
                curriculumCell.Controls.Add(curriculumHyperLink);
                curriculumRow.Cells.Add(curriculumCell);
                CurriculumsTable.Rows.Add(curriculumRow);
            }

            if (CurriculumsTable.Rows.Count == 0)
            {
                TableRow curriculumRow = new TableRow();
                TableCell curriculumCell = new TableCell();
                HyperLink curriculumLink = new HyperLink();
                curriculumLink.Text = noCurriculums;
                curriculumLink.NavigateUrl = ServerModel.Forms.BuildRedirectUrl<CurriculumEditController>
                    (new CurriculumEditController { BackUrl = RawUrl });
                curriculumCell.Controls.Add(curriculumLink);
                curriculumRow.Cells.Add(curriculumCell);
                CurriculumsTable.Rows.Add(curriculumRow);
            }
        }

    }
}
