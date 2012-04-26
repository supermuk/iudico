using System;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem;

    [TestFixture]
    public class CompileServiceTests
    {
        private readonly CompileService compileService = new CompileService();

        private readonly string[] input = new string[0];

        private readonly string[] output = new string[0];

        private readonly string[] inputStrings = new[] { "1 2" };

        private readonly string[] outputStrings = new[] { "12" };

        private const string Language = "CPP8";

        [Test]
        public void CompileServiceCompileTest()
        {
            var expected = this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                Language, 
                this.inputStrings, 
                this.outputStrings, 
                1000, 
                3000);

            Assert.AreEqual(expected, "Accepted");
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceIncorrectLanguageTest()
        {
            this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                "IncorrectLanguageName", 
                this.input, 
                this.output, 
                100, 
                100);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceEmptyLanguageTest()
        {
            this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, string.Empty, this.input, this.output, 100, 100);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceIncorrectTimelimitTest()
        {
            this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                Language, 
                this.inputStrings, 
                this.outputStrings, 
                -5, 
                100);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceIncorrectMemorylimitTest()
        {
            this.compileService.Compile(
                CompileServiceLanguageSourceCode.CPPCorrectSourceCode, 
                Language, 
                this.inputStrings, 
                this.outputStrings, 
                100, 
                -5);
        }
    }
}