using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumEditController : ControllerBase
    {

        public TreeView CourseTree { get; set; }
        public TreeView CurriculumTree { get; set; }
        public Button CreateCurriculumButton { get; set; }
        public Button CreateStageButton { get; set; }
        public Button AddThemeButton { get; set; }
        public Button DeleteButton { get; set; }
        public Button ModifyButton { get; set; }
        public TextBox NameTextBox { get; set; }
        public TextBox DescriptionTextBox { get; set; }
        public Label NotifyLabel { get; set; }

        private string rawUrl;

        //"magic words"
        private const string pageDescription = "This is curriculum edit page page. Create/Edit/Delete your curriculum here.";
        private const string noCourses = "You have no courses, upload some first.";

        public void PageLoad(object sender, EventArgs e)
        {
            //registering for events            
            CreateCurriculumButton.Click += new EventHandler(CreateCurriculumButton_Click);
            AddThemeButton.Click += new EventHandler(AddThemeButton_Click);
            CreateStageButton.Click += new EventHandler(CreateStageButton_Click);
            DeleteButton.Click += new EventHandler(DeleteButton_Click);
            ModifyButton.Click += new EventHandler(ModifyButton_Click);
            CurriculumTree.SelectedNodeChanged += new EventHandler(CurriculumTree_SelectedNodeChanged);

            rawUrl = (sender as Page).Request.RawUrl;
            NotifyLabel.Text = pageDescription;
            if (!(sender as Page).IsPostBack)
            {
                fillCourseTree();
                fillCurriculumTree();

                ModifyButton.Enabled = false;
                DeleteButton.Enabled = false;
                CreateStageButton.Enabled = false;
                AddThemeButton.Enabled = false;
            }
        }

        private void CurriculumTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            IdendtityNode selectedNode = CurriculumTree.SelectedNode as IdendtityNode;

            switch (selectedNode.Type)
            {
                case NodeType.Curriculum:
                    {
                        CreateStageButton.Enabled = true;
                        DeleteButton.Enabled = true;
                        ModifyButton.Enabled = true;
                        AddThemeButton.Enabled = false;
                        break;
                    }
                case NodeType.Stage:
                    {
                        CreateStageButton.Enabled = false;
                        DeleteButton.Enabled = true;
                        ModifyButton.Enabled = true;
                        AddThemeButton.Enabled = true;
                        break;
                    }
                case NodeType.Theme:
                    {
                        CreateStageButton.Enabled = false;
                        DeleteButton.Enabled = true;
                        ModifyButton.Enabled = false;
                        AddThemeButton.Enabled = false;
                        break;
                    }
            }
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            //modifying selected node
            IdendtityNode modifiedNode = CurriculumTree.SelectedNode as IdendtityNode;
            if (modifiedNode.Type == NodeType.Stage)
            {
                TblStages stage = ServerModel.DB.Load<TblStages>(modifiedNode.ID);
                stage.Name = NameTextBox.Text;
                stage.Description = DescriptionTextBox.Text;
                ServerModel.DB.Update<TblStages>(stage);
            }
            if (modifiedNode.Type == NodeType.Curriculum)
            {
                TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(modifiedNode.ID);
                curriculum.Name = NameTextBox.Text;
                curriculum.Description = DescriptionTextBox.Text;
                ServerModel.DB.Update<TblCurriculums>(curriculum);
            }

            modifiedNode.Text = NameTextBox.Text;
            modifiedNode.ToolTip = DescriptionTextBox.Text;
        }

        private void CreateStageButton_Click(object sender, EventArgs e)
        {
            //adding new stage
            IdendtityNode curriculumNode = CurriculumTree.SelectedNode as IdendtityNode;

            //Create new stage
            TblStages stage = new TblStages();
            stage.Name = NameTextBox.Text;
            stage.Description = DescriptionTextBox.Text;
            stage.CurriculumRef = curriculumNode.ID;
            ServerModel.DB.Insert<TblStages>(stage);

            //Update curriculum tree
            curriculumNode.ChildNodes.Add(new IdendtityNode(stage));
        }

        private void CreateCurriculumButton_Click(object sender, EventArgs e)
        {
            //create new curriculum
            TblCurriculums curriculum = new TblCurriculums();
            curriculum.Name = NameTextBox.Text;
            curriculum.Description = DescriptionTextBox.Text;
            curriculum.ID = ServerModel.DB.Insert<TblCurriculums>(curriculum);

            //grant permissions for this curriculum
//            PermissionsManager.Grand(curriculum, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
//            PermissionsManager.Grand(curriculum, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

            //Update curriculum tree
            CurriculumTree.Nodes.Add(new IdendtityNode(curriculum));
        }

        private void AddThemeButton_Click(object sender, EventArgs e)
        {
            foreach (IdendtityNode themeNode in CourseTree.CheckedNodes)
            {
                IdendtityNode stageNode = CurriculumTree.SelectedNode as IdendtityNode;

                if (TeacherHelper.StageContainsTheme(stageNode.ID, themeNode.ID))
                {
                    break;
                }
                else
                {
                    ServerModel.DB.Link(
                        ServerModel.DB.Load<TblStages>(stageNode.ID),
                        ServerModel.DB.Load<TblThemes>(themeNode.ID));
                    stageNode.ChildNodes.Add(new IdendtityNode(ServerModel.DB.Load<TblThemes>(themeNode.ID)));
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            IdendtityNode deletedNode = CurriculumTree.SelectedNode as IdendtityNode;
            int themeID = -1;
            int stageID = -1;
            int curriculumID = -1;
            switch (deletedNode.Type)
            {
                case NodeType.Theme:
                    {
                        themeID = deletedNode.ID;
                        stageID = (deletedNode.Parent as IdendtityNode).ID;
                        curriculumID = (deletedNode.Parent.Parent as IdendtityNode).ID;
                        break;
                    }
                case NodeType.Stage:
                    {
                        stageID = deletedNode.ID;
                        curriculumID = (deletedNode.Parent as IdendtityNode).ID;
                        break;
                    }
                case NodeType.Curriculum:
                    {
                        curriculumID = deletedNode.ID;
                        break;
                    }
            }

            Redirect(ServerModel.Forms.BuildRedirectUrl<CurriculumDeleteConfirmationController>(
                 new CurriculumDeleteConfirmationController
                 {
                     BackUrl = rawUrl,
                     CurriculumID = curriculumID,
                     StageID = stageID,
                     ThemeID = themeID
                 }));

        }

        private void fillCourseTree()
        {
            CourseTree.Nodes.Clear();

            foreach (TblCourses course in TeacherHelper.MyCourses(FxCourseOperations.Use))
            {
                IdendtityNode courseNode = new IdendtityNode(course);
                foreach (TblThemes theme in TeacherHelper.ThemesForCourse(course))
                {
                    IdendtityNode themeNode = new IdendtityNode(theme);
                    courseNode.ChildNodes.Add(themeNode);
                }
                CourseTree.Nodes.Add(courseNode);
            }
            CourseTree.ExpandAll();
            if (CourseTree.Nodes.Count == 0)
            {
                NotifyLabel.Text = noCourses;
            }

        }

        private void fillCurriculumTree()
        {
            CurriculumTree.Nodes.Clear();

            foreach (TblCurriculums curriculum in TeacherHelper.MyCurriculums(FxCurriculumOperations.Modify))
            {
                IdendtityNode curriculumNode = new IdendtityNode(curriculum);
                foreach (TblStages stage in TeacherHelper.StagesForCurriculum(curriculum))
                {
                    IdendtityNode stageNode = new IdendtityNode(stage);
                    foreach (TblThemes theme in TeacherHelper.ThemesForStage(stage))
                    {
                        IdendtityNode themeNode = new IdendtityNode(theme);
                        stageNode.ChildNodes.Add(themeNode);
                    }
                    curriculumNode.ChildNodes.Add(stageNode);
                }
                CurriculumTree.Nodes.Add(curriculumNode);
            }
            CurriculumTree.ExpandAll();
        }
    }
}
