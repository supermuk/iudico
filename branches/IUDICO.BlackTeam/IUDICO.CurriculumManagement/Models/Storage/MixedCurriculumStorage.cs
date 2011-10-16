using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using System.Data.Linq;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.CurriculumManagement.Models.Database;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class MixedCurriculumStorage : ICurriculumStorage
    {
        private readonly ILmsService _LmsService;

        public MixedCurriculumStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
            //RefreshState();
        }

        #region IStorageInterface Members

        #region External methods

        public void RefreshState()
        {
            //db = new DBDataContext(lmsService.GetDbConnectionString());
            //_Db = _LmsService.GetDbDataContext();
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

        public Curriculum GetCurriculum(int id)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Curriculums.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
            }
            //return GetDbDataContext().Curriculums.Single(item => item.Id == id && !item.IsDeleted);
        }

        public IEnumerable<Curriculum> GetCurriculums()
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Curriculums.Where(item => !item.IsDeleted);
            }
            //return GetDbDataContext().Curriculums.Where(item => !item.IsDeleted);
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
            }
        }

        public IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return GetCurriculumAssignmentsByGroupId(groupId).Select(item => item.Curriculum);
            }
        }

        public IEnumerable<Curriculum> GetCurriculumsWithThemesOwnedByUser(User user)
        {
            IEnumerable<int> courseIds = GetCoursesOwnedByUser(user)
                .Select(item => item.Id)
                .ToList();
            return GetCurriculums()
                .Where(item => GetThemesByCurriculumId(item.Id)
                             .Any(theme => courseIds.Contains(theme.CourseRef)));
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                curriculum.Created = DateTime.Now;
                curriculum.Updated = DateTime.Now;
                curriculum.IsDeleted = false;

                db.Curriculums.Add(curriculum);
                db.SaveChanges();

                return curriculum.Id;
            }
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var oldCurriculum = GetCurriculum(curriculum.Id);

            using (var db = new CurriculumManagementDBContext())
            {
                db.Curriculums.Attach(oldCurriculum);

                oldCurriculum.Name = curriculum.Name;
                oldCurriculum.Updated = DateTime.Now;

                db.SaveChanges();
            }
        }

        public void DeleteCurriculum(int id)
        {
            var curriculum = GetCurriculum(id);

            using (var db = new CurriculumManagementDBContext())
            {
                db.Curriculums.Attach(curriculum);

                //delete stages
                var stageIds = GetStages(id).Select(item => item.Id);
                DeleteStages(stageIds);

                //delete corresponding CurriculumAssignments
                var curriculumAssignmentIds = GetCurriculumAssignmnetsByCurriculumId(id).Select(item => item.Id);
                DeleteCurriculumAssignments(curriculumAssignmentIds);

                curriculum.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteCurriculum(id);
            }
        }

        #endregion

        #region Stage methods

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Stages.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted);
            }
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Stages.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
            }
        }

        public Stage GetStage(int id)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Stages.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
            }
        }

        public int AddStage(Stage stage)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                stage.Created = DateTime.Now;
                stage.Updated = DateTime.Now;
                stage.IsDeleted = false;

                db.Stages.Add(stage);
                db.SaveChanges();

                return stage.Id;
            }
        }

        public void UpdateStage(Stage stage)
        {
            Stage oldStage = GetStage(stage.Id);

            using (var db = new CurriculumManagementDBContext())
            {
                db.Stages.Attach(oldStage);

                oldStage.Name = stage.Name;
                oldStage.Updated = DateTime.Now;

                db.SaveChanges();
            }
        }

        public void DeleteStage(int id)
        {
            var stage = GetStage(id);

            using (var db = new CurriculumManagementDBContext())
            {
                db.Stages.Attach(stage);

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
                db.SaveChanges();
            }
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

        public Theme GetTheme(int id)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Themes.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
            }
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Themes.Where(item => ids.Contains(item.Id) && !item.IsDeleted).OrderBy(item => item.SortOrder);
            }
        }

        public IEnumerable<Theme> GetThemesByStageId(int stageId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Themes.Where(item => item.StageRef == stageId && !item.IsDeleted).OrderBy(item => item.SortOrder);
            }
        }

        public IEnumerable<Theme> GetThemesByCurriculumId(int curriculumId)
        {
            return GetStages(GetCurriculum(curriculumId).Id).SelectMany(item => GetThemesByStageId(item.Id));
        }

        public IEnumerable<Theme> GetThemesByGroupId(int groupId)
        {
            return GetCurriculumsByGroupId(groupId).SelectMany(item => GetThemesByCurriculumId(item.Id));
        }

        public IEnumerable<Theme> GetThemesByCourseId(int courseId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Themes.Where(item => item.CourseRef == courseId && !item.IsDeleted);
            }
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
                .Select(item => GetGroup(item.UserGroupRef));
        }

        public int AddTheme(Theme theme)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                theme.Created = DateTime.Now;
                theme.Updated = DateTime.Now;
                theme.IsDeleted = false;

                db.Themes.Add(theme);
                db.SaveChanges();

                theme.SortOrder = theme.Id;
                UpdateTheme(theme);
            }

            //if it is "Test" then add corresponding ThemeAssignments.
            if (theme.ThemeType.Id == (int)IUDICO.CurriculumManagement.Models.Enums.ThemeType.Test)
            {
                AddThemeAssignments(theme);
            }

            return theme.Id;
        }

        public void UpdateTheme(Theme theme)
        {
            var oldTheme = GetTheme(theme.Id);

            using (var db = new CurriculumManagementDBContext())
            {
                /*try
                {*/
                db.Themes.Attach(oldTheme);
                
                oldTheme.Name = theme.Name;
                oldTheme.SortOrder = theme.SortOrder;
                oldTheme.CourseRef = theme.CourseRef;
                oldTheme.Updated = DateTime.Now;

                //if ThemeType has changed then add or remove corresponding ThemeAssignments.
                if (oldTheme.ThemeTypeRef != theme.ThemeTypeRef)
                {
                    oldTheme.ThemeTypeRef = theme.ThemeTypeRef;
                    if (theme.ThemeType.Id == (int)IUDICO.CurriculumManagement.Models.Enums.ThemeType.Test)
                    {
                        AddThemeAssignments(theme);
                    }
                    else
                    {
                        DeleteThemeAssignments(theme);
                    }
                }

                db.SaveChanges();
                /*}
                catch (ChangeConflictException)
                {
                    foreach (ObjectChangeConflict conflict in db.ChangeConflicts)
                    {
                        conflict.Resolve(RefreshMode.KeepChanges);
                    }
                }*/
            }
        }

        public void DeleteTheme(int id)
        {
            var theme = GetTheme(id);

            using (var db = new CurriculumManagementDBContext())
            {
                db.Themes.Attach(theme);

                //if it is "Test" then delete corresponding ThemeAssignments.
                if (theme.ThemeType.Id == (int)IUDICO.CurriculumManagement.Models.Enums.ThemeType.Test)
                {
                    DeleteThemeAssignments(theme);
                }

                theme.IsDeleted = true;

                db.SaveChanges();
            }
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
            var theme = GetTheme(themeId);
            IList<Theme> themes = GetThemesByStageId(theme.StageRef).ToList();

            using (var db = new CurriculumManagementDBContext())
            {
                int index = themes.IndexOf(theme);
                if (index != -1 && index != 0)
                {
                    db.Themes.Attach(themes[index - 1]);

                    int temp = themes[index - 1].SortOrder;
                    themes[index - 1].SortOrder = theme.SortOrder;
                    theme.SortOrder = temp;
                }

                db.SaveChanges();
            }

            return theme;
        }

        public Theme ThemeDown(int themeId)
        {
            var theme = GetTheme(themeId);
            IList<Theme> themes = GetThemesByStageId(theme.StageRef).ToList();

            using (var db = new CurriculumManagementDBContext())
            {
                int index = themes.IndexOf(theme);
                if (index != -1 && index != themes.Count - 1)
                {
                    db.Themes.Attach(themes[index - 1]);

                    int temp = themes[index + 1].SortOrder;
                    themes[index + 1].SortOrder = theme.SortOrder;
                    theme.SortOrder = temp;
                }

                db.SaveChanges();
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

        public IEnumerable<ThemeType> GetThemeTypes()
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.ThemeTypes;
            }
        }

        #endregion

        #region CurriculumAssignment methods

        public CurriculumAssignment GetCurriculumAssignment(int curriculumAssignmentId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.CurriculumAssignments.SingleOrDefault(item => item.Id == curriculumAssignmentId && !item.IsDeleted);
            }
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignments()
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.CurriculumAssignments.Where(item => !item.IsDeleted);
            }
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignments(IEnumerable<int> ids)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.CurriculumAssignments.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
            }
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignmnetsByCurriculumId(int curriculumId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.CurriculumAssignments.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted);
            }
        }

        public IEnumerable<CurriculumAssignment> GetCurriculumAssignmentsByGroupId(int groupId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.CurriculumAssignments.Where(item => item.UserGroupRef == groupId && !item.IsDeleted);
            }
        }

        public int AddCurriculumAssignment(CurriculumAssignment curriculumAssignment)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                curriculumAssignment.IsDeleted = false;

                db.CurriculumAssignments.Add(curriculumAssignment);
                db.SaveChanges();
            }

            //add corresponding ThemeAssignments
            var themesInCurrentCurriculum = GetThemesByCurriculumId(curriculumAssignment.CurriculumRef)
                .Where(item => item.ThemeTypeRef == (int)IUDICO.CurriculumManagement.Models.Enums.ThemeType.Test);
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
            //var oldCurriculumAssignment = GetCurriculumAssignment(curriculumAssignment.Id);

            using (var db = new CurriculumManagementDBContext())
            {
                db.CurriculumAssignments.Attach(curriculumAssignment);
                curriculumAssignment.UserGroupRef = curriculumAssignment.UserGroupRef;

                db.SaveChanges();
            }
        }

        public void DeleteCurriculumAssignment(int curriculumAssignmentId)
        {
            var curriculumAssignment = GetCurriculumAssignment(curriculumAssignmentId);

            //delete corresponding CurriculumAssignmentTimelines
            var curriculumAssignmentTimelineIds = GetCurriculumAssignmentTimelines(curriculumAssignmentId).Select(item => item.Id);
            DeleteTimelines(curriculumAssignmentTimelineIds);

            //delete corresponding StageTimelines
            var stageTimelineIds = GetStageTimelinesByCurriculumAssignmentId(curriculumAssignmentId).Select(item => item.Id);
            DeleteTimelines(stageTimelineIds);

            //delete corresponding ThemeAssignments
            var themeAssignmentIds = GetThemeAssignmentsByCurriculumAssignmentId(curriculumAssignmentId).Select(item => item.Id);
            DeleteThemeAssignments(themeAssignmentIds);

            using (var db = new CurriculumManagementDBContext())
            {
                db.CurriculumAssignments.Attach(curriculumAssignment);

                curriculumAssignment.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public void DeleteCurriculumAssignments(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteCurriculumAssignment(id);
            }
        }

        #endregion

        #region ThemeAssignment methods

        public ThemeAssignment GetThemeAssignment(int themeAssignmentId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.ThemeAssignments.SingleOrDefault(item => item.Id == themeAssignmentId && !item.IsDeleted);
            }
        }

        public IEnumerable<ThemeAssignment> GetThemeAssignmentsByCurriculumAssignmentId(int curriculumAssignmentId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.ThemeAssignments.Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && !item.IsDeleted);
            }
        }

        public IEnumerable<ThemeAssignment> GetThemeAssignmentsByThemeId(int themeId)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.ThemeAssignments.Where(item => item.ThemeRef == themeId && !item.IsDeleted);
            }
        }

        public IEnumerable<ThemeAssignment> GetThemeAssignments(IEnumerable<int> ids)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.ThemeAssignments.Where(item => ids.Contains(item.Id) && !item.IsDeleted);
            }
        }

        public int AddThemeAssignment(ThemeAssignment themeAssignment)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                db.ThemeAssignments.Add(themeAssignment);
                db.SaveChanges();
            }

            return themeAssignment.Id;
        }

        public void UpdateThemeAssignment(ThemeAssignment themeAssignment)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                db.ThemeAssignments.Attach(themeAssignment);
                
                themeAssignment.MaxScore = themeAssignment.MaxScore;

                db.SaveChanges();
            }
        }

        public void DeleteThemeAssignments(IEnumerable<int> ids)
        {
            var themeAssignments = GetThemeAssignments(ids);

            using (var db = new CurriculumManagementDBContext())
            {
                foreach (ThemeAssignment item in themeAssignments)
                {
                    db.ThemeAssignments.Attach(item);
                    item.IsDeleted = true;
                }

                db.SaveChanges();
            }
        }

        #endregion

        #region Timeline methods

        private IEnumerable<Timeline> GetTimelines()
        {
            using (var db = new CurriculumManagementDBContext())
            {
                return db.Timelines.Where(item => !item.IsDeleted);
            }
        }

        public Timeline GetTimeline(int timelineId)
        {
            return GetTimelines().SingleOrDefault(item => item.Id == timelineId);
        }

        public IEnumerable<Timeline> GetCurriculumAssignmentTimelines(int curriculumAssignmentId)
        {
            return GetTimelines().Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && item.StageRef == null);
        }

        public IEnumerable<Timeline> GetStageTimelinesByCurriculumAssignmentId(int curriculumAssignmentId)
        {
            return GetTimelines().Where(item => item.CurriculumAssignmentRef == curriculumAssignmentId && item.StageRef != null);
        }

        public IEnumerable<Timeline> GetStageTimelinesByStageId(int stageId)
        {
            return GetTimelines().Where(item => item.StageRef == stageId && item.StageRef != null);
        }

        public IEnumerable<Timeline> GetStageTimelines(int stageId, int curriculumAssignmentId)
        {
            return GetTimelines().Where(item => item.StageRef == stageId && item.CurriculumAssignmentRef == curriculumAssignmentId
                && item.StageRef != null);
        }

        public IEnumerable<Timeline> GetTimelines(IEnumerable<int> timelineIds)
        {
            return GetTimelines().Where(item => timelineIds.Contains(item.Id));
        }

        public int AddTimeline(Timeline timeline)
        {
            timeline.IsDeleted = false;

            using (var db = new CurriculumManagementDBContext())
            {
                db.Timelines.Add(timeline);
                db.SaveChanges();
            }

            return timeline.Id;
        }

        public void UpdateTimeline(Timeline timeline)
        {
            using (var db = new CurriculumManagementDBContext())
            {
                db.Timelines.Attach(timeline);

                timeline.StartDate = timeline.StartDate;
                timeline.EndDate = timeline.EndDate;

                db.SaveChanges();
            }
        }

        public void DeleteTimeline(int timelineId)
        {
            var timeline = GetTimeline(timelineId);

            using (var db = new CurriculumManagementDBContext())
            {
                db.Timelines.Attach(timeline);

                timeline.IsDeleted = true;

                db.SaveChanges();
            }
        }

        public void DeleteTimelines(IEnumerable<int> timelineIds)
        {
            var timelines = GetTimelines(timelineIds);

            using (var db = new CurriculumManagementDBContext())
            {
                foreach (Timeline timeline in timelines)
                {
                    db.Timelines.Attach(timeline);
                    timeline.IsDeleted = true;
                }

                db.SaveChanges();
            }
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