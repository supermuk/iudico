using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumDeleteConfirmationController : BaseTeacherController
    {
        [ControllerParameter]
        public int CurriculumID;
        [ControllerParameter]
        public int StageID;
        [ControllerParameter]
        public int ThemeID;

        public BulletedList GroupsBulletedList { get; set; }
        public Button DeleteButton { get; set; }
        public Label NotifyLabel { get; set; }

        //"magic words"
        private const string pageCaption = "Deleting {0}: {1}";
        private const string stageStr = "stage";
        private const string themeStr = "theme";
        private const string curriculumStr = "curriculum";
        private const string pageDescription = "You want to delete {1}: {2}{3}, which is assigned to next groups:";
        private const string usedInCurriculum = ". It is used in curriculum {0}";
        private const string noneMessage = "None.";

        private TblCurriculums curriculum;
        private TblStages stage;
        private TblThemes theme;

        public override void Loaded()
        {
            base.Loaded();

            Caption.Value = pageCaption;
            Description.Value = pageDescription;

            curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);

            if (ThemeID != -1)
            {
                theme = ServerModel.DB.Load<TblThemes>(ThemeID);
                stage = ServerModel.DB.Load<TblStages>(StageID);
                Description.Value = Description.Value.
                    Replace("{1}", themeStr).
                    Replace("{2}", theme.Name).
                    Replace("{3}", usedInCurriculum.Replace("{0}", curriculum.Name));
                Caption.Value = Caption.Value.
                    Replace("{0}", themeStr).
                    Replace("{1}", theme.Name);
            }
            else
            {
                if (StageID != -1)
                {
                    stage = ServerModel.DB.Load<TblStages>(StageID);
                    Description.Value = Description.Value.
                        Replace("{1}", stageStr).
                        Replace("{2}", stage.Name).
                        Replace("{3}", usedInCurriculum.Replace("{0}", curriculum.Name));
                    Caption.Value = Caption.Value.
                        Replace("{0}", stageStr).
                        Replace("{1}", stage.Name);
                }
                else
                {
                    Description.Value = Description.Value.
                        Replace("{1}", curriculumStr).
                        Replace("{2}", curriculum.Name).
                        Replace("{3}", string.Empty);
                    Caption.Value = Caption.Value.
                        Replace("{0}", curriculumStr).
                        Replace("{1}", curriculum.Name);
                }
            }

            Title.Value = Caption.Value;
        }

        public void DeleteButton_Click()
        {
            if (ThemeID != -1)
            {
                deleteTheme(theme, stage);
            }
            else
            {
                if (StageID != -1)
                {
                    deleteStage(stage);
                }
                else
                {
                    deleteCurriculum(curriculum);
                }
            }

            Redirect(BackUrl);
        }

        public IList<string> GetGroups()
        {
            IList<string> result = new List<string>();
            foreach (TblGroups group in TeacherHelper.GetGroupsForCurriculum(curriculum))
            {
                result.Add(group.Name);
            }
            if (result.Count == 0)
            {
                Message.Value = noneMessage;
            }

            return result;
        }

        private void deleteTheme(TblThemes theme, TblStages parentStage)
        {
            //remove permissions
            ServerModel.DB.UnLink(theme, parentStage);
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForTheme(theme));
            ServerModel.DB.Delete<TblThemes>(theme.ID);
        }

        private void deleteStage(TblStages stage)
        {
            foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
            {
                deleteTheme(theme, stage);
            }

            //remove permissions
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForStage(stage));
            ServerModel.DB.Delete<TblStages>(stage.ID);
        }

        private void deleteCurriculum(TblCurriculums curriculum)
        {
            foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
            {
                deleteStage(stage);
            }
            //remove permissions
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForCurriculum(curriculum));
            ServerModel.DB.Delete<TblCurriculums>(curriculum.ID);
        }


    }
}
