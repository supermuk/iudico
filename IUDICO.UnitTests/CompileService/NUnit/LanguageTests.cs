using System;
using CompileSystem.Classes.Testing;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class LanguageTests
    {
        private readonly CompileSystem.CompileService _compileService = new CompileSystem.CompileService();

        [Test]
        public void CorrectLanguageTest()
        {
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
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
            //CS and CPP
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
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
            _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                    "",
                                    CompileServiceHelper.EmptyInput,
                                    CompileServiceHelper.EmptyOutput,
                                    CompileServiceHelper.TimeLimit,
                                    CompileServiceHelper.MemoryLimit);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void NullLanguageTest()
        {
            _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
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
            _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                    "undefined",
                                    CompileServiceHelper.EmptyInput,
                                    CompileServiceHelper.EmptyOutput,
                                    CompileServiceHelper.TimeLimit,
                                    CompileServiceHelper.MemoryLimit);
        }
    }
}