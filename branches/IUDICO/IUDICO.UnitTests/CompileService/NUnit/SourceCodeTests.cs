using System;
using CompileSystem.Classes.Testing;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class SourceCodeTests
    {
        private readonly CompileSystem.CompileService _compileService = new CompileSystem.CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPSourceCodeTest()
        {
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                          CompileServiceHelper.CPPLanguageName,
                                                          CompileServiceHelper.EmptyInput,
                                                          CompileServiceHelper.EmptyOutput,
                                                          CompileServiceHelper.TimeLimit,
                                                          CompileServiceHelper.MemoryLimit);
            Assert.AreEqual(CompileServiceHelper.AcceptedTestResult, actualResult);
        }

        public void IncorrectCPPSourceCodeTest()
        {
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPIncorrectSourceCode,
                                                          CompileServiceHelper.CPPLanguageName,
                                                          CompileServiceHelper.EmptyInput,
                                                          CompileServiceHelper.EmptyOutput,
                                                          CompileServiceHelper.TimeLimit,
                                                          CompileServiceHelper.MemoryLimit);
            Assert.AreEqual(CompileServiceHelper.CompilationErrorResult, actualResult);
        }

        #endregion

        #region Java tests

        [Test]
        public void CorrectJavaSourceCodeTest()
        {
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.JavaCorrectSourceCode,
                                                          CompileServiceHelper.JavaLanguageName,
                                                          CompileServiceHelper.EmptyInput,
                                                          CompileServiceHelper.EmptyOutput,
                                                          CompileServiceHelper.TimeLimit,
                                                          CompileServiceHelper.MemoryLimit);
            Assert.AreEqual(CompileServiceHelper.AcceptedTestResult, actualResult);
        }

        public void IncorrectJavaSourceCodeTest()
        {
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.JavaIncorrectSourceCode,
                                                          CompileServiceHelper.JavaLanguageName,
                                                          CompileServiceHelper.EmptyInput,
                                                          CompileServiceHelper.EmptyOutput,
                                                          CompileServiceHelper.TimeLimit,
                                                          CompileServiceHelper.MemoryLimit);
            Assert.AreEqual(CompileServiceHelper.CompilationErrorResult, actualResult);
        }

        #endregion
    }
}