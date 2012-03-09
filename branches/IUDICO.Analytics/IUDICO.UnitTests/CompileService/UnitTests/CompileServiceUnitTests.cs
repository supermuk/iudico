using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CompileSystem;
using CompileSystem.Compiling;
using CompileSystem.Compiling.Compile;
using CompileSystem.Compiling.Run;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingSystem;

namespace IUDICO.UnitTests.CompileService.UnitTests
{
    [TestClass]
    public class CompileServiceUnitTests
    {
        //CompileException Tests
        #region CompileException tests
 
        /// <summary>
        ///A test for Compile exception 
        ///</summary>
        [TestMethod]
        public void CompileExceptionTest()
        {
            CompileException exception = new CompileException("Bad input value");
            Assert.AreEqual(exception != null, true);

            exception = new CompileException("Bad input value", new Exception());
            Assert.AreEqual(exception != null, true);
        }

        #endregion
        
        //Compilation Tester Tests
        #region CompilationTester tests

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
                Assert.AreEqual(true, true);
            }
        }

        #endregion

        //Compiler Tests
        #region Compiler tests

        /// <summary>
        ///A test for Compiler Constructor
        ///</summary>
        [TestMethod]
        public void CompilerConstructorTest()
        {
            Language language = Language.CSharp3;
            string location = "C:/Windows";
            string arguments = "temp";
            string extension = ".cs";

            Compiler compiler;

            compiler = Compiler.DotNet2Compiler;
            Assert.AreEqual(compiler != null, true);

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

        #region Correct Source Code Compile

        /// <summary>
        ///A test for CS Compile
        ///</summary>
        [TestMethod]
        public void CSCorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                result = Compiler.DotNet3Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for CPP Compile
        ///</summary>
        [TestMethod]
        public void CPPCorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                                Language.VC8);
                result = Compiler.VC8Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Java Compile
        ///</summary>
        [TestMethod]
        public void JavaCorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                                Language.Java6);
                result = Compiler.Java6Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Delphi Compile
        ///</summary>
        [TestMethod]
        public void DelphiCorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(
                    CompileServiceLanguageSourceCode.DelphiCorrectSourceCode,
                    Language.Delphi7);
                result = Compiler.Delphi7Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        #endregion

        #region Incorrect Source Code Compile

        /// <summary>
        ///A test for CS Compile
        ///</summary>
        [TestMethod]
        public void CSIncorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSIncorrectSourceCode,
                                                                Language.CSharp3);
                result = Compiler.DotNet3Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for CPP Compile
        ///</summary>
        [TestMethod]
        public void CPPIncorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPIncorrectSourceCode,
                                                Language.VC8);
                result = Compiler.VC8Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Java Compile
        ///</summary>
        [TestMethod]
        public void JavaIncorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaIncorrectSourceCode,
                                                Language.Java6);
                result = Compiler.Java6Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Delphi Compile
        ///</summary>
        [TestMethod]
        public void DelphiIncorrectCodeCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.DelphiIncorrectSourceCode,
                                                Language.Delphi7);
                result = Compiler.Delphi7Compiler.Compile(programPath);
                CompileServiceHelper.ValidateIncorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        #endregion

        /// <summary>
        ///A test for Compile
        ///</summary>
        [TestMethod]
        public void GeneralCompileTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            CompileResult result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                result = Compiler.DotNet3Compiler.Compile(programPath);
                CompileServiceHelper.ValidateCorrectCompilationResult(result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
       
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
    
        #endregion

        //Function container Tests
        #region FunctionContainer tests

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
            Assert.AreEqual(program.Language, Language.VC8);

            languageString = "CS";
            result = FunctionContainer.AssignLanguageForProgram(languageString, ref program);
            Assert.AreEqual(result, String.Empty);
            Assert.AreEqual(program.Language, Language.CSharp3);

            languageString = "Java";
            result = FunctionContainer.AssignLanguageForProgram(languageString, ref program);
            Assert.AreEqual(result, String.Empty);
            Assert.AreEqual(program.Language,Language.Java6);

            languageString = "Delphi";
            result = FunctionContainer.AssignLanguageForProgram(languageString, ref program);
            Assert.AreEqual(result, String.Empty);
            Assert.AreEqual(program.Language,Language.Delphi7);

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

        #endregion

        //Memory Counter Tests
        #region MemoryCounter tests

        /// <summary>
        ///A test for MemoryCounter Constructor
        ///</summary>
        [TestMethod]
        public void MemoryCounterConstructorTest1()
        {
            Process process = new Process();
            int interval = 10000;

            //Correct interval && correct process
            try
            {
                MemoryCounter memoryCounter = new MemoryCounter(process, interval);
                Assert.AreEqual(true, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for MemoryCounter Constructor
        ///</summary>
        [TestMethod]
        public void MemoryCounterConstructorTest2()
        {
            Process process = null;
            int interval = 10000;
            try
            {
                MemoryCounter memoryCounter = new MemoryCounter(process, interval);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        /// <summary>
        ///A test for MemoryCounter Constructor
        ///</summary>
        [TestMethod]
        public void MemoryCounterConstructorTest3()
        {
            Process process = new Process();
            int interval = 0;
            try
            {
                MemoryCounter memoryCounter = new MemoryCounter(process, interval);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        /// <summary>
        ///A test for MemoryCounter Get Process
        ///</summary>
        [TestMethod]
        public void MemoryCounterConstructorTest4()
        {
            Process process = new Process();
            int interval = 10000;
            MemoryCounter memoryCounter = new MemoryCounter(process,interval);
            var currentProcess = memoryCounter.Process;
            Assert.AreEqual(currentProcess != null,true);
        }
        
        #endregion

        //Program Tests
        #region Program tests

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

            //TODO: it must be done
            /*
            memoryLimit = -1;
            program.MemoryLimit = memoryLimit;
            Assert.AreEqual(true, program.MemoryLimit > 0);*/
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

        #endregion

        //Runner Tests
        #region Runner tests

        #region CorrectParametersTests

        /// <summary>
        ///A test for CS Execute
        ///</summary>
        [TestMethod]
        public void ExecuteCSCorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2 3";
                program.OutputTest = "23";
                program.Language = Language.CSharp3;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for CPP Execute
        ///</summary>
        [TestMethodAttribute]
        public void ExecuteCPPCorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                Language.VC8);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2 3";
                program.OutputTest = "23";
                program.Language = Language.VC8;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Java Execute
        ///</summary>
        [TestMethodAttribute]
        public void ExecuteJavaCorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                Language.Java6);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2 3";
                program.OutputTest = "23";
                program.Language = Language.Java6;
                result = Runner.Execute(exeString, program);
                Assert.AreEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Delphi Execute
        ///</summary>
        [TestMethodAttribute]
        public void ExecuteDelphiCorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
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

        #endregion

        #region IncorrectParametersTests

        /// <summary>
        ///A test for CS Execute
        ///</summary>
        [TestMethod]
        public void ExecuteCSIncorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CSCorrectSourceCode,
                                                                Language.CSharp3);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2";
                program.OutputTest = "23";
                program.Language = Language.CSharp3;
                result = Runner.Execute(exeString, program);
                Assert.AreNotEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for CPP Execute
        ///</summary>
        [TestMethod]
        public void ExecuteCPPIncorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                Language.VC8);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2";
                program.OutputTest = "23";
                program.Language = Language.VC8;
                result = Runner.Execute(exeString, program);
                Assert.AreNotEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }
        
        /// <summary>
        ///A test for Java Execute
        ///</summary>
        [TestMethod]
        public void ExecuteJavaIncorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
                programPath = tester.GenerateFileForCompilation(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                Language.Java6);
                exeString = Path.ChangeExtension(programPath, "exe");
                program.InputTest = "2";
                program.OutputTest = "23";
                program.Language = Language.Java6;
                result = Runner.Execute(exeString, program);
                Assert.AreNotEqual(result.ProgramStatus, Status.Accepted);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        ///A test for Delphi Execute
        ///</summary>
        [TestMethod]
        public void ExecuteDelphiIncorrectParametersTest()
        {
            CompilationTester tester = new CompilationTester(FunctionContainer.CreateDefaultSetting());
            string programPath;
            string exeString;
            Program program = FunctionContainer.CreateProgram("", 10000, 100000);
            Result result;

            try
            {
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

        #endregion

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethodAttribute]
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

        #endregion
    }
}
