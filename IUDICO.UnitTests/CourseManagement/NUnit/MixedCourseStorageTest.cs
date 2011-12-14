using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.Storage;
using NUnit.Framework;
using Moq;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class MixedCourceStorageTest
    {
        protected CourseManagementTest _Tests = CourseManagementTest.GetInstance();

        protected ICourseStorage _Storage
        {
            get
            {
                return _Tests.Storage;
            }
        }

        [SetUp]
        protected void Initialize()
        {
            _Tests.ClearTables();
        }


        #region Test Course methods

        #region Test GetCourses methods
        [Test]
        [Category("GetCoursesMethods")]
        public void GetCourseIDTest()
        {
            Course course = _Storage.GetCourse(1);
            Assert.AreEqual(course.Id, 1);
            Assert.AreEqual(course.Name, "Some course");
        }

        [Test]
        [Category("GetCoursesMethods")]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetCourseIDNotFoundTest()
        {
            Course course = _Storage.GetCourse(333);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesTest()
        {
            var courses = _Storage.GetCourses();
            Assert.AreEqual(courses.Count(), 3);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserIdTest()
        {
            var users = _Storage.GetCourseUsers(1);
            var courses = _Storage.GetCourses(users.ToArray()[0].Id);
            Assert.AreEqual(courses.Count(), 2);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserIdNotExistTest()
        {
            var courses = _Storage.GetCourses(Guid.NewGuid());
            Assert.AreEqual(courses.Count(), 0);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesOwnerTest()
        {
            var courses = _Storage.GetCourses("lex");
            Assert.AreEqual(courses.Count(), 2);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesOwnerNotExistTest()
        {
            var courses = _Storage.GetCourses("unknown");
            Assert.AreEqual(courses.Count(), 0);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserOwnerTest()
        {
            var courses = _Storage.GetCourses(new User
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
            var courses = _Storage.GetCourses(new User
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
            var courses = _Storage.GetCourseUsers(1);
            Assert.AreEqual(courses.ToArray()[0].Username, "lex");
        }

        [Test]
        public void GetCourseUsersNotFoundTest()
        {
            var courses = _Storage.GetCourseUsers(-1);
            Assert.AreEqual(courses.Count(), 0);
        }

        #endregion
    }
}
