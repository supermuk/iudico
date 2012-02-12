using IUDICO.TestingSystem.Models.VOs;
using Microsoft.LearningComponents;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    internal class TrainingTests
    {
        private Training training;

        [SetUp]
        public void TrainingTestsSetUp()
        {
            training = new Training(123, 456, 789, AttemptStatus.Active, 34.6F);
        }

        [Test]
        public void TrainingPropertiesTest()
        {
            Assert.That(training.AttemptId, Is.EqualTo(789));
            Assert.That(training.AttemptStatusProp, Is.EqualTo(AttemptStatus.Active));
            Assert.That(training.OrganizationId, Is.EqualTo(456));
            Assert.That(training.PackageId, Is.EqualTo(123));
            Assert.That(training.TotalPoints, Is.EqualTo(34.6F));
            Assert.That(training.UploadDateTime, Is.Null);
        }

        [Test]
        public void TrainingPlayIdPropertyTest()
        {
            TrainingTestsSetUp();

            training.AttemptStatusProp = null;
            Assert.AreEqual(training.PlayId, training.OrganizationId);

            training.AttemptStatusProp = AttemptStatus.Active;
            Assert.AreEqual(training.PlayId, training.AttemptId);

            training.AttemptStatusProp = AttemptStatus.Abandoned;
            Assert.AreEqual(training.PlayId, training.AttemptId);

            training.AttemptStatusProp = AttemptStatus.Suspended;
            Assert.AreEqual(training.PlayId, training.AttemptId);

            training.AttemptStatusProp = AttemptStatus.Completed;
            Assert.AreEqual(training.PlayId, training.OrganizationId);
        }

        [Test]
        public void TrainingPlayCreateFromDataRowTest()
        {
            // DataTable dataTable = new DataTable();
            // dataTable.Columns.Add("PackageId", new TypeDelegator(PackageItem.ItemTypeName).GetType());
            // dataTable.Columns.Add("OrganizationId");
            // dataTable.Columns.Add("AttemptId");
            // dataTable.Columns.Add("AttemptStatus");
            // dataTable.Columns.Add("TotalPoints");
            // DataRow dataRow = dataTable.NewRow();
            // dataRow["PackageId"]=Pa;
            // training=new Training(dataRow);

            // Assert.AreEqual(training.PackageId,123);
        }
    }
}