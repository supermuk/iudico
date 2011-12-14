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
    public class SeeGroupsDetails
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        [Test]
        public void GetGroup()
        {
            var group = new Group() { Deleted = false, Id = 323, Name = "pmp41" };
            _Tests.Storage.CreateGroup(group);
            Assert.AreEqual(group.Name, _Tests.Storage.GetGroup(group.Id).Name);
        }
    }
}