using System;
using System.IO;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.Controllers
{
    public class ImportCourseController : ControllerBase
    {
        public TextBox Name { get; set; }

        public TextBox Description { get; set; }

        public FileUpload CourseUpload { get; set; }

        public HyperLink EditLink { get; set; }

        private const string editLinkUrl = "EditCourse.aspx?courseId={0}";

        private readonly ProjectPaths projectPaths = new ProjectPaths();

        public void importButton_Click(object sender, EventArgs e)
        {
            if (CourseUpload.HasFile)
            {
                PrepareCourse();

                if (Name.Text.Equals(string.Empty))
                    Name.Text = Path.GetFileNameWithoutExtension(projectPaths.PathToCourseZipFile);

                int courseId = CourseManager.Import(projectPaths, Name.Text, Description.Text);

                ShowEditLink(courseId);
            }
        }

        private void ShowEditLink(int courseId)
        {
            EditLink.NavigateUrl = string.Format(editLinkUrl, courseId);
            EditLink.Visible = true;
        }

        private void PrepareCourse()
        {
            InitializePaths(CourseUpload.FileName);
            CourseUpload.SaveAs(projectPaths.PathToCourseZipFile);
            Zipper.ExtractZipFile(projectPaths.PathToCourseZipFile, projectPaths.PathToTempCourseFolder);
        }

        private void InitializePaths(string fileName)
        {
            projectPaths.PathToTemp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(projectPaths.PathToTemp);

            projectPaths.PathToCourseZipFile = Path.Combine(projectPaths.PathToTemp, fileName);
            projectPaths.PathToTempCourseFolder = Path.Combine(projectPaths.PathToTemp,
                                                               Path.GetFileNameWithoutExtension(fileName));
        }
    }
}
