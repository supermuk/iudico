using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTests.DataGenerator.Fakes;
using IUDICO.DataGenerator.Models.Storage;
using IUDICO.DataGenerator.Models.Generators;

namespace IUDICO.UnitTests.DataGenerator.NUnit
{
    [TestFixture]
    public class UserGenerationTest
    {
        protected FakeUserStorage tests = FakeUserStorage.GetInstance();
        protected DemoStorage demoStorage = new DemoStorage();

        [SetUp]
        public void Init()
        {
            this.tests.SetupTables();
        }

        [TearDown]
        public void OnTearDown()
        {
            this.tests.Storage.DeleteGroup(this.tests.Storage.GetGroups().SingleOrDefault(g => g.Name == "Демонстраційна група").Id);

            foreach (var user in this.demoStorage.GetStudents())
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }

            foreach (var user in this.demoStorage.GetTeachers())
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }

            foreach (var user in this.demoStorage.GetCourseCreators())
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }

            foreach (var user in this.demoStorage.GetAdministrators())
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        public void CreateGroupTest()
        {
            Assert.AreEqual(0, this.tests.Storage.GetGroups().Where(g => g.Name == "Демонстраційна група").Count());

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);

            Assert.AreEqual(1, this.tests.Storage.GetGroups().Where(g => g.Name == "Демонстраційна група").Count());

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);

            Assert.AreEqual(1, this.tests.Storage.GetGroups().Where(g => g.Name == "Демонстраційна група").Count());
        }

        [Test]
        public void CreateStudentsTest()
        {
            foreach (var user in this.demoStorage.GetStudents())
            {
                Assert.IsNull(this.tests.Storage.GetUser(user.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var user in this.demoStorage.GetStudents())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsersInRole(Common.Models.Role.Student).Count(u => u.Username == user.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var user in this.demoStorage.GetStudents())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsers(u => u.Username == user.Username).Count());
            }
        }

        [Test]
        public void CreateTeachersTest()
        {
            foreach (var teacher in this.demoStorage.GetTeachers())
            {
                Assert.IsNull(this.tests.Storage.GetUser(teacher.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var teacher in this.demoStorage.GetTeachers())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsersInRole(Common.Models.Role.Teacher).Count(u => u.Username == teacher.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var user in this.demoStorage.GetTeachers())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsers(u => u.Username == user.Username).Count());
            }
        }

        [Test]
        public void CreateCourseCreatorsTest()
        {
            foreach (var creator in this.demoStorage.GetCourseCreators())
            {
                Assert.IsNull(this.tests.Storage.GetUser(creator.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var creator in this.demoStorage.GetCourseCreators())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsersInRole(Common.Models.Role.CourseCreator).Count(u => u.Username == creator.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var user in this.demoStorage.GetCourseCreators())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsers(u => u.Username == user.Username).Count());
            }
        }

        [Test]
        public void CreateAdministratorsTest()
        {
            foreach (var admin in this.demoStorage.GetAdministrators())
            {
                Assert.IsNull(this.tests.Storage.GetUser(admin.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var admin in this.demoStorage.GetAdministrators())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsersInRole(Common.Models.Role.Admin).Count(u => u.Username == admin.Username));
            }

            UserGenerator.Generate(this.tests.Storage, this.demoStorage);
            foreach (var user in this.demoStorage.GetAdministrators())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsers(u => u.Username == user.Username).Count());
            }
        }

        [Test]
        public void AddStudentsToGroupTest()
        {
            UserGenerator.Generate(this.tests.Storage, this.demoStorage);

            foreach (var user in this.demoStorage.GetStudents())
            {
                Assert.AreEqual(1, this.tests.Storage.GetUsersInGroup(this.tests.Storage.GetGroups().SingleOrDefault(g => g.Name == "Демонстраційна група")).Where(u => u.Username == user.Username).Count());
            }
        }
    }
}
