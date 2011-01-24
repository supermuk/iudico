using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class MixedCurriculumStorage : ICurriculumStorage
    {
        private readonly ILmsService _LmsService;
        private DBDataContext _Db;

        public MixedCurriculumStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
            RefreshState();
        }

        #region IStorageInterface Members

        #region External methods

        public void RefreshState()
        {
            //db = new DBDataContext(lmsService.GetDbConnectionString());
            _Db = _LmsService.GetDbDataContext();
        }

        public IEnumerable<Course> GetCourses()
        {
            return _LmsService.FindService<ICourseService>().GetCourses();
        }

        public Course GetCourse(int id)
        {
            return _LmsService.FindService<ICourseService>().GetCourse(id);
        }

        public Group GetGroup(int id)
        {
            return _LmsService.FindService<IUserService>().GetGroup(id);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _LmsService.FindService<IUserService>().GetGroups();
        }

        #endregion

        #region Curriculum methods

        public Curriculum GetCurriculum(int id)
        {
            return _Db.Curriculums.Single(item => item.Id == id && !item.IsDeleted);
        }

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return _Db.Curriculums.Where(item => !item.IsDeleted);
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return _Db.Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
        }

        public IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId)
        {
            return GetCurriculumAssignmentsByGroupId(groupId).Select(item => item.Curriculum);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            curriculum.Created = DateTime.Now;
            curriculum.Updated = DateTime.Now;
            curriculum.IsDeleted = false;

            _Db.Curriculums.InsertOnSubmit(curriculum);
            _Db.SubmitChanges();

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var oldCurriculum = GetCurriculum(curriculum.Id);

            oldCurriculum.Name = curriculum.Name;
            oldCurriculum.Updated = DateTime.Now;

            _Db.SubmitChanges();
        }

        public void DeleteCurriculum(int id)
        {
            var curriculum = GetCurriculum(id);

            //delete stages
            var stageIds = GetStages(id).Select(item => item.Id);
            DeleteStages(stageIds);

            curriculum.IsDeleted = true;
            //db.Curriculums.DeleteOnSubmit(curriculum);
            _Db.SubmitChanges();
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
            _Db.SubmitChanges();
        }

        #endregion

        #region Stage methods

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            return _Db.Stages.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted);
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            return _Db.Stages.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
        }

        public Stage GetStage(int id)
        {
            return _Db.Stages.Single(item => item.Id == id && !item.IsDeleted);
        }

        public int AddStage(Stage stage)
        {
            stage.Created = DateTime.Now;
            stage.Updated = DateTime.Now;
            stage.IsDeleted = false;

            _Db.Stages.InsertOnSubmit(stage);
            _Db.SubmitChanges();

            return stage.Id;
        }

        public void UpdateStage(Stage stage)
        {
            Stage oldStage = GetStage(stage.Id);

            oldStage.Name = stage.Name;
            oldStage.Updated = DateTime.Now;

            _Db.SubmitChanges();
        }

        public void DeleteStage(int id)
        {
            var stage = GetStage(id);

            var themeIds = GetThemes(id).Select(item => item.Id);
            DeleteThemes(themeIds);

            stage.IsDeleted = true;
            //db.Stages.DeleteOnSubmit(stage);
            _Db.SubmitChanges();
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
            _Db.SubmitChanges();
        }

        #endregion

        #region Theme methods

        public Theme GetTheme(int id)
        {
            return _Db.Themes.Single(item => item.Id == id && !item.IsDeleted);
        }

        public IEnumerable<Theme> GetThemes(int stageId)
        {
            return _Db.Themes.Where(item => item.StageRef == stageId && !item.IsDeleted).OrderBy(item => item.SortOrder);
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            return _Db.Themes.Where(item => ids.Contains(item.Id) && !item.IsDeleted).OrderBy(item => item.SortOrder);
        }

        public IEnumerable<Theme> GetThemesByCurriculumId(int curriculumId)
        {
            return GetStages(GetCurriculum(curriculumId).Id).SelectMany(item => GetThemes(item.Id));
        }

        public IEnumerable<Theme> GetThemesByGroupId(int groupId)
        {
            return GetCurriculumsByGroupId(groupId).SelectMany(item => GetThemesByCurriculumId(item.Id));
        }

        //TODO:what the fuck??
        public int AddTheme(Theme theme, Course course)
        {
            theme.Name = course.Name;
            theme.Created = DateTime.Now;
            theme.Updated = DateTime.Now;
            theme.IsDeleted = false;

            _Db.Themes.InsertOnSubmit(theme);
            _Db.SubmitChanges();

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

            _Db.SubmitChanges();
        }

        public void DeleteTheme(int id)
        {
            var theme = GetTheme(id);

            theme.IsDeleted = true;
            //db.Themes.DeleteOnSubmit(theme);
            _Db.SubmitChanges();
        }

        public void DeleteThemes(IEnumerable<int> ids)
        {
            var themes = GetThemes(ids);

            foreach (Theme theme in themes)
            {
                theme.IsDeleted = true;
            }
            //db.Themes.DeleteAllOnSubmit(themes);
            _Db.SubmitChanges();
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

                _Db.SubmitChanges();
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

                _Db.SubmitChanges();
            }

            return theme;
        }

        #endregion

        #region ThemeType methods

        public IEnumerable<ThemeType> GetThemeTypes()
        {
            return _Db.ThemeTypes;
        }

        #endregion

        #region CurriculumAssignment methods

        public CurriculumAssignment GetCurriculumAssignment(int curriculumAssignmentId)
        {
            return _Db.CurriculumAssignments.Single(item => item.Id == curriculumAssignmentId && !item.IsDeleted);
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignmnets()
        {
            return _Db.CurriculumAssignments.Where(item => !item.IsDeleted);
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignments(IEnumerable<int> ids)
        {
            return _Db.CurriculumAssignments.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignmnetsByCurriculumId(int curriculumId)
        {
            return _Db.CurriculumAssignments.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted);
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignmentsByGroupId(int groupId)
        {
            return _Db.CurriculumAssignments.Where(item => item.UserGroupRef == groupId && !item.IsDeleted);
        }

        public int AddCurriculumAssignment(CurriculumAssignment curriculumAssignment)
        {
            curriculumAssignment.IsDeleted = false;

            var themesInCurrentCurriculum = GetThemesByCurriculumId(curriculumAssignment.CurriculumRef);

            _Db.CurriculumAssignments.InsertOnSubmit(curriculumAssignment);
            _Db.SubmitChanges();

            foreach (var theme in themesInCurrentCurriculum)
            {
                if (theme.ThemeTypeRef == 1)
                {
                    ThemeAssignment newThemeAssingment = new ThemeAssignment()
                    {
                        CurriculumAssignmentRef = curriculumAssignment.Id,
                        ThemeRef = theme.Id,
                        MaxScore = Constants.DefaultThemeMaxScore
                    };

                    AddThemeAssignment(newThemeAssingment);
                }
            }

            return curriculumAssignment.Id;
        }

        public void UpdateCurriculumAssignment(CurriculumAssignment curriculumAssignment)
        {
            var oldCurriculumAssignment = GetCurriculumAssignment(curriculumAssignment.Id);

            oldCurriculumAssignment.UserGroupRef = curriculumAssignment.UserGroupRef;

            _Db.SubmitChanges();
        }

        public void DeleteCurriculumAssignment(int curriculumAssignmentId)
        {
            var curriculumAssignment = GetCurriculumAssignment(curriculumAssignmentId);

            //delete timelines
            var timelineIds = GetCurriculumAssignmentTimelines(curriculumAssignmentId).Select(item => item.Id);
            DeleteTimelines(timelineIds);

            curriculumAssignment.IsDeleted = true;
            _Db.SubmitChanges();
        }

        public void DeleteCurriculumAssignments(IEnumerable<int> curriculumAssignmentIds)
        {
            foreach (int id in curriculumAssignmentIds)
            {
                DeleteCurriculumAssignment(id);
            }
        }

        #endregion

        #region ThemeAssignment methods

        public IEnumerable<ThemeAssignment> GetThemeAssignmentsByCurriculumAssignmentId(int curriculumAssignmentId)
        {
            return _Db.ThemeAssignments.Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId);
        }

        public int AddThemeAssignment(ThemeAssignment themeAssignment)
        {
            _Db.ThemeAssignments.InsertOnSubmit(themeAssignment);
            _Db.SubmitChanges();

            return themeAssignment.Id;
        }

        public ThemeAssignment GetThemeAssignment(int themeAssignmentId)
        {
            return _Db.ThemeAssignments.Single(item => item.Id == themeAssignmentId);
        }

        public void UpdateThemeAssignment(ThemeAssignment themeAssignment)
        {
            var oldThemeAssignment = GetThemeAssignment(themeAssignment.Id);

            oldThemeAssignment.MaxScore = themeAssignment.MaxScore;

            _Db.SubmitChanges();
        }

        #endregion

        #region Timeline methods

        public Timeline GetTimeline(int timelineId)
        {
            return _Db.Timelines.Single(item => item.Id == timelineId && !item.IsDeleted);
        }

        public IEnumerable<Timeline> GetCurriculumAssignmentTimelines(int curriculumAssignmentId)
        {
            return _Db.Timelines.Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && item.StageRef == null && !item.IsDeleted);
        }

        public IEnumerable<Timeline> GetStageTimelines(int curriculumAssignmentId)
        {
            return _Db.Timelines.Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && item.StageRef != null && !item.IsDeleted);
        }

        public IEnumerable<Timeline> GetTimelines(IEnumerable<int> timelineIds)
        {
            return _Db.Timelines.Where(item => timelineIds.Contains(item.Id) && !item.IsDeleted);
        }

        public int AddTimeline(Timeline timeline)
        {
            timeline.IsDeleted = false;

            _Db.Timelines.InsertOnSubmit(timeline);
            _Db.SubmitChanges();

            return timeline.Id;
        }

        public void UpdateTimeline(Timeline timeline)
        {
            var oldTimeline = GetTimeline(timeline.Id);

            oldTimeline.StartDate = timeline.StartDate;
            oldTimeline.EndDate = timeline.EndDate;
            oldTimeline.OperationRef = timeline.OperationRef;

            _Db.SubmitChanges();
        }

        public void DeleteTimeline(int timelineId)
        {

            var timeline = GetTimeline(timelineId);

            timeline.IsDeleted = true;

            _Db.SubmitChanges();
        }

        public void DeleteTimelines(IEnumerable<int> timelineIds)
        {
            var timelines = GetTimelines(timelineIds);

            foreach (Timeline timeline in timelines)
            {
                timeline.IsDeleted = true;
            }

            _Db.SubmitChanges();
        }

        #endregion

        #region Operation methods

        public IEnumerable<Operation> GetOperations()
        {
            return _Db.Operations;
        }

        #endregion

        #region Group methods

        public IEnumerable<Group> GetAssignedGroups(int curriculumId)
        {
            return GetCurriculumAssignmnetsByCurriculumId(curriculumId).Select(item => GetGroup(item.UserGroupRef));
        }

        public IEnumerable<Group> GetNotAssignedGroups(int curriculumId)
        {
            var assignedGroupIds = GetAssignedGroups(curriculumId).Select(item => item.Id);
            return GetGroups().Where(item => !assignedGroupIds.Contains(item.Id)).Select(item => item);
        }

        #endregion

        #endregion
    }
}