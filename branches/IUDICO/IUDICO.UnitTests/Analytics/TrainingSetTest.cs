using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using IUDICO.Analytics.Models.AnomalyDetectionAlg;

namespace IUDICO.UnitTests.Analytics
{
    [TestFixture]
    public class TrainingSetTest
    {
        [Test]
        [Category("TrainingSetAddTest")]
        public void TrainingSetAdd()
        {
            TrainingSet set = new TrainingSet(3);
            set.AddRecord(new double[] { 3.0, 4.0, 5.0 });
            set.AddRecord(new double[] { 3.0, 4.0, 5.0 });
            set.AddRecord(new double[] { 3.0, 4.0, 5.0 });
            Assert.AreEqual(set.GetCountOfRecords(), 3);
            try
            {
                set.AddRecord(new double[] { 3.0, 4.0, 5.0, 6.0 });
            }
            catch (Exception ex)
            {
                Assert.Pass();
                return;
            }
            Assert.Fail();
        }

        [Test]
        [Category("TrainingSetGetDimmensionCountTest")]
        public void TrainingSetGetDimmensionCount()
        {
            TrainingSet set = new TrainingSet(3);
            Assert.AreEqual(set.GetDimensionsCount(), 3);
        }
    }
}
