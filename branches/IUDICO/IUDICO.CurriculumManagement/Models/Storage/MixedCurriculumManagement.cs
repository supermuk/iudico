using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class MixedCurriculumManagement : ICurriculumManagement
    {
        protected ILmsService _LmsService;

        public MixedCurriculumManagement(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        protected DBDataContext GetDbContext()
        {
            return _LmsService.GetDbDataContext();
        }

        #region IStorageInterface Members

        #region Curriculum methods

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return GetDbContext().Curriculums.ToList();
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return GetDbContext().Curriculums.Where(item => ids.Contains(item.Id));
        }

        public Curriculum GetCurriculum(int id)
        {
            return GetDbContext().Curriculums.Single(item => item.Id == id);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            var db = GetDbContext();

            curriculum.Created = DateTime.Now;
            curriculum.Updated = DateTime.Now;

            db.Curriculums.InsertOnSubmit(curriculum);
            db.SubmitChanges();

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var oldCurriculum = GetCurriculum(curriculum.Id);

            oldCurriculum.Name = curriculum.Name;
            oldCurriculum.Updated = DateTime.Now;

            GetDbContext().SubmitChanges();
        }

        public void DeleteCurriculum(int id)
        {
            var db = GetDbContext();

            var curriculum = GetCurriculum(id);

            db.Curriculums.DeleteOnSubmit(curriculum);
            db.SubmitChanges();
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            var db = GetDbContext();

            var curriculums = GetCurriculums(ids);

            db.Curriculums.DeleteAllOnSubmit(curriculums);
            db.SubmitChanges();
        }

        #endregion

        #region Stage methods

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            return GetDbContext().Stages.Where(item => item.CurriculumRef == curriculumId);
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            return GetDbContext().Stages.Where(item => ids.Contains(item.Id));
        }

        public Stage GetStage(int id)
        {
            return GetDbContext().Stages.Single(s => s.Id == id);
        }

        public int AddStage(Stage stage)
        {
            var db = GetDbContext();

            stage.Created = DateTime.Now;
            stage.Updated = DateTime.Now;

            db.Stages.InsertOnSubmit(stage);
            db.SubmitChanges();

            return stage.Id;
        }

        public void UpdateStage(Stage stage)
        {
            Stage oldStage = GetStage(stage.Id);

            oldStage.Name = stage.Name;
            oldStage.Updated = DateTime.Now;

            GetDbContext().SubmitChanges();
        }

        public void DeleteStage(int id)
        {
            var db = GetDbContext();

            var stage = GetStage(id);

            db.Stages.DeleteOnSubmit(stage);
            db.SubmitChanges();
        }

        public void DeleteStages(IEnumerable<int> ids)
        {
            var db = GetDbContext();

            var stages = GetStages(ids);

            db.Stages.DeleteAllOnSubmit(stages);
            db.SubmitChanges();
        }

        #endregion

        #region Theme methods

        public IEnumerable<Theme> GetThemes(int stageId)
        {
            return GetDbContext().Themes.Where(item => item.StageRef == stageId).OrderBy(item => item.SortOrder);
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            return GetDbContext().Themes.Where(item => ids.Contains(item.Id)).OrderBy(item => item.SortOrder);
        }

        public Theme GetTheme(int id)
        {
            return GetDbContext().Themes.Single(item => item.Id == id);
        }

        public int AddTheme(Theme theme, Course course)
        {
            var db = GetDbContext();

            theme.Name = course.Name;
            theme.Created = DateTime.Now;
            theme.Updated = DateTime.Now;

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

            GetDbContext().SubmitChanges();
        }

        public void DeleteTheme(int id)
        {
            var db = GetDbContext();

            var theme = GetTheme(id);

            db.Themes.DeleteOnSubmit(theme);
            db.SubmitChanges();
        }

        public void DeleteThemes(IEnumerable<int> ids)
        {
            var db = GetDbContext();

            var themes = GetThemes(ids);

            db.Themes.DeleteAllOnSubmit(themes);
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

                GetDbContext().SubmitChanges();
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

                GetDbContext().SubmitChanges();
            }

            return theme;
        }

        #endregion

        #region Assignment methods

        public IEnumerable<Group> GetGroups()
        {
            return GetDbContext().Groups;
        }

        public Group GetGroup(int curriculumId)
        {
            return null;
        }

        public IEnumerable<Timeline> GetTimelines()
        {
            return GetDbContext().Timelines;
        }
        
        #endregion

        #endregion
    }
}