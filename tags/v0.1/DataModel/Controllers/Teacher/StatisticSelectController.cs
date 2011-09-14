using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class StatisticSelectController : BaseTeacherController
    {
        public DropDownList GroupsDropDownList { get; set; }
        public DropDownList CurriculumsDropDownList { get; set; }

        [PersistantField]
        public IVariable<bool> ShowButtonEnabled = false.AsVariable();

        //"magic words"
        private const string pageCaption = "Statistic selection.";
        private const string pageDescription = "Select group and then select curriculum to view statistic.";
        private const string noCurriculums = "You have no curriculums assigned for this group.";
        private const string noGroups = "You have no groups at all. Create some first.";

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
                    CurriculumsDropDownList.Enabled = false;
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
            RedirectToController<StatisticShowController>(new StatisticShowController
            {
                CurriculumID = int.Parse(CurriculumsDropDownList.SelectedValue),
                GroupID = int.Parse(GroupsDropDownList.SelectedValue),
                BackUrl = RawUrl
            });
        }

        private void GroupsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
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
                ShowButtonEnabled.Value = false;

                Message.Value = noCurriculums;
            }
            else
            {
                CurriculumsDropDownList.Enabled = true;
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
