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
        private string rawUrl;

        //"magic words"
        private const string pageDescription = "This is course upload page. Please selected course, specify name and description and upload it.";
        private const string uploadSucces = "Course was uploaded successfully.";
        private const string uploadError = "Error occurred during course upload.";
        private const string fileNotFound = "Specify course path.";

        public void PageLoad(object sender, EventArgs e)
        {
            //registering for events            
            ImportButton.Click += new EventHandler(ImportButton_Click);
            DeleteButton.Click += new EventHandler(DeleteButton_Click);
            CourseTree.SelectedNodeChanged += new EventHandler(CourseTree_SelectedNodeChanged);

            rawUrl = (sender as Page).Request.RawUrl;
            NotifyLabel.Text = pageDescription;
            if (!(sender as Page).IsPostBack)
            {
                fillCourseTree();

                DeleteButton.Enabled = false;
            }
        }

        void CourseTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            if ((CourseTree.SelectedNode as IdendtityNode).Type == NodeType.Course)
            {
                DeleteButton.Enabled = true;
            }
            else
            {
                DeleteButton.Enabled = false;
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            IdendtityNode courseNode = CourseTree.SelectedNode as IdendtityNode;
            TblCourses course = ServerModel.DB.Load<TblCourses>(courseNode.ID);
            Redirect(ServerModel.Forms.BuildRedirectUrl<CourseDeleteConfirmationController>(
                new CourseDeleteConfirmationController
                {
                    CourseID = course.ID,
                    BackUrl = rawUrl
                }));
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (CourseUpload.HasFile)
            {
                try
                {
                    PrepareCourse();

                    string courseName = NameTextBox.Text == "" ? Path.GetFileNameWithoutExtension(CourseUpload.FileName) : NameTextBox.Text;
                    int courseId = CourseManager.Import(projectPaths, courseName, DescriptionTextBox.Text);
                    TblCourses course = ServerModel.DB.Load<TblCourses>(courseId);

                    //grant permissions for this course
                    PermissionsManager.Grand(course, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
                    PermissionsManager.Grand(course, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

                    //update view
                    addCourseNode(course);
                    NotifyLabel.Text = uploadSucces;
                }
                catch
                {
                    NotifyLabel.Text = uploadError;
                }
            }
            else
            {
                NotifyLabel.Text = fileNotFound;
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
