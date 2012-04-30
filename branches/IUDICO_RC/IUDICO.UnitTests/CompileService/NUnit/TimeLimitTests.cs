using System;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem;

    [TestFixture]
    public class TimeLimitTests
    {
        private readonly CompileService compileService = new CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPTimeLimitTest()
        {
            var actualResult = this.compileService.Compile(
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
            var actualResult = this.compileService.Compile(
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
            var actualResult = this.compileService.Compile(
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
            var actualResult = this.compileService.Compile(
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