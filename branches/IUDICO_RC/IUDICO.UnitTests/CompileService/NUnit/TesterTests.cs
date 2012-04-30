using System;
using System.IO;

using CompileSystem.Classes.Compiling;
using CompileSystem.Classes.Testing;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem.Classes;

    [TestFixture]
    public class TesterTests
    {
        private readonly Compiler compiler = new Compiler
            {
                Name = "CPP", 
                Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CPP8\Compiler\CL.EXE"), 
                Extension = "cpp", 
                Arguments = "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"", 
                CompiledExtension = "exe", 
                IsNeedShortPath = true
            };

        [Test]
        public void TesterTestTest()
        {
            var filePath = Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, this.compiler.Extension);
            string output, error;
            this.compiler.Compile(filePath, out output, out error);
            filePath = Path.ChangeExtension(filePath, this.compiler.CompiledExtension);

            var testingResult = Tester.Test(filePath, string.Empty, string.Empty, 10000, 3000);
            Assert.AreEqual("Accepted", testingResult.TestResult);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TesterBadPathTestTest()
        {
            Tester.Test("badFilePath", string.Empty, string.Empty, 1, 1);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TesterBadTimelimitTestTest()
        {
            var filePath = Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, this.compiler.Extension);

            Tester.Test(filePath, string.Empty, string.Empty, -5, 1);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TesterBadMemorylimitTestTest()
        {
            var filePath = Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, this.compiler.Extension);

            Tester.Test(filePath, string.Empty, string.Empty, 1, -5);
        }
    }
}