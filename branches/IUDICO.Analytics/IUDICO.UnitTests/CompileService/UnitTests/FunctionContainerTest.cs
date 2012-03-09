using System;
using CompileSystem;
using IUDICO.UnitTests.CompileService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using CompileSystem.Compiling;

namespace IUDICO.UnitTests
{
    /// <summary>
    ///This is a test class for FunctionContainerTest and is intended
    ///to contain all FunctionContainerTest Unit Tests
    ///</summary>
    [TestClass]
    public class FunctionContainerTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for AssignLanguageForProgram
        ///</summary>
        [TestMethod]
        public void AssignLanguageForProgramTest()
        {
            string languageString = "CPP";
            string result;
            Program program = new Program();
            result = FunctionContainer.AssignLanguageForProgram(languageString, ref program);
            
            Assert.AreEqual(result, String.Empty);

            languageString = "cpp";
            result = FunctionContainer.AssignLanguageForProgram(languageString, ref program);
            Assert.AreEqual(result, "Unsupported language");

            languageString = "unknown";
            result = FunctionContainer.AssignLanguageForProgram(languageString, ref program);
            Assert.AreEqual(result, "Unsupported language");
        }

        /// <summary>
        ///A test for CreateCompilationTester
        ///</summary>
        [TestMethod]
        public void CreateCompilationTesterTest()
        {
            Settings settings = FunctionContainer.CreateDefaultSetting();
            CompilationTester tester;
            try
            {
                tester = FunctionContainer.CreateCompilationTester(settings);
                Assert.AreEqual(tester.Settings, settings);
            }
            catch (Exception)
            {
                Assert.AreEqual(false,true);
            }

            try
            {
                tester = FunctionContainer.CreateCompilationTester(null);
                Assert.AreEqual(false,true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true,true);
            }

        }

        /// <summary>
        ///A test for CreateDefaultSetting
        ///</summary>
        [TestMethod]
        public void CreateDefaultSettingTest()
        {
            Settings defaultSettings = FunctionContainer.CreateDefaultSetting();
            Assert.AreEqual(defaultSettings.TestingDirectory.Length > 0, true);
            Assert.AreNotEqual(defaultSettings.Compilers, null);
        }

        /// <summary>
        ///A test for CreateProgram
        ///</summary>
        [TestMethod]
        public void CreateProgramTest()
        {
            string source = CompileServiceLanguageSourceCode.CPPCorrectSourceCode;
            int timelimit = 1;
            int memorylimit = 1;

            Program program = FunctionContainer.CreateProgram(source, memorylimit, timelimit);
            Assert.AreEqual(program.Source, source);
            Assert.AreEqual(program.TimeLimit, timelimit);
            Assert.AreEqual(program.MemoryLimit, memorylimit);

            timelimit = -1;
            try
            {
                program = FunctionContainer.CreateProgram(source, memorylimit, timelimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true,true);
            }

            timelimit = 1;
            memorylimit = -1;
            try
            {
                program = FunctionContainer.CreateProgram(source, memorylimit, timelimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
    }
}
