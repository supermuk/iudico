using TestingSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Diagnostics;
using System.Timers;

namespace IUDICO.UnitTests
{
    /// <summary>
    ///This is a test class for MemoryCounterTest and is intended
    ///to contain all MemoryCounterTest Unit Tests
    ///</summary>
    [TestClass]
    public class MemoryCounterTest
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

        /// <summary>
        ///A test for MemoryCounter Constructor
        ///</summary>
        [TestMethod]
        public void MemoryCounterConstructorTest()
        {
            Process process = new Process();
            int interval = 0;

            //Correct interval && correct process
            try
            {
                MemoryCounter memoryCounter = new MemoryCounter(process, interval);
                Assert.AreEqual(true, true);
            }
            catch(Exception)
            {
                Assert.AreEqual(true,false);
            }

            //Correct interval && null process
            try
            {
                process = null;
                MemoryCounter memoryCounter = new MemoryCounter(process, interval);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            //Incorrect(minus eq 0) interval && correct process
            try
            {
                interval = 0;
                MemoryCounter memoryCounter = new MemoryCounter(process, interval);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
    }
}
