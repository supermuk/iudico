// -----------------------------------------------------------------------
// <copyright file="TrainingControllerTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using System;

    using IUDICO.Common.Models.Services;
    using IUDICO.TestingSystem.Controllers;

    using Moq;

    using global::NUnit.Framework;

    /// <summary>
    /// This is a bunch of tests for <see cref="TrainingController"/>
    /// </summary>
    [TestFixture]
    public class TrainingControllerTests
    {
        [Test]
        public void PassNotExistingCourseIdToPlayAction()
        {
            var curriculumServiceMock = new Mock<ICurriculumService>(MockBehavior.Strict);

            var courseServiceMock = new Mock<ICourseService>(MockBehavior.Strict);

            var lmsServiceMock = new Mock<ILmsService>(MockBehavior.Strict);

            lmsServiceMock.Setup(mock => mock.FindService<ICurriculumService>()).Returns(curriculumServiceMock.Object);
            lmsServiceMock.Setup(mock => mock.FindService<ICourseService>()).Returns(courseServiceMock.Object);

            // TODO: finish test
        }
    }
}
