using System.Collections.Generic;
using NUnit.Framework;
using IUDICO.Common.Models;
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
            var actualCurriculums = new List<Curriculum>()
            {
                new Curriculum() { Name = "Curriculum1" },
                new Curriculum() { Name = "Curriculum2" },
                new Curriculum() { Name = "Curriculum3" },
                new Curriculum() { Name = "Curriculum4" }
            };
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
    }
}
