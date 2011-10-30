using System.Collections.Generic;
using NUnit.Framework;
using IUDICO.Common.Models;
using Moq;
using System.Linq;
using System.Data.Linq;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    [TestFixture]
    public class CurriculumTests
    {
        protected CurriculumManagementTests _Tests = CurriculumManagementTests.GetInstance();

        [Test]
        public void AddCurriculum()
        {
            Curriculum curriculum = new Curriculum { Id = 2, Name = "Curriculum2" };
            var a = _Tests.MockDataContext.Object.Curriculums.Count();
            _Tests.Storage.AddCurriculum(curriculum);
            var b = _Tests.MockDataContext.Object.Curriculums.Count();

            _Tests.MockDataContext.Verify(item => item.SubmitChanges());
            _Tests.Curriculums.Verify(item =>
                item.InsertOnSubmit(It.Is<Curriculum>(c => new CurriculumComparer().Equals(c, curriculum))));
        }

        [Test]
        public void GetCurriculum()
        {
            Curriculum curriculum = new Curriculum { Id = 3, Name = "Curriculum3" };
            _Tests.Storage.AddCurriculum(curriculum);
            var actualCurriculum = _Tests.Storage.GetCurriculum(curriculum.Id);
            //_Tests.MockStorage.Verify(item=>new CurriculumComparer().Equals(item, item.GetCurriculum(curriculum.Id)
            AdvAssert.AreEqual(curriculum, actualCurriculum);

            _Tests.Curriculums.Verify(item =>
                item.InsertOnSubmit(It.Is<Curriculum>(c => new CurriculumComparer().Equals(c, curriculum))));
        }
    }
}
