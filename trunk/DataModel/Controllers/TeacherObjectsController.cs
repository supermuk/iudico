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
    public class TeacherObjectsController : ControllerBase
    {

        public Table CoursesTable { get; set; }
        public Table CurriculumsTable { get; set; }
        public Label NotifyLabel { get; set; }
        private string rawUrl;


        public void PageLoad(object sender, EventArgs e)
        {
            NotifyLabel.Text = "All your data objects are listes here. Choose any for share.";
            rawUrl = (sender as Page).Request.RawUrl;
            fillCoursesList();
            fillCurriculumsList();
        }

        private void fillCoursesList()
        {
            CoursesTable.Rows.Clear();
            foreach (TblCourses course in TeacherHelper.MyCourses(FxCourseOperations.Use))
            {
                TableRow courseRow = new TableRow();
                TableCell courseCell = new TableCell();
                HyperLink courseHyperLink = new HyperLink();
                courseHyperLink.Text = course.Name;
                courseHyperLink.NavigateUrl = ServerModel.Forms.BuildRedirectUrl<CourseShareController>(
                    new CourseShareController { CourseId = course.ID, BackUrl = rawUrl });
                courseCell.Controls.Add(courseHyperLink);

                courseRow.Cells.Add(courseCell);
                CoursesTable.Rows.Add(courseRow);
            }

            if (CoursesTable.Rows.Count == 0)
            {
                TableRow courseRow = new TableRow();
                TableCell courseCell = new TableCell();
                courseCell.Text = "None (Click to upload some)";
                courseRow.Cells.Add(courseCell);
                CoursesTable.Rows.Add(courseRow);
            }
        }

        private void fillCurriculumsList()
        {
            CurriculumsTable.Rows.Clear();
            foreach (TblCurriculums curriculum in TeacherHelper.MyCurriculums(FxCurriculumOperations.Use))
            {
                TableRow curriculumRow = new TableRow();
                TableCell curriculumCell = new TableCell();
                curriculumCell.Text = curriculum.Name;
                curriculumRow.Cells.Add(curriculumCell);
                CurriculumsTable.Rows.Add(curriculumRow);
            }

            if (CurriculumsTable.Rows.Count == 0)
            {
                TableRow curriculumRow = new TableRow();
                TableCell curriculumCell = new TableCell();
                curriculumCell.Text = "None (Click to create some)";
                curriculumRow.Cells.Add(curriculumCell);
                CurriculumsTable.Rows.Add(curriculumRow);
            }
        }

    }
}
