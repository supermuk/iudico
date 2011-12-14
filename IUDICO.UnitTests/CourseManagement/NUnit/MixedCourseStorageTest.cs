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
            Assert.AreEqual(courses.Count(), 4);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserIdTest()
        {
            var users = _Storage.GetCourseUsers(1);
            var courses = _Storage.GetCourses(users.ToArray()[0].Id);
            Assert.AreEqual(courses.Count(), 1);
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

        #region Test CourseUsers Methods

        [Test]
        [Category("CourseUsersMethods")]
        public void GetCourseUsersTest()
        {
            var courses = _Storage.GetCourseUsers(1);
            Assert.AreEqual(courses.ToArray()[0].Username, "user1");
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void GetCourseUsersNotFoundTest()
        {
            var courses = _Storage.GetCourseUsers(-1);
            Assert.AreEqual(courses.Count(), 0);
        }


        // Problem or not?
        [Test]
//        [Ignore]
        [Category("CourseUsersMethods")]
        public void UpdateCourseUsersValidCourse()
        {
            List<Guid> guids = new List<Guid>();
            //valid Guids
            guids.Add(new Guid("55345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("66345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            //Invalid Guids
            guids.Add(new Guid("77345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("88345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            _Storage.UpdateCourseUsers(1, guids.AsEnumerable());

            Assert.AreEqual(4, _Storage.GetCourseUsers(1).Count());
        }


        [Test]
//        [Ignore]
        [Category("CourseUsersMethods")]
        public void UpdateCourseUsersNewCourse()
        {
            List<Guid> guids = new List<Guid>();
            //valid Guids
            guids.Add(new Guid("55345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("66345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            //Invalid Guids
            guids.Add(new Guid("77345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("88345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            _Storage.UpdateCourseUsers(100, guids.AsEnumerable());

            Assert.AreEqual(4, _Storage.GetCourseUsers(1).Count());
        }

        [Test]
//        [Ignore]
        [Category("CourseUsersMethods")]
        public void UpdateCourseUsersEmptyEnumerableOfGuid()
        {
            List<Guid> guids = new List<Guid>();
            _Storage.UpdateCourseUsers(1, guids.AsEnumerable());

            Assert.AreEqual(0, _Storage.GetCourseUsers(1).Count());
        }


        [Test]
        [Category("CourseUsersMethods")]
        public void DeleteCourseUsersValidGuidTest()
        {
            Guid user2 = new Guid("22345200-abe8-4f60-90c8-0d43c5f6c0f6");
            _Storage.DeleteCourseUsers(user2);
            var users = _Storage.GetCourseUsers(1);
            Assert.AreEqual(users.Count(), 2);
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void DeleteCourseUsersInvalidGuidTest()
        {
            Guid user1 = new Guid("00000000-abe8-4f60-90c8-0d43c5f6c0f6");
            int countOldUsers =0;
            foreach (var course in _Storage.GetCourses())
            {
                countOldUsers += _Storage.GetCourseUsers(course.Id).Count();
            } 

            _Storage.DeleteCourseUsers(user1);

            int countNewUsers = 0;
            foreach (var course in _Storage.GetCourses())
            {
                countNewUsers += _Storage.GetCourseUsers(course.Id).Count();
            } 

            Assert.AreEqual(countOldUsers, countNewUsers);
        }

        #endregion

        #region Test AddCourse Methods

        [Test]
        [Category("AddCourseMethodsTest")]
        public void AddCourseTest()
        {
            Course course = new Course { Owner = "lex", Name = "new Course" };

            int id = _Storage.AddCourse(course);

            Course c = _Storage.GetCourse(id);

            Assert.AreEqual(course.Name, c.Name);
            Assert.AreEqual(course.Owner, c.Owner);

        }

        #endregion

        #region Test UpdateCourse Methods

        [Test]
        [Category("UpdateCourseMethodsTest")]
        public void UpdateCourseTest()
        {
            Course oldCourse = _Storage.GetCourse(2);

            Course newCourse = new Course { Name = "New Course" };

            _Storage.UpdateCourse(2, newCourse);
            Assert.AreNotEqual(oldCourse.Owner, newCourse.Owner);
            Assert.AreEqual(oldCourse.Name, newCourse.Name);
            Assert.AreNotEqual(oldCourse.Updated, newCourse.Updated);
        }

        #endregion

        #region Test DeletaCourse Methods

        [Test]
        [Category("DeleteCourse")]
        public void DeleteOwnCourseValidIDTest()
        {
            _Storage.DeleteCourse(1);
            Course course = _Storage.GetCourse(1);
            Assert.IsTrue(course.Deleted);
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteAlienCourseValidIDTest()
        {
            _Storage.DeleteCourse(2);
            Course course = _Storage.GetCourse(2);
            Assert.IsFalse(course.Deleted);
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteCourseInvalidIDTest()
        {
            try
            {
                _Storage.DeleteCourse(200);
            }
            catch (InvalidOperationException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteCoursesEmptyList()
        {
            int countCourses = _Storage.GetCourses().Count();
            _Storage.DeleteCourses(new List<int>());

            Assert.AreEqual(countCourses,_Storage.GetCourses().Count());
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteOwnCoursesList()
        {
            List<int> coursesIds = new List<int>();
            coursesIds.Add(1);
            coursesIds.Add(7);
            _Storage.DeleteCourses(new List<int>());

            Assert.AreEqual(0, _Storage.GetCourses("lex").Count());
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteOwnAndAlienCoursesList()
        {
            List<int> coursesIds = new List<int>();
            coursesIds.Add(1);
            coursesIds.Add(2);
            _Storage.DeleteCourses(new List<int>());

            Assert.AreEqual(1, _Storage.GetCourses("lex").Count());
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteAlienCoursesList()
        {
            List<int> coursesIds = new List<int>();
            coursesIds.Add(2);
            coursesIds.Add(3);
            _Storage.DeleteCourses(new List<int>());

            Assert.AreEqual(2, _Storage.GetCourses("lex").Count());
        }
        #endregion

        #region Test Export Methods

        [Test]
        [Category("ExportMethods")]
        public void ExportInvalidId()
        {
            try
            {
                _Storage.Export(-200);
            }
            catch (InvalidOperationException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        [Category("ExportMethods")]
        public void ExportLockedValidIdCourse()
        {
            string path = _Storage.Export(2);

            Assert.AreEqual(@"d:\Tests\1\.zip", path);
        }

        [Test]
        [Category("ExportMethods")]
        public void ExportUnLockedValidIdCourse()
        {
            string path = _Storage.Export(1);

            string zip = "Some course.zip";

            string last = path.Substring(path.Length - zip.Length);
            Assert.AreEqual(zip, last);
        }

        #endregion

        #endregion

        #region Test Node methods

        #endregion

        #region Test NodeResource methods

        #endregion
    }
}
