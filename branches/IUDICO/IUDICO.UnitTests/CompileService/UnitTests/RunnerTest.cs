using System.IO;
using CompileSystem;
using CompileSystem.Compiling.Compile;
using CompileSystem.Compiling.Run;
using IUDICO.UnitTests.CompileService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using CompileSystem.Compiling;

namespace IUDICO.UnitTests
{
    /// <summary>
    ///This is a test class for RunnerTest and is intended
    ///to contain all RunnerTest Unit Tests
    ///</summary>
    [TestClass]
    public class RunnerTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod]
        public void ExecuteCorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                //-----------CS-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2 3";
                program.OutputTest = "23";
                program.Language = Language.CSharp3;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.Accepted);

                //-----------CPP-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                Language.VC8);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2 3";
                program.OutputTest = "23";
                program.Language = Language.VC8;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.Accepted);

                //-----------Java-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                Language.Java6);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2 3";
                program.OutputTest = "23";
                program.Language = Language.Java6;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.Accepted);

                //-----------Delphi-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.DelphiCorrectSourceCode,
                                                Language.Delphi7);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "";
                program.OutputTest = "Hello, world!";
                program.Language = Language.Delphi7;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod]
        public void ExecuteIncorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                //-----------CS-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2";
                program.OutputTest = "23";
                program.Language = Language.CSharp3;
                result = Runner.Execute(exeString, program);
                Assert.AreNotEqual(result.ProgramStatus, Status.Accepted);

                //-----------CPP-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                Language.VC8);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2";
                program.OutputTest = "23";
                program.Language = Language.VC8;
                result = Runner.Execute(exeString, program);
                Assert.AreNotEqual(result.ProgramStatus, Status.Accepted);

                //-----------Java-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                Language.Java6);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2";
                program.OutputTest = "23";
                program.Language = Language.Java6;
                result = Runner.Execute(exeString, program);
                Assert.AreNotEqual(result.ProgramStatus, Status.Accepted);

                //-----------Delphi-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.DelphiCorrectSourceCode,
                                                Language.Delphi7);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "";
                program.OutputTest = " world!";
                program.Language = Language.Delphi7;
                result = Runner.Execute(exeString, program);
                Assert.AreNotEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod]
        public void ExecuteIncorrectMemoryTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 0, 100000);
            program.InputTest = "2 3";
            program.OutputTest = "23";
            Result result;

            try
            {
                //-----------CS-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.Language = Language.CSharp3;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.MemoryLimit);

                //-----------CPP-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                Language.VC8);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.Language = Language.VC8;

                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.MemoryLimit);

                //-----------Java-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                Language.Java6);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.Language = Language.Java6;

                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.MemoryLimit);

                //-----------Delphi-----------
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.DelphiCorrectSourceCode,
                                                Language.Delphi7);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "";
                program.OutputTest = "Hello, world!";
                program.Language = Language.Delphi7;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.MemoryLimit);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }
    }
}
