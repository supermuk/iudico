using System;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem;

    [TestFixture]
    public class LanguageTests
    {
        private readonly CompileService compileService = new CompileService();

        [Test]
        public void CorrectLanguageTest()
        {
            var actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                CompileServiceHelper.CPPLanguageName, 
                CompileServiceHelper.EmptyInput, 
                CompileServiceHelper.EmptyOutput, 
                CompileServiceHelper.TimeLimit, 
                CompileServiceHelper.MemoryLimit);

            Assert.AreEqual(CompileServiceHelper.AcceptedTestResult, actualResult);
        }

        [Test]
        public void IncorrectLanguageTest()
        {
            // CS and CPP
            var actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                CompileServiceHelper.JavaLanguageName, 
                CompileServiceHelper.EmptyInput, 
                CompileServiceHelper.EmptyOutput, 
                CompileServiceHelper.TimeLimit, 
                CompileServiceHelper.MemoryLimit);

            Assert.AreEqual(CompileServiceHelper.CompilationErrorResult, actualResult);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void EmptyLanguageTest()
        {
            this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                string.Empty, 
                CompileServiceHelper.EmptyInput, 
                CompileServiceHelper.EmptyOutput, 
                CompileServiceHelper.TimeLimit, 
                CompileServiceHelper.MemoryLimit);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void NullLanguageTest()
        {
            this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                null, 
                CompileServiceHelper.EmptyInput, 
                CompileServiceHelper.EmptyOutput, 
                CompileServiceHelper.TimeLimit, 
                CompileServiceHelper.MemoryLimit);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void UndefinedLanguageTest()
        {
            this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                "undefined", 
                CompileServiceHelper.EmptyInput, 
                CompileServiceHelper.EmptyOutput, 
                CompileServiceHelper.TimeLimit, 
                CompileServiceHelper.MemoryLimit);
        }
    }
}