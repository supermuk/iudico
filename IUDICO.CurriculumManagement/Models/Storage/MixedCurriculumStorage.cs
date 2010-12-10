using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Security.AccessControl;
using IUDICO.Common.Models;
using System.Data.Common;
using System.Data.Linq;
using IUDICO.Common.Models.Services;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class MixedCurriculumStorage : ICurriculumManagement
    {
        protected DBDataContext db = new DBDataContext();

        #region IStorageInterface Members

        #region Curriculum methods

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return db.Curriculums;
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return db.Curriculums.Where(item => ids.Contains(item.Id));
        }

        public Curriculum GetCurriculum(int id)
        {
            return db.Curriculums.Single(item => item.Id == id);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            curriculum.Created = DateTime.Now;
            curriculum.Updated = DateTime.Now;

            db.Curriculums.InsertOnSubmit(curriculum);
            db.SubmitChanges();

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            Curriculum oldCurriculum = GetCurriculum(curriculum.Id);

            oldCurriculum.Name = curriculum.Name;
            oldCurriculum.Updated = DateTime.Now;

            db.SubmitChanges();
        }

        public void DeleteCurriculum(int id)
        {
            Curriculum curriculum = GetCurriculum(id);

            db.Curriculums.DeleteOnSubmit(curriculum);
            db.SubmitChanges();
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            var curriculums = GetCurriculums(ids);

            db.Curriculums.DeleteAllOnSubmit(curriculums);
            db.SubmitChanges();
        }

        #endregion

        #region Stage methods

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            return db.Stages.Where(item => item.CurriculumRef == curriculumId);
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            return db.Stages.Where(item => ids.Contains(item.Id));
        }

        public Stage GetStage(int id)
        {
            return db.Stages.Single(s => s.Id == id);
        }

        public int AddStage(Stage stage)
        {
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

            db.SubmitChanges();
        }

        public void DeleteStage(int id)
        {
            Stage stage = GetStage(id);

            db.Stages.DeleteOnSubmit(stage);
            db.SubmitChanges();
        }

        public void DeleteStages(IEnumerable<int> ids)
        {
            var stages = GetStages(ids);

            db.Stages.DeleteAllOnSubmit(stages);
            db.SubmitChanges();
        }

        #endregion

        #region Theme methods

        public IEnumerable<Theme> GetThemes(int stageId)
        {
            return db.Themes.Where(item => item.StageRef == stageId).OrderBy(item => item.SortOrder);
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            return db.Themes.Where(item => ids.Contains(item.Id)).OrderBy(item => item.SortOrder);
        }

        public Theme GetTheme(int id)
        {
            return db.Themes.Single(item => item.Id == id);
        }

        public int AddTheme(Theme theme)
        {
            theme.Name = GetCourse(theme.CourseRef).Name;
            theme.Created = DateTime.Now;
            theme.Updated = DateTime.Now;

            db.Themes.InsertOnSubmit(theme);
            db.SubmitChanges();

            theme.SortOrder = theme.Id;
            UpdateTheme(theme);

            return theme.Id;
        }

        public void UpdateTheme(Theme theme)
        {
            Theme oldTheme = GetTheme(theme.Id);

            oldTheme.Name = GetCourse(theme.CourseRef).Name;
            oldTheme.SortOrder = theme.SortOrder;
            oldTheme.CourseRef = theme.CourseRef;
            oldTheme.Updated = DateTime.Now;

            db.SubmitChanges();
        }

        public void DeleteTheme(int id)
        {
            Theme theme = GetTheme(id);

            db.Themes.DeleteOnSubmit(theme);
            db.SubmitChanges();
        }

        public void DeleteThemes(IEnumerable<int> ids)
        {
            var themes = GetThemes(ids);

            db.Themes.DeleteAllOnSubmit(themes);
            db.SubmitChanges();
        }

        public Theme ThemeUp(int themeId)
        {
            Theme theme = GetTheme(themeId);
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
            Theme theme = GetTheme(themeId);
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

        #region Call of external methods

        public Course GetCourse(int id)
        {
            //GetCourseMessage message = new GetCourseMessage { Input = id };
            //MvcContrib.Bus.Send(message);

            //return message.Result.Data as Course;
            return null;
        }

        public List<Course> GetCourses()
        {
            //GetCoursesMessage message = new GetCoursesMessage { };
            //MvcContrib.Bus.Send(message);

            //return message.Result.Data as List<Course>;
            return null;
        }

        #endregion

        #region ICurriculumManagement Members


        public int AddTheme(Theme theme, Course course)
        {
            throw new NotImplementedException();
        }

        public void UpdateTheme(Theme theme, Course course)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}