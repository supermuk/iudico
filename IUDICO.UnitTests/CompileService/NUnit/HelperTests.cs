using System;
using System.IO;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    using CompileSystem.Classes;
    using CompileSystem;

    [TestFixture]
    public class HelperTests
    {
        private const string Source = "My source code";

          [Test]
        public void CreateFileForCompilationTest()
        {
            const string Extension = "cpp";
            var currentFilePath = Helper.CreateFileForCompilation(Source, Extension);
            Assert.AreNotEqual(true, string.IsNullOrEmpty(currentFilePath));
            Assert.AreEqual(File.Exists(currentFilePath), true);

            var text = File.ReadAllText(currentFilePath);
            Assert.AreNotEqual(true, string.IsNullOrEmpty(text));
            Assert.AreEqual(text, Source);
        }
          [Test]
          [ExpectedException(typeof(Exception))]
          public void CreateInvalidFileForCompilationTest()
          {
            const string Extension = "BadExtension";
            var currentFilePath = Helper.CreateFileForCompilation(Source, Extension);
            Assert.AreNotEqual(true, string.IsNullOrEmpty(currentFilePath));
            Assert.AreEqual(File.Exists(currentFilePath), true);

            var text = File.ReadAllText(currentFilePath);
            Assert.AreNotEqual(true, string.IsNullOrEmpty(text));
            Assert.AreEqual(text, Source);
          }
        [Test]
        [ExpectedException(typeof(Exception))]
        public void CreateNullExtensionTest()
        {
            Helper.CreateFileForCompilation(Source, null);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CreateEmptyExtensionTest()
        {
            Helper.CreateFileForCompilation(Source, string.Empty);
        }
    }
}