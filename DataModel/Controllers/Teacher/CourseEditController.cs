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
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    public class CourseEditController : BaseTeacherController
    {
        public IVariable<string> CourseName = string.Empty.AsVariable();
        public IVariable<string> CourseDescription = string.Empty.AsVariable();
        [PersistantField]
        public IVariable<bool> DeleteButtonEnabled = false.AsVariable();

        public FileUpload CourseUpload { get; set; }
        public TreeView CourseTree { get; set; }

        private readonly ProjectPaths projectPaths = new ProjectPaths();

        //"magic words"
        private const string pageCaption = "Course management by: ";
        private const string pageDescription = "This is course upload page. Please selected course, specify name and description and upload it.";
        private const string uploadSucces = "Course was uploaded successfully.";
        private const string uploadError = "Error occurred during course upload.";
        private const string fileNotFound = "Specify course path.";


        public override void Loaded()
        {
            base.Loaded();

            Caption.Value = pageCaption + ServerModel.User.Current.UserName;
            Description.Value = pageDescription;
            Title.Value = Caption.Value;
            Message.Value = string.Empty;

            CourseTree.SelectedNodeChanged += new EventHandler(CourseTree_SelectedNodeChanged);
        }

        private void CourseTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            if ((CourseTree.SelectedNode as IdendtityNode).Type == NodeType.Course)
            {
                DeleteButtonEnabled.Value = true;
            }
            else
            {
                DeleteButtonEnabled.Value = false;
            }

        }

        public void DeleteButton_Click()
        {
            IdendtityNode courseNode = CourseTree.SelectedNode as IdendtityNode;
            Redirect(ServerModel.Forms.BuildRedirectUrl<CourseDeleteConfirmationController>(
                new CourseDeleteConfirmationController
                {
                    CourseID = courseNode.ID,
                    BackUrl = RawUrl
                }));
        }

        public void ImportButton_Click()
        {
            if (CourseUpload.HasFile)
            {
                try
                {
                    PrepareCourse();

                    string courseName = CourseName.Value == "" ? Path.GetFileNameWithoutExtension(CourseUpload.FileName) : CourseName.Value;
                    int courseId = CourseManager.Import(projectPaths, courseName, CourseDescription.Value);
                    TblCourses course = ServerModel.DB.Load<TblCourses>(courseId);

                    //grant permissions for this course
                    PermissionsManager.Grand(course, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
                    PermissionsManager.Grand(course, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

                    //Update course tree
                    CourseTree.Nodes.Add(new IdendtityNode(course));
                }
                catch
                {
                    Message.Value = uploadError;
                }
            }
            else
            {
                Message.Value = fileNotFound;
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

        public IList<TblCourses> GetCourses()
        {
            return TeacherHelper.CurrentUserCourses(FxCourseOperations.Modify);
        }
    }
}
