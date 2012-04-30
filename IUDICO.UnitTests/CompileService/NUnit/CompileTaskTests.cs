using System;
using System.IO;

using CompileSystem.Classes.Compiling;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem.Classes;

    [TestFixture]
    public class CompileTastTests
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
        public void CompileTastConstructorTest()
        {
            var sourceCodeFilePath =
                Helper.CreateFileForCompilation(
                    CompileServiceLanguageSourceCode.CPPCorrectSourceCode, this.compiler.Extension);

            var compileTask = new CompileTask(this.compiler, sourceCodeFilePath);

            File.Delete(sourceCodeFilePath);
        }

        [Test]
        public void CompileTaskGetStandardStringTest()
        {
            // create default compile task
            var sourceCodeFilePath = Helper.CreateFileForCompilation("my incorrect code", this.compiler.Extension);

            var compileTask = new CompileTask(this.compiler, sourceCodeFilePath);
            var result = compileTask.Execute();

            Assert.AreEqual(result, false);
            Assert.AreEqual(string.IsNullOrEmpty(compileTask.StandardError), false);
            Assert.AreEqual(string.IsNullOrEmpty(compileTask.StandardOutput), false);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileTaskNullCompilerTest()
        {
            var sourceCodeFilePath =
                Helper.CreateFileForCompilation(
                    CompileServiceLanguageSourceCode.CPPCorrectSourceCode, this.compiler.Extension);

            var compileTask = new CompileTask(null, sourceCodeFilePath);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CompileTaskBadPathTest()
        {
            var compileTask = new CompileTask(this.compiler, "BadFilePath");
        }
    }
}