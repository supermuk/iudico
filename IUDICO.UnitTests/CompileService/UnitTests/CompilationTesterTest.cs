using System.Collections.Generic;
using CompileSystem;
using CompileSystem.Compiling;
using IUDICO.UnitTests.CompileService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using CompileSystem.Compiling.Compile;

namespace IUDICO.UnitTests
{
    /// <summary>
    ///This is a test class for CompilationTesterTest and is intended
    ///to contain all CompilationTesterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompilationTesterTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Generating correct program files and returning their path
        ///</summary>
        [TestMethod]
        public void GenerateFileForCompilationTest()
        {
            Settings settings = FunctionContainer.CreateDefaultSetting();
            CompilationTester compilationTester = new CompilationTester(settings);
            string source, programPath;
            Language language;

            //language tests
            source = CompileServiceLanguageSourceCode.CSCorrectSourceCode;
            language = Language.CSharp3;
            programPath = compilationTester.GenerateFileForCompilation(source, language);
            Assert.AreEqual(true, CompileServiceHelper.ValidatePath(programPath));

            source = CompileServiceLanguageSourceCode.CPPCorrectSourceCode;
            language = Language.VC8;
            programPath = compilationTester.GenerateFileForCompilation(source, language);
            Assert.AreEqual(true, CompileServiceHelper.ValidatePath(programPath));

            source = CompileServiceLanguageSourceCode.JavaCorrectSourceCode;
            language = Language.Java6;
            programPath = compilationTester.GenerateFileForCompilation(source, language);
            Assert.AreEqual(true, CompileServiceHelper.ValidatePath(programPath));

            source = CompileServiceLanguageSourceCode.DelphiCorrectSourceCode;
            language = Language.Delphi7;
            programPath = compilationTester.GenerateFileForCompilation(source, language);
            Assert.AreEqual(true, CompileServiceHelper.ValidatePath(programPath));

            //incorrect source
            source = "";
            language = Language.CSharp3;
            try
            {
                programPath = compilationTester.GenerateFileForCompilation(source, language);
                Assert.AreEqual(false,true);
            }
            catch(Exception)
            {
                Assert.AreEqual(true,true);
            }

            //incorrect compiler
            settings.Compilers = new List<Compiler>();
            compilationTester = new CompilationTester(settings);
            source = "";
            language = Language.CSharp3;
            try
            {
                programPath = compilationTester.GenerateFileForCompilation(source, language);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true,true);
            }
        }

        /// <summary>
        ///A test for CompilationTester Constructor
        ///</summary>
        [TestMethod]
        public void CompilationTesterConstructorTest()
        {
            Settings settings = new Settings()
                                    {
                                        TestingDirectory = "Some path",
                                        Compilers = new List<Compiler>()
                                    };

            CompilationTester tester = new CompilationTester(settings);
            Assert.AreEqual(settings, tester.Settings);

            try
            {
                settings = null;
                tester = new CompilationTester(settings);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true,true);
            }
        }
    }
}
