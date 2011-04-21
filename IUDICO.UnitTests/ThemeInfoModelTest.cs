using IUDICO.Statistics.Models.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IUDICO.Common.Models;

namespace IUDICO.UnitTests
{

    [TestClass()]
    public class ThemeInfoModelTest
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
            ThemeInfoModel target = ThemeInfoModel.ThemeInfoModelTestObject();
            Nullable<double> percent = new Nullable<double>();
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
        ///Test for GetAllThemesInSelectedCurriculumMaxMark
        ///</summary>
        [TestMethod()]
        public void GetAllThemesInSelectedCurriculumMaxMarkTest()
        {
            ThemeInfoModel target = ThemeInfoModel.ThemeInfoModelTestObject();
            Nullable<double> expected = new Nullable<double>();
            expected = 200;
            Nullable<double> actual;
            actual = target.GetAllThemesInSelectedCurriculumMaxMark();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test for GetMaxResautForTheme
        ///</summary>
        [TestMethod()]
        public void GetMaxResutForThemeTest()
        {
            ThemeInfoModel target = ThemeInfoModel.ThemeInfoModelTestObject();
            Theme selectTheme = null;
            Nullable<double> expected = new Nullable<double>();
            expected = 100;
            Nullable<double> actual;
            actual = target.GetMaxResutForTheme(selectTheme);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test for GetStudentResultForAllThemesInSelectedCurriculum
        ///</summary>
        [TestMethod()]
        public void GetStudentResultForAllThemesInSelectedCurriculumTest()
        {
            ThemeInfoModel target = ThemeInfoModel.ThemeInfoModelTestObject();
            Nullable<double> expected = new Nullable<double>();
            Nullable<double> actual;
            foreach (var student in target.SelectStudents)
            {
                if (student.Name == "user1")
                {
                    expected = 120;
                    actual = target.GetStudentResultForAllThemesInSelectedCurriculum(student);
                    Assert.AreEqual(expected, actual);
                }
                else if (student.Name == "user2")
                {
                    expected = 180;
                    actual = target.GetStudentResultForAllThemesInSelectedCurriculum(student);
                    Assert.AreEqual(expected, actual);
                }
                else
                    Assert.Fail();
            }
        }
    }
}
