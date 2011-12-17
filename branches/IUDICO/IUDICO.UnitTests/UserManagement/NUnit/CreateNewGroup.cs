using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;
namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class CreateNewGroup
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        
        [Test]
        public void CreateGroupValid()
        {
            var group = new Group { Id = 12345678, Name = "pmi31" };
            
            _Tests.Storage.CreateGroup(group);

            Assert.AreEqual(group, _Tests.Storage.GetGroup(group.Id));
            
            _Tests.Storage.DeleteGroup(group.Id);
        }
        [Test]
        public void CreateGroupInvalid()
        {
            var group = new Group { Id = 12345678, Name = "pmi31" };

            _Tests.Storage.CreateGroup(group);

            Assert.AreEqual(group, _Tests.Storage.GetGroup(group.Id));

            _Tests.Storage.DeleteGroup(group.Id);
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
