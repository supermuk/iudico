using CompileSystem;
using CompileSystem.Compiling.Compile;
using IUDICO.UnitTests.CompileService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using CompileSystem.Compiling;
using System.Text;

namespace IUDICO.UnitTests
{
    /// <summary>
    ///This is a test class for CompilerTest and is intended
    ///to contain all CompilerTest Unit Tests
    ///</summary>
    [TestClass]
    public class CompilerTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Compiler Constructor
        ///</summary>
        [TestMethod]
        public void CompilerConstructorTest()
        {
            Language language = Language.CSharp3;
            string location = "D:/";
            string arguments = "temp";
            string extension = ".cs";

            Compiler compiler;
            try
            {
                compiler = new Compiler(language, location, arguments, extension);
                Assert.AreEqual(true, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(false,true);
            }

            try
            {
                compiler = new Compiler(language, null, arguments, extension);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                compiler = new Compiler(language, location, null, extension);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                compiler = new Compiler(language, location, arguments, null);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        /// <summary>
        ///A test for Compile
        ///</summary>
        [TestMethod]
        public void CompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            #region CorrectLangugeSource

            try
            {
                //-----------CS-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                result = Compiler.DotNet3Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);

                //-----------CPP-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                Language.VC8);
                result = Compiler.VC8Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);

                //-----------Java-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                Language.Java6);
                result = Compiler.Java6Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);

                //-----------Delphi-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.DelphiCorrectSourceCode,
                                                Language.Delphi7);
                result = Compiler.Delphi7Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true,false);
            }
            
            #endregion

            #region IncorrectLangugeSource

            try
            {
                //-----------CS-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSIncorrectSourceCode,
                                                                Language.CSharp3);
                result = Compiler.DotNet3Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);

                //-----------CPP-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPIncorrectSourceCode,
                                                Language.VC8);
                result = Compiler.VC8Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);

                //-----------Java-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaIncorrectSourceCode,
                                                Language.Java6);
                result = Compiler.Java6Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);

                //-----------Delphi-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.DelphiIncorrectSourceCode,
                                                Language.Delphi7);
                result = Compiler.Delphi7Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }

            #endregion

            // bad input parameters
            try
            {
                result = Compiler.DotNet3Compiler.Compile(null);
                Assert.AreEqual(true,false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true,true);
            }

            try
            {
                result = Compiler.DotNet3Compiler.Compile("");
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
    }
}
