using IUDICO.Statistics.Models.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared.Statistics;

namespace TestProject1
{
    /* TODO:FIX IT!!!!
    
    /// <summary>
    ///This is a test class for SpecializedResultTest and is intended
    ///to contain all SpecializedResultTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpecializedResultTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CalculateSpecializedResult
        ///</summary>
        [TestMethod()]
        public void CalculateSpecializedResultTest()
        {
            IUDICO.Common.Models.User usr = new IUDICO.Common.Models.User() { Username = "Bob" };
            IUDICO.Common.Models.Theme thm = new IUDICO.Common.Models.Theme() { Name = "Theme One" };
            IUDICO.Common.Models.Shared.Statistics.AttemptResult AR = new IUDICO.Common.Models.Shared.Statistics.AttemptResult(1, usr, thm, IUDICO.Common.Models.Shared.Statistics.CompletionStatus.Completed, IUDICO.Common.Models.Shared.Statistics.AttemptStatus.Completed, IUDICO.Common.Models.Shared.Statistics.SuccessStatus.Passed, 0.5f);

            ThemeResult themeRes = new ThemeResult(usr, thm);
            List<AttemptResult> ARL = new List<AttemptResult>();
            ARL.Add(AR);
            themeRes.AttemptResults = ARL;
            themeRes.GetThemeResultScore();

            CurriculumResult currRes = new CurriculumResult();
            currRes.ThemeResult.Add(themeRes);
            Curriculum curr = null;
            currRes.CalculateSumAndMax(usr, curr);

            SpecializedResult target = new SpecializedResult();
            target.CurriculumResult.Add(currRes);
            target.CalculateSpecializedResult(usr);

            double? ExpectedSum = 50.0;
            double? ExpectedMax = 100.0;
            double? ExpectedPercent = 50.0;
            char ExpextedECTS = 'F';

            Assert.AreEqual(ExpectedSum, target.Sum);
            Assert.AreEqual(ExpectedMax, target.Max);
            Assert.AreEqual(ExpectedPercent, target.Percent);
            Assert.AreEqual(ExpextedECTS, target.ECTS);
        }
    }
    */
}
