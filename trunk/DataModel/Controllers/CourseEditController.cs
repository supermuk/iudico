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

namespace IUDICO.DataModel.Controllers
{
    public class CourseEditController : ControllerBase
    {
        public TextBox NameTextBox { get; set; }
        public TextBox DescriptionTextBox { get; set; }
        public FileUpload CourseUpload { get; set; }
        public TreeView CourseTree { get; set; }
        public Label NotifyLabel { get; set; }
        public Button ImportButton { get; set; }
        public Button DeleteButton { get; set; }

        private readonly ProjectPaths projectPaths = new ProjectPaths();

        public void PageLoad(object sender, EventArgs e)
        {
            //registering for events            
            ImportButton.Click += new EventHandler(ImportButton_Click);
            DeleteButton.Click += new EventHandler(DeleteButton_Click);
            if (!(sender as Page).IsPostBack)
            {
                fillCourseTree();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //argument validation
            if (CourseTree.CheckedNodes.Count == 0)
            {
                NotifyLabel.Text = "Check some courses to delete.";
                return;
            }

            //validate if current course is used in any curriculum
            for (int i = 0; i < CourseTree.CheckedNodes.Count; i++)
            {
                IdendtityNode courseNode = CourseTree.CheckedNodes[i] as IdendtityNode;
                TblCourses course = ServerModel.DB.Load<TblCourses>(courseNode.ID);

                foreach (TblThemes theme in TeacherHelper.ThemesForCourse(course))
                {
                    TblStages relatedStage = TeacherHelper.StageForTheme(theme);
                    if (relatedStage != null)
                    {
                        TblCurriculums relatedCurriculum = ServerModel.DB.Load<TblCurriculums>((int)relatedStage.CurriculumRef);
                        TblUsers owner = TeacherHelper.GetCurriculumOwner(relatedCurriculum);
                        NotifyLabel.Text = "Theme " + theme.Name + " of course: " + course.Name +
                            " is used in stage: " + relatedStage.Name + " in curriculum: " + relatedCurriculum.Name +
                            " by: " + owner.DisplayName;
                        return;
                    }

                }

                //remove permissions
                IList<TblPermissions> permissions = TeacherHelper.PermissionsForCourse(course);
                ServerModel.DB.Delete<TblPermissions>(permissions);

                //remove course
                CourseCleaner.deleteCourse(course.ID);
                CourseTree.Nodes.Remove(courseNode);
                i--;
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            //validate arguments
            if (NameTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter course name.";
                return;
            }
            if (DescriptionTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter course description.";
                return;
            }

            if (CourseUpload.HasFile)
            {
                try
                {
                    PrepareCourse();
                    int courseId = CourseManager.Import(projectPaths, NameTextBox.Text, DescriptionTextBox.Text);
                    TblCourses course = ServerModel.DB.Load<TblCourses>(courseId);

                    //grant permissions for this course
                    PermissionsManager.Grand(course, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
                    PermissionsManager.Grand(course, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

                    //update view
                    addCourseNode(course);
                    NotifyLabel.Text = "Course was uploaded successfully.";
                }
                catch
                {
                    NotifyLabel.Text = "Error occurred during course upload.";
                }
            }
            else
            {
                NotifyLabel.Text = "Specify course path.";
            }
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

        private void fillCourseTree()
        {
            CourseTree.Nodes.Clear();

            foreach (TblCourses course in TeacherHelper.MyCourses(FxCourseOperations.Modify))
            {
                addCourseNode(course);
            }
            CourseTree.ExpandAll();
        }

        private void addCourseNode(TblCourses course)
        {
            IdendtityNode courseNode = new IdendtityNode(course);
            foreach (TblThemes theme in TeacherHelper.ThemesForCourse(course))
            {
                IdendtityNode themeNode = new IdendtityNode(theme);
                courseNode.ChildNodes.Add(themeNode);
            }
            CourseTree.Nodes.Add(courseNode);
        }
    }
}
