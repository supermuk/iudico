using IUDICO.UserManagement.Controllers;

using NUnit.Framework;
using System.Web.Mvc;

using IUDICO.Common.Models.Shared;

using Moq;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    using System.IO;

    using IUDICO.UnitTests.Base;

    [TestFixture]
    internal class GroupControllerTest : BaseControllerTest
    {
        protected GroupController groupController;

        [SetUp]
        public void SetUp()
        {
            this.groupController = this.GetController<GroupController>();
        }

        [Test]
        public void GroupIndex()
        {
            var result = this.groupController.Index() as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", Path.GetFileNameWithoutExtension(result.ViewName));
        }

        [Test]
        public void GroupCreate()
        {
            this.groupController.RouteData.Values["action"] = "Create";

            var result = this.groupController.Create() as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", Path.GetFileNameWithoutExtension(result.ViewName));
        }

        [Test]
        public void GroupCreateGroup()
        {
            var group = new Group { Name = "group1" };
            var result = this.groupController.Create(group) as RedirectToRouteResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void GroupCreateGroupFail()
        {
            this.groupController.RouteData.Values["action"] = "Create";

            var group = new Group();
            
            this.groupController.ModelState.AddModelError("key", "error");
            
            var result = this.groupController.Create(group) as ViewResult;

            Assert.IsNotNull(result);

            var p = Path.GetFileNameWithoutExtension(result.ViewName);
            Assert.AreEqual("Create", Path.GetFileNameWithoutExtension(result.ViewName));
        }
    }
}