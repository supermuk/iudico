using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class MixedCurriculumStorage : ICurriculumStorage, ICurriculumService
    {
        private ILmsService lmsService;
        private DBDataContext db;

        public MixedCurriculumStorage(ILmsService lmsService)
        {
            this.lmsService = lmsService;
            RefreshState();
        }

        #region IStorageInterface Members

        #region Helper methods

        public void RefreshState()
        {
            //db = new DBDataContext(lmsService.GetDbConnectionString());
            db = lmsService.GetDbDataContext();
        }

        #endregion

        #region Curriculum methods

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return db.Curriculums.Where(item => !item.IsDeleted);
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return db.Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
        }

        public Curriculum GetCurriculum(int id)
        {
            return db.Curriculums.Single(item => item.Id == id && !item.IsDeleted);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            curriculum.Created = DateTime.Now;
            curriculum.Updated = DateTime.Now;
            curriculum.IsDeleted = false;

            db.Curriculums.InsertOnSubmit(curriculum);
            db.SubmitChanges();

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var oldCurriculum = GetCurriculum(curriculum.Id);

            oldCurriculum.Name = curriculum.Name;
            oldCurriculum.Updated = DateTime.Now;

            db.SubmitChanges();
        }

        public void DeleteCurriculum(int id)
        {
            var curriculum = GetCurriculum(id);

            //delete stages
            var stageIds = GetStages(id).Select(item => item.Id);
            DeleteStages(stageIds);

            curriculum.IsDeleted = true;
            //db.Curriculums.DeleteOnSubmit(curriculum);
            db.SubmitChanges();
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            var curriculums = GetCurriculums(ids);

            //delete stages
            var stageIds = from id in ids
                           from stage in GetStages(id)
                           select stage.Id;
            DeleteStages(stageIds);

            foreach (Curriculum curriculum in curriculums)
            {
                curriculum.IsDeleted = true;
            }
            //db.Curriculums.DeleteAllOnSubmit(curriculums);
            db.SubmitChanges();
        }

        #endregion

        #region Stage methods

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            return db.Stages.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted);
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            return db.Stages.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
        }

        public Stage GetStage(int id)
        {
            return db.Stages.Single(item => item.Id == id && !item.IsDeleted);
        }

        public int AddStage(Stage stage)
        {
            stage.Created = DateTime.Now;
            stage.Updated = DateTime.Now;
            stage.IsDeleted = false;

            db.Stages.InsertOnSubmit(stage);
            db.SubmitChanges();

            return stage.Id;
        }

        public void UpdateStage(Stage stage)
        {
            Stage oldStage = GetStage(stage.Id);

            oldStage.Name = stage.Name;
            oldStage.Updated = DateTime.Now;

            db.SubmitChanges();
        }

        public void DeleteStage(int id)
        {
            var stage = GetStage(id);

            var themeIds = GetThemes(id).Select(item => item.Id);
            DeleteThemes(themeIds);

            stage.IsDeleted = true;
            //db.Stages.DeleteOnSubmit(stage);
            db.SubmitChanges();
        }

        public void DeleteStages(IEnumerable<int> ids)
        {
            var stages = GetStages(ids);

            var themeIds = from id in ids
                           from theme in GetThemes(id)
                           select theme.Id;
            DeleteThemes(themeIds);

            foreach (Stage stage in stages)
            {
                stage.IsDeleted = true;
            }
            //db.Stages.DeleteAllOnSubmit(stages);
            db.SubmitChanges();
        }

        #endregion

        #region Theme methods

        public IEnumerable<Theme> GetThemes(int stageId)
        {
            return db.Themes.Where(item => item.StageRef == stageId && !item.IsDeleted).OrderBy(item => item.SortOrder);
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            return db.Themes.Where(item => ids.Contains(item.Id) && !item.IsDeleted).OrderBy(item => item.SortOrder);
        }

        public Theme GetTheme(int id)
        {
            return db.Themes.Single(item => item.Id == id && !item.IsDeleted);
        }

        public int AddTheme(Theme theme, Course course)
        {
            theme.Name = course.Name;
            theme.Created = DateTime.Now;
            theme.Updated = DateTime.Now;
            theme.IsDeleted = false;

            db.Themes.InsertOnSubmit(theme);
            db.SubmitChanges();

            theme.SortOrder = theme.Id;
            UpdateTheme(theme, course);

            return theme.Id;
        }

        public void UpdateTheme(Theme theme, Course course)
        {
            var oldTheme = GetTheme(theme.Id);

            oldTheme.Name = course.Name;
            oldTheme.SortOrder = theme.SortOrder;
            oldTheme.CourseRef = theme.CourseRef;
            oldTheme.Updated = DateTime.Now;

            db.SubmitChanges();
        }

        public void DeleteTheme(int id)
        {
            var theme = GetTheme(id);

            theme.IsDeleted = true;
            //db.Themes.DeleteOnSubmit(theme);
            db.SubmitChanges();
        }

        public void DeleteThemes(IEnumerable<int> ids)
        {
            var themes = GetThemes(ids);

            foreach (Theme theme in themes)
            {
                theme.IsDeleted = true;
            }
            //db.Themes.DeleteAllOnSubmit(themes);
            db.SubmitChanges();
        }

        public Theme ThemeUp(int themeId)
        {
            var theme = GetTheme(themeId);
            IList<Theme> themes = GetThemes(theme.StageRef).ToList();

            int index = themes.IndexOf(theme);
            if (index != -1 && index != 0)
            {
                int temp = themes[index - 1].SortOrder;
                themes[index - 1].SortOrder = theme.SortOrder;
                theme.SortOrder = temp;

                db.SubmitChanges();
            }

            return theme;
        }

        public Theme ThemeDown(int themeId)
        {
            var theme = GetTheme(themeId);
            IList<Theme> themes = GetThemes(theme.StageRef).ToList();

            int index = themes.IndexOf(theme);
            if (index != -1 && index != themes.Count - 1)
            {
                int temp = themes[index + 1].SortOrder;
                themes[index + 1].SortOrder = theme.SortOrder;
                theme.SortOrder = temp;

                db.SubmitChanges();
            }

            return theme;
        }

        #endregion

        #region Assignment methods

        public IEnumerable<Group> GetGroups()
        {
            return db.Groups;
        }

        public Group GetGroup(int curriculumId)
        {
            return null;
        }

        public IEnumerable<Timeline> GetTimelines()
        {
            return db.Timelines;
        }

        #endregion

        #endregion
    }
}