using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class SeeGroupsDetails
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void GetGroupExisting()
        {
            var group = new Group { Id = 12345678, Name = "pmi31" };

            _Tests.Storage.CreateGroup(group);

            Assert.AreEqual(group, _Tests.Storage.GetGroup(group.Id));

            _Tests.Storage.DeleteGroup(group.Id);
        }
        [Test]
        public void GetGroupNonExisting()
        {
            Assert.AreEqual(null, _Tests.Storage.GetGroup(123));
        }

        protected class GroupComparer : IEqualityComparer<Group>
        {
            public bool Equals(Group x, Group y)
            {
                return x.Name == y.Name && x.Id == y.Id;
            }

            public int GetHashCode(Group obj)
            {
                return (obj.Name + obj.Id).GetHashCode();
            }
        }
    }
}