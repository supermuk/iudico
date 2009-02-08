using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumAssignmentController : ControllerBase
    {
        public TreeView AssigmentsTree { get; set; }

        public Button AssignButton { get; set; }
        public Button SwitchViewButton { get; set; }
        public Button UnsignButton { get; set; }

        public ListBox CurriculumsListBox { get; set; }
        public ListBox GroupsListBox { get; set; }

        public Label NotifyLabel { get; set; }

        [PersistantField]
        private bool curriculumToGroupView = true;

        public void PageLoad(object sender, EventArgs e)
        {
            AssignButton.Click += new EventHandler(AssignButton_Click);
            SwitchViewButton.Click += new EventHandler(SwitchViewButton_Click);
            if (!(sender as Page).IsPostBack)
            {
                fillGroupsList();
                fillCurriculumsList();
                fillAssigmentsTree();
            }
        }

        void SwitchViewButton_Click(object sender, EventArgs e)
        {
            curriculumToGroupView = !curriculumToGroupView;
            fillAssigmentsTree();
        }

        void AssignButton_Click(object sender, EventArgs e)
        {
            if (GroupsListBox.SelectedValue != ""
                && CurriculumsListBox.SelectedValue != "")
            {
                int groupID = int.Parse(GroupsListBox.SelectedValue);
                int curriculumID = int.Parse(CurriculumsListBox.SelectedValue);
                TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
                TblGroups group = ServerModel.DB.Load<TblGroups>(groupID);

                PermissionsManager.Grand(curriculum, FxCurriculumOperations.View
                    , null, groupID, DateTimeInterval.Full);

                if (curriculumToGroupView)
                {
                    foreach (IdendtityNode curriculumNode in AssigmentsTree.Nodes)
                    {
                        if (curriculumNode.ID == curriculumID)
                        {
                            IdendtityNode groupNode = new IdendtityNode(group.Name, group.ID);
                            curriculumNode.ChildNodes.Add(groupNode);
                            curriculumNode.Expand();
                            break;
                        }
                    }
                }
                else
                {
                    foreach (IdendtityNode groupNode in AssigmentsTree.Nodes)
                    {
                        if (groupNode.ID == groupID)
                        {
                            IdendtityNode curriculumNode = new IdendtityNode(curriculum.Name, curriculum.ID);
                            groupNode.ChildNodes.Add(curriculumNode);
                            groupNode.Expand();
                            break;
                        }
                    }
                }
            }
            else
            {
                NotifyLabel.Text = "Select a group and curriculum to assign.";
            }
        }

        private void fillGroupsList()
        {
            GroupsListBox.Items.Clear();
            foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
            {
                ListItem groupItem = new ListItem(group.Name, group.ID.ToString());
                GroupsListBox.Items.Add(groupItem);
            }
            if (GroupsListBox.Items.Count == 0)
            {
                NotifyLabel.Text = "You have no groups, create some first.";
            }
        }

        private void fillCurriculumsList()
        {
            CurriculumsListBox.Items.Clear();

            IList<int> myCurriculumsIDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
                ServerModel.User.Current.ID, FxCurriculumOperations.Use.ID, DateTime.Now);

            //foreach (TblCurriculums curriculum in ServerModel.DB.Load<TblCurriculums>(myCurriculumsIDs))
            foreach (TblCurriculums curriculum in ServerModel.DB.Query<TblCurriculums>(null))
            {
                ListItem curriculumItem = new ListItem(curriculum.Name, curriculum.ID.ToString());
                CurriculumsListBox.Items.Add(curriculumItem);
            }
            if (CurriculumsListBox.Items.Count == 0)
            {
                NotifyLabel.Text = "You have no curriculums, create some first.";
            }
        }

        private void fillAssigmentsTree()
        {
            AssigmentsTree.Nodes.Clear();
            if (curriculumToGroupView)
            {
                IList<int> myCurriculumsIDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
                ServerModel.User.Current.ID, FxCurriculumOperations.Use.ID, DateTime.Now);

                //foreach (TblCurriculums curriculum in ServerModel.DB.Load<TblCurriculums>(myCurriculumsIDs))
                foreach (TblCurriculums curriculum in ServerModel.DB.Query<TblCurriculums>(null))
                {
                    TreeNode curriculumNode = new IdendtityNode(curriculum.Name, curriculum.ID);
                    AssigmentsTree.Nodes.Add(curriculumNode);
                    //get groups for currics
                }
            }
            else
            {
                foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
                {
                    TreeNode groupNode = new IdendtityNode(group.Name, group.ID);
                    AssigmentsTree.Nodes.Add(groupNode);
                    //get currics for groups
                }
            }
        }


    }


}
