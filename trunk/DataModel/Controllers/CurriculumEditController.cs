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

        public void PageLoad(object sender, EventArgs e)
        {
            //registering for events            
            CreateCurriculumButton.Click += new EventHandler(CreateCurriculumButton_Click);
            AddThemeButton.Click += new EventHandler(AddThemeButton_Click);
            CreateStageButton.Click += new EventHandler(CreateStageButton_Click);
            DeleteButton.Click += new EventHandler(DeleteButton_Click);
            ModifyButton.Click += new EventHandler(ModifyButton_Click);

            if (!(sender as Page).IsPostBack)
            {
                fillCourseTree();
                fillCurriculumTree();
            }
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            //argument validation
            bool checkedNodes = false;
            if (NameTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter curriculum new name.";
                return;
            }
            if (DescriptionTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter curriculum new description.";
                return;
            }

            //modifying selected nodes
            foreach (IdendtityNode node in CurriculumTree.CheckedNodes)
            {
                if (node.Type == NodeType.Stage)
                {
                    checkedNodes = true;
                    TblStages stage = ServerModel.DB.Load<TblStages>(node.ID);
                    stage.Name = NameTextBox.Text;
                    stage.Description = DescriptionTextBox.Text;
                    ServerModel.DB.Update<TblStages>(stage);

                    node.Text = stage.Name;
                    node.ToolTip = stage.Description;
                }
                if (node.Type == NodeType.Curriculum)
                {
                    checkedNodes = true;
                    TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(node.ID);
                    curriculum.Name = NameTextBox.Text;
                    curriculum.Description = DescriptionTextBox.Text;
                    ServerModel.DB.Update<TblCurriculums>(curriculum);

                    node.Text = curriculum.Name;
                    node.ToolTip = curriculum.Description;
                }
            }
            if (!checkedNodes)
            {
                NotifyLabel.Text = "Check some curriculums or stages to modify.";
                return;
            }
        }

        private void CreateStageButton_Click(object sender, EventArgs e)
        {
            //argument validation
            bool checkedCurriculums = false;
            if (NameTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter stage name.";
                return;
            }
            if (DescriptionTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter stage description.";
                return;
            }

            //adding new stage
            foreach (IdendtityNode curriculum in CurriculumTree.CheckedNodes)
            {
                if (curriculum.Type == NodeType.Curriculum)
                {
                    checkedCurriculums = true;
                    //Create new stage
                    TblStages stage = new TblStages();
                    stage.Name = NameTextBox.Text;
                    stage.Description = DescriptionTextBox.Text;
                    stage.CurriculumRef = curriculum.ID;
                    stage.ID = ServerModel.DB.Insert<TblStages>(stage);

                    //Update curriculum tree
                    curriculum.ChildNodes.Add(new IdendtityNode(stage));
                }
            }
            if (!checkedCurriculums)
            {
                NotifyLabel.Text = "Check some curriculums to which stage will be added.";
                return;
            }
        }

        private void CreateCurriculumButton_Click(object sender, EventArgs e)
        {
            //argument validation
            if (NameTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter curriculum name.";
                return;
            }
            if (DescriptionTextBox.Text.Trim() == "")
            {
                NotifyLabel.Text = "Enter curriculum description.";
                return;
            }
            //create new curriculum
            TblCurriculums curriculum = new TblCurriculums();
            curriculum.Name = NameTextBox.Text;
            curriculum.Description = DescriptionTextBox.Text;
            curriculum.ID = ServerModel.DB.Insert<TblCurriculums>(curriculum);

            //grant permissions for this curriculum
            PermissionsManager.Grand(curriculum, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
            PermissionsManager.Grand(curriculum, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

            //Update curriculum tree
            CurriculumTree.Nodes.Add(new IdendtityNode(curriculum));
        }

        private void AddThemeButton_Click(object sender, EventArgs e)
        {
            bool added = false;
            foreach (IdendtityNode themeNode in CourseTree.CheckedNodes)
            {
                if (themeNode.Type == NodeType.Theme)
                {
                    foreach (IdendtityNode stageNode in CurriculumTree.CheckedNodes)
                    {
                        if (stageNode.Type == NodeType.Stage)
                        {
                            bool stageAlreadyHaveThisTheme = false;
                            added = true;
                            foreach (IdendtityNode stageChild in stageNode.ChildNodes)
                            {
                                if (stageChild.ID == themeNode.ID)
                                {
                                    stageAlreadyHaveThisTheme = true;
                                    break;
                                }
                            }
                            if (stageAlreadyHaveThisTheme)
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
                }
            }
            if (!added)
            {
                NotifyLabel.Text = "Check some themes and some stages to link with.";
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (CurriculumTree.CheckedNodes.Count == 0)
            {
                NotifyLabel.Text = "Check some node to delete.";
                return;
            }

            for (int i = 0; i < CurriculumTree.CheckedNodes.Count; i++)
            {
                IdendtityNode node = CurriculumTree.CheckedNodes[i] as IdendtityNode;
                if (node != null)
                {
                    switch (node.Type)
                    {
                        case NodeType.Curriculum:
                            {
                                TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(node.ID);
                                IList<TblGroups> groups = TeacherHelper.GetCurriculumGroups(curriculum);
                                if (groups.Count != 0)
                                {
                                    NotifyLabel.Text = "Curriculum " + curriculum.Name + " is assigned to next group(s): ";
                                    foreach (TblGroups group in groups)
                                    {
                                        NotifyLabel.Text += group.Name + ", ";
                                    }
                                    NotifyLabel.Text = NotifyLabel.Text.Trim(' ', ',');
                                    NotifyLabel.Text += ". Remove assigments first.";
                                    return;
                                }

                                deleteCurriculum(node);
                                break;
                            }
                        case NodeType.Stage:
                            {
                                deleteStage(node);
                                break;
                            }
                        case NodeType.Theme:
                            {
                                deleteTheme(node);
                                break;
                            }
                    }

                    if (node.Parent != null)
                    {
                        node.Parent.ChildNodes.Remove(node);
                    }
                    else
                    {
                        CurriculumTree.Nodes.Remove(node);
                    }
                }
                i--;
            }

        }

        private void deleteTheme(IdendtityNode theme)
        {
            //remove permissions
            IList<TblPermissions> permissions = TeacherHelper.PermissionsForTheme
                (ServerModel.DB.Load<TblThemes>(theme.ID));
            ServerModel.DB.Delete<TblPermissions>(permissions);

            ServerModel.DB.UnLink(
                        ServerModel.DB.Load<TblStages>(((theme.Parent) as IdendtityNode).ID),
                        ServerModel.DB.Load<TblThemes>(theme.ID));
        }

        private void deleteStage(IdendtityNode stage)
        {
            foreach (IdendtityNode theme in stage.ChildNodes)
            {
                deleteTheme(theme);
            }

            //remove permissions
            IList<TblPermissions> permissions = TeacherHelper.PermissionsForStage
                (ServerModel.DB.Load<TblStages>(stage.ID));
            ServerModel.DB.Delete<TblPermissions>(permissions);

            ServerModel.DB.Delete<TblStages>(stage.ID);
        }

        private void deleteCurriculum(IdendtityNode curriculum)
        {


            foreach (IdendtityNode stage in curriculum.ChildNodes)
            {
                deleteStage(stage);
            }
            //remove permissions
            IList<TblPermissions> permissions = TeacherHelper.PermissionsForCurriculum
                (ServerModel.DB.Load<TblCurriculums>(curriculum.ID));
            ServerModel.DB.Delete<TblPermissions>(permissions);

            ServerModel.DB.Delete<TblCurriculums>(curriculum.ID);
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
                NotifyLabel.Text = "You have no courses, upload some first.";
            }

        }

        private void fillCurriculumTree()
        {
            CurriculumTree.Nodes.Clear();

            foreach (TblCurriculums curriculum in TeacherHelper.MyCurriculums(FxCurriculumOperations.Modify))
            {
                IdendtityNode curriculumNode = new IdendtityNode(curriculum);
                List<int> stagesIDs = ServerModel.DB.LookupIds<TblStages>(curriculum, null);
                foreach (TblStages stage in ServerModel.DB.Load<TblStages>(stagesIDs))
                {
                    IdendtityNode stageNode = new IdendtityNode(stage);
                    List<int> themesIDs = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
                    foreach (TblThemes theme in ServerModel.DB.Load<TblThemes>(themesIDs))
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
