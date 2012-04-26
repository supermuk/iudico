using System;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class TimeLimitTests
    {
        private readonly CompileSystem.CompileService compileService = new CompileSystem.CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPTimeLimitTest()
        {
            string actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                CompileServiceHelper.CPPLanguageName,
                CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          CompileServiceHelper.TimeLimit,
                                                          CompileServiceHelper.MemoryLimit);

            Assert.AreEqual(CompileServiceHelper.AcceptedTestResult, actualResult);
        }

        [Test]
        public void IncorrectCPPTimeLimitTest()
        {
            string actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                CompileServiceHelper.CPPLanguageName,
                CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          1,
                                                          CompileServiceHelper.MemoryLimit);

            Assert.AreNotEqual(CompileServiceHelper.AcceptedTestResult, actualResult);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void NullCPPTimeLimitTest()
        {
            string actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                CompileServiceHelper.CPPLanguageName,
                CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          0,
                                                          CompileServiceHelper.MemoryLimit);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void LessNullCPPTimeLimitTest()
        {
            string actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                CompileServiceHelper.CPPLanguageName,
                CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          -5,
                                                          CompileServiceHelper.MemoryLimit);
        }

        #endregion
    }
}