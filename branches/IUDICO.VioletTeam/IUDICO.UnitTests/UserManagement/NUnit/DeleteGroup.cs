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
    public class DeleteGroup
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        [Test]
        public void DeleteGroupSuccess()
        {
            var group = new Group() { Deleted = false, Id = 333, Name = "pma31" };
            _Tests.Storage.CreateGroup(group);
            _Tests.Storage.DeleteGroup(group.Id);
            Assert.AreEqual(_Tests.Storage.GetGroups().Count(),1 );
        }
    }
}