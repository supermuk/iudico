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
    public class CourseDeleteConfirmationController : ControllerBase
    {
        [ControllerParameter]
        public int CourseID;

        public GridView DependenciesGridView { get; set; }
        public Button DeleteButton { get; set; }
        public Label NotifyLabel { get; set; }

        private TblCourses course;

        public void PageLoad(object sender, EventArgs e)
        {
            course = ServerModel.DB.Load<TblCourses>(CourseID);
            NotifyLabel.Text = "You want to delete course: " + course.Name + ".This course is used in next objects: ";
            fillDependenciesGrid();
            if(DependenciesGridView.Rows.Count==0)
            {
                NotifyLabel.Text += "None";
            }
            DeleteButton.Click += new EventHandler(DeleteButton_Click);
            
        }

        void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (TblThemes theme in TeacherHelper.ThemesForCourse(course))
            {
                ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForTheme(theme));
            }
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForCourse(course));

            CourseCleaner.deleteCourse(course.ID);
            Redirect(BackUrl);
        }

        private void fillDependenciesGrid()
        {
            DataTable dependenciesData = new DataTable();
            dependenciesData.Columns.Add("Theme");
            dependenciesData.Columns.Add("is used in Curriculum");
            dependenciesData.Columns.Add("by");

            foreach (TblThemes theme in TeacherHelper.ThemesForCourse(course))
            {
                IList<TblStages> relatedStages = TeacherHelper.StagesForTheme(theme);
                foreach (TblStages relatedStage in relatedStages)
                {
                    TblCurriculums relatedCurriculum = ServerModel.DB.Load<TblCurriculums>((int)relatedStage.CurriculumRef);
                    IList<TblUsers> curriculumOwners = TeacherHelper.GetCurriculumOwners(relatedCurriculum);
                    string owners = "";
                    foreach (TblUsers curriculumOwner in curriculumOwners)
                    {
                        owners += curriculumOwner.DisplayName + ", ";
                    }
                    owners = owners.TrimEnd(' ', ',');
                    dependenciesData.Rows.Add(theme.Name, relatedCurriculum.Name, owners);
                }
            }

            DependenciesGridView.DataSource = dependenciesData;
            DependenciesGridView.DataBind();
        }

    }
}
