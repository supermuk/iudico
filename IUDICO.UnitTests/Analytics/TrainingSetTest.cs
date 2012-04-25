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
            var set = new TrainingSet(3);

            set.AddRecord(new[] { 3.0, 4.0, 5.0 });
            set.AddRecord(new[] { 3.0, 4.0, 5.0 });
            set.AddRecord(new[] { 3.0, 4.0, 5.0 });
            
            Assert.AreEqual(set.GetCountOfRecords(), 3);
            
            try
            {
                set.AddRecord(new[] { 3.0, 4.0, 5.0, 6.0 });
            }
            catch (Exception)
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
            var set = new TrainingSet(3);

            Assert.AreEqual(set.GetDimensionsCount(), 3);
        }
    }
}
