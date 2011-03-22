using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.UnitTests
{
    [TestClass]
    public class CurriculumStorageTest
    {
        MixedCurriculumStorage storage { get; set; }
        static ILmsService fakeLmsService { get; set; }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            fakeLmsService = new FakeLmsService();
            DBDataContext context = fakeLmsService.GetDbDataContext();
            //clear all tables before test runs!
            context.Curriculums.DeleteAllOnSubmit(context.Curriculums);
            context.SubmitChanges();
            //...
        }

        [TestInitialize()]
        public void InitializeTest()
        {
            storage = new MixedCurriculumStorage(fakeLmsService);
        }

        #region CurriculumTestMethods

        [TestMethod]
        public void TestMethod1()
        {
            Curriculum curriculum = new Curriculum
            {
                Name = "Bob",
                IsDeleted = false,
            };
            int id = storage.AddCurriculum(curriculum);
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));
            //storage.GetCurriculum(1);
            //викликає Exception оскільки нема такого запису,треба шось підмутити
            //питання - якщо нема такого,то шо вертати??
        }

        [TestMethod]
        public void TestMethod2()
        {
            Curriculum curriculum1 = new Curriculum { Name = "FirstTestCurr" };
            Curriculum curriculum2 = new Curriculum { Name = "SecondTestCurr" };
            storage.AddCurriculum(curriculum1);
            storage.AddCurriculum(curriculum2);
            int expected = 2;
            int actual = storage.GetCurriculums().ToList().Count;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Curriculum curriculum = new Curriculum { Name = "CurriculumToUpdate" };
            int id = storage.AddCurriculum(curriculum);
            curriculum.Name = "UpdatedCurriculum";
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Curriculum curriculum = new Curriculum { Name = "CurriculumToDelete" };
            int id = storage.AddCurriculum(curriculum);
            storage.DeleteCurriculum(id);
            //якщо шо - поправити
            try
            {
                storage.GetCurriculum(id);
                Assert.AreEqual(true, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        #endregion

        #region StageTestMethods

        [TestMethod]
        public void TestMethod5()
        {
            Stage stage = new Stage { Name = "StageToAdd" };
            int id = storage.AddStage(stage);
            AdvAssert.AreEqual(storage.GetStage(id), stage);
        }

        [TestMethod]
        public void TestMethod6()
        {
            Curriculum curriculum = new Curriculum { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);
            Stage stage = new Stage { Name = "Stage", CurriculumRef = curriculumId};
            int stageId = storage.AddStage(stage);
            List<Stage> listWithStagesForTest = new List<Stage>();
            listWithStagesForTest.Add(stage);
            AdvAssert.AreEqual(listWithStagesForTest, storage.GetStages(curriculumId).ToList());
        }

        [TestMethod]
        public void TestMethod7()
        {
            Stage stage = new Stage() { Name = "StageForUpdate" };
            int id = storage.AddStage(stage);
            stage.Name = "UpdatedStage";
            storage.UpdateStage(stage);

            AdvAssert.AreEqual(stage, storage.GetStage(id));
        }

        [TestMethod]
        public void TestMethod8()
        {
            Stage stage = new Stage() { Name = "StageForDelete" };
            int id = storage.AddStage(stage);
            AdvAssert.AreEqual(storage.GetStage(id), stage);
            storage.DeleteStage(id);

            try
            {
                storage.GetStage(id);
                Assert.AreEqual(true, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true,true);
            }
        }

        #endregion

        #region ThemeMethods

        //як розуміти метод private Theme GetTheme(int id, DBDataContext db)???

        [TestMethod]
        public void TestMethod9()
        {
            Theme theme = new Theme() { Name = "theme" };
            int id = storage.AddTheme(theme);
            AdvAssert.AreEqual(theme, storage.GetTheme(id));
        }

        [TestMethod]
        public void TestMethod10()
        {


        }


        #endregion
    }
}
