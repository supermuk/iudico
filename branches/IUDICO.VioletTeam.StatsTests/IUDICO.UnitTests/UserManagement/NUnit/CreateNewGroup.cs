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
            var group = new Group { Id = 12578, Name = "pmp41" };
            
            _Tests.Storage.CreateGroup(group);

            Assert.IsTrue(group.Name == _Tests.Storage.GetGroup(group.Id).Name && group.Id == _Tests.Storage.GetGroup(group.Id).Id);
            
            _Tests.Storage.DeleteGroup(group.Id);
        }
        [Test]
        public void CreateGroupInvalid()
        {
            var group = new Group { Id = 4567, Name = "pma51" };

            _Tests.Storage.CreateGroup(group);

            Assert.IsTrue(group.Name == _Tests.Storage.GetGroup(group.Id).Name && group.Id == _Tests.Storage.GetGroup(group.Id).Id);
            
            _Tests.Storage.DeleteGroup(group.Id);
        }
    }
}
