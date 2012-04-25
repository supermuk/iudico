using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class GetGroups
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void GetGroupsNonDeleted()
        {
            var groups = new List<Group>
                {
                    new Group { Deleted = false, Id = 1, Name = "pmi51" },
                    new Group { Deleted = false, Id = 2, Name = "pmi41" },
                    new Group { Deleted = false, Id = 3, Name = "pmi31" }
                };

            foreach (var group in groups)
            {
                this.tests.Storage.CreateGroup(group);
            }

            Assert.IsTrue(this.TestGroup(this.tests.Storage.GetGroups().Where(g => g.Deleted == false), groups));
        }

        protected bool TestGroup(IEnumerable<Group> users, IEnumerable<Group> inserted)
        {
            return inserted.Except(users, new GroupComparer()).Count() == 0;
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