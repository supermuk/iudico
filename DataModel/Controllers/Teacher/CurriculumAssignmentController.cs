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

        public void PageLoad(object sender, EventArgs e)
        {
            AssignButton.Click += new EventHandler(AssignButton_Click);

            if (!(sender as Page).IsPostBack)
            {
                fillGroupsList();
                fillCurriculumsList();
            }
        }

        void AssignButton_Click(object sender, EventArgs e)
        {
            if (GroupsListBox.SelectedValue != null
                && CurriculumsListBox.SelectedValue != null)
            {
                int groupID = int.Parse(GroupsListBox.SelectedValue);
                int curriculumID = int.Parse(CurriculumsListBox.SelectedValue);

                PermissionsManager.Grand(
                    ServerModel.DB.Load<TblCurriculums>(curriculumID),
                    FxCurriculumOperations.View
                    , null, groupID, DateTimeInterval.Full);

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
        }

        private void fillCurriculumsList()
        {
            CurriculumsListBox.Items.Clear();
            foreach (TblCurriculums curriculum in ServerModel.DB.Query<TblCurriculums>(null))
            {
                ListItem curriculumItem = new ListItem(curriculum.Name, curriculum.ID.ToString());
                CurriculumsListBox.Items.Add(curriculumItem);
            }
        }

        private void fillAssigmentsTree()
        {
            AssigmentsTree.Nodes.Clear();
            foreach (TblCurriculums curriculum in ServerModel.DB.Query<TblCurriculums>(null))
            {
                TreeNode curriculumNode = new TreeNode(curriculum.Name, curriculum.ID.ToString());
                //CurriculumsListBox.Items.Add(curriculumItem);
            }
        }


    }


}
