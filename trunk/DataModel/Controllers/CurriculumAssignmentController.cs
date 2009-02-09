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
            UnsignButton.Click += new EventHandler(UnsignButton_Click);
            SwitchViewButton.Click += new EventHandler(SwitchViewButton_Click);

            if (!(sender as Page).IsPostBack)
            {
                fillGroupsList();
                fillCurriculumsList();
                fillAssigmentsTree();
            }
        }

        private void UnsignButton_Click(object sender, EventArgs e)
        {
            if (GroupsListBox.SelectedValue != ""
                 && CurriculumsListBox.SelectedValue != "")
            {
                int groupID = int.Parse(GroupsListBox.SelectedValue);
                int curriculumID = int.Parse(CurriculumsListBox.SelectedValue);
                TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
                TblGroups group = ServerModel.DB.Load<TblGroups>(groupID);

                bool isAssigned = false;
                IList<TblGroups> assignedGroups = TeacherHelper.GetCurriculumGroups(curriculum);
                foreach (TblGroups assignedGroup in assignedGroups)
                {
                    if (assignedGroup.ID == group.ID)
                    {
                        isAssigned = true;
                        break;
                    }
                }
                if (!isAssigned)
                {
                    NotifyLabel.Text = "Group: " + group.Name + " and curriculum: " + curriculum.Name +
                            " are not assigned yet. You can't unsign them.";
                }
                else
                {
                    TeacherHelper.UnSignGroupFromCurriculum(group, curriculum);

                    if (curriculumToGroupView)
                    {
                        foreach (IdendtityNode curriculumNode in AssigmentsTree.Nodes)
                        {
                            if (curriculumNode.ID == curriculumID)
                            {
                                foreach (IdendtityNode groupNode in curriculumNode.ChildNodes)
                                {
                                    if (groupNode.ID == group.ID)
                                    {
                                        groupNode.Parent.ChildNodes.Remove(groupNode);
                                        break;
                                    }
                                }
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
                                foreach (IdendtityNode curriculumNode in AssigmentsTree.Nodes)
                                {
                                    if (curriculumNode.ID == curriculumID)
                                    {
                                        curriculumNode.Parent.ChildNodes.Remove(curriculumNode);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                NotifyLabel.Text = "Select a group and curriculum to unsign.";
            }
        }

        private void SwitchViewButton_Click(object sender, EventArgs e)
        {
            curriculumToGroupView = !curriculumToGroupView;
            fillAssigmentsTree();
        }

        private void AssignButton_Click(object sender, EventArgs e)
        {
            if (GroupsListBox.SelectedValue != ""
                && CurriculumsListBox.SelectedValue != "")
            {
                int groupID = int.Parse(GroupsListBox.SelectedValue);
                int curriculumID = int.Parse(CurriculumsListBox.SelectedValue);
                TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
                TblGroups group = ServerModel.DB.Load<TblGroups>(groupID);

                IList<TblGroups> assignedGroups = TeacherHelper.GetCurriculumGroups(curriculum);
                foreach (TblGroups assignedGroup in assignedGroups)
                {
                    if (assignedGroup.ID == group.ID)
                    {
                        NotifyLabel.Text = "Group: " + group.Name + " and curriculum: " + curriculum.Name +
                            " are already assigned.";
                        return;
                    }
                }

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

            foreach (TblCurriculums curriculum in TeacherHelper.MyCurriculums(FxCurriculumOperations.Use))
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
                foreach (TblCurriculums curriculum in TeacherHelper.MyCurriculums(FxCurriculumOperations.Use))
                {
                    IdendtityNode curriculumNode = new IdendtityNode(curriculum.Name, curriculum.ID);
                    AssigmentsTree.Nodes.Add(curriculumNode);

                    IList<TblGroups> assignedGroups = TeacherHelper.GetCurriculumGroups(curriculum);
                    foreach (TblGroups group in assignedGroups)
                    {
                        IdendtityNode groupNode = new IdendtityNode(group.Name, group.ID);
                        //groupNode.NavigateUrl = "~/Teacher/CurriculumAssignment.aspx?groupID=" + group.ID +
                        //    "&curriculumID=" + curriculum.ID;
                        curriculumNode.ChildNodes.Add(groupNode);
                    }
                }
            }
            else
            {
                foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
                {
                    TreeNode groupNode = new IdendtityNode(group.Name, group.ID);
                    AssigmentsTree.Nodes.Add(groupNode);

                    IList<int> assignedCurriculumsIDs = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.CURRICULUM, group.ID, null, null);
                    foreach (TblCurriculums curriculum in ServerModel.DB.Load<TblCurriculums>(assignedCurriculumsIDs))
                    {
                        IdendtityNode curriculumNode = new IdendtityNode(curriculum.Name, curriculum.ID);
                        groupNode.ChildNodes.Add(curriculumNode);
                    }
                }
            }
        }
    }


}
