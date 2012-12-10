using System;
using System.Collections.Generic;
using System.Linq;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.Analytics.NUnit
{
    [TestFixture]
    public class Tags
    {
        private AnalyticsTests tests = AnalyticsTests.GetInstance();

        [Test]
        public void CreateTag()
        {
            var tag = new Tag { Id = 1, Name = "C++" };
            this.tests.Storage.CreateTag(tag);
            Assert.AreEqual(this.tests.Storage.GetTags().Count(), 5);
        }
        [Test]
        public void EditTag()
        {
            var tag = new Tag { Id = 1, Name = "C--" };
            this.tests.Storage.EditTag(2, tag);
            Assert.AreEqual(this.tests.Storage.GetTag(2).Name, "C--");
        }
        [Test]
        public void DeleteTag()
        {
            var tag = new Tag { Id = 10, Name = "C+++" };
            this.tests.Storage.CreateTag(tag);
            this.tests.Storage.DeleteTag(10);
            Assert.AreEqual(this.tests.Storage.GetTag(10), null);
        }

        [Test]
        public void GetTagDetails()
        {
            Assert.AreEqual(this.tests.Storage.GetTagDetails(3).Tag.Name, "C#");
        } 		

        [Test]
        public void CreateDuplicateTagTest()
        {
            var tag1 = new Tag { Id = 1, Name = "C++" };
            var tag2 = new Tag { Id = 1, Name = "CC+" };
            try
            {
                this.tests.Storage.CreateTag(tag1);
                this.tests.Storage.CreateTag(tag2);
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}
