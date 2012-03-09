using IUDICO.Statistics.Models.StatisticsModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UnitTests
{

    [TestClass()]
    public class TopicInfoModelTest
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///Test for Ects
        ///</summary>
        [TestMethod()]
        public void EctsTest()
        {
            TopicInfoModel target = TopicInfoModel.TopicInfoModelTestObject();
            double percent;
            char expected;
            char actual;

            percent = 90;
            expected = 'A';
            actual = target.Ects(percent);
            Assert.AreEqual(expected, actual);

            percent = 88;
            expected = 'B';
            actual = target.Ects(percent);
            Assert.AreEqual(expected, actual);

            percent = 73;
            expected = 'C';
            actual = target.Ects(percent);
            Assert.AreEqual(expected, actual);

            percent = 67;
            expected = 'D';
            actual = target.Ects(percent);
            Assert.AreEqual(expected, actual);

            percent = 53;
            expected = 'E';
            actual = target.Ects(percent);
            Assert.AreEqual(expected, actual);

            percent = 44;
            expected = 'F';
            actual = target.Ects(percent);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test for GetAllTopicsInSelectedDisciplineMaxMark
        ///</summary>
        [TestMethod()]
        public void GetAllTopicsInSelectedDisciplineMaxMarkTest()
        {
            TopicInfoModel target = TopicInfoModel.TopicInfoModelTestObject();
            double expected;
            expected = 200;
            double actual;
            actual = target.GetAllTopicsInSelectedDisciplineMaxMark();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test for GetMaxResautForTopic
        ///</summary>
        [TestMethod()]
        public void GetMaxResutForTopicTest()
        {
            TopicInfoModel target = TopicInfoModel.TopicInfoModelTestObject();
            Topic selectTopic = null;
            Nullable<double> expected = new Nullable<double>();
            expected = 100;
            Nullable<double> actual;
            actual = target.GetMaxResutForTopic(selectTopic);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test for GetStudentResultForAllTopicsInSelectedDiscipline
        ///</summary>
        [TestMethod()]
        public void GetStudentResultForAllTopicsInSelectedDisciplineTest()
        {
            TopicInfoModel target = TopicInfoModel.TopicInfoModelTestObject();
            double expected;
            double actual;
            foreach (var student in target.GetSelectStudents())
            {
                if (student.Name == "user1")
                {
                    expected = 120;
                    actual = target.GetStudentResultForAllTopicsInSelectedDiscipline(student);
                    Assert.AreEqual(expected, actual);
                }
                else if (student.Name == "user2")
                {
                    expected = 180;
                    actual = target.GetStudentResultForAllTopicsInSelectedDiscipline(student);
                    Assert.AreEqual(expected, actual);
                }
                else
                    Assert.Fail();
            }
        }
    }
}
