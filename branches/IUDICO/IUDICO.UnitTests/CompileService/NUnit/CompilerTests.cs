using System;
using System.IO;

using CompileSystem.Classes.Compiling;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem.Classes;

    [TestFixture]
    public class CompilerTests
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
        public void CompileTest()
        {
            var filePath = Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, this.compiler.Extension);

            try
            {
                string output;
                string error;
                var result = this.compiler.Compile(filePath, out output, out error);
                Assert.AreEqual(true, result);
            }
            catch (Exception)
            {
                File.Delete(filePath);
            }

            // remove file
            File.Delete(filePath);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CompilerBadFilePathTest()
        {
            string output, error;
            this.compiler.Compile("BadFileName", out output, out error);
        }
    }
}