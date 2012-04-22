using System;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class InputOutputParameterTests
    {
        private readonly CompileSystem.CompileService _compileService = new CompileSystem.CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPInputOutputParamTest()
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
        public void IncorrectOneCPPInputOutputParamTest()
        {
            //first parameter fail
            string[] inputParam = {"2 5", "7 5"};
            string[] outputParam = {"35", "75"};

            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPIncorrectSourceCode,
                                                          CompileServiceHelper.CPPLanguageName,
                                                          inputParam,
                                                          outputParam,
                                                          CompileServiceHelper.TimeLimit,
                                                          CompileServiceHelper.MemoryLimit);

            Assert.AreEqual(CompileServiceHelper.WrongAnswerOneResult, actualResult);
        }

        [Test]
        public void IncorrectTwoCPPInputOutputParamTest()
        {
            //second parameter fail
            string[] inputParam = { "2 5", "7 5" };
            string[] outputParam = { "25", "95" };

            string actualResult = _compileService.Compile(CompileServiceLanguageSourceCode.CPPIncorrectSourceCode,
                                                          CompileServiceHelper.CPPLanguageName,
                                                          inputParam,
                                                          outputParam,
                                                          CompileServiceHelper.TimeLimit,
                                                          CompileServiceHelper.MemoryLimit);

            Assert.AreEqual(CompileServiceHelper.WrongAnswerTwoResult, actualResult);
        }

        #endregion
    }
}