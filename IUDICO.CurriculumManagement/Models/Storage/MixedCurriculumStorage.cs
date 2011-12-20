using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using System.Data.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Notifications;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class MixedCurriculumStorage : ICurriculumStorage
    {
        private readonly ILmsService _LmsService;

        protected virtual IDataContext GetDbContext()
        {
            return new DBDataContext();
        }

        public MixedCurriculumStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
            //RefreshState();
        }

        #region IStorageInterface Members

        #region External methods

        public User GetCurrentUser()
        {
            return _LmsService.FindService<IUserService>().GetCurrentUser();
        }

        public IEnumerable<Course> GetCourses()
        {
            return _LmsService.FindService<ICourseService>().GetCourses(GetCurrentUser());
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

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            return _LmsService.FindService<IUserService>().GetGroupsByUser(user);
        }

        public IEnumerable<Course> GetCoursesOwnedByUser(User user)
        {
            return _LmsService.FindService<ICourseService>().GetCourses(user);
        }

        #endregion

        #region Curriculum methods

        private Curriculum GetCurriculum(IDataContext db, int id)
        {
            return db.Curriculums.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Curriculum GetCurriculum(int id)
        {
            return GetCurriculum(GetDbContext(), id);
        }

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return GetDbContext().Curriculums.Where(item => !item.IsDeleted).ToList();
        }

        public IEnumerable<Curriculum> GetCurriculums(User owner)
        {
            return GetDbContext().Curriculums.Where(item => !item.IsDeleted && item.Owner == owner.Username).ToList();
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            //return _Db.Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
            return GetDbContext().Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId)
        {
            return GetCurriculumAssignmentsByGroupId(groupId).Select(item => item.Curriculum).ToList();
        }

        //TODO:what the fuckin method?
        public IEnumerable<Curriculum> GetCurriculumsWithThemesOwnedByUser(User user)
        {
            IEnumerable<int> courseIds = GetCoursesOwnedByUser(user)
                .Select(item => item.Id)
                .ToList();
            return GetCurriculums(user) //?
                .Where(item => GetThemesByCurriculumId(item.Id)
                             .Any(theme => courseIds.Contains(theme.CourseRef ?? Constants.NoCourseId)))
                             .ToList();
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            var db = GetDbContext();

            curriculum.Created = DateTime.Now;
            curriculum.Updated = DateTime.Now;
            curriculum.IsDeleted = false;
            curriculum.IsValid = true;
            curriculum.Owner = GetCurrentUser().Username;

            db.Curriculums.InsertOnSubmit(curriculum);
            db.SubmitChanges();

            _LmsService.Inform(CurriculumNotifications.CurriculumCreate, curriculum);

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var db = GetDbContext();
            var oldCurriculum = GetCurriculum(db, curriculum.Id);
            var newCurriculum = GetCurriculum(db, curriculum.Id);

            newCurriculum.Name = curriculum.Name;
            newCurriculum.Updated = DateTime.Now;

            db.SubmitChanges();

            object[] data = new object[2];
            data[0] = oldCurriculum;
            data[1] = newCurriculum;

            _LmsService.Inform(CurriculumNotifications.CurriculumEdit, data);
        }

        public void DeleteCurriculum(int id)
        {
            var db = GetDbContext();
            var curriculum = GetCurriculum(db, id);

            //delete stages
            var stageIds = GetStages(id).Select(item => item.Id);
            DeleteStages(stageIds);

            //delete corresponding CurriculumAssignments
            var curriculumAssignmentIds = GetCurriculumAssignmnetsByCurriculumId(id).Select(item => item.Id);
            DeleteCurriculumAssignments(curriculumAssignmentIds);

            curriculum.IsDeleted = true;
            db.SubmitChanges();

            _LmsService.Inform(CurriculumNotifications.CurriculumDelete, id);
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteCurriculum(id);
            }
        }

        public void MakeCurriculumInvalid(int courseId)
        {
            var db = GetDbContext();
            var themeIds = GetThemesByCourseId(courseId).Select(item => item.Id);
            var curriculums = db.Curriculums.Where(item => themeIds.Contains(item.Id) && !item.IsDeleted);
            foreach (Curriculum curriculum in curriculums)
            {
                curriculum.IsValid = false;
            }
            db.SubmitChanges();
        }

        #endregion

        #region Stage methods

        private Stage GetStage(IDataContext db, int id)
        {
            return db.Stages.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Stage GetStage(int id)
        {
            return GetStage(GetDbContext(), id);
        }

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            return GetDbContext().Stages.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted).ToList();
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            return GetDbContext().Stages.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public int AddStage(Stage stage)
        {
            var db = GetDbContext();
            stage.Created = DateTime.Now;
            stage.Updated = DateTime.Now;
            stage.IsDeleted = false;

            db.Stages.InsertOnSubmit(stage);
            db.SubmitChanges();

            return stage.Id;
        }

        public void UpdateStage(Stage stage)
        {
            var db = GetDbContext();
            Stage oldStage = GetStage(db, stage.Id);

            oldStage.Name = stage.Name;
            oldStage.Updated = DateTime.Now;

            db.SubmitChanges();
        }

        public void DeleteStage(int id)
        {
            var db = GetDbContext();
            var stage = GetStage(db, id);

            //delete themes
            var themeIds = GetThemesByStageId(id).Select(item => item.Id);
            DeleteThemes(themeIds);

            //delete corresponding StageTimelines
            var stageTimelines = GetStageTimelinesByStageId(id);
            foreach (Timeline timeline in stageTimelines)
            {
                DeleteTimeline(timeline.Id);
            }

            stage.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteStages(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteStage(id);
            }
        }

        #endregion

        #region Theme methods

        private Theme GetTheme(IDataContext db, int id)
        {
            return db.Themes.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Theme GetTheme(int id)
        {
            return GetTheme(GetDbContext(), id);
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            return GetDbContext().Themes.Where(item => ids.Contains(item.Id) && !item.IsDeleted).OrderBy(item => item.SortOrder).ToList();
        }

        private IEnumerable<Theme> GetThemesByStageId(IDataContext db, int stageId)
        {
            return db.Themes.Where(item => item.StageRef == stageId && !item.IsDeleted).OrderBy(item => item.SortOrder).ToList();
        }

        public IEnumerable<Theme> GetThemesByStageId(int stageId)
        {
            return GetThemesByStageId(GetDbContext(), stageId);
        }

        public IEnumerable<Theme> GetThemesByCurriculumId(int curriculumId)
        {
            return GetStages(GetCurriculum(curriculumId).Id).SelectMany(item => GetThemesByStageId(item.Id)).ToList();
        }

        public IEnumerable<Theme> GetThemesByGroupId(int groupId)
        {
            return GetCurriculumsByGroupId(groupId).SelectMany(item => GetThemesByCurriculumId(item.Id)).ToList();
        }

        public IEnumerable<Theme> GetThemesOwnedByUser(User owner)
        {
            return GetCurriculums(owner).SelectMany(item => GetThemesByCurriculumId(item.Id)).ToList();
        }

        public IEnumerable<Theme> GetThemesByCourseId(int courseId)
        {
            return GetDbContext().Themes.Where(item => item.CourseRef == courseId && !item.IsDeleted).ToList();
        }

        public IEnumerable<ThemeDescription> GetThemesAvailableForUser(User user)
        {
            IEnumerable<Group> groups = GetGroupsByUser(user).ToList(); //get groups for user
            DateTime dateTime = DateTime.Now;
            List<ThemeDescription> result = new List<ThemeDescription>();

            var curriculumAssignments = groups.SelectMany(group => GetCurriculumAssignmentsByGroupId(group.Id)) //get curriculum assignments
                   .Where(curriculumAssignment => GetCurriculumAssignmentTimelines(curriculumAssignment.Id) //select those curriculum assignments,
                   .Any(timeline => dateTime.IsIn(timeline))); //for which specified date is in any of the curriculum assignment timelines

            foreach (CurriculumAssignment curriculumAssignment in curriculumAssignments)
            {
                //get stages
                foreach (Stage stage in GetStages(curriculumAssignment.CurriculumRef))
                {
                    var stageTimelines = GetStageTimelines(stage.Id, curriculumAssignment.Id)
                                        .ToList();
                    //select those stages, which doesn't have stage timelines or for which specified date is in any of the stage timelines
                    if (stageTimelines.Count() == 0 || stageTimelines.Any(timeline => dateTime.IsIn(timeline)))
                    {
                        //get themes
                        //result.AddRange(GetThemesByStageId(stage.Id));
                        foreach (Theme theme in GetThemesByStageId(stage.Id))
                        {
                            result.Add(new ThemeDescription()
                            {
                                Theme = theme,
                                Stage = stage,
                                Curriculum = stage.Curriculum,
                                Timelines = stageTimelines.Count() == 0 ?
                                    GetCurriculumAssignmentTimelines(curriculumAssignment.Id)
                                        .Where(timeline => dateTime.IsIn(timeline)).ToList() :
                                    stageTimelines
                                        .Where(timeline => dateTime.IsIn(timeline)).ToList()
                            });
                        }
                    }
                };
            }

            return result;
        }

        public IEnumerable<Group> GetGroupsAssignedToTheme(int themeId)
        {
            return GetTheme(themeId)
                .Stage
                .Curriculum
                .CurriculumAssignments
                .Select(item => GetGroup(item.UserGroupRef))
                .ToList();
        }

        public int AddTheme(Theme theme)
        {
            var db = GetDbContext();

            theme.Created = DateTime.Now;
            theme.Updated = DateTime.Now;
            theme.IsDeleted = false;

            db.Themes.InsertOnSubmit(theme);
            db.SubmitChanges();
            _LmsService.Inform(CurriculumNotifications.ThemeCreate, theme);

            theme.SortOrder = theme.Id;
            UpdateTheme(theme);

            //if it is "Test" then add corresponding ThemeAssignments.
            if (Converters.ConvertToThemeType(theme.ThemeType) == Enums.ThemeType.Test ||
                Converters.ConvertToThemeType(theme.ThemeType) == Enums.ThemeType.TestWithoutCourse)
            {
                AddThemeAssignments(theme);
            }



            return theme.Id;
        }

        public void UpdateTheme(Theme theme)
        {
            var db = GetDbContext();
            object[] data = new object[2];
            var oldTheme = GetTheme(db, theme.Id);
            var newTheme = oldTheme;

            newTheme.Name = theme.Name;
            newTheme.SortOrder = theme.SortOrder;
            newTheme.CourseRef = theme.CourseRef;
            newTheme.Updated = DateTime.Now;

            if (newTheme.ThemeTypeRef != theme.ThemeTypeRef)
            {
                newTheme.ThemeTypeRef = theme.ThemeTypeRef;
                if (Converters.ConvertToThemeType(theme.ThemeType) == Enums.ThemeType.Test ||
                    Converters.ConvertToThemeType(theme.ThemeType) == Enums.ThemeType.TestWithoutCourse)
                {
                    AddThemeAssignments(theme);
                }
                else
                {
                    DeleteThemeAssignments(theme);
                }
            }
            db.SubmitChanges();
            data[0] = oldTheme;
            data[1] = newTheme;

            _LmsService.Inform(CurriculumNotifications.ThemeEdit, data);
        }

        public void DeleteTheme(int id)
        {
            var db = GetDbContext();
            var theme = GetTheme(db, id);

            //if it is "Test" then delete corresponding ThemeAssignments.
            if (Converters.ConvertToThemeType(theme.ThemeType) == Enums.ThemeType.Test ||
                Converters.ConvertToThemeType(theme.ThemeType) == Enums.ThemeType.TestWithoutCourse)
            {
                DeleteThemeAssignments(theme);
            }
            theme.IsDeleted = true;
            db.SubmitChanges();

            _LmsService.Inform(CurriculumNotifications.ThemeDelete, theme);
        }

        public void DeleteThemes(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteTheme(id);
            }
        }

        public Theme ThemeUp(int themeId)
        {
            var db = GetDbContext();
            var theme = GetTheme(db, themeId);
            IList<Theme> themes = GetThemesByStageId(db, theme.StageRef).ToList();

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
            var db = GetDbContext();
            var theme = GetTheme(db, themeId);
            IList<Theme> themes = GetThemesByStageId(db, theme.StageRef).ToList();

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

        /// <summary>
        /// Adds theme assignments for theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        private void AddThemeAssignments(Theme theme)
        {
            var curriculumAssignments = GetCurriculumAssignmnetsByCurriculumId(theme.Stage.CurriculumRef);
            foreach (CurriculumAssignment curriculumAssignment in curriculumAssignments)
            {
                ThemeAssignment newThemeAssignment = new ThemeAssignment()
                {
                    CurriculumAssignmentRef = curriculumAssignment.Id,
                    ThemeRef = theme.Id,
                    MaxScore = Constants.DefaultThemeMaxScore
                };

                AddThemeAssignment(newThemeAssignment);
            }
        }

        /// <summary>
        /// Deletes theme assignments for theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        private void DeleteThemeAssignments(Theme theme)
        {
            var themeAssignmentIds = GetThemeAssignmentsByThemeId(theme.Id)
                .Select(item => item.Id);
            DeleteThemeAssignments(themeAssignmentIds);
        }

        #endregion

        #region ThemeType methods

        public ThemeType GetThemeType(int id)
        {
            return GetDbContext().ThemeTypes.SingleOrDefault(item => item.Id == id);
        }

        public IEnumerable<ThemeType> GetThemeTypes()
        {
            return GetDbContext().ThemeTypes.ToList();
        }

        #endregion

        #region CurriculumAssignment methods

        private CurriculumAssignment GetCurriculumAssignment(IDataContext db, int curriculumAssignmentId)
        {
            return db.CurriculumAssignments.SingleOrDefault(item => item.Id == curriculumAssignmentId && !item.IsDeleted);
        }

        public CurriculumAssignment GetCurriculumAssignment(int curriculumAssignmentId)
        {
            return GetCurriculumAssignment(GetDbContext(), curriculumAssignmentId);
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignments()
        {
            return GetDbContext().CurriculumAssignments.Where(item => !item.IsDeleted).ToList();
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignments(IEnumerable<int> ids)
        {
            return GetDbContext().CurriculumAssignments.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignmnetsByCurriculumId(int curriculumId)
        {
            return GetDbContext().CurriculumAssignments.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted).ToList();
        }

        private IEnumerable<CurriculumAssignment> GetCurriculumAssignmentsByGroupId(IDataContext db, int groupId)
        {
            return db.CurriculumAssignments.Where(item => item.UserGroupRef == groupId && !item.IsDeleted).ToList();
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignmentsByGroupId(int groupId)
        {
            return GetCurriculumAssignmentsByGroupId(GetDbContext(), groupId);
            //return GetDbContext().CurriculumAssignments.Where(item => item.UserGroupRef == groupId && !item.IsDeleted).ToList();
        }

        public int AddCurriculumAssignment(CurriculumAssignment curriculumAssignment)
        {
            var db = GetDbContext();
            curriculumAssignment.IsDeleted = false;
            curriculumAssignment.IsValid = true;

            db.CurriculumAssignments.InsertOnSubmit(curriculumAssignment);
            db.SubmitChanges();

            //add corresponding ThemeAssignments
            var themesInCurrentCurriculum = GetThemesByCurriculumId(curriculumAssignment.CurriculumRef)
                .Where(item => item.ThemeTypeRef == (int)Enums.ThemeType.Test ||
                    item.ThemeTypeRef == (int)Enums.ThemeType.TestWithoutCourse);
            foreach (var theme in themesInCurrentCurriculum)
            {
                ThemeAssignment newThemeAssingment = new ThemeAssignment()
                {
                    CurriculumAssignmentRef = curriculumAssignment.Id,
                    ThemeRef = theme.Id,
                    MaxScore = Constants.DefaultThemeMaxScore
                };

                AddThemeAssignment(newThemeAssingment);
            }

            return curriculumAssignment.Id;
        }

        public void UpdateCurriculumAssignment(CurriculumAssignment curriculumAssignment)
        {
            var db = GetDbContext();
            var oldCurriculumAssignment = GetCurriculumAssignment(db, curriculumAssignment.Id);

            oldCurriculumAssignment.UserGroupRef = curriculumAssignment.UserGroupRef;
            oldCurriculumAssignment.IsValid = true;

            db.SubmitChanges();
        }

        public void DeleteCurriculumAssignment(int curriculumAssignmentId)
        {
            var db = GetDbContext();
            var curriculumAssignment = GetCurriculumAssignment(db, curriculumAssignmentId);

            //delete corresponding CurriculumAssignmentTimelines
            var curriculumAssignmentTimelineIds = GetCurriculumAssignmentTimelines(curriculumAssignmentId).Select(item => item.Id);
            DeleteTimelines(curriculumAssignmentTimelineIds);

            //delete corresponding StageTimelines
            var stageTimelineIds = GetStageTimelinesByCurriculumAssignmentId(curriculumAssignmentId).Select(item => item.Id);
            DeleteTimelines(stageTimelineIds);

            //delete corresponding ThemeAssignments
            var themeAssignmentIds = GetThemeAssignmentsByCurriculumAssignmentId(curriculumAssignmentId).Select(item => item.Id);
            DeleteThemeAssignments(themeAssignmentIds);

            curriculumAssignment.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteCurriculumAssignments(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteCurriculumAssignment(id);
            }
        }

        public void MakeCurriculumAssignmentsInvalid(int groupId)
        {
            var db = GetDbContext();
            var curriculumAssignments = GetCurriculumAssignmentsByGroupId(db, groupId);
            foreach (var curriculumAssignment in curriculumAssignments)
            {
                curriculumAssignment.IsValid = false;
            }
            db.SubmitChanges();
        }

        #endregion

        #region ThemeAssignment methods

        private ThemeAssignment GetThemeAssignment(IDataContext db, int themeAssignmentId)
        {
            return db.ThemeAssignments.SingleOrDefault(item => item.Id == themeAssignmentId && !item.IsDeleted);
        }

        public ThemeAssignment GetThemeAssignment(int themeAssignmentId)
        {
            return GetThemeAssignment(GetDbContext(), themeAssignmentId);
        }

        public IEnumerable<ThemeAssignment> GetThemeAssignmentsByCurriculumAssignmentId(int curriculumAssignmentId)
        {
            return GetDbContext().ThemeAssignments.Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && !item.IsDeleted).ToList();
        }

        public IEnumerable<ThemeAssignment> GetThemeAssignmentsByThemeId(int themeId)
        {
            return GetDbContext().ThemeAssignments.Where(item => item.ThemeRef == themeId && !item.IsDeleted).ToList();
        }

        private IEnumerable<ThemeAssignment> GetThemeAssignments(IDataContext db, IEnumerable<int> ids)
        {
            return db.ThemeAssignments.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<ThemeAssignment> GetThemeAssignments(IEnumerable<int> ids)
        {
            return GetThemeAssignments(GetDbContext(), ids);
        }

        public int AddThemeAssignment(ThemeAssignment themeAssignment)
        {
            var db = GetDbContext();
            db.ThemeAssignments.InsertOnSubmit(themeAssignment);
            db.SubmitChanges();

            return themeAssignment.Id;
        }

        public void UpdateThemeAssignment(ThemeAssignment themeAssignment)
        {
            var db = GetDbContext();
            var oldThemeAssignment = GetThemeAssignment(db, themeAssignment.Id);

            oldThemeAssignment.MaxScore = themeAssignment.MaxScore;

            db.SubmitChanges();
        }

        public void DeleteThemeAssignments(IEnumerable<int> ids)
        {
            var db = GetDbContext();
            var themeAssignments = GetThemeAssignments(db, ids);

            foreach (ThemeAssignment item in themeAssignments)
            {
                item.IsDeleted = true;
            }

            db.SubmitChanges();
        }

        #endregion

        #region Timeline methods

        private IEnumerable<Timeline> GetTimelines(IDataContext db)
        {
            return db.Timelines.Where(item => !item.IsDeleted).ToList();
        }

        private Timeline GetTimeline(IDataContext db, int timelineId)
        {
            return GetTimelines(db).SingleOrDefault(item => item.Id == timelineId);
        }

        public Timeline GetTimeline(int timelineId)
        {
            return GetTimeline(GetDbContext(), timelineId);
        }

        public IEnumerable<Timeline> GetCurriculumAssignmentTimelines(int curriculumAssignmentId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && item.StageRef == null).ToList();
        }

        public IEnumerable<Timeline> GetStageTimelinesByCurriculumAssignmentId(int curriculumAssignmentId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && item.StageRef != null).ToList();
        }

        public IEnumerable<Timeline> GetStageTimelinesByStageId(int stageId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.StageRef == stageId && item.StageRef != null).ToList();
        }

        public IEnumerable<Timeline> GetStageTimelines(int stageId, int curriculumAssignmentId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.StageRef == stageId && item.CurriculumAssignmentRef == curriculumAssignmentId
                && item.StageRef != null).ToList();
        }

        private IEnumerable<Timeline> GetTimelines(IDataContext db, IEnumerable<int> timelineIds)
        {
            return GetTimelines(db).Where(item => timelineIds.Contains(item.Id)).ToList();
        }

        public IEnumerable<Timeline> GetTimelines(IEnumerable<int> timelineIds)
        {
            return GetTimelines(GetDbContext(), timelineIds);
        }

        public int AddTimeline(Timeline timeline)
        {
            var db = GetDbContext();
            timeline.IsDeleted = false;

            db.Timelines.InsertOnSubmit(timeline);
            db.SubmitChanges();

            return timeline.Id;
        }

        public void UpdateTimeline(Timeline timeline)
        {
            var db = GetDbContext();
            var oldTimeline = GetTimeline(db, timeline.Id);

            oldTimeline.StartDate = timeline.StartDate;
            oldTimeline.EndDate = timeline.EndDate;

            db.SubmitChanges();
        }

        public void DeleteTimeline(int timelineId)
        {
            var db = GetDbContext();
            var timeline = GetTimeline(db, timelineId);

            timeline.IsDeleted = true;

            db.SubmitChanges();
        }

        public void DeleteTimelines(IEnumerable<int> timelineIds)
        {
            var db = GetDbContext();
            var timelines = GetTimelines(db, timelineIds);

            foreach (Timeline timeline in timelines)
            {
                timeline.IsDeleted = true;
            }

            db.SubmitChanges();
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

        public IEnumerable<Group> GetNotAssignedGroupsWithCurrentGroup(int curriculumId, int currentGroupId)
        {
            IEnumerable<Group> groups = GetNotAssignedGroups(curriculumId);

            //add current group
            List<Group> assignedGroup = new List<Group>();
            assignedGroup.Add(GetGroup(currentGroupId));
            groups = groups.Concat(assignedGroup);

            return groups;
        }

        #endregion

        #endregion
    }
}