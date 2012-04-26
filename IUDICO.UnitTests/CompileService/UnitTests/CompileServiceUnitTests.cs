using System;
using System.IO;

using CompileSystem.Classes.Compiling;
using CompileSystem.Classes.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IUDICO.UnitTests.CompileService.UnitTests
{
    using CompileSystem;
    using CompileSystem.Classes;

    [TestClass]
    public class CompileServiceUnitTests
    {
        // Status tests
        #region StatusTests

        /// <summary>
        /// A test for Status Constructor
        /// </summary>
        [TestMethod]
        public void StatusConstructorTest()
        {
            const string testResult = "Accepted";
            var target = new Status(testResult);

            Assert.AreEqual(false, testResult == null);
            Assert.AreEqual(testResult, target.TestResult);
        }

        #endregion

        // Helper tests
        #region HelperTests

        /// <summary>
        /// A test for CreateFileForCompilation
        /// </summary>
        [TestMethod]
        public void CreateFileForCompilationTest()
        {
            string extension = null;
            const string source = "My source code";
            string currentFilePath;

            try
            {
                currentFilePath = Helper.CreateFileForCompilation(source, extension);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                extension = "cpp";
                currentFilePath = Helper.CreateFileForCompilation(source, extension);
                Assert.AreNotEqual(true, string.IsNullOrEmpty(currentFilePath));
                Assert.AreEqual(File.Exists(currentFilePath), true);

                var text = File.ReadAllText(currentFilePath);
                Assert.AreNotEqual(true, string.IsNullOrEmpty(text));
                Assert.AreEqual(text, source);
            }
            catch (Exception)
            {
                Assert.AreEqual(false, true);
            }
        }

        #endregion

        // Compilers tests
        #region CompilersTests

        /// <summary>
        /// A test for Compilers Constructor
        /// </summary>
        [TestMethod]
        public void CompilersConstructorTest()
        {
            string compilersDirectory = null;
            Compilers target;

            try
            {
                target = new Compilers(compilersDirectory);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                compilersDirectory = "Directory";
                target = new Compilers(compilersDirectory);
                Assert.AreEqual(true, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        /// A test for AddCompiler
        /// </summary>
        [TestMethod]
        public void AddCompilerTest()
        {
            var compilersDirectory = "Directory";
            Compilers target;

            try
            {
                target = new Compilers(compilersDirectory);
                target.AddCompiler(null);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                target = new Compilers(compilersDirectory);
                Assert.AreEqual(target.Count, 0);
                target.AddCompiler(new Compiler());
                Assert.AreEqual(target.Count, 1);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        /// A test for Clear
        /// </summary>
        [TestMethod]
        public void ClearTest()
        {
            var target = new Compilers("Directory");
            Assert.AreEqual(target.Count, 0);
            target.AddCompiler(new Compiler());
            Assert.AreEqual(target.Count, 1);
            target.Clear();
            Assert.AreEqual(target.Count, 0);
        }

        /// <summary>
        /// A test for Contains
        /// </summary>
        [TestMethod]
        public void ContainsTest()
        {
            var target = new Compilers("Directory");
            Assert.AreEqual(target.Count, 0);
            var newCompiler = new Compiler();
            newCompiler.Name = "CPP";
            target.AddCompiler(newCompiler);
            var result = target.Contains("CPP");
            Assert.AreEqual(true, result);

            result = target.Contains("BadCompilerName");
            Assert.AreEqual(result, false);
        }

        /// <summary>
        /// A test for GetCompiler
        /// </summary>
        [TestMethod]
        public void GetCompilerTest()
        {
            var target = new Compilers("Directory");
            Assert.AreEqual(target.Count, 0);
            var newCompiler = new Compiler();
            newCompiler.Name = "CPP";
            target.AddCompiler(newCompiler);
            var result = target.GetCompiler("CPP");
            Assert.AreNotEqual(result, null);
            Assert.AreEqual("CPP", result.Name);

            result = target.GetCompiler("BadName");
            Assert.AreEqual(result, null);
        }

        /// <summary>
        /// A test for GetCompilers
        /// </summary>
        [TestMethod]
        public void GetCompilersTest()
        {
            var target = new Compilers("Directory");
            var newCompiler = new Compiler();
            newCompiler.Name = "CPP";
            var resultList = target.GetCompilers();
            Assert.AreEqual(resultList.Count, 0);

            target.AddCompiler(newCompiler);
            resultList = target.GetCompilers();
            Assert.AreEqual(resultList.Count, 1);
        }

        /// <summary>
        /// A test for Load
        /// </summary>
        [TestMethod]
        public void LoadTest()
        {
            Compilers compilers;

            // empty compiler folder
            try
            {
                compilers = new Compilers("EmptyCompiler");
                compilers.Load();
                Assert.AreEqual(compilers.Count, 0);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }

            // compiler folder with bad information
            try
            {
                compilers = new Compilers("BadCompilers");
                compilers.Load();
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            // compiler folder with correct information
            try
            {
                compilers = new Compilers("TestCompilers");
                compilers.Load();
                Assert.AreEqual(compilers.Count, 2);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        /// <summary>
        /// A test for Parse
        /// </summary>
        [TestMethod]
        public void ParseTest()
        {
            var correctXmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestCompilers\CPP8.xml");
            var incorrectXmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestCompilers\CSharp.xml");

            var compilers = new Compilers("Compilers");
            var privateObject = new PrivateObject(compilers, new PrivateType(typeof(Compilers)));

            // incorrect xml file
            try
            {
                privateObject.Invoke("Parse", incorrectXmlFilePath);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            // correct xml file
            try
            {
                Assert.AreNotEqual(null, privateObject.Invoke("Parse", correctXmlFilePath));
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        #endregion

        // Compiler tests
        #region CompilerTests

        /// <summary>
        /// A test for Compile
        /// </summary>
        [TestMethod]
        public void CompileTest()
        {
            // create compiler
            var compiler = new Compiler
                {
                    Name = "CPP", 
                    Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CPP8\Compiler\CL.EXE"), 
                    Extension = "cpp", 
                    Arguments = "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"", 
                    CompiledExtension = "exe", 
                    IsNeedShortPath = true
                };

            var filePath = Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, compiler.Extension);
            string output, error;
            bool result;
            try
            {
                result = compiler.Compile("BadFileName", out output, out error);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                result = compiler.Compile(filePath, out output, out error);
                Assert.AreEqual(true, result);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }

            // remove file
            File.Delete(filePath);
        }

        #endregion

        // Tester tests
        #region TesterTests

        /// <summary>
        /// A test for Test
        /// </summary>
        [TestMethod]
        public void TestTest()
        {
            // create compiler
            var compiler = new Compiler
                {
                    Name = "CPP", 
                    Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CPP8\Compiler\CL.EXE"), 
                    Extension = "cpp", 
                    Arguments = "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"", 
                    CompiledExtension = "exe", 
                    IsNeedShortPath = true
                };

            var filePath = Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, compiler.Extension);
            string output, error;
            var result = compiler.Compile(filePath, out output, out error);
            if (result)
            {
                filePath = Path.ChangeExtension(filePath, compiler.CompiledExtension);
                Status testingResult;

                // check file path
                try
                {
                    Tester.Test("badFilePath", string.Empty, string.Empty, 1, 1);
                    Assert.AreEqual(true, false);
                }
                catch (Exception)
                {
                    Assert.AreEqual(true, true);
                }

                // check correct timelimit
                try
                {
                    Tester.Test(filePath, string.Empty, string.Empty, -5, 1);
                    Assert.AreEqual(true, false);
                }
                catch (Exception)
                {
                    Assert.AreEqual(true, true);
                }

                // check correct memorylimit
                try
                {
                    Tester.Test(filePath, string.Empty, string.Empty, 1, -5);
                    Assert.AreEqual(true, false);
                }
                catch (Exception)
                {
                    Assert.AreEqual(true, true);
                }

                // test with correct parameters
                try
                {
                    testingResult = Tester.Test(filePath, string.Empty, string.Empty, 10000, 3000);
                    Assert.AreEqual("Accepted", testingResult.TestResult);
                }
                catch (Exception)
                {
                    Assert.AreEqual(true, false);
                }
            }
        }

        #endregion

        // CompileTask tests
        #region CompileTaskTests

        /// <summary>
        /// A test for CompileTask Constructor
        /// </summary>
        [TestMethod]
        public void CompileTaskConstructorTest()
        {
            // create compiler
            var compiler = new Compiler
                {
                    Name = "CPP", 
                    Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CPP8\Compiler\CL.EXE"), 
                    Extension = "cpp", 
                    Arguments = "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"", 
                    CompiledExtension = "exe", 
                    IsNeedShortPath = true
                };
            var sourceCodeFilePath =
                Helper.CreateFileForCompilation(
                    CompileServiceLanguageSourceCode.CPPCorrectSourceCode, compiler.Extension);

            try
            {
                var compileTask = new CompileTask(null, sourceCodeFilePath);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                var compileTask = new CompileTask(compiler, "BadFilePath");
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                var compileTask = new CompileTask(compiler, sourceCodeFilePath);
                Assert.AreEqual(true, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }

            File.Delete(sourceCodeFilePath);
        }

        #endregion

        // Compile service tests
        #region CompileServiceTests

        /// <summary>
        /// A test for Compile
        /// </summary>
        [TestMethod]
        public void CompileServiceTest()
        {
            var _compileService = new CompileService();
            var source = CompileServiceLanguageSourceCode.CPPCorrectSourceCode;
            var language = "CPP8";
            var input = new string[0];
            var output = new string[0];
            var inputStrings = new[] { "1 2" };
            var outputStrings = new[] { "12" };

            string expected;

            // compile with incorrect language parameter
            try
            {
                expected = _compileService.Compile(source, "IncorrectLanguageName", input, output, 100, 100);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            try
            {
                expected = _compileService.Compile(source, string.Empty, input, output, 100, 100);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            // compile with incorrect timelimit parameter
            try
            {
                expected = _compileService.Compile(source, language, inputStrings, outputStrings, -5, 100);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            // compile with incorrect memorylimit parameter
            try
            {
                expected = _compileService.Compile(source, language, inputStrings, outputStrings, 100, -5);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            // compile with correct parameters
            try
            {
                input = new[] { "2 5", "7 5" };
                output = new[] { "25", "75" };
                expected = _compileService.Compile(source, language, input, output, 1000, 1000);
                Assert.AreEqual(expected, "Accepted");
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        #endregion
    }
}