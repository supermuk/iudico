using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using System.Collections.Generic;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for  StatisticSelect.aspx page
    /// </summary>
    public class StatisticSelectController : BaseTeacherController
    {
        public DropDownList GroupsDropDownList { get; set; }
        public string CheckedCurriculums { get; set; }
        public CheckBoxList CurriculumsCheckBoxList { get; set; }

        [PersistantField]
        public IVariable<bool> ShowButtonEnabled = false.AsVariable();

        //"magic words"
        private readonly string pageCaption = Translations.StatisticSelectController_pageCaption_Statistic_selection_;
        private readonly string pageDescription = Translations.StatisticSelectController_pageDescription_Select_group_and_then_select_curriculum_to_view_statistic_;
        private readonly string noCurriculums = Translations.StatisticSelectController_noCurriculums_You_have_no_curriculums_assigned_for_this_group_;
        private readonly string noGroups = Translations.StatisticSelectController_noGroups_You_have_no_groups_at_all__Create_some_first_;

        public override void Loaded()
        {
            base.Loaded();
            Caption.Value = pageCaption;
            Description.Value = pageDescription;
            Title.Value = Caption.Value;

            GroupsDropDownList.SelectedIndexChanged += new EventHandler(GroupsDropDownList_SelectedIndexChanged);
            if (!IsPostBack)
            {
                fillGroupsList();
                if (GroupsDropDownList.Items.Count == 0)
                {
                    GroupsDropDownList.Enabled = false;
                    CurriculumsCheckBoxList.Enabled = false;
                    ShowButtonEnabled.Value = false;

                    Message.Value = noGroups;
                }
                else
                {
                    GroupsDropDownList_SelectedIndexChanged(GroupsDropDownList, EventArgs.Empty);
                }
            }
        }



        public void ShowButton_Click()
        {
            int selected_item_count = 0;
            for (int i = 0; i < CurriculumsCheckBoxList.Items.Count; i++)
            {
                if (CurriculumsCheckBoxList.Items[i].Selected == true)
                {
                    selected_item_count += 1;
                    CheckedCurriculums += (int.Parse(CurriculumsCheckBoxList.Items[i].Value)).ToString() + "_";
                }
            }
            if (selected_item_count == 1)
            {
                string tmp = "";
                for (int i = 0; i < CheckedCurriculums.Length-1; i++)
                {
                    tmp += CheckedCurriculums[i].ToString();
                }
                    RedirectToController<StatisticShowController>(new StatisticShowController
                    {
                        GroupID = int.Parse(GroupsDropDownList.SelectedValue),
                        CurriculumID =Convert.ToInt32(tmp),
                        BackUrl = RawUrl
                    });
            }
            else if(selected_item_count >1)
            {
                RedirectToController<StatisticShowCurriculumsController>(new StatisticShowCurriculumsController
                {
                    GroupID = int.Parse(GroupsDropDownList.SelectedValue),
                    CurriculumsID = CheckedCurriculums,
                    BackUrl = RawUrl
                });
            }
           
        }

        private void GroupsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurriculumsCheckBoxList.Items.Clear();
            TblGroups selectedGroup = ServerModel.DB.Load<TblGroups>(int.Parse(GroupsDropDownList.SelectedValue));

            foreach (TblCurriculums curr in TeacherHelper.GetCurriculumsForGroup(selectedGroup))
            {
                CurriculumsCheckBoxList.Items.Add(new ListItem(curr.Name, curr.ID.ToString()));
            }

            if (CurriculumsCheckBoxList.Items.Count == 0)
            {
                CurriculumsCheckBoxList.Enabled= false;
                ShowButtonEnabled.Value = false;

                Message.Value = noCurriculums;
            }
            else
            {
                CurriculumsCheckBoxList.Enabled = true;
                ShowButtonEnabled.Value = true;
            }
        }

        private void fillGroupsList()
        {
            GroupsDropDownList.Items.Clear();
            foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
            {
                GroupsDropDownList.Items.Add(new ListItem(group.Name, group.ID.ToString()));
            }
        }





    }
}
