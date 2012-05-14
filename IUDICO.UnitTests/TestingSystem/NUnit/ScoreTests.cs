// -----------------------------------------------------------------------
// <copyright file="ScoreTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using System;

    using IUDICO.Common.Models.Shared.Statistics;

    using global::NUnit.Framework;

    /// <summary>
    /// Bunch of tests for <see cref="Score"/>.
    /// </summary>
    [TestFixture]
    public class ScoreTests
    {
        [Test]
        public void ConstructScoreWithValidParams()
        {
            float? scaledScore = -0.3f;
            var score = new Score(scaledScore);

            Assert.AreEqual(scaledScore, score.ScaledScore);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructScoreWithInvalidParams()
        {
            new Score(94f);
        }

        [Test]
        public void TestToPercentsMethod()
        {
            var score = new Score(0.65f);
            Assert.AreEqual(65, score.ToPercents());
        }
    }
}
