using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class DeleteGroup
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        
        [Test]
        public void DeleteGroupExisting()
        {
            var group = new Group { Id = 123, Name = "pmi31" };
            
            _Tests.Storage.CreateGroup(group);
            _Tests.Storage.DeleteGroup(group.Id);

            Assert.IsTrue(_Tests.Storage.GetGroup(group.Id) == null);
        }
        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DeleteGroupNonExisting()
        {
            _Tests.Storage.DeleteGroup(123);
        }
    }
}