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
        public void DeleteGroupSuccess()
        {
            var group = new Group { Deleted = false, Id = 333123456, Name = "pma31" };
            
            _Tests.Storage.CreateGroup(group);
            _Tests.Storage.DeleteGroup(group.Id);

            Assert.IsTrue(_Tests.Storage.GetGroup(group.Id) == null);
        }
    }
}