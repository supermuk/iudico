using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.ImportManagers.RemoveManager;
using IUDICO.DataModel.Security;
using System.Data;

namespace IUDICO.DataModel.Controllers
{
    public class StatisticSelectController : ControllerBase
    {

        public DropDownList GroupsDropDownList { get; set; }
        public DropDownList CurriculumsDropDownList { get; set; }
        public Button ShowButton { get; set; }
        public Label NotifyLabel { get; set; }
        private string rawUrl;

        public void PageLoad(object sender, EventArgs e)
        {
            GroupsDropDownList.SelectedIndexChanged += new EventHandler(GroupsDropDownList_SelectedIndexChanged);
            ShowButton.Click += new EventHandler(ShowButton_Click);
            NotifyLabel.Text = "This is statistic selection page. Select group and curriculum to view statistic";

            rawUrl = (sender as Page).Request.RawUrl;
            if (!(sender as Page).IsPostBack)
            {
                fillGroupsList();
                GroupsDropDownList_SelectedIndexChanged(GroupsDropDownList, EventArgs.Empty);
            }
        }

        void ShowButton_Click(object sender, EventArgs e)
        {
            RedirectToController<StatisticShowController>(new StatisticShowController
            {
                CurriculumID = int.Parse(CurriculumsDropDownList.SelectedValue),
                GroupID = int.Parse(GroupsDropDownList.SelectedValue),
                BackUrl = rawUrl
            });
        }

        void GroupsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurriculumsDropDownList.Items.Clear();
            TblGroups selectedGroup = ServerModel.DB.Load<TblGroups>(int.Parse(GroupsDropDownList.SelectedValue));

            foreach (TblCurriculums curriculum in TeacherHelper.GetCurriculumsForGroup(selectedGroup))
            {
                CurriculumsDropDownList.Items.Add(new ListItem(curriculum.Name, curriculum.ID.ToString()));
            }

            if (CurriculumsDropDownList.Items.Count == 0)
            {
                CurriculumsDropDownList.Enabled = false;
                ShowButton.Enabled = false;

                NotifyLabel.Text = "You have no curriculums assigned for this group.";
            }
            else
            {
                CurriculumsDropDownList.Enabled = true;
                ShowButton.Enabled = true;
            }
        }

        private void fillGroupsList()
        {
            GroupsDropDownList.Items.Clear();
            foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
            {
                GroupsDropDownList.Items.Add(new ListItem(group.Name, group.ID.ToString()));
            }

            if (GroupsDropDownList.Items.Count == 0)
            {
                GroupsDropDownList.Enabled = false;
                CurriculumsDropDownList.Enabled = false;
                ShowButton.Enabled = false;

                NotifyLabel.Text = "You have no groups at all. Create some first.";
            }
        }





    }
}
