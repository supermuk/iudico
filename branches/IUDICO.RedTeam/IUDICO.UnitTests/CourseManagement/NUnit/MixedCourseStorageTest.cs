using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models.Storage;
using NUnit.Framework;
using Moq;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class MixedCourceStorageTest
    {
        protected CourseManagementTest _Tests;

        [SetUp]
        public void Initialize()
        {
            _Tests = CourseManagementTest.GetInstance();
        }

        #region Test Course methods

        #region Test GetCourses methods
        [Test]
        [Category("GetCoursesMethods")]
        public void GetCourseIDTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            Course course = mcs.GetCourse(1);
            Assert.AreEqual(course.Id, 1);
            Assert.AreEqual(course.Name, "Some course");
        }

        [Test]
        [Category("GetCoursesMethods")]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetCourseIDNotFoundTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            Course course = mcs.GetCourse(333);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourses();
            Assert.AreEqual(courses.Count(), 3);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserIdTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var users = mcs.GetCourseUsers(1);
            var courses = mcs.GetCourses(users.ToArray()[0].Id);
            Assert.AreEqual(courses.Count(), 2);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserIdNotExistTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourses(Guid.NewGuid());
            Assert.AreEqual(courses.Count(), 0);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesOwnerTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourses("lex");
            Assert.AreEqual(courses.Count(), 2);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesOwnerNotExistTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourses("unknown");
            Assert.AreEqual(courses.Count(), 0);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserOwnerTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourses(new User
                                             {
                                                 Deleted = false,
                                                 Name = "lex",
                                                 Username = "lex"
                                             }
                                         );

            Assert.AreEqual(courses.Count(), 2);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserOwnerNotExistTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourses(new User
                                            {
                                                Deleted = false,
                                                Name = "unknown",
                                                Username = "unknown"
                                            }
                                        );

            Assert.AreEqual(courses.Count(), 0);
        }

        #endregion

        [Test]
        public void GetCourseUsersTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourseUsers(1);
            Assert.AreEqual(courses.ToArray()[0].Username, "lex");
        }

        [Test]
        public void GetCourseUsersNotFoundTest()
        {
            MixedCourseStorage mcs = new MixedCourseStorage(_Tests.LmsService);
            var courses = mcs.GetCourseUsers(-1);
            Assert.AreEqual(courses.Count(), 0);
        }

        #endregion

        #region Test Node methods

        #endregion

        #region Test NodeResource methods

        #endregion
    }
}
