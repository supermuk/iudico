using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using System.Web.UI.WebControls;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for CurriculumAssignment.aspx page
    /// </summary>
    public class CurriculumAssignmentController : BaseTeacherController
    {
        public IVariable<bool> MainTableVisible = true.AsVariable();
        [PersistantField]
        public IVariable<string> VisibleGroupID = "-1".AsVariable();
        public DropDownList GroupList;
        //"magic words"
        private readonly string pageCaption = Translations.CurriculumAssignmentController_pageCaption_Curriculum_assignment_;
        private readonly string pageDescription = Translations.CurriculumAssignmentController_pageDescription_This_is_curriculum_assignment_page__Select_group_and_curriculum_to_assign_;
        private readonly string noCurriculums = Translations.CurriculumAssignmentController_noCurriculums_You_have_no_curriculums_to_assign__Create_some_first_;
        private readonly string noGroups = Translations.CurriculumAssignmentController_noGroups_You_have_no_groups_to_assign__Create_some_first_;
        private readonly string neitherCurriculumsNorGroup = Translations.CurriculumAssignmentController_neitherCurriculumsNorGroup_You_have_neither_groups_nor_curriculums_to_assign__Create_some_first_;


        public override void Loaded()
        {
            base.Loaded();

            Caption.Value = pageCaption;
            Description.Value = pageDescription;
            Title.Value = Caption.Value;

            List<TblGroups> groups = ServerModel.DB.Query<TblGroups>(null);
            IList<TblCurriculums> curriculums = TeacherHelper.CurrentUserCurriculums(FxCurriculumOperations.Use);

            if (curriculums.Count == 0)
            {
                MainTableVisible.Value = false;
                if (groups.Count == 0)
                {
                    Message.Value = neitherCurriculumsNorGroup;
                }
                else
                {
                    Message.Value = noCurriculums;
                }
            }
            else
            {
                if (groups.Count == 0)
                {
                    MainTableVisible.Value = false;
                    Message.Value = noGroups;
                }
            }
        }

        public void AddGroup()
        {
            VisibleGroupID.Value = GroupList.SelectedValue;
        }
    }


}
