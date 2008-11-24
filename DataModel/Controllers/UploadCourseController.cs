using System;
using System.IO;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.Controllers
{
    public class UploadCourseController : ControllerBase
    {
        public TextBox Name { get; set; }

        public TextBox Description { get; set; }

        public FileUpload FileUpload { get; set; }

        public void submitButton_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                var projectPaths = new ProjectPaths();
                InitializePaths(FileUpload.FileName, projectPaths);
                FileUpload.SaveAs(projectPaths.PathToCourseZipFile);
                Import(projectPaths);
            }
        }
        public void Import(ProjectPaths projectPaths)
        {
            Zipper.ExtractZipFile(projectPaths.PathToCourseZipFile, projectPaths.PathToTempCourseFolder);
            File.Delete(projectPaths.PathToCourseZipFile);

            CourseManager.Import(projectPaths, Name.Text, Description.Text);

            //CleanDirectory(projectPaths.PathToTempCourseFolder);
        }

        public void InitializePaths(string fileName, ProjectPaths projectPaths)
        {
            projectPaths.PathToTemp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(projectPaths.PathToTemp);

            projectPaths.PathToCourseZipFile = Path.Combine(projectPaths.PathToTemp, fileName);
            projectPaths.PathToTempCourseFolder = Path.Combine(projectPaths.PathToTemp,
                                                               Path.GetFileNameWithoutExtension(fileName));
        }

/*
        private static void CleanDirectory(string path)
        {
            var d = new DirectoryInfo(path);

            foreach (FileInfo f in d.GetFiles())
            {
                File.Delete(f.FullName);
            }
            foreach (DirectoryInfo f in d.GetDirectories())
            {
                CleanDirectory(f.FullName);
            }
            Directory.Delete(path);
        }
*/
    }
}
