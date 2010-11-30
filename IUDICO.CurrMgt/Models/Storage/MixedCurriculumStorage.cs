using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Security.AccessControl;
using IUDICO.Common.Models;
using System.Data.Common;
using System.Data.Linq;
using IUDICO.Common.Messages.CourseMgt;

namespace IUDICO.CurrMgt.Models.Storage
{
    public class MixedCurriculumStorage : ICurriculumStorage
    {
        protected DB db = DB.Instance;

        #region IStorageInterface Members

        #region Curriculum methods

        public IEnumerable<Curriculum> GetCurriculums()
        {
            try
            {
                return db.Curriculums.ToList();
            }
            catch
            {
                return null;
            }
        }

        public Curriculum GetCurriculum(int id)
        {
            try
            {
                return db.Curriculums.Single(c => c.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public int? AddCurriculum(Curriculum curriculum)
        {
            try
            {
                curriculum.Created = DateTime.Now;
                curriculum.Updated = DateTime.Now;

                db.Curriculums.InsertOnSubmit(curriculum);
                db.SubmitChanges();

                return curriculum.Id;
            }
            catch
            {
                db = new DB();
                return null;
            }
        }

        public bool UpdateCurriculum(int id, Curriculum curriculum)
        {
            try
            {
                Curriculum oldCurriculum = db.Curriculums.Single(c => c.Id == id);

                oldCurriculum.Name = curriculum.Name;
                oldCurriculum.Updated = DateTime.Now;

                db.SubmitChanges();

                return true;
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        public bool DeleteCurriculum(int id)
        {
            try
            {
                Curriculum curriculum = db.Curriculums.Single(c => c.Id == id);

                db.Curriculums.DeleteOnSubmit(curriculum);
                db.SubmitChanges();

                return true;
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        public bool DeleteCurriculums(IEnumerable<int> ids)
        {
            try
            {
                var curriculums = (from curriculum in db.Curriculums where ids.Contains(curriculum.Id) select curriculum);
                //IEnumerable<Stage> stages=new List<Stage>();
                //foreach curriculum in DeleteStages(

                db.Curriculums.DeleteAllOnSubmit(curriculums);
                db.SubmitChanges();

                return true;
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        #endregion

        #region Stage methods

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            try
            {
                return db.Stages.Where(c => c.CurriculumRef == curriculumId);
            }
            catch
            {
                return null;
            }
        }

        public int? AddStage(Stage stage)
        {
            try
            {
                stage.Created = DateTime.Now;
                stage.Updated = DateTime.Now;

                db.Stages.InsertOnSubmit(stage);
                db.SubmitChanges();

                return stage.Id;
            }
            catch
            {
                db = new DB();
                return null;
            }
        }

        public Stage GetStage(int id)
        {
            try
            {
                return db.Stages.Single(s => s.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateStage(int id, Stage stage)
        {
            try
            {
                Stage oldStage = GetStage(id);

                oldStage.Name = stage.Name;
                oldStage.Updated = DateTime.Now;
                stage.CurriculumRef = oldStage.CurriculumRef;

                db.SubmitChanges();

                return true;
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        public bool DeleteStage(int id)
        {
            try
            {
                Stage stage = db.Stages.Single(s => s.Id == id);

                db.Stages.DeleteOnSubmit(stage);

                db.SubmitChanges();

                return true;
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        public bool DeleteStages(IEnumerable<int> ids)
        {
            try
            {
                var stages = from stage in db.Stages where ids.Contains(stage.Id) select stage;

                db.Stages.DeleteAllOnSubmit(stages);

                db.SubmitChanges();

                return true;
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        #endregion

        #region Theme methods

        public IEnumerable<Theme> GetThemes(int stageId)
        {
            try
            {
                return db.Themes.Where(c => c.StageRef == stageId).OrderBy(c => c.SortOrder);
            }
            catch
            {
                return null;
            }
        }

        public Theme GetTheme(int id)
        {
            try
            {
                return db.Themes.Single(c => c.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public int? AddTheme(Theme theme)
        {
            try
            {
                theme.Name = GetCourse(theme.CourseRef).Name;
                theme.Created = DateTime.Now;
                theme.Updated = DateTime.Now;

                db.Themes.InsertOnSubmit(theme);
                db.SubmitChanges();

                theme.SortOrder = theme.Id;
                UpdateTheme(theme.Id, theme);

                return theme.Id;
            }
            catch
            {
                db = new DB();
                return null;
            }
        }

        public bool UpdateTheme(int id, Theme theme)
        {
            try
            {
                Theme oldTheme = db.Themes.Single(c => c.Id == id);

                oldTheme.Name = GetCourse(theme.CourseRef).Name;
                oldTheme.SortOrder = theme.SortOrder;
                oldTheme.CourseRef = theme.CourseRef;
                oldTheme.Updated = DateTime.Now;

                db.SubmitChanges();

                return true;
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        public bool ThemeUp(int themeId)
        {
            try
            {
                Theme theme = db.Themes.Single(c => c.Id == themeId);
                IList<Theme> themes = (from c in db.Themes
                                       where c.StageRef == theme.StageRef
                                       orderby c.SortOrder
                                       select c).ToList();

                int index = themes.IndexOf(theme);
                if (index == -1)
                {
                    return false;
                }
                else if (index == 0)
                {
                    return true;
                }
                else
                {
                    int temp = themes[index - 1].SortOrder;
                    themes[index - 1].SortOrder = theme.SortOrder;
                    theme.SortOrder = temp;

                    db.SubmitChanges();

                    return true;
                }
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        public bool ThemeDown(int themeId)
        {
            try
            {
                Theme theme = db.Themes.Single(c => c.Id == themeId);
                IList<Theme> themes = (from c in db.Themes
                                       where c.StageRef == theme.StageRef
                                       orderby c.SortOrder
                                       select c).ToList();

                int index = themes.IndexOf(theme);
                if (index == -1)
                {
                    return false;
                }
                else if (index == themes.Count - 1)
                {
                    return true;
                }
                else
                {
                    int temp = themes[index + 1].SortOrder;
                    themes[index + 1].SortOrder = theme.SortOrder;
                    theme.SortOrder = temp;

                    db.SubmitChanges();

                    return true;
                }
            }
            catch
            {
                db = new DB();
                return false;
            }
        }

        #endregion

        public Course GetCourse(int id)
        {
            GetCourseMessage message = new GetCourseMessage { Input = id };
            MvcContrib.Bus.Send(message);

            return message.Result.Data as Course;
        }

        public List<Course> GetCourses()
        {
            GetCoursesMessage message = new GetCoursesMessage { };
            MvcContrib.Bus.Send(message);

            return message.Result.Data as List<Course>;
        }

        #endregion
    }
}