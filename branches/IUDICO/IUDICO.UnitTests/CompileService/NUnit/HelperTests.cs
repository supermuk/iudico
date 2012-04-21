using System;
using System.IO;
using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class HelperTests
    {
        private const string Source = "My source code";

        [Test]
        public void CreateFileForCompilationTest()
        {
            const string extension = "cpp";
            string currentFilePath = CompileSystem.Classes.Helper.CreateFileForCompilation(Source, extension);
            Assert.AreNotEqual(true, string.IsNullOrEmpty(currentFilePath));
            Assert.AreEqual(File.Exists(currentFilePath), true);

            string text = File.ReadAllText(currentFilePath);
            Assert.AreNotEqual(true, string.IsNullOrEmpty(text));
            Assert.AreEqual(text, Source);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CreateNullExtensionTest()
        {
            CompileSystem.Classes.Helper.CreateFileForCompilation(Source, null);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CreateEmptyExtensionTest()
        {
            CompileSystem.Classes.Helper.CreateFileForCompilation(Source, "");
        }
    }
}
