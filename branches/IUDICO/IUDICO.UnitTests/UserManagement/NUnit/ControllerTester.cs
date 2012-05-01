using IUDICO.UserManagement.Controllers;

using NUnit.Framework;
using System.Web.Mvc;

using IUDICO.Common.Models.Shared;

using Moq;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Routing;

    using Moq.Protected;

    [TestFixture]
    internal class ControllerTester
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        protected Mock<ControllerBase> mockGroup;
        protected Mock<UserController> mockUser;

        protected GroupController GroupController { get { return this.mockGroup.Object; } }
        protected UserController UserController { get { return this.mockUser.Object; } }

        [SetUp]
        public void SetUp()
        {
            ControllerBase groupController = new GroupController(this.tests.Storage);

            this.mockGroup = Mock.Get(groupController);
            this.mockGroup.Protected().Setup<ViewResult>("View").Returns(new ViewResult());
            this.mockGroup.Protected().Setup<PartialViewResult>("PartialView").Returns(new PartialViewResult());

//            this.mockUser = new Mock<UserController>();
//            this.mockUser.Protected().Setup<ViewResult>("View").Returns(new ViewResult());
//            this.mockUser.Protected().Setup<PartialViewResult>("PartialView").Returns(new PartialViewResult());

            var httpContextBase = new Mock<HttpContextBase>();
            var httpRequestBase = new Mock<HttpRequestBase>();
            var respone = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var routes = new RouteCollection();

            httpContextBase.Setup(x => x.Response).Returns(respone.Object);
            httpContextBase.Setup(x => x.Request).Returns(httpRequestBase.Object);
            httpContextBase.Setup(x => x.Session).Returns(session.Object);
            httpRequestBase.Setup(x => x.Form).Returns(new NameValueCollection());

            this.GroupController.ControllerContext = new ControllerContext(httpContextBase.Object, new RouteData(), this.GroupController);
            this.GroupController.Url = new UrlHelper(new RequestContext(this.GroupController.HttpContext, new RouteData()), routes);
        }

        [Test]
        public void GroupIndex()
        {
            this.GroupController.RouteData.Values.Add("action", "Index");
            var result = this.GroupController.Index() as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void GroupCreate()
        {
            var result = this.GroupController.Create() as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void GroupCreateGroup()
        {
            var group = new Group { Name = "group1" };
            var result = this.GroupController.Create(group) as RedirectToRouteResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            this.tests.MockStorage.Verify(s => s.CreateGroup(group), Times.Once());
        }

        [Test]
        public void GroupCreateGroupFail()
        {
            var group = new Group();
            
            this.GroupController.ModelState.AddModelError("key", "error");
            
            var result = this.GroupController.Create(group) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);

            this.tests.MockStorage.Verify(s => s.CreateGroup(group), Times.Never());
        }
    }
}