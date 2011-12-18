﻿using System;
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
    public class CreateNewGroup
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        [Test]
        public void CreateGroup()
        {
            var group = new Group() { Deleted = false, Id = 123, Name = "pmi31" };
            _Tests.Storage.CreateGroup(group);
            Assert.AreEqual(group.Name, _Tests.Storage.GetGroup(group.Id).Name);
            _Tests.Storage.DeleteGroup(group.Id);
        }
    }
}