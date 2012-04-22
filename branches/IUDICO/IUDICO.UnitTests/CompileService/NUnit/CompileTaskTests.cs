﻿using System;
using System.IO;
using CompileSystem.Classes.Compiling;
using CompileSystem.Classes.Testing;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class CompileTastTests
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
        public void CompileTastConstructorTest()
        {
            var sourceCodeFilePath = CompileSystem.Classes.Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, _compiler.Extension);
            
            var compileTask = new CompileTask(_compiler, sourceCodeFilePath);

            File.Delete(sourceCodeFilePath);
        }

        [Test]
        public void CompileTaskGetStandardStringTest()
        {
            //create default compile task
            var sourceCodeFilePath = CompileSystem.Classes.Helper.CreateFileForCompilation(
                "my incorrect code", _compiler.Extension);

            var compileTask = new CompileTask(_compiler, sourceCodeFilePath);
            bool result = compileTask.Execute();
            
            Assert.AreEqual(result, false);
            Assert.AreEqual(string.IsNullOrEmpty(compileTask.StandardError), false);
            Assert.AreEqual(string.IsNullOrEmpty(compileTask.StandardOutput), false);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileTaskNullCompilerTest()
        {
            var sourceCodeFilePath = CompileSystem.Classes.Helper.CreateFileForCompilation(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, _compiler.Extension);

            var compileTask = new CompileTask(null, sourceCodeFilePath);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CompileTaskBadPathTest()
        {
            var compileTask = new CompileTask(_compiler, "BadFilePath");
        }
    }
}