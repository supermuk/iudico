using IUDICO.Statistics.Models.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IUDICO.Common.Models.Shared;

namespace TestProject1
{

    
    /// <summary>
    ///This is a test class for TopicResultTest and is intended
    ///to contain all TopicResultTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TopicResultTest
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
        ///A test for GetTopicResultScore
        ///</summary>
        [TestMethod()]
        public void GetTopicResultScoreTest()
        {
            User usr = new User() { Username = "Bob" };
            Topic thm = new Topic() { Name = "Topic One" };
            IUDICO.Common.Models.Shared.Statistics.AttemptResult AR = new IUDICO.Common.Models.Shared.Statistics.AttemptResult(1,usr,thm, IUDICO.Common.Models.Shared.Statistics.CompletionStatus.Completed, IUDICO.Common.Models.Shared.Statistics.AttemptStatus.Completed,IUDICO.Common.Models.Shared.Statistics.SuccessStatus.Passed, DateTime.Now, 0.5f);
            
            TopicResult target = new TopicResult(usr, thm);
            List<IUDICO.Common.Models.Shared.Statistics.AttemptResult> ARL = new List<IUDICO.Common.Models.Shared.Statistics.AttemptResult>();
            ARL.Add(AR);
            target.AttemptResults = ARL;

            double? expected = 50.0;
            double? actual;
            actual = target.GetTopicResultScore();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTopicResultScore
        ///</summary>
        [TestMethod()] 
        public void GetTopicResultScoreTest1()
        {
            User usr = new User() { Username = "Bob" };
            Topic thm = new Topic() { Name = "Topic One" };
            IUDICO.Common.Models.Shared.Statistics.AttemptResult AR = new IUDICO.Common.Models.Shared.Statistics.AttemptResult(1, usr, thm, IUDICO.Common.Models.Shared.Statistics.CompletionStatus.Completed, IUDICO.Common.Models.Shared.Statistics.AttemptStatus.Completed, IUDICO.Common.Models.Shared.Statistics.SuccessStatus.Passed, DateTime.Now, null);

            TopicResult target = new TopicResult(usr, thm);
            List<IUDICO.Common.Models.Shared.Statistics.AttemptResult> ARL = new List<IUDICO.Common.Models.Shared.Statistics.AttemptResult>();
            ARL.Add(AR);
            target.AttemptResults = ARL;

            double? expected = 0.0;
            double? actual;
            actual = target.GetTopicResultScore();
            Assert.AreEqual(expected, actual);
        }
    }

}
