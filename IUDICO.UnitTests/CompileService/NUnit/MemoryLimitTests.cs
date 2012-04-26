using System;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class MemoryLimitTests
    {
        private readonly CompileSystem.CompileService compileService = new CompileSystem.CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPMemoryLimitTest()
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
        public void IncorrectCPPMemoryLimitTest()
        {
            string actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                CompileServiceHelper.CPPLanguageName,
                CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          CompileServiceHelper.TimeLimit,
                                                          1);

            Assert.AreEqual(CompileServiceHelper.MemoryLimitOneResult, actualResult);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void NullCPPMemoryLimitTest()
        {
            string actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                CompileServiceHelper.CPPLanguageName,
                CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          CompileServiceHelper.TimeLimit,
                                                          0);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void LessNullCPPMemoryLimitTest()
        {
            string actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                CompileServiceHelper.CPPLanguageName,
                CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          CompileServiceHelper.TimeLimit,
                                                          -5);
        }

        #endregion
    }
}