using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            //PermissionsManager.Grand(curriculum, FxCourseOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
            //PermissionsManager.Grand(curriculum, FxCourseOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

            //Update curriculum tree
            CurriculumTree.Nodes.Add(new IdendtityNode(curriculum));
        }

        private void AddThemeButton_Click(object sender, EventArgs e)
        {
            bool added = false;
            foreach (IdendtityNode theme in CourseTree.CheckedNodes)
            {
                if (theme.Type == NodeType.Theme)
                {
                    foreach (IdendtityNode stage in CurriculumTree.CheckedNodes)
                    {
                        if (stage.Type == NodeType.Stage)
                        {
                            bool stageAlreadyHaveThisTheme = false;
                            added = true;
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
            if (!added)
            {
                NotifyLabel.Text = "Check some themes and some stages to link with.";
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //CHECK IF CURRICULUM IS ASSIGNED TO SOME GROUP
            if (CurriculumTree.CheckedNodes.Count == 0)
            {
                NotifyLabel.Text = "Check some node to delete.";
                return;
            }
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

        private void deleteTheme(IdendtityNode theme, List<IdendtityNode> deletedNodes)
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
            IList<int> myCoursesIDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE,
                ServerModel.User.Current.ID, FxCourseOperations.Use.ID, DateTime.Now);

            //foreach (TblCourses course in ServerModel.DB.Load<TblCourses>(myCoursesIDs))
            foreach (TblCourses course in ServerModel.DB.Query<TblCourses>(null))
            {
                IdendtityNode courseNode = new IdendtityNode(course);
                List<int> themesIDs = ServerModel.DB.LookupIds<TblThemes>(course, null);
                foreach (TblThemes theme in ServerModel.DB.Load<TblThemes>(themesIDs))
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
            IList<int> myCurriculumsIDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
                ServerModel.User.Current.ID, FxCurriculumOperations.Modify.ID, DateTime.Now);

            //foreach (TblCurriculums curriculum in ServerModel.DB.Load<TblCurriculums>(myCurriculumsIDs))
            foreach (TblCurriculums curriculum in ServerModel.DB.Query<TblCurriculums>(null))
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

    public enum NodeType { Curriculum, Stage, Theme, Course };

    public class IdendtityNode : TreeNode
    {
        int id;
        NodeType type;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public NodeType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public IdendtityNode(string text)
            : base(text)
        {
        }

        public IdendtityNode(string text, int id)
            : this(text)
        {
            ID = id;
        }

        public IdendtityNode(string text, int id, string description)
            : this(text, id)
        {
            ToolTip = description;
        }

        public IdendtityNode(TblCourses course)
            : this(course.Name, course.ID, course.Description)
        {
            Type = NodeType.Course;
        }

        public IdendtityNode(TblCurriculums curriculum)
            : this(curriculum.Name, curriculum.ID, curriculum.Description)
        {
            Type = NodeType.Curriculum;
        }

        public IdendtityNode(TblStages stage)
            : this(stage.Name, stage.ID, stage.Description)
        {
            Type = NodeType.Stage;
        }

        public IdendtityNode(TblThemes theme)
            : this(theme.Name, theme.ID)
        {
            Type = NodeType.Theme;
        }

        public IdendtityNode()
            : base() { }

        protected override object SaveViewState()
        {
            object[] newState = { base.SaveViewState(), id, Enum.GetName(typeof(NodeType), type) };
            return newState;
        }

        protected override void LoadViewState(object state)
        {
            object[] newState = state as object[];
            base.LoadViewState(newState[0]);
            id = (int)newState[1];
            type = (NodeType)Enum.Parse(typeof(NodeType), newState[2].ToString());
        }

        public static Type GetTable(NodeType type)
        {
            switch (type)
            {
                case NodeType.Course:
                    {
                        return typeof(TblCourses);
                    }
                case NodeType.Curriculum:
                    {
                        return typeof(TblCurriculums);
                    }
                case NodeType.Stage:
                    {
                        return typeof(TblStages);
                    }
                case NodeType.Theme:
                    {
                        return typeof(TblThemes);
                    }
            }
            return null;
        }

    }
}
