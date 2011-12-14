using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models;
using IUDICO.Common.Models.Notifications;
using Moq;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class GetGroups
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        [Test]
        public void GetGroupsSuccess()
        {
            var groups = new List<Group>
                            {
                                new Group { Deleted = false, Id = 123, Name = "pmi51" },
                                new Group{ Deleted = false, Id = 122, Name = "pmi21" },
                                new Group{ Deleted = false, Id = 13, Name = "pmi11" }
                            };
            foreach (var group in groups)
            {
                _Tests.Storage.CreateGroup(group);
            }
            Assert.IsTrue(TestGroup(_Tests.Storage.GetGroups(), groups));
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