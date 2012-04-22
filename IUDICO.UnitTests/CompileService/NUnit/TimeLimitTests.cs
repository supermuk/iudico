using System;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class TimeLimitTests
    {
        private readonly CompileSystem.CompileService _compileService = new CompileSystem.CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPTimeLimitTest()
        {

            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
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
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                          CompileServiceHelper.CPPLanguageName,
                                                          CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          1,
                                                          CompileServiceHelper.MemoryLimit);

            Assert.AreEqual(CompileServiceHelper.TimeLimitOneResult, actualResult);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void NullCPPTimeLimitTest()
        {
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
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
            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                          CompileServiceHelper.CPPLanguageName,
                                                          CompileServiceHelper.Input,
                                                          CompileServiceHelper.Output,
                                                          -5,
                                                          CompileServiceHelper.MemoryLimit);
        }

        #endregion
    }
}