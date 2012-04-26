using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem;

    [TestFixture]
    public class InputOutputParameterTests
    {
        private readonly CompileService compileService = new CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPInputOutputParamTest()
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
        public void IncorrectOneCPPInputOutputParamTest()
        {
            // first parameter fail
            string[] inputParam = { "2 5", "7 5" };
            string[] outputParam = { "35", "75" };

            var actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
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
            // second parameter fail
            string[] inputParam = { "2 5", "7 5" };
            string[] outputParam = { "25", "95" };

            var actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
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