using System;
using System.IO;
using CompileSystem.Classes.Compiling;
using CompileSystem.Classes.Testing;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class TesterTests
    {
        private readonly Compiler _compiler = new Compiler
        {
            Name = "CPP",
            Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CPP8\Compiler\CL.EXE"),
            Extension = "cpp",
            Arguments =
                "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"",
            CompiledExtension = "exe",
            IsNeedShortPath = true
        };

        [Test]
        public void TesterTestTest()
        {
            var filePath = CompileSystem.Classes.Helper.CreateFileForCompilation(
                    CompileServiceLanguageSourceCode.CPPCorrectSourceCode, _compiler.Extension);
            string output, error;
            _compiler.Compile(filePath, out output, out error);
            filePath = Path.ChangeExtension(filePath, _compiler.CompiledExtension);

            var testingResult = Tester.Test(filePath, "", "", 10000, 3000);
            Assert.AreEqual("Accepted", testingResult.TestResult);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TesterBadPathTestTest()
        {
            Tester.Test("badFilePath", "", "", 1, 1);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TesterBadTimelimitTestTest()
        {
            var filePath = CompileSystem.Classes.Helper.CreateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                _compiler.Extension);

            Tester.Test(filePath, "", "", -5, 1);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TesterBadMemorylimitTestTest()
        {
            var filePath = CompileSystem.Classes.Helper.CreateFileForCompilation(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                _compiler.Extension);

            Tester.Test(filePath, "", "", 1, -5);
        }
    }
}
