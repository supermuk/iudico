using System;
using CompileSystem.Classes.Testing;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class CompileServiceTests
    {
        private readonly CompileSystem.CompileService _compileService = new CompileSystem.CompileService();
        private readonly string[] _input = new string[0];
        private readonly string[] _output = new string[0];
        private readonly string[] _inputStrings = new string[1] { "1 2" };
        private readonly string[] _outputStrings = new string[1] { "12" };
        private readonly string _language = "CPP8";

        [Test]
        public void CompileServiceCompileTest()
        {
            string expected = _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                                      _language, _inputStrings, _outputStrings, 1000, 1000);
            Assert.AreEqual(expected, "Accepted");
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceIncorrectLanguageTest()
        {
            _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode,
                                    "IncorrectLanguageName", _input, _output, 100, 100);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceEmptyLanguageTest()
        {
            _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode, "", _input, _output, 100, 100);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceIncorrectTimelimitTest()
        {
            _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode, _language, _inputStrings,
                                    _outputStrings, -5, 100);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CompileServiceIncorrectMemorylimitTest()
        {
            _compileService.Compile(CompileServiceLanguageSourceCode.CPPCorrectSourceCode, _language, _inputStrings,
                                    _outputStrings, 100, -5);
        }
    }
}
