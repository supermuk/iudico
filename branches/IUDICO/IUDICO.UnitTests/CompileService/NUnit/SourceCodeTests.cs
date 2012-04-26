using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem;

    [TestFixture]
    public class SourceCodeTests
    {
        private readonly CompileService compileService = new CompileService();

        #region CPP tests

        [Test]
        public void CorrectCPPSourceCodeTest()
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

        public void IncorrectCPPSourceCodeTest()
        {
            var actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPIncorrectSourceCode, 
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
            var actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.JavaCorrectSourceCode, 
                CompileServiceHelper.JavaLanguageName, 
                CompileServiceHelper.EmptyInput, 
                CompileServiceHelper.EmptyOutput, 
                CompileServiceHelper.TimeLimit, 
                CompileServiceHelper.MemoryLimit);
            Assert.AreEqual(CompileServiceHelper.AcceptedTestResult, actualResult);
        }

        public void IncorrectJavaSourceCodeTest()
        {
            var actualResult = this.compileService.Compile(
                CompileServiceLanguageSourceCode.JavaIncorrectSourceCode, 
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