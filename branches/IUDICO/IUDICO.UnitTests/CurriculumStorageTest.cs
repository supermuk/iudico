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

        [TestMethod]
        public void TestMethod1()
        {
            Curriculum curriculum = new Curriculum
            {
                Name="Bob",
                IsDeleted=false,
            };
            int id = storage.AddCurriculum(curriculum);
            storage.GetCurriculum(1);
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));
        }
    }
}
