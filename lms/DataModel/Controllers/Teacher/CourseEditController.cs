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
    /// <summary>
    /// Controller for CourseEdit.aspx page
    /// </summary>
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
        private readonly string pageCaption = Translations.CourseEditController_pageCaption_Course_management_;
        private readonly string pageDescription = Translations.CourseEditController_pageDescription_This_is_course_upload_page__Please_selected_course__specify_name_and_description_and_upload_it_;
        private readonly string uploadSucces = Translations.CourseEditController_uploadSucces_Course_was_uploaded_successfully_;
        private readonly string uploadError = Translations.CourseEditController_uploadError_Error_occurred_during_course_upload_;
        private readonly string fileNotFound = Translations.CourseEditController_fileNotFound_Specify_course_path_;


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
