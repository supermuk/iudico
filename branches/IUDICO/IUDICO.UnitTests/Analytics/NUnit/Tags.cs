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
            int id = 1;
            while (this.tests.Storage.GetTag(id) != null)
            {
                ++id;
            }
            var tag = new Tag { Id = id, Name = "Test tag name" };
            int count = this.tests.Storage.GetTags().Count();
            this.tests.Storage.CreateTag(tag);
            Assert.AreEqual(this.tests.Storage.GetTags().Count(), count + 1);
            this.tests.Storage.DeleteTag(id);
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

        /// <summary>
        /// Author - Pohlod Yaroslav
        /// </summary>
        [Test]
        public void CreateDuplicateTagTest()
        {
            // Create 2 same tags for test
            var tag1 = new Tag { Id = 1, Name = "C++" };
            var tag2 = new Tag { Id = 1, Name = "C++" };
            try
            {
                //try to create both tags
                this.tests.Storage.CreateTag(tag1);
                this.tests.Storage.CreateTag(tag2);
            }
            catch (Exception)
            {
                // Pass test if was thrown excepton about duplicate.
                Assert.Pass();
            }
            // Exception wasn`t thrown
            Assert.Fail();
        }
    }
}
