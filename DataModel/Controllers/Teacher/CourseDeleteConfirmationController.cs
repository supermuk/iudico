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
    public class CourseDeleteConfirmationController : BaseTeacherController
    {
        [ControllerParameter]
        public int CourseID;

        private TblCourses course;

        //"magic words"
        private const string pageCaption = "Deleting course: {0}.";
        private const string pageDescription = "You want to delete course: {0}. This course is used in next objects:";
        private const string noneMessage = "None.";

        public override void Loaded()
        {
            base.Loaded();

            course = ServerModel.DB.Load<TblCourses>(CourseID);
            Caption.Value = pageCaption.Replace("{0}", course.Name);
            Description.Value = pageDescription.
                Replace("{0}", course.Name);
            Title.Value = Caption.Value;
        }

        public void DeleteButton_Click()
        {
            foreach (TblThemes theme in TeacherHelper.ThemesOfCourse(course))
            {
                ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForTheme(theme));
            }
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForCourse(course));

            CourseCleaner.DeleteCourse(course.ID);
            Redirect(BackUrl);
        }

        public DataTable GetDependencies()
        {
            DataTable dependenciesData = new DataTable();
            dependenciesData.Columns.Add("Theme");
            dependenciesData.Columns.Add("is used in Curriculum");
            dependenciesData.Columns.Add("by");

            foreach (TblThemes theme in TeacherHelper.ThemesOfCourse(course))
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

            if (dependenciesData.Rows.Count == 0)
            {
                Message.Value = noneMessage;
            }

            return dependenciesData;
        }

    }
}
