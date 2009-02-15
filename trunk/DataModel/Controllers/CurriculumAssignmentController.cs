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

        string rawUrl;

        public void PageLoad(object sender, EventArgs e)
        {
            AssignButton.Click += new EventHandler(AssignButton_Click);
            UnsignButton.Click += new EventHandler(UnsignButton_Click);
            SwitchViewButton.Click += new EventHandler(SwitchViewButton_Click);

            rawUrl = (sender as Page).Request.RawUrl;
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
                IList<TblGroups> assignedGroups = TeacherHelper.GetGroupsForCurriculum(curriculum);
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
                                foreach (IdendtityNode curriculumNode in groupNode.ChildNodes)
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

                IList<TblGroups> assignedGroups = TeacherHelper.GetGroupsForCurriculum(curriculum);
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
                PermissionsManager.Grand(curriculum, FxCurriculumOperations.Pass
                    , null, groupID, DateTimeInterval.Full);

                if (curriculumToGroupView)
                {
                    foreach (IdendtityNode curriculumNode in AssigmentsTree.Nodes)
                    {
                        if (curriculumNode.ID == curriculumID)
                        {
                            addGroupNode(group, curriculumNode);
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
                            addCurriculumNode(curriculum, groupNode);
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

                    foreach (TblGroups group in TeacherHelper.GetGroupsForCurriculum(curriculum))
                    {
                        addGroupNode(group, curriculumNode);
                    }
                }
            }
            else
            {
                foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
                {
                    IdendtityNode groupNode = new IdendtityNode(group.Name, group.ID);
                    AssigmentsTree.Nodes.Add(groupNode);

                    foreach (TblCurriculums curriculum in TeacherHelper.GetCurriculumsForGroup(group))
                    {
                        addCurriculumNode(curriculum, groupNode);
                    }
                }
            }
        }

        private void addGroupNode(TblGroups group, IdendtityNode parent)
        {
            IdendtityNode groupNode = new IdendtityNode(group.Name, group.ID);
            //groupNode.NavigateUrl = ServerModel.Forms.BuildRedirectUrl(
            //    new CurriculumTimelineController { GroupID = group.ID, CurriculumID = parent.ID });
            parent.ChildNodes.Add(groupNode);
        }

        private void addCurriculumNode(TblCurriculums curriculum, IdendtityNode parent)
        {
            IdendtityNode groupNode = new IdendtityNode(curriculum.Name, curriculum.ID);
            //groupNode.NavigateUrl = ServerModel.Forms.BuildRedirectUrl(
            //    new CurriculumTimelineController {  GroupID = parent.ID, CurriculumID = curriculum.ID });
            parent.ChildNodes.Add(groupNode);
        }
    }


}
