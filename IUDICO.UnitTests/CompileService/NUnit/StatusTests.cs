using System;

using CompileSystem.Classes.Testing;

using NUnit.Framework;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class StatusTests
    {
        [Test]
        public void StatusConstructorTest()
        {
            const string TestResult = "Accepted";
            var target = new Status(TestResult);

            Assert.AreEqual(false, TestResult == null);
            Assert.AreEqual(TestResult, target.TestResult);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void StatusNullStringConstructorTest()
        {
            var target = new Status(null);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void StatusEmptyStringConstructorTest()
        {
            var target = new Status(string.Empty);
        }
    }
}