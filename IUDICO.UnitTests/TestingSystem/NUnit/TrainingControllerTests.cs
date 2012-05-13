// -----------------------------------------------------------------------
// <copyright file="TrainingControllerTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using System;
    using System.Web.Mvc;

    using Castle.Windsor;

    using IUDICO.Common;
    using IUDICO.Common.Controllers;
    using IUDICO.Common.Models.Services;
    using IUDICO.Common.Models.Shared;
    using IUDICO.Common.Models.Shared.DisciplineManagement;
    using IUDICO.LMS.Models;
    using IUDICO.TestingSystem.Controllers;
    using IUDICO.TestingSystem.Models;
    using IUDICO.TestingSystem.ViewModels;

    using Moq;

    using global::NUnit.Framework;

    /// <summary>
    /// This is a bunch of tests for <see cref="TrainingController"/>
    /// </summary>
    [TestFixture]
    public class TrainingControllerTests
    {
        #region Tested Object

        private TrainingController trainingController;

        #endregion

        #region Mocks

        private Mock<LmsService> lmsServiceMock;

        private Mock<IWindsorContainer> windsorContainerMock;

        private Mock<IMlcProxy> mlcProxyMock;

        private Mock<IUserService> userServiceMock;

        private Mock<ICourseService> courseServiceMock;

        private Mock<ICurriculumService> curriculumServiceMock;

        #endregion

        #region Data

        private Course dummyCourse;

        private CurriculumChapterTopic dummyCurriculumChapterTopic;

        private User dummyUser;

        private const long DummyAttemptId = 23;

        private const int DummyCurriculumChapterTopicId = 9474;

        private const TopicTypeEnum DummyTopicType = TopicTypeEnum.Theory;

        private const int DummyCourseId = 394;

        #endregion

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            // defining mocks
            this.windsorContainerMock = new Mock<IWindsorContainer>();
            this.lmsServiceMock = new Mock<LmsService>(this.windsorContainerMock.Object);

            this.userServiceMock = new Mock<IUserService>();
            this.courseServiceMock = new Mock<ICourseService>();
            this.curriculumServiceMock = new Mock<ICurriculumService>();

            this.mlcProxyMock = new Mock<IMlcProxy>();

            this.trainingController = new TrainingController(this.mlcProxyMock.Object);

            PluginController.LmsService = this.lmsServiceMock.Object;

            // setuping mocks
            this.mlcProxyMock.Setup(
                proxy => proxy.GetAttemptId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TopicTypeEnum>())).Returns(
                    DummyAttemptId);

            this.windsorContainerMock.Setup(container => container.Resolve<IUserService>()).Returns(
                this.userServiceMock.Object);
            this.windsorContainerMock.Setup(container => container.Resolve<ICourseService>()).Returns(
                this.courseServiceMock.Object);
            this.windsorContainerMock.Setup(container => container.Resolve<ICurriculumService>()).Returns(
                this.curriculumServiceMock.Object);

            this.dummyCurriculumChapterTopic = new CurriculumChapterTopic
                { Id = DummyCurriculumChapterTopicId, Topic = new Topic { Name = "C++ Testing" } };
            this.dummyCourse = new Course { Id = DummyCourseId };
            this.dummyUser = new User();
        }

        [SetUp]
        public void SetUpTest()
        {
            this.curriculumServiceMock.Setup(service => service.GetCurriculumChapterTopicById(It.IsAny<int>())).Returns(
                this.dummyCurriculumChapterTopic);

            this.curriculumServiceMock.Setup(
                service =>
                service.CanPassCurriculumChapterTopic(
                    It.IsAny<User>(), It.IsAny<CurriculumChapterTopic>(), It.IsAny<TopicTypeEnum>())).Returns(true);

            this.courseServiceMock.Setup(service => service.GetCourse(It.IsAny<int>())).Returns(this.dummyCourse);

            this.userServiceMock.Setup(service => service.GetCurrentUser()).Returns(this.dummyUser);
        }

        /// <summary>
        /// Incapsulates call to action Play of <see cref="TrainingController"/>.
        /// </summary>
        /// <returns><see cref="ActionResult"/> result.</returns>
        protected ActionResult CallPlayAction()
        {
            return this.trainingController.Play(DummyCurriculumChapterTopicId, DummyCourseId, DummyTopicType);
        }

        [Test]
        public void PassNotExistingCurriculumChapterTopicIdToPlayAction()
        {
            // setup
            this.curriculumServiceMock.Setup(service => service.GetCurriculumChapterTopicById(It.IsAny<int>())).Throws<InvalidOperationException>().Verifiable();

            // call
            var result = this.CallPlayAction();

            // verify
            this.curriculumServiceMock.Verify();

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);

            Assert.AreEqual(
                "~/Plugins/IUDICO.TestingSystem.DLL/IUDICO.TestingSystem/Views/Training/Error.aspx", viewResult.ViewName);
            Assert.AreEqual("~/Views/Shared/Site.Master", viewResult.MasterName);

            Assert.AreEqual(
                Localization.GetMessage("Topic_Not_Found", "IUDICO.TestingSystem"), viewResult.ViewData.Model);
        }

        [Test]
        public void PassNonExistingCourseIdToPlayAction()
        {
            // setup
            this.courseServiceMock.Setup(service => service.GetCourse(It.IsAny<int>())).Throws<InvalidOperationException>().Verifiable();

            // call
            var result = this.CallPlayAction();

            // verify
            this.courseServiceMock.Verify();

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);

            Assert.AreEqual(
                "~/Plugins/IUDICO.TestingSystem.DLL/IUDICO.TestingSystem/Views/Training/Error.aspx", viewResult.ViewName);
            Assert.AreEqual("~/Views/Shared/Site.Master", viewResult.MasterName);
            Assert.AreEqual(
                Localization.GetMessage("Course_Not_Found", "IUDICO.TestingSystem"), viewResult.ViewData.Model);
        }

        [Test]
        public void NotAllowedCurriculumChapterTopic()
        {
            // setup
            this.curriculumServiceMock.Setup(
                service =>
                service.CanPassCurriculumChapterTopic(It.IsAny<User>(), It.IsAny<CurriculumChapterTopic>(), It.IsAny<TopicTypeEnum>())).Returns(false).Verifiable();

            // call
            var result = this.CallPlayAction();

            // verify
            this.courseServiceMock.Verify();

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);

            Assert.AreEqual(
                "~/Plugins/IUDICO.TestingSystem.DLL/IUDICO.TestingSystem/Views/Training/Error.aspx", viewResult.ViewName);
            Assert.AreEqual("~/Views/Shared/Site.Master", viewResult.MasterName);
            Assert.AreEqual(
                Localization.GetMessage("Not_Allowed_Pass_Topic", "IUDICO.TestingSystem"), viewResult.ViewData.Model);
        }

        [Test]
        public void PlayActionInitializesServicesProxy()
        {
            // call
            this.CallPlayAction();

            // verify
            Assert.AreSame(this.lmsServiceMock.Object, ServicesProxy.Instance.LmsService);
        }

        [Test]
        public void PlayActionReturnsCorrectView()
        {
            // call
            var result = this.CallPlayAction();

            // verify
            var viewResult = result as ViewResult;

            Assert.NotNull(viewResult);
            Assert.AreEqual(
                "~/Plugins/IUDICO.TestingSystem.DLL/IUDICO.TestingSystem/Views/Training/Play.aspx", viewResult.ViewName);
        }

        [Test]
        public void PlayActionReturnsStronglyTypedModel()
        {
            // call
            var result = this.CallPlayAction();

            // verify
            var viewResult = result as ViewResult;
            if (viewResult == null)
            {
                Assert.Fail("Result of calling action Play is not ViewResult");
            }

            Assert.IsInstanceOf<PlayModel>(viewResult.ViewData.Model);
        }

        [Test]
        public void PlayActionReturnsCorrectlyInitializedModel()
        {
            // call
            var result = this.CallPlayAction();

            // verify
            var viewResult = result as ViewResult;
            if (viewResult == null)
            {
                Assert.Fail("Result of calling action Play is not ViewResult");
            }

            var playModel = viewResult.ViewData.Model as PlayModel;
            if (playModel == null)
            {
                Assert.Fail("Model is not PlayModel");
            }

            Assert.AreEqual(DummyAttemptId, playModel.AttemptId);
            Assert.AreEqual(DummyCurriculumChapterTopicId, playModel.CurriculumChapterTopicId);
            Assert.IsTrue(playModel.TopicName.Contains(this.dummyCurriculumChapterTopic.Topic.Name));
            Assert.AreEqual(DummyTopicType, playModel.TopicType);
        }
    }
}