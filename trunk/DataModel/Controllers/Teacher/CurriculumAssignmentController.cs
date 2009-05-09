using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumAssignmentController : BaseTeacherController
    {
        public IVariable<bool> MainTableVisible = true.AsVariable();

        //"magic words"
        private const string pageCaption = "Curriculum assignment.";
        private const string pageDescription = "This is curriculum assignment page. Select group and curriculum to assign.";
        private const string noCurriculums = "You have no curriculums to assign. Create some first.";
        private const string noGroups = "You have no groups to assign. Create some first.";
        private const string neitherCurriculumsNorGroup = "You have neither groups nor curriculums to assign. Create some first.";


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
    }


}
