using CompileSystem.Compiling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IUDICO.UnitTests
{
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProgramTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for InputTest
        ///</summary>
        [TestMethod]
        public void InputTestTest()
        {
            var program = new Program();
            string inputValue = "string1";

            program.InputTest = inputValue;
            Assert.AreEqual(program.InputTest,inputValue);

            inputValue = String.Empty;
            program.InputTest = inputValue;
            Assert.AreEqual(program.InputTest, inputValue);
        }

        /// <summary>
        ///A test for Language
        ///</summary>
        [TestMethod]
        public void LanguageTest()
        {
            var program = new Program();
            var language = Language.CSharp3;
            program.Language = language;

            Assert.AreEqual(language, program.Language);
        }

        /// <summary>
        ///A test for MemoryLimit
        ///</summary>
        [TestMethod]
        public void MemoryLimitTest()
        {
            var program = new Program();
            int memoryLimit;
            //Correct memory limit
            memoryLimit = 100;
            program.MemoryLimit = memoryLimit;
            Assert.AreEqual(memoryLimit,program.MemoryLimit);

            //None/minus memory limit
            memoryLimit = 0;
            program.MemoryLimit = memoryLimit;
            Assert.AreEqual(memoryLimit, program.MemoryLimit);

            memoryLimit = -1;
            program.MemoryLimit = memoryLimit;
            Assert.AreEqual(true, program.MemoryLimit > 0);
            //TODO: memoryLimit value must be greater than 0
        }

        /// <summary>
        ///A test for OutputTest
        ///</summary>
        [TestMethod]
        public void OutputTestTest()
        {
            var program = new Program();
            string outputValue = "string1";

            program.OutputTest = outputValue;
            Assert.AreEqual(program.OutputTest, outputValue);

            outputValue = String.Empty;
            program.InputTest = outputValue;
            Assert.AreEqual(program.InputTest, outputValue);
        }

        /// <summary>
        ///A test for Source
        ///</summary>
        [TestMethod]
        public void SourceTest()
        {
            var program = new Program();
            string source = "source";

            program.Source = source;
            Assert.AreEqual(source, program.Source);

            source = "";
            program.Source = source;
            Assert.AreEqual(source,program.Source);
        }

        /// <summary>
        ///A test for TimeLimit
        ///</summary>
        [TestMethod]
        public void TimeLimitTest()
        {
            var program = new Program();
            int timelimit;

            //Correct timelimit
            timelimit = 5;
            program.TimeLimit = timelimit;
            Assert.AreEqual(timelimit,program.TimeLimit);

            //Incorrect timelimit
            timelimit = 0;
            program.TimeLimit = timelimit;
            Assert.AreEqual(timelimit, program.TimeLimit);

            timelimit = -5;
            program.TimeLimit = timelimit;
            Assert.AreEqual(timelimit, program.TimeLimit);
            //TODO: timelimit value must be greater than 0
        }
    }
}
