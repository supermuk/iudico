using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
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

                foreach (TblThemes theme in getThemesOfCourse(course))
                {
                    List<int> stagesIDs = ServerModel.DB.LookupMany2ManyIds<TblStages>(theme, null);
                    IList<TblStages> relatedStages = ServerModel.DB.Load<TblStages>(stagesIDs);
                    if (relatedStages.Count > 0)
                    {
                        TblCurriculums relatedCurriculum = ServerModel.DB.Load<TblCurriculums>((int)relatedStages[0].CurriculumRef);
                        NotifyLabel.Text = "Theme " + theme.Name + " of course: " + course.Name +
                            " is used in stage: " + relatedStages[0].Name + " in curriculum: " + relatedCurriculum.Name;
                        return;
                    }

                }
                //REMOVE PERMISSIONS
                //REMOVE COURSE FROM DATABASE!!!
                //CourseCleaner.deleteCourse(course.ID);
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

                    if (NameTextBox.Text.Equals(string.Empty))
                        NameTextBox.Text = Path.GetFileNameWithoutExtension(projectPaths.PathToCourseZipFile);


                    int courseId = CourseManager.Import(projectPaths, NameTextBox.Text, DescriptionTextBox.Text);

                    
                    

                    TblCourses course = ServerModel.DB.Load<TblCourses>(courseId);

                    //grant permissions for this course
                    //PermissionsManager.Grand(course, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
                    //PermissionsManager.Grand(course, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

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
                return;
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
            IList<int> myCoursesIDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE,
                ServerModel.User.Current.ID, FxCourseOperations.Modify.ID, DateTime.Now);

            //foreach (TblCourses course in ServerModel.DB.Load<TblCourses>(myCoursesIDs))
            foreach (TblCourses course in ServerModel.DB.Query<TblCourses>(null))
            {
                addCourseNode(course);
            }
            CourseTree.ExpandAll();
        }

        private void addCourseNode(TblCourses course)
        {
            IdendtityNode courseNode = new IdendtityNode(course);
            foreach (TblThemes theme in getThemesOfCourse(course))
            {
                IdendtityNode themeNode = new IdendtityNode(theme);
                courseNode.ChildNodes.Add(themeNode);
            }
            CourseTree.Nodes.Add(courseNode);
        }

        private IList<TblThemes> getThemesOfCourse(TblCourses course)
        {
            List<int> themesIDs = ServerModel.DB.LookupIds<TblThemes>(course, null);
            return ServerModel.DB.Load<TblThemes>(themesIDs);
        }

        static class CourseCleaner
        {
            public static void deleteCourse(int courseId)
            {
                var course = ServerModel.DB.Load<TblCourses>(courseId);
                var themes = ServerModel.DB.LookupIds<TblThemes>(course, null);

                foreach (var i in themes)
                {
                    deleteTheme(i);
                }
                ServerModel.DB.Delete<TblCourses>(courseId);
            }

            public static void deleteTheme(int themeId)
            {
                var theme = ServerModel.DB.Load<TblThemes>(themeId);
                var pages = ServerModel.DB.LookupIds<TblPages>(theme, null);

                foreach (var i in pages)
                {
                    deletePage(i);
                }
                ServerModel.DB.Delete<TblThemes>(themeId);
            }

            public static void deletePage(int pageId)
            {
                var page = ServerModel.DB.Load<TblPages>(pageId);
                deleteFiles(page);

                var question = ServerModel.DB.LookupIds<TblQuestions>(page, null);

                foreach (var i in question)
                {
                    deleteQuestion(i);
                }

                ServerModel.DB.Delete<TblPages>(pageId);
            }

            private static void deleteFiles(TblPages page)
            {
                var files = ServerModel.DB.Load<TblFiles>(ServerModel.DB.LookupIds<TblFiles>(page, null));

                foreach (var file in files)
                {
                    if (file.PID != null)
                    {
                        ServerModel.DB.Delete<TblFiles>(file.ID);
                    }
                }

                var folders = ServerModel.DB.Load<TblFiles>(ServerModel.DB.LookupIds<TblFiles>(page, null));

                foreach (var file in folders)
                {
                    ServerModel.DB.Delete<TblFiles>(file.ID);
                }
            }

            private static void deleteQuestion(int questionId)
            {
                var question = ServerModel.DB.Load<TblQuestions>(questionId);

                if (question.IsCompiled)
                {
                    deleteCompiledQuestion((int)question.CompiledQuestionRef);
                }

                ServerModel.DB.Delete<TblQuestions>(questionId);
            }

            private static void deleteCompiledQuestion(int compiledQuestionId)
            {
                var compiledQuestion = (ServerModel.DB.Load<TblCompiledQuestions>(compiledQuestionId));

                var compiledQuestionsData = ServerModel.DB.LookupIds<TblCompiledQuestionsData>(compiledQuestion, null);

                foreach (var i in compiledQuestionsData)
                {
                    deleteCompiledQuestionData(i);
                }

                ServerModel.DB.Delete<TblCompiledQuestions>(compiledQuestionId);
            }

            private static void deleteCompiledQuestionData(int compiledQuestionDataId)
            {
                ServerModel.DB.Delete<TblCompiledQuestionsData>(compiledQuestionDataId);
            }
        }
    }
}
