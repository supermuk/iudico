using System;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem;

    [TestFixture]
    public class MemoryLimitTests
    {
        private readonly CompileService compileService = new CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPMemoryLimitTest()
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
        public void IncorrectCPPMemoryLimitTest()
        {
            var actualResult = this.compileService.Compile(
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
            var actualResult = this.compileService.Compile(
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
            var actualResult = this.compileService.Compile(
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