using System;
using System.Collections.Generic;
using NUnit.Framework;
using IUDICO.Common.Models.Services;
using System.Linq;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    [TestFixture]
    public class CurriculumTests
    {
        protected CurriculumManagementTests _Tests = CurriculumManagementTests.GetInstance();
        protected ICurriculumStorage _Storage
        {
            get
            {
                return _Tests.Storage;
            }
        }
        protected List<Curriculum> CreateDefaultData()
        {
            var curriculums = new List<Curriculum>()
            {
                new Curriculum() { Name = "Curriculum1" },
                new Curriculum() { Name = "Curriculum2" },
                new Curriculum() { Name = "Curriculum3" },
                new Curriculum() { Name = "Curriculum4" }
            };
            return curriculums;
        }

        [SetUp]
        public void InitializeTest()
        {
            _Tests.ClearTables();
        }


        #region CurriculumMethodsTests
        [Test]
        public void AddCurriculum()
        {
            List<Curriculum> curriculums = CreateDefaultData();
            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();
            curriculums.Select((item, index) => index).ToList()
                .ForEach(index => AdvAssert.AreEqual(curriculums[index], _Storage.GetCurriculum(ids[index])));
            try
            {
                _Storage.AddCurriculum(new Curriculum());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddCurriculum(new Curriculum { });
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetCurriculum()
        {
            List<Curriculum> curriculums = CreateDefaultData();
            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();
            curriculums.Select((item, i) => i).ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));
            #region WhyDoesItWork
            Curriculum cur = _Storage.GetCurriculum(0);
            Assert.AreEqual(null, cur);
            Curriculum curriculumWithExistesId = new Curriculum { Name = "ExistedCurriculum", Id = ids[0] };
            _Storage.AddCurriculum(curriculumWithExistesId);
            _Storage.GetCurriculum(ids[0]);
            #endregion
            try
            {
                _Storage.GetCurriculum(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                curriculumWithExistesId = new Curriculum { Name = "ExistedCurriculum", Id = ids[0] };
                _Storage.AddCurriculum(curriculumWithExistesId);
                _Storage.GetCurriculum(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetCurriculums()
        {
            User user = new User { Id = Guid.NewGuid(), Username = "user1" };
            List<Curriculum> curriculums = CreateDefaultData();
            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();
            _Storage.GetCurriculum(ids[3]).Owner = user.Username;
            //Tests GetCurriculums(IEnumerable<int> ids)
            Assert.AreEqual(curriculums, _Storage.GetCurriculums(ids));
            //Tests GetCurriculums()
            Assert.AreEqual(curriculums, _Storage.GetCurriculums());
            //Tests GetCurriculums(User owner)
            AdvAssert.AreEqual(curriculums[3], _Storage.GetCurriculums(user).First());
            List<int> empty = new List<int>();
            try
            {
                _Storage.GetCurriculums(empty);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetCurriculumsByGroupId()
        {
            List<Curriculum> curriculums = CreateDefaultData();
            curriculums.ForEach(item => _Storage.AddCurriculum(item));
            Group group = new Group { Id = 1, Name = "Group1" };
            var curriculumAssignments = curriculums.Select(item => new CurriculumAssignment { Curriculum = item, UserGroupRef = group.Id })
                .ToList();
            curriculumAssignments.ForEach(i => _Storage.AddCurriculumAssignment(i));
            Assert.AreEqual(curriculums, _Storage.GetCurriculumsByGroupId(group.Id).ToList());
        }
        [Test]
        public void UpdateCurriculum()
        {
            Curriculum curriculum = new Curriculum { Id = 1, Name = "Curriculum1" };
            _Storage.AddCurriculum(curriculum);
            curriculum.Name = "UpdatedCurriculum";
            _Storage.UpdateCurriculum(curriculum);
            var actualCurriculum = _Storage.GetCurriculum(curriculum.Id);
            AdvAssert.AreEqual(curriculum, actualCurriculum);
            curriculum.Name = "NotUpdatedCurriculum";
            actualCurriculum = _Storage.GetCurriculum(curriculum.Id);
            Assert.AreNotEqual(curriculum.Name, actualCurriculum.Name);
            try
            {
                _Storage.UpdateCurriculum(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteCurriculium()
        {
            List<Curriculum> curriculums = CreateDefaultData();
            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();
            _Storage.DeleteCurriculum(ids[0]);
            Assert.AreEqual(null, _Storage.GetCurriculum(ids[0]));
            Assert.AreNotEqual(null, _Storage.GetCurriculum(ids[1]));
            ids.RemoveAt(0);
            _Storage.DeleteCurriculums(ids);
            try
            {
                ids.ForEach(i => _Storage.GetCurriculum(i));
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteCurriculum(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteCurriculum(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void MakeCurriculumInvalid()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum1" };
            var id = _Storage.AddCurriculum(curriculum);
            Stage stage = new Stage() { Curriculum = curriculum, Name = "Stage1" };
            _Storage.AddStage(stage);
            Theme theme = new Theme() { Name = "Theme1", Stage = stage, ThemeType = _Storage.GetThemeType(1), CourseRef = 1 };
            _Storage.AddTheme(theme);
            _Storage.MakeCurriculumInvalid(id);
            Assert.AreEqual(false, _Storage.GetCurriculum(id).IsValid);
        }
        #endregion
        #region StageMethodsTests
        [Test]
        public void AddStage()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item }).ToList();
            var ids = stages.Select(item => _Storage.AddStage(item)).ToList();
            ids.Select((item, i) => i).ToList().ForEach(item => AdvAssert.AreEqual(stages[item], _Storage.GetStage(ids[item])));
            try
            {
                _Storage.AddStage(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddStage(new Stage { });
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetStage()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item }).ToList();
            var ids = stages.Select(item => _Storage.AddStage(item)).ToList();
            ids.Select((item, i) => i).ToList().ForEach(item => AdvAssert.AreEqual(stages[item], _Storage.GetStage(ids[item])));
            try
            {
                _Storage.GetStage(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddStage(new Stage { Name = "ExistedStage", Curriculum = curriculums[0], Id = ids[0] });
                _Storage.GetStage(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetStages()
        {
            var curriculums = CreateDefaultData();
            var curIds = curriculums.Select(item => _Storage.AddCurriculum(item));
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item }).ToList();
            var ids = stages.Select(item => _Storage.AddStage(item)).ToList();
            AdvAssert.AreEqual(stages.ToArray(), _Storage.GetStages(ids).ToArray());
            AdvAssert.AreEqual(stages[0], _Storage.GetStages(curriculums[0].Id).First());
            try
            {
                ids.Clear();
                _Storage.GetStages(ids);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteCurriculum(curriculums[0].Id);
                _Storage.GetStages(curriculums[0].Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void UpdateStage()
        {
            Curriculum curric = new Curriculum() { Name = "Curriculum1", Id = 1 };
            Stage stage = new Stage { Name = "Stage1", Curriculum = curric, Id = 1 };
            _Storage.AddStage(stage);
            stage.Name = "ChangedName";
            _Storage.UpdateStage(stage);
            AdvAssert.AreEqual(stage, _Storage.GetStage(1));
            try
            {
                _Storage.UpdateStage(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            stage.Name = "NotUpdatedChange";
            Assert.AreNotEqual(stage.Name, _Storage.GetStage(1).Name);
        }
        [Test]
        public void DeleteStage()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item }).ToList();
            var ids = stages.Select(item => _Storage.AddStage(item)).ToList();
            _Storage.DeleteStage(ids[0]);
            Assert.AreEqual(null, _Storage.GetStage(ids[0]));
            try
            {
                _Storage.DeleteStage(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteStages()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item }).ToList();
            var ids = stages.Select(item => _Storage.AddStage(item)).ToList();
            Stage notDeleted = new Stage { Name = "NotDeletedStage", Curriculum = curriculums[0] };
            var id = _Storage.AddStage(notDeleted);
            _Storage.DeleteStages(ids);
            Assert.AreEqual(0, _Storage.GetStages(ids).ToArray().Count());
            AdvAssert.AreEqual(notDeleted, _Storage.GetStage(id));
            try
            {
                _Storage.DeleteStages(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteStages(ids);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteStageIfCurriculumIsDeleted()
        {
            Curriculum curriculum = new Curriculum { Name = "Curriculum" };
            var currId = _Storage.AddCurriculum(curriculum);
            Stage stage = new Stage { Name = "Stage", Curriculum = curriculum };
            var stageId = _Storage.AddStage(stage);
            _Storage.DeleteCurriculum(currId);
            Assert.AreEqual(null, _Storage.GetStage(stageId));
        }
        #endregion
        #region ThemeMethodsTests
        [Test]
        public void AddTheme()
        {
            Curriculum cur = new Curriculum() { Name = "Curriculum" };
            _Storage.AddCurriculum(cur);
            CurriculumAssignment as1 = new CurriculumAssignment() { Curriculum = cur, UserGroupRef = 1 };
            _Storage.AddCurriculumAssignment(as1);
            Stage st = new Stage() { Name = "Stage", Curriculum = cur };
            _Storage.AddStage(st);
            Theme theme = new Theme() { Name = "Theme", Stage = st, ThemeType = _Storage.GetThemeType(1) };
            int id = _Storage.AddTheme(theme);
            AdvAssert.AreEqual(theme, _Storage.GetTheme(id));
            Assert.AreEqual(1, _Storage.GetThemeAssignmentsByThemeId(id).Count());
            try
            {
                Theme theme1 = new Theme { Name = "Theme", Stage = st, ThemeType = _Storage.GetThemeType(1) };
                _Storage.AddTheme(theme);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddTheme(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetTheme()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group group = userService.GetGroup(1);

            var curriculums = CreateDefaultData();

            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item }).ToList();
            var themes = stages.Select(item => new Theme { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) }).ToList();
            var ids = themes.Select(item => _Storage.AddTheme(item)).ToList();
            themes.Select((item, i) => i).
                ToList().ForEach(item => AdvAssert.AreEqual(themes[item], _Storage.GetTheme(ids[item])));
            try
            {
                Theme existed = new Theme { Name = "Theme", Stage = stages[0], ThemeType = _Storage.GetThemeType(1), Id = ids[0] };
                _Storage.AddTheme(existed);
                _Storage.GetTheme(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.GetTheme(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetThemes()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item })
                .ToList();
            var themes = stages.Select(item => new Theme { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) })
                .ToList();
            var ids = themes.Select(item => _Storage.AddTheme(item))
                .ToList();
            AdvAssert.AreEqual(themes.ToArray(), _Storage.GetThemes(ids).ToArray());
            try
            {
                _Storage.GetThemes(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetThemesByStageId()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item })
                .ToList();
            var themes = stages.Select(item => new Theme { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) })
                .ToList();
            themes.ForEach(item => _Storage.AddTheme(item));
            AdvAssert.AreEqual(themes[0], _Storage.GetThemesByStageId(themes[0].Stage.Id).First());
            try
            {
                _Storage.DeleteTheme(themes[0].Id);
                _Storage.GetThemesByStageId(themes[0].Stage.Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetThemesByCurriculumId()
        {
            var curriculums = CreateDefaultData();
            curriculums.ForEach(item => _Storage.AddCurriculum(item));
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item })
                .ToList();
            stages.ForEach(item => _Storage.AddStage(item));
            var themes = stages.Select(item => new Theme { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) })
                .ToList();
            themes.Add(new Theme() { Name = "Theme", Stage = stages[0], ThemeType = _Storage.GetThemeType(1) });
            themes.ForEach(item => _Storage.AddTheme(item));
            List<Theme> expected = new List<Theme>() { themes[0], themes[themes.Count - 1] };
            AdvAssert.AreEqual(expected, _Storage.GetThemesByCurriculumId(curriculums[0].Id)
                .ToList());
            try
            {
                _Storage.DeleteCurriculum(curriculums[0].Id);
                _Storage.GetThemesByCurriculumId(curriculums[0].Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetThemesByGroupId()
        {
            List<Curriculum> curriculums = CreateDefaultData();
            curriculums.ForEach(item => _Storage.AddCurriculum(item));

            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group group = userService.GetGroup(1);

            var curriculumAssignments = curriculums.Select(item => new CurriculumAssignment { Curriculum = item, UserGroupRef = group.Id })
                .ToList();
            curriculumAssignments.ForEach(i => _Storage.AddCurriculumAssignment(i));

            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item })
                .ToList();
            stages.ForEach(item => _Storage.AddStage(item));

            var themes = stages.Select(item => new Theme { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) })
                .ToList();
            themes.ForEach(item => _Storage.AddTheme(item));

            AdvAssert.AreEqual(themes, _Storage.GetThemesByGroupId(group.Id).ToList());
        }
        [Test]
        public void GetThemesByCourseId()
        {
            Curriculum cur = new Curriculum() { Name = "Curriculum", Id = 1 };
            _Storage.AddCurriculum(cur);

            Stage stage = new Stage() { Name = "Stage", Curriculum = cur, Id = 1 };
            _Storage.AddStage(stage);

            Course course = new Course() { Name = "Course", Id = 1 };
            Theme theme = new Theme() { Name = "Theme", Stage = stage, ThemeType = _Storage.GetThemeType(1), Id = 1, CourseRef = course.Id };
            Theme theme1 = new Theme() { Name = "Theme1", Stage = stage, ThemeType = _Storage.GetThemeType(1), Id = 2, CourseRef = course.Id };
            _Storage.AddTheme(theme);
            AdvAssert.AreEqual(theme, _Storage.GetThemesByCourseId(course.Id).First());
            _Storage.AddTheme(theme1);
            List<Theme> expected = new List<Theme>() { theme, theme1 };
            AdvAssert.AreEqual(expected, _Storage.GetThemesByCourseId(course.Id).ToList());
            try
            {
                _Storage.DeleteTheme(theme.Id);
                _Storage.DeleteTheme(theme1.Id);
                _Storage.GetThemesByCourseId(course.Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetGroupsAssignedToTheme()
        {
            Curriculum cur = new Curriculum() { Name = "Curriculum" };
            Curriculum cur1 = new Curriculum() { Name = "Curriculum1" };

            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr1 = userService.GetGroup(2);
            Group gr2 = userService.GetGroup(1);

            _Storage.AddCurriculum(cur);
            _Storage.AddCurriculum(cur1);

            CurriculumAssignment ass = new CurriculumAssignment() { Curriculum = cur, UserGroupRef = gr1.Id, Id = 1 };
            CurriculumAssignment ass1 = new CurriculumAssignment() { Curriculum = cur1, UserGroupRef = gr2.Id, Id = 2 };
            _Storage.AddCurriculumAssignment(ass);
            _Storage.AddCurriculumAssignment(ass1);

            Stage stage = new Stage() { Name = "Stage", Curriculum = cur };
            Stage stage1 = new Stage() { Name = "Stage1", Curriculum = cur1 };
            _Storage.AddStage(stage);
            _Storage.AddStage(stage1);

            Theme theme = new Theme() { Name = "Theme", Stage = stage, ThemeType = _Storage.GetThemeType(1) };
            Theme theme1 = new Theme() { Name = "Theme1", Stage = stage1, ThemeType = _Storage.GetThemeType(1) };
            var id = _Storage.AddTheme(theme);
            var id1 = _Storage.AddTheme(theme1);

            Assert.AreEqual(gr1.Id, _Storage.GetGroupsAssignedToTheme(id).First().Id);
            Assert.AreEqual(gr2.Id, _Storage.GetGroupsAssignedToTheme(id1).First().Id);
            _Storage.DeleteTheme(id1);
            try
            {
                _Storage.GetGroupsAssignedToTheme(id1);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteCurriculumAssignment(1);
                _Storage.GetGroupsAssignedToTheme(id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetThemesAvailableForUser()
        {
            Curriculum curr = new Curriculum() { Name = "Curriculum" };
            Curriculum curr1 = new Curriculum() { Name = "Curriculum1" };
            _Storage.AddCurriculum(curr);
            _Storage.AddCurriculum(curr1);

            DateTime dtStart = new DateTime(2011, 11, 11, 0, 0, 0);
            DateTime dtIn = new DateTime(2040, 11, 11, 0, 0, 0);
            DateTime dtOf = new DateTime(2011, 11, 12, 0, 0, 0);
            CurriculumAssignment as1 = new CurriculumAssignment() { Curriculum = curr, UserGroupRef = 1 };
            CurriculumAssignment as2 = new CurriculumAssignment() { Curriculum = curr1, UserGroupRef = 1 };
            _Storage.AddCurriculumAssignment(as1);
            _Storage.AddCurriculumAssignment(as2);

            Timeline tml = new Timeline() { CurriculumAssignment = as1, StartDate = dtStart, EndDate = dtIn };
            Timeline tml1 = new Timeline() { CurriculumAssignment = as2, StartDate = dtStart, EndDate = dtOf };
            _Storage.AddTimeline(tml);
            _Storage.AddTimeline(tml1);

            Stage st = new Stage() { Name = "Stage1", Curriculum = curr };
            Stage st1 = new Stage() { Name = "Stage2", Curriculum = curr1 };
            _Storage.AddStage(st);
            _Storage.AddStage(st1);

            Theme th1 = new Theme() { Name = "Theme1", Stage = st, ThemeType = _Storage.GetThemeType(1) };
            Theme th2 = new Theme() { Name = "Theme2", Stage = st1, ThemeType = _Storage.GetThemeType(1) };
            _Storage.AddTheme(th1);
            _Storage.AddTheme(th2);

            List<ThemeDescription> result = new List<ThemeDescription> { new ThemeDescription() { Theme = th1, Stage = st, Curriculum = curr,
                Timelines = new List<Timeline>() { tml } } };
            IUserService serv = _Tests.LmsService.FindService<IUserService>();
            User us = serv.GetUsers().First();
            AdvAssert.AreEqual(result, _Storage.GetThemesAvailableForUser(us));

            Timeline tml2 = new Timeline() { StageRef = st.Id, CurriculumAssignment = as1, StartDate = dtStart, EndDate = new DateTime(2011, 12, 9, 0, 0, 0) };
            _Storage.AddTimeline(tml2);
            result.Clear();
            AdvAssert.AreEqual(result, _Storage.GetThemesAvailableForUser(us));
            try
            {
                User notExistedUser = new User() { Id = Guid.NewGuid(), Username = "mad", Email = "none@gmail.com", Password = "" };
                _Storage.GetThemesAvailableForUser(notExistedUser);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void UpdateTheme()
        {
            Curriculum cur = new Curriculum() { Name = "Curriculum" };
            _Storage.AddCurriculum(cur);

            CurriculumAssignment as1 = new CurriculumAssignment() { Curriculum = cur, UserGroupRef = 1 };
            _Storage.AddCurriculumAssignment(as1);

            Stage st = new Stage() { Name = "Stage", Curriculum = cur };
            _Storage.AddStage(st);

            Theme theme = new Theme() { Name = "Theme", Stage = st, ThemeType = _Storage.GetThemeType(1) };
            int id = _Storage.AddTheme(theme);
            theme.Name = "UpdatedName";
            _Storage.UpdateTheme(theme);
            AdvAssert.AreEqual(theme, _Storage.GetTheme(id));

            theme.ThemeType = _Storage.GetThemeType(2);
            _Storage.UpdateTheme(theme);
            Assert.AreEqual(0, _Storage.GetThemeAssignmentsByThemeId(id).Count());

            theme.ThemeType = _Storage.GetThemeType(1);
            _Storage.UpdateTheme(theme);
            Assert.AreEqual(1, _Storage.GetThemeAssignmentsByThemeId(id).Count());

            theme.Name = "NotUpdatedName";
            Assert.AreNotEqual(theme.Name, _Storage.GetTheme(id).Name);
            try
            {
                _Storage.UpdateTheme(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteTheme()
        {
            Curriculum cur = new Curriculum() { Name = "Curriculum" };
            _Storage.AddCurriculum(cur);
            CurriculumAssignment as1 = new CurriculumAssignment() { Curriculum = cur, UserGroupRef = 1 };
            _Storage.AddCurriculumAssignment(as1);
            Stage st = new Stage() { Name = "Stage", Curriculum = cur };
            _Storage.AddStage(st);
            Theme theme = new Theme() { Name = "Theme", Stage = st, ThemeType = _Storage.GetThemeType(1) };
            int id = _Storage.AddTheme(theme);
            _Storage.DeleteTheme(id);
            Assert.AreEqual(null, _Storage.GetTheme(id));
            Assert.AreEqual(0, _Storage.GetThemeAssignmentsByThemeId(id).Count());
            try
            {
                _Storage.DeleteTheme(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteThemes()
        {
            List<Curriculum> cur = CreateDefaultData();
            cur.ForEach(item => _Storage.AddCurriculum(item));

            List<Stage> stages = cur.Select(item => new Stage() { Name = "Stage", Curriculum = item })
                .ToList();
            stages.ForEach(item => _Storage.AddStage(item));

            List<Theme> themes = stages.Select(item => new Theme() { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) })
                .ToList();
            var ids = themes.Select(item => _Storage.AddTheme(item));

            Theme last = new Theme() { Name = "LastTheme", Stage = stages[0], ThemeType = _Storage.GetThemeType(1) };
            var id = _Storage.AddTheme(last);

            _Storage.DeleteThemes(ids);
            AdvAssert.AreEqual(last, _Storage.GetTheme(id));
            try
            {
                _Storage.GetThemes(ids);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void ThemeUp()
        {
            List<Curriculum> cur = CreateDefaultData();
            cur.ForEach(item => _Storage.AddCurriculum(item));

            List<Stage> stages = cur.Select(item => new Stage() { Name = "Stage", Curriculum = item })
                .ToList();
            stages.ForEach(item => _Storage.AddStage(item));

            List<Theme> themes = stages.Select(item => new Theme() { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) })
                .ToList();
            Theme theme = new Theme() { Name = "Theme1", Stage = stages[0], ThemeType = _Storage.GetThemeType(1) };
            var ids = themes.Select(item => _Storage.AddTheme(item))
                .ToList();
            var id = _Storage.AddTheme(theme);
            theme = _Storage.GetThemesByStageId(theme.Stage.Id).ToList()[1];

            _Storage.ThemeUp(id);
            AdvAssert.AreEqual(theme, _Storage.GetThemesByStageId(theme.Stage.Id).ToList()[0]);

            _Storage.ThemeUp(id);
            AdvAssert.AreEqual(theme, _Storage.GetThemesByStageId(theme.Stage.Id).ToList()[0]);
            try
            {
                _Storage.ThemeUp(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void ThemeDown()
        {
            List<Curriculum> cur = CreateDefaultData();
            cur.ForEach(item => _Storage.AddCurriculum(item));

            List<Stage> stages = cur.Select(item => new Stage() { Name = "Stage", Curriculum = item })
                .ToList();
            stages.ForEach(item => _Storage.AddStage(item));

            List<Theme> themes = stages.Select(item => new Theme() { Name = "Theme", Stage = item, ThemeType = _Storage.GetThemeType(1) })
                .ToList();
            Theme theme = new Theme() { Name = "Theme1", Stage = stages[0], ThemeType = _Storage.GetThemeType(1) };
            var ids = themes.Select(item => _Storage.AddTheme(item))
                .ToList();
            var id = _Storage.AddTheme(theme);

            _Storage.ThemeUp(id);
            theme = _Storage.GetThemesByStageId(theme.Stage.Id).ToList()[0];

            _Storage.ThemeDown(id);
            AdvAssert.AreEqual(theme, _Storage.GetThemesByStageId(theme.Stage.Id).ToList()[1]);
            try
            {
                _Storage.ThemeDown(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeletingThemeWhenStageDeleted()
        {
            Curriculum cur = new Curriculum() { Name = "Curriculum" };
            _Storage.AddCurriculum(cur);
            CurriculumAssignment as1 = new CurriculumAssignment() { Curriculum = cur, UserGroupRef = 1 };
            _Storage.AddCurriculumAssignment(as1);
            Stage st = new Stage() { Name = "Stage", Curriculum = cur };
            var stageId = _Storage.AddStage(st);
            Theme theme = new Theme() { Name = "Theme", Stage = st, ThemeType = _Storage.GetThemeType(1) };
            int id = _Storage.AddTheme(theme);
            _Storage.DeleteStage(stageId);
            Assert.AreEqual(null, _Storage.GetTheme(id));
        }
        #endregion
        #region TimelineMethods
        [Test]
        public void AddTimeline()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select(item => new CurriculumAssignment { Curriculum = item }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item }).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.AddTimeline(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.AddTimeline(new Timeline { });
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTimeline()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(timelines[i].Id)));

            try
            {
                _Storage.GetTimeline(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetCurriculumAssignmentTimelines()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item, Id = 1 }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetCurriculumAssignmentTimelines(timelines[i].CurriculumAssignment.Id).ToList()[i]));

            try
            {
                _Storage.GetCurriculumAssignmentTimelines(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetStageTimelinesByCurriculumAssignmentId()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item, Id = 1 }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item, StageRef = 1 }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetStageTimelinesByCurriculumAssignmentId(timelines[i].CurriculumAssignment.Id).ToList()[i]));

            try
            {
                _Storage.GetStageTimelinesByCurriculumAssignmentId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetStageTimelinesByStageId()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item, StageRef = 1 }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetStageTimelinesByStageId(timelines[i].StageRef.Value).ToList()[i]));

            try
            {
                _Storage.GetStageTimelinesByStageId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetStageTimelines()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item, Id = 1 }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item, StageRef = 1 }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetStageTimelines(timelines[i].StageRef.Value, timelines[i].CurriculumAssignment.Id).ToList()[i]));

            try
            {
                _Storage.GetStageTimelines(0, 0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.GetStageTimelines(0, timelines[0].CurriculumAssignment.Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.GetStageTimelines(timelines[0].StageRef.Value, 0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTimelines()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item, Id = 1 }).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();
            Assert.AreEqual(timelines.Count, ids.Count);
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimelines(ids).ToList()[i]));

            try
            {
                _Storage.GetTimelines(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void UpdateTimeline()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item }).ToList();
            var timelines = curriculumAssignment.Select(item => new Timeline { CurriculumAssignment = item, Id = 1 }).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            timelines.ForEach(item => item.EndDate = System.DateTime.Now);

            timelines.ForEach(item => _Storage.UpdateTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.UpdateTimeline(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateTimeline(new Timeline());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteTimeline()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item }).ToList();
            List<Timeline> timelines = new List<Timeline>();
            for (int i = 0; i < 4; ++i)
            {
                timelines.Add(new Timeline() { CurriculumAssignment = curriculumAssignment[i], Id = i });
            }
            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.ForEach(item => _Storage.DeleteTimeline(item.Id));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.DeleteTimeline(5);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteTimelines()
        {
            var curriculums = CreateDefaultData();
            var curriculumAssignment = curriculums.Select((item, i) => new CurriculumAssignment { Curriculum = item }).ToList();
            List<Timeline> timelines = new List<Timeline>();
            for (int i = 0; i < 4; ++i)
            {
                timelines.Add(new Timeline() { CurriculumAssignment = curriculumAssignment[i], Id = i });
            }
            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            _Storage.DeleteTimelines(ids);

            timelines.Select((item, i) => i)
                    .ToList()
                    .ForEach(i => Assert.AreEqual(null, _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.DeleteTimelines(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        #endregion
        #region ThemeAssignmentMethods
        [Test]
        public void AddThemeAssignment()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage() { Curriculum = item, Id = 1, Name = "stage" }).ToList();
            var theme = stages.Select(item => new Theme() { Stage = item, Name = "theme" }).ToList();
            var themeassignment = theme.Select(item => new ThemeAssignment() { Theme = item }).ToList();
            for (int i = 0; i < themeassignment.Count; ++i)
            {
                themeassignment[i].Id = i;
            }

            var ids = themeassignment.Select(item => _Storage.AddThemeAssignment(item)).ToList();

            themeassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignment(ids[i])));

            try
            {
                _Storage.AddThemeAssignment(new ThemeAssignment());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.AddThemeAssignment(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetThemeAssignment()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage() { Curriculum = item, Id = 1, Name = "stage" }).ToList();
            var theme = stages.Select(item => new Theme() { Stage = item, Name = "theme" }).ToList();
            var themeassignment = theme.Select(item => new ThemeAssignment() { Theme = item }).ToList();
            for (int i = 0; i < themeassignment.Count; ++i)
            {
                themeassignment[i].Id = i;
            }

            themeassignment.ForEach(item => _Storage.AddThemeAssignment(item));

            themeassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignment(themeassignment[i].Id)));

            try
            {
                _Storage.GetThemeAssignment(5);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetThemeAssignmentsByCurriculumAssignmentId()
        {
            var curriculums = CreateDefaultData();
            var curriculumsassignment = curriculums.Select(item => new CurriculumAssignment() { Curriculum = item, Id = 1 }).ToList();
            var stages = curriculums.Select(item => new Stage() { Curriculum = item, Id = 1, Name = "stage" }).ToList();
            var theme = stages.Select(item => new Theme() { Stage = item, Name = "theme" }).ToList();
            var themeassignment = theme.Select(item => new ThemeAssignment() { Theme = item }).ToList();
            for (int i = 0; i < themeassignment.Count; ++i)
            {
                themeassignment[i].CurriculumAssignment = curriculumsassignment[i];
            }
            themeassignment.ForEach(item => _Storage.AddThemeAssignment(item));

            themeassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignmentsByCurriculumAssignmentId(themeassignment[i].CurriculumAssignment.Id).ToList()[i]));

            try
            {
                _Storage.GetThemeAssignmentsByCurriculumAssignmentId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetThemeAssignmentsByThemeId()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage() { Curriculum = item, Name = "stage" }).ToList();
            var theme = stages.Select(item => new Theme() { Stage = item, Name = "theme", Id = 1 }).ToList();

            var themeassignment = theme.Select(item => new ThemeAssignment() { Theme = item }).ToList();

            themeassignment.ForEach(item => _Storage.AddThemeAssignment(item));

            themeassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignmentsByThemeId(themeassignment[i].Theme.Id).ToList()[i]));

            try
            {
                _Storage.GetThemeAssignmentsByThemeId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetThemeAssignments()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage() { Curriculum = item, Name = "stage" }).ToList();
            var theme = stages.Select(item => new Theme() { Stage = item, Name = "theme", Id = 1 }).ToList();

            var themeassignment = theme.Select(item => new ThemeAssignment() { Theme = item }).ToList();

            var ids = themeassignment.Select(item => _Storage.AddThemeAssignment(item)).ToList();

            themeassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignments(ids).ToList()[i]));

            try
            {
                _Storage.GetThemeAssignments(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void UpdateThemeAssignment()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage() { Curriculum = item, Name = "stage" }).ToList();
            var theme = stages.Select(item => new Theme() { Stage = item, Name = "theme", Id = 1 }).ToList();

            var themeassignment = theme.Select(item => new ThemeAssignment() { Theme = item }).ToList();
            for (int i = 0; i < themeassignment.Count; ++i)
            {
                themeassignment[i].MaxScore = 20 * i;
                themeassignment[i].Id = i;
            }
            themeassignment.ForEach(item => _Storage.AddThemeAssignment(item));

            themeassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignment(themeassignment[i].Id)));


            themeassignment.ForEach(item => item.MaxScore = 0);
            themeassignment.ForEach(item => _Storage.UpdateThemeAssignment(item));

            themeassignment.Select((item, i) => i)
               .ToList()
               .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignment(themeassignment[i].Id)));

            try
            {
                _Storage.UpdateThemeAssignment(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateThemeAssignment(new ThemeAssignment());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteThemeAssignments()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage() { Curriculum = item, Name = "stage" }).ToList();
            var theme = stages.Select(item => new Theme() { Stage = item, Name = "theme", Id = 1 }).ToList();

            var themeassignment = theme.Select(item => new ThemeAssignment() { Theme = item }).ToList();
            for (int i = 0; i < themeassignment.Count; ++i)
            {
                themeassignment[i].Id = i;
            }
            var ids = themeassignment.Select(item => _Storage.AddThemeAssignment(item)).ToList();

            themeassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeassignment[i], _Storage.GetThemeAssignment(ids[i])));

            _Storage.DeleteThemeAssignments(ids);

            themeassignment.Select((item, i) => i)
               .ToList()
               .ForEach(i => Assert.AreEqual(null, _Storage.GetThemeAssignment(ids[i])));

        }
        #endregion
    }
}
