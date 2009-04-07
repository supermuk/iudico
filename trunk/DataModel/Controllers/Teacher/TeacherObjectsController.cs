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
    public class TeacherObjectsController : BaseTeacherController
    {

        public Table CoursesTable { get; set; }
        public Table CurriculumsTable { get; set; }

        //"magic words"
        private const string pageCaption = "Teacher objects.";
        private const string pageDescription = "Here are listed all your courses and curriculums. Select object to proceed.";
        private const string noCourses = "None (Click to upload some)";
        private const string noCurriculums = "None (Click to create some)";

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
