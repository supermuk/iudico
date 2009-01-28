using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.DB;
using System.Web.UI;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.DB.Base;

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
            foreach (IdendtityNode node in CurriculumTree.CheckedNodes)
            {
                if (node.Type == NodeType.Stage)
                {
                    TblStages stage = ServerModel.DB.Load<TblStages>(node.ID);
                    stage.Name = NameTextBox.Text;
                    stage.Description = DescriptionTextBox.Text;
                    ServerModel.DB.Update<TblStages>(stage);

                    node.Text = stage.Name;
                    node.ToolTip = stage.Description;
                }
                if (node.Type == NodeType.Curriculum)
                {
                    TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(node.ID);
                    curriculum.Name = NameTextBox.Text;
                    curriculum.Description = DescriptionTextBox.Text;
                    ServerModel.DB.Update<TblCurriculums>(curriculum);

                    node.Text = curriculum.Name;
                    node.ToolTip = curriculum.Description;
                }
            }
        }

        private void CreateStageButton_Click(object sender, EventArgs e)
        {
            foreach (IdendtityNode curriculum in CurriculumTree.CheckedNodes)
            {
                if (curriculum.Type == NodeType.Curriculum)
                {
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
        }

        private void CreateCurriculumButton_Click(object sender, EventArgs e)
        {
            //create new curriculum
            TblCurriculums curriculum = new TblCurriculums();
            curriculum.Name = NameTextBox.Text;
            curriculum.Description = DescriptionTextBox.Text;
            curriculum.ID = ServerModel.DB.Insert<TblCurriculums>(curriculum);

            //Update curriculum tree
            CurriculumTree.Nodes.Add(new IdendtityNode(curriculum));
        }

        private void AddThemeButton_Click(object sender, EventArgs e)
        {
            foreach (IdendtityNode theme in CourseTree.CheckedNodes)
            {
                if (theme.Type == NodeType.Theme)
                {
                    foreach (IdendtityNode stage in CurriculumTree.CheckedNodes)
                    {
                        if (stage.Type == NodeType.Stage)
                        {
                            bool stageAlreadyHaveThisTheme = false;
                            foreach (IdendtityNode stageChild in stage.ChildNodes)
                            {
                                if (stageChild.ID == theme.ID)
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
                                    ServerModel.DB.Load<TblStages>(stage.ID),
                                    ServerModel.DB.Load<TblThemes>(theme.ID));
                                stage.ChildNodes.Add(new IdendtityNode(ServerModel.DB.Load<TblThemes>(theme.ID)));
                            }
                        }
                    }
                }

            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            List<IdendtityNode> deletedNodes = new List<IdendtityNode>();
            foreach (IdendtityNode node in CurriculumTree.CheckedNodes)
            {
                switch (node.Type)
                {
                    case NodeType.Curriculum:
                        {
                            deleteCurriculum(node, deletedNodes);
                            break;
                        }
                    case NodeType.Stage:
                        {
                            deleteStage(node, deletedNodes);
                            break;
                        }
                    case NodeType.Theme:
                        {
                            deleteTheme(node, deletedNodes);
                            break;
                        }
                }

            }
            foreach (IdendtityNode deletedNode in deletedNodes)
            {
                if (deletedNode != null)
                {
                    if (deletedNode.Parent != null)
                    {
                        deletedNode.Parent.ChildNodes.Remove(deletedNode);
                    }
                    else
                    {
                        CurriculumTree.Nodes.Remove(deletedNode);
                    }
                }
            }


        }

        private void deleteTheme(IdendtityNode theme,List<IdendtityNode> deletedNodes)
        {
            if (!deletedNodes.Contains(theme))
            {
                ServerModel.DB.UnLink(
                            ServerModel.DB.Load<TblStages>(((theme.Parent) as IdendtityNode).ID),
                            ServerModel.DB.Load<TblThemes>(theme.ID));
                deletedNodes.Add(theme);
            }
        }

        private void deleteStage(IdendtityNode stage, List<IdendtityNode> deletedNodes)
        {
            if (!deletedNodes.Contains(stage))
            {
                foreach (IdendtityNode theme in stage.ChildNodes)
                {
                    deleteTheme(theme, deletedNodes);
                }
                ServerModel.DB.Delete<TblStages>(stage.ID);
                deletedNodes.Add(stage);
            }
        }

        private void deleteCurriculum(IdendtityNode curriculum, List<IdendtityNode> deletedNodes)
        {
            if (!deletedNodes.Contains(curriculum))
            {
                foreach (IdendtityNode stage in curriculum.ChildNodes)
                {
                    deleteStage(stage, deletedNodes);
                }
                ServerModel.DB.Delete<TblCurriculums>(curriculum.ID);
                deletedNodes.Add(curriculum);
            }
        }

        private void fillCourseTree()
        {
            CourseTree.Nodes.Clear();
            foreach (TblCourses course in ServerModel.DB.Query<TblCourses>(null))
            {
                IdendtityNode courseNode = new IdendtityNode(course);
                foreach (TblThemes theme in ServerModel.DB.Query<TblThemes>
                    (new CompareCondition(
                        DataObject.Schema.CourseRef,
                        new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL)))
                {
                    IdendtityNode themeNode = new IdendtityNode(theme);
                    courseNode.ChildNodes.Add(themeNode);
                }
                CourseTree.Nodes.Add(courseNode);
            }
            CourseTree.ExpandAll();


        }

        private void fillCurriculumTree()
        {
            CurriculumTree.Nodes.Clear();
            foreach (TblCurriculums curriculum in ServerModel.DB.Query<TblCurriculums>(null))
            {
                IdendtityNode curriculumNode = new IdendtityNode(curriculum);
                foreach (TblStages stage in ServerModel.DB.Query<TblStages>
                    (new CompareCondition(
                        DataObject.Schema.CurriculumRef,
                        new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)))
                {
                    IdendtityNode stageNode = new IdendtityNode(stage);
                    foreach (TblThemes theme in ServerModel.DB.Query<TblThemes>
                        (new InCondition(
                            DataObject.Schema.ID,
                            new SubSelectCondition<RelStagesThemes>("ThemeRef",
                               new CompareCondition(
                                  DataObject.Schema.StageRef,
                                  new ValueCondition<int>(stage.ID), COMPARE_KIND.EQUAL)))))
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
