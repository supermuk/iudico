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
    public class CurriculumDeleteConfirmationController : ControllerBase
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

        private TblCurriculums curriculum;
        private TblStages stage;
        private TblThemes theme;

        public void PageLoad(object sender, EventArgs e)
        {
            curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);
            NotifyLabel.Text = "You want to delete ";
            if (ThemeID != -1)
            {
                theme = ServerModel.DB.Load<TblThemes>(ThemeID);
                stage = ServerModel.DB.Load<TblStages>(StageID);
                NotifyLabel.Text += "theme: " + theme.Name + ".This theme is used in curriculum: " + curriculum.Name;
            }
            else
            {
                if (StageID != -1)
                {
                    stage = ServerModel.DB.Load<TblStages>(StageID);
                    NotifyLabel.Text += "stage: " + stage.Name + ".This stage is used in curriculum: " + curriculum.Name;
                }
                else
                {
                    NotifyLabel.Text += "curiculum: " + curriculum.Name;

                }
            }
            NotifyLabel.Text += ".And this curriculum is assigned to next groups: ";

            fillGroupsList();
            if (GroupsBulletedList.Items.Count == 0)
            {
                NotifyLabel.Text += "None";
            }
            DeleteButton.Click += new EventHandler(DeleteButton_Click);

        }

        private void DeleteButton_Click(object sender, EventArgs e)
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

        private void fillGroupsList()
        {
            GroupsBulletedList.Items.Clear();
            foreach (TblGroups group in TeacherHelper.GetGroupsForCurriculum(curriculum))
            {
                GroupsBulletedList.Items.Add(group.Name);
            }
        }

        private void deleteTheme(TblThemes theme, TblStages parentStage)
        {
            //remove permissions
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForTheme(theme));
            ServerModel.DB.UnLink(theme, parentStage);
        }

        private void deleteStage(TblStages stage)
        {
            foreach (TblThemes theme in TeacherHelper.ThemesForStage(stage))
            {
                deleteTheme(theme, stage);
            }

            //remove permissions
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForStage(stage));
            ServerModel.DB.Delete<TblStages>(stage.ID);
        }

        private void deleteCurriculum(TblCurriculums curriculum)
        {
            foreach (TblStages stage in TeacherHelper.StagesForCurriculum(curriculum))
            {
                deleteStage(stage);
            }
            //remove permissions
            ServerModel.DB.Delete<TblPermissions>(TeacherHelper.AllPermissionsForCurriculum(curriculum));
            ServerModel.DB.Delete<TblCurriculums>(curriculum.ID);
        }


    }
}
