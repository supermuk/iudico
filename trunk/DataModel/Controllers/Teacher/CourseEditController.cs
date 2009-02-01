using System;
using System.IO;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.ImportManagers;
using System.Web.UI;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using System.Collections.Generic;

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
            foreach (TblCourses course in CourseTree.CheckedNodes)
            {
                foreach (TblThemes theme in getThemesOfCourse(course))
                {
                    TblStages relatedStage = ServerModel.DB.QuerySingle<TblStages>
                        (new InCondition(
                            DataObject.Schema.ID,
                            new SubSelectCondition<RelStagesThemes>("StageRef",
                               new CompareCondition(
                                  DataObject.Schema.ThemeRef,
                                  new ValueCondition<int>(theme.ID), COMPARE_KIND.EQUAL))));
                    if (relatedStage != null)
                    { 
                        TblCurriculums relatedCurriculum = ServerModel.DB.Load<TblCurriculums>((int)relatedStage.CurriculumRef);
                        NotifyLabel.Text = "Theme " + theme.Name + "of course " + course.Name +
                            "is used in stage " + relatedStage.Name + " in curriculum " + relatedCurriculum.Name;
                        NotifyLabel.Style["color"] = "violet";
                        return;
                    }

                }
                //REMOVE COURSE!
            }


        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (CourseUpload.HasFile)
            {
                try
                {
                    PrepareCourse();

                    if (NameTextBox.Text.Equals(string.Empty))
                        NameTextBox.Text = Path.GetFileNameWithoutExtension(projectPaths.PathToCourseZipFile);


                    int courseId = CourseManager.Import(projectPaths, NameTextBox.Text, DescriptionTextBox.Text);

                    //update view
                    NotifyLabel.Text = "Course was uploaded successfully.";
                    NotifyLabel.Style["color"] = "blue";

                    TblCourses course = ServerModel.DB.Load<TblCourses>(courseId);

                    //grant permissions for this course
                    PermissionsManager.Grand(course, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
                    PermissionsManager.Grand(course, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

                    //Update course tree
                    addCourseNode(course);
                }
                catch
                {
                    NotifyLabel.Text = "Error occurred during course upload.";
                    NotifyLabel.Style["color"] = "red";
                }
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

            foreach (TblCourses course in ServerModel.DB.Load<TblCourses>(myCoursesIDs))
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

        private List<TblThemes> getThemesOfCourse(TblCourses course)
        {
            return ServerModel.DB.Query<TblThemes>
                    (new CompareCondition(
                        DataObject.Schema.CourseRef,
                        new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL));
        }
    }
}
