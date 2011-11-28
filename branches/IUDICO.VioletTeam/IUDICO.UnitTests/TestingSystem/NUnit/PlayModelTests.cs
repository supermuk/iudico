using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.TestingSystem.Models.VOs;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    class PlayModelTests
    {
        private PlayModel playModel;

        [SetUp]
        public void PlayModelTestsSetUp()
        {
            playModel = new PlayModel() { AttemptId = 12345, ThemeId = 1 };
        }
        [Test]
        public void PlayModelPropertiesTest()
        {
            PlayModelTestsSetUp();
            Assert.AreEqual(playModel.AttemptId, 12345);
            Assert.AreEqual(playModel.ThemeId, 1);
        }
    }
}
