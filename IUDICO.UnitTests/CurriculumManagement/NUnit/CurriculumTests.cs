using System;
using System.Collections.Generic;
using NUnit.Framework;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using Moq;
using System.Linq;
using System.Data.Linq;
using IUDICO.CurriculumManagement.Models.Storage;

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

        [Test]
        public void SimpleTest()
        {
            //заєбашим пару курікулумів. У реальних тестах створення пари курікулумів\стейджів\тем скоріше всього будуть повторюватись тому їх краще
            //винести в метод типу AddDefaultData();
            var actualCurriculums = CreateDefaultData();            
            var expectedCurriculums = new List<Curriculum>()
            {
                new Curriculum() { Name = "Curriculum1", Owner="panza", Id=1, IsDeleted=false },
                new Curriculum() { Name = "Curriculum2", Owner="panza", Id=2, IsDeleted=false },
                new Curriculum() { Name = "Curriculum3", Owner="panza", Id=3, IsDeleted=false },
                new Curriculum() { Name = "Curriculum4", Owner="panza", Id=4, IsDeleted=false }
            };

            //нука перевіримо додавання
            var ids = actualCurriculums.Select(item => _Storage.AddCurriculum(item)).ToList();
            actualCurriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(expectedCurriculums[i], _Storage.GetCurriculum(ids[i])));

            //додамо трохи лоску-стейдж і тему. Проставляйте завжди проперті Curriculum і Stage а не CurriculumRef i StageRef.
            var aStage = new Stage { Name = "Stage", Curriculum = _Storage.GetCurriculum(expectedCurriculums[0].Id) };//actualStage
            var eStage = new Stage { Name = "Stage", Curriculum = _Storage.GetCurriculum(expectedCurriculums[0].Id), Id = 1 };//expectedStage
            _Storage.AddStage(aStage);
            AdvAssert.AreEqual(eStage, _Storage.GetStage(eStage.Id));

            var aTheme = new Theme { Name = "Theme", Stage = _Storage.GetStage(eStage.Id), ThemeType = _Storage.GetThemeType(1), CourseRef = 1 };
            var eTheme = new Theme { Name = "Theme", Stage = _Storage.GetStage(eStage.Id), ThemeType = _Storage.GetThemeType(1), CourseRef = 1, Id = 1, };
            _Storage.AddTheme(aTheme);
            AdvAssert.AreEqual(eTheme, _Storage.GetTheme(eTheme.Id));

            //нука впіндюримо пару асайментів, тут-то і починається єбздєц. Ніколи не проставляйте проперті CurriculumRef, тільки Curriculum
            var aCurrAss = new CurriculumAssignment() { Curriculum = _Storage.GetCurriculum(expectedCurriculums[0].Id), UserGroupRef = 1 };
            var eCurrAss = new CurriculumAssignment() { Curriculum = _Storage.GetCurriculum(expectedCurriculums[0].Id), UserGroupRef = 1, Id = 1 };
            _Storage.AddCurriculumAssignment(aCurrAss);
            AdvAssert.AreEqual(eCurrAss, _Storage.GetCurriculumAssignment(eCurrAss.Id));

            //заціним чи проставились максимальні оціночки за тему(див реалізацію додавання курікулум асаймента)
            var eThemeAss = new ThemeAssignment()
            {
                CurriculumAssignment = _Storage.GetCurriculumAssignment(eCurrAss.Id),
                MaxScore = 1,
                Theme = _Storage.GetTheme(eTheme.Id),
                Id = 1
            };
            var aThemeAsses = _Storage.GetThemeAssignmentsByCurriculumAssignmentId(eCurrAss.Id).ToList();
            Assert.AreEqual(1, aThemeAsses.Count);
            AdvAssert.AreEqual(eThemeAss, aThemeAsses[0]);
        }
        #region CurriculumMethodsTests
        [Test]
        public void AddCurriclulum()
        {
            List<Curriculum> curriculums = CreateDefaultData();
            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();
            curriculums.Select((item, index) => index).ToList()
                .ForEach(index=>AdvAssert.AreEqual(curriculums[index],_Storage.GetCurriculum(ids[index])));
            try
            {
                _Storage.AddCurriculum(new Curriculum());
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddCurriculum(new Curriculum { });
                Assert.Fail();
            }
            catch (Exception ex)
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
                .ForEach(i=>AdvAssert.AreEqual(curriculums[i],_Storage.GetCurriculum(ids[i])));
            #region WhyDoesItWorks
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            _Storage.GetCurriculum(ids[3]).Owner=user.Username;
            //Tests GetCurriculums(IEnumerable<int> ids)
            Assert.AreEqual(curriculums, _Storage.GetCurriculums(ids));
            //Tests GetCurriculums()
            Assert.AreEqual(curriculums, _Storage.GetCurriculums());
            //Tests GetCurriculums(User owner)
            AdvAssert.AreEqual(curriculums[3],_Storage.GetCurriculums(user).First());
            List<int> empty = new List<int>();
            try
            {
                _Storage.GetCurriculums(empty);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetCurriculumsByGroupId()
        {
            List<Curriculum> curriculums = CreateDefaultData();            
            curriculums.ForEach(item=>_Storage.AddCurriculum(item));
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
            var actualCurriculum=_Storage.GetCurriculum(curriculum.Id);
            AdvAssert.AreEqual(curriculum,actualCurriculum);
            try
            {
                _Storage.UpdateCurriculum(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteCurriculium()
        {
            List<Curriculum> curriculums=CreateDefaultData();
            var ids=curriculums.Select(item=>_Storage.AddCurriculum(item)).ToList();
            _Storage.DeleteCurriculum(ids[0]);
            Assert.AreEqual(null, _Storage.GetCurriculum(ids[0]));
            Assert.AreNotEqual(null,_Storage.GetCurriculum(ids[1]));
            ids.RemoveAt(0);
            _Storage.DeleteCurriculums(ids);
            try
            {
                ids.ForEach(i=>_Storage.GetCurriculum(i));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteCurriculum(ids[0]);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteCurriculum(0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void MakeCurriculumInvalid()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum1" };
            Stage stage = new Stage() { Curriculum = curriculum, Name = "Stage1" };
            Theme theme = new Theme() { Name = "Theme1", Stage = stage, ThemeType = _Storage.GetThemeType(1), CourseRef = 1 };
            _Storage.AddCurriculum(curriculum);
            _Storage.AddStage(stage);
            _Storage.AddTheme(theme);
            _Storage.MakeCurriculumInvalid(1);
            _Storage.GetCurriculums().ToList()
                .ForEach(item => Assert.AreEqual(false, item.IsValid));
        }
        #endregion
        #region StageMethodsTests
        [Test]
        public void AddStage()
        {
            var curriculums = CreateDefaultData();
            var stages = curriculums.Select(item => new Stage { Name = "Stage", Curriculum = item }).ToList();
            var ids = stages.Select(item=>_Storage.AddStage(item)).ToList();
            ids.Select((item, i) => i).ToList().ForEach(item => AdvAssert.AreEqual(stages[item], _Storage.GetStage(ids[item])));
            try
            {
                _Storage.AddStage(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddStage(new Stage { });
                Assert.Fail();
            }
            catch(Exception ex)
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
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }
        #endregion
    }
}
