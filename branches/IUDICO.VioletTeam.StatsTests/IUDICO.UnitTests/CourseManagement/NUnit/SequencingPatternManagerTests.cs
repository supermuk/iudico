using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    class SequencingPatternManagerTests
    {
        [Test]
        public void ApplyDefaultChapterSequencing()
        {
            var seq = new Sequencing();
            SequencingPatternManager.ApplyDefaultChapterSequencing(seq);

            Assert.AreEqual(seq.ControlMode.Flow, true);
            Assert.AreEqual(seq.ControlMode.Choice, true);
        }

        [Test]
        public void ApplyControlChapterSequencing()
        {
            var seq = new Sequencing();
            SequencingPatternManager.ApplyControlChapterSequencing(seq);

            Assert.AreEqual(seq.ControlMode.Choice, false);
            Assert.AreEqual(seq.ControlMode.Flow, true);
            Assert.AreEqual(seq.ControlMode.ForwardOnly, true);
            Assert.AreEqual(seq.ControlMode.ChoiceExit, false);
            Assert.AreEqual(seq.LimitConditions.AttemptLimit, 1);
        }

        [Test]
        public void ApplyRandomSetSequencingPattern()
        {
            var seq = new Sequencing();
            const int count = 10;
            SequencingPatternManager.ApplyRandomSetSequencingPattern(seq, count);

            Assert.AreEqual(seq.RandomizationControls.ReorderChildren, true);
            Assert.AreEqual(seq.RandomizationControls.SelectionTiming, Timing.Once);
            Assert.AreEqual(seq.RandomizationControls.SelectCount, count);
        }
    }
}
