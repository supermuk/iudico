using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.Controllers.Teacher;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class CourseEditController : BaseTeacherController
    {
        [PersistantField]
        public IVariable<string> CourseName = string.Empty.AsVariable();
        [PersistantField]
        public IVariable<string> CourseDescription = string.Empty.AsVariable();
        [PersistantField]
        public IVariable<bool> DeleteButtonEnabled = false.AsVariable();

        public FileUpload CourseUpload { get; set; }
        public TreeView CourseTree { get; set; }

        private readonly ProjectPaths projectPaths = new ProjectPaths();

        //"magic words"
        private const string pageCaption = "Course management.";
        private const string pageDescription = "This is course upload page. Please selected course, specify name and description and upload it.";
        private const string uploadSucces = "Course was uploaded successfully.";
        private const string uploadError = "Error occurred during course upload.";
        private const string fileNotFound = "Specify course path.";


        public override void Loaded()
        {
            base.Loaded();

            Caption.Value = pageCaption;
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
                    projectPaths.Initialize(CourseUpload.FileName);
                    CourseUpload.SaveAs(projectPaths.PathToCourseZipFile);
                    Zipper.ExtractZipFile(projectPaths.PathToCourseZipFile, projectPaths.PathToTempCourseFolder);

                    string courseName = CourseName.Value == "" ? Path.GetFileNameWithoutExtension(CourseUpload.FileName) : CourseName.Value;
                    int courseId = CourseManager.Import(projectPaths, courseName, CourseDescription.Value);
                    TblCourses course = ServerModel.DB.Load<TblCourses>(courseId);

                    //grant permissions for this course
                    PermissionsManager.Grand(course, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
                    PermissionsManager.Grand(course, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

                    //Update course tree
                    CourseTree.DataSource = GetCourses();
                }
                catch(Exception e)
                {
                    //Message.Value = e.ToString();
                    Message.Value = e.Message;
                    //Message.Value = uploadError;
                }
            }
            else
            {
                Message.Value = fileNotFound;
            }
        }

        public IList<TblCourses> GetCourses()
        {
            return TeacherHelper.CurrentUserCourses(FxCourseOperations.Modify);
        }

        public void CourseBehaviourButton_Click()
        {
		    var selectedNode = (IdendtityNode)CourseTree.SelectedNode;

		    if(selectedNode != null)
			    if(selectedNode.Type == NodeType.Course)
				      RedirectToController(new CourseBehaviorController
                                                     {
                                                         BackUrl = string.Empty,
                                                         CourseId = selectedNode.ID
                                                     });                      
        }
    }
}
