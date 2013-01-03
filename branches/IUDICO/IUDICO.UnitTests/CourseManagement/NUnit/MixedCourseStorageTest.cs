using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels;
using IUDICO.Common.Controllers;

using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class MixedCourceStorageTest : BaseCourseManagementTest
    {
        protected CourseManagementTest tests = CourseManagementTest.GetInstance();

        protected ICourseStorage Storage
        {
            get
            {
                return this.tests.Storage;
            }
        }

        [SetUp]
        protected void Initialize()
        {
            this.tests.ClearTables();
        }

        #region Test Course methods

        #region Test GetCourses methods

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCourseIDTest()
        {
            var course = this.Storage.GetCourse(1);
            Assert.AreEqual(course.Id, 1);
            Assert.AreEqual(course.Name, "Some course");
        }

        [Test]
        [Category("GetCoursesMethods")]
        //[ExpectedException(typeof(InvalidOperationException))]
        public void GetCourseIDNotFoundTest()
        {
            var course = this.Storage.GetCourse(333);
            Assert.IsTrue(course == null);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesTest()
        {
            var courses = this.Storage.GetCourses();
            Assert.AreEqual(courses.Count(), 4);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserIdTest()
        {
            var users = this.Storage.GetCourseUsers(1);
            var courses = this.Storage.GetCourses(users.ToArray()[0].Id);
            Assert.AreEqual(courses.Count(), 1);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserIdNotExistTest()
        {
            var courses = this.Storage.GetCourses(Guid.NewGuid());
            Assert.AreEqual(courses.Count(), 0);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesOwnerTest()
        {
            var courses = this.Storage.GetCourses("lex");
            Assert.AreEqual(courses.Count(), 2);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesOwnerNotExistTest()
        {
            var courses = this.Storage.GetCourses("unknown");
            Assert.AreEqual(courses.Count(), 0);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserOwnerTest()
        {
            var courses = this.Storage.GetCourses(new User { Deleted = false, Name = "lex", Username = "lex" });

            Assert.AreEqual(courses.Count(), 2);
        }

        [Test]
        [Category("GetCoursesMethods")]
        public void GetCoursesUserOwnerNotExistTest()
        {
            var courses = this.Storage.GetCourses(new User { Deleted = false, Name = "unknown", Username = "unknown" });

            Assert.AreEqual(courses.Count(), 0);
        }

        #endregion

        #region Test CourseUsers Methods

        [Test]
        [Category("CourseUsersMethods")]
        public void GetCourseUsersTest()
        {
            var courses = this.Storage.GetCourseUsers(1);
            Assert.AreEqual(courses.ToArray()[0].Username, "user1");
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void GetCourseUsersNotFoundTest()
        {
            var courses = this.Storage.GetCourseUsers(-1);
            Assert.AreEqual(0, courses.Count());
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void UpdateCourseUsersValidCourse()
        {
            var guids = new List<Guid>();

            // valid Guids
            guids.Add(new Guid("55345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("66345200-abe8-4f60-90c8-0d43c5f6c0f6"));

            // Invalid Guids
            guids.Add(new Guid("77345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("88345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            this.Storage.UpdateCourseUsers(1, guids.AsEnumerable());

            Assert.AreEqual(2, this.Storage.GetCourseUsers(1).Count()); // Expected: 0 But was: 3
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void UpdateCourseUsersNewCourse()
        {
            var guids = new List<Guid>();

            // valid Guids
            guids.Add(new Guid("55345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("66345200-abe8-4f60-90c8-0d43c5f6c0f6"));

            // Invalid Guids
            guids.Add(new Guid("77345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            guids.Add(new Guid("88345200-abe8-4f60-90c8-0d43c5f6c0f6"));
            this.Storage.UpdateCourseUsers(100, guids.AsEnumerable());

            Assert.AreEqual(2, this.Storage.GetCourseUsers(100).Count()); // Expected: 4 But was: 3
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void UpdateCourseUsersEmptyEnumerableOfGuid()
        {
            var guids = new List<Guid>();
            this.Storage.UpdateCourseUsers(1, guids.AsEnumerable());

            Assert.AreEqual(0, this.Storage.GetCourseUsers(1).Count()); // Expected: 0 But was: 3
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void DeleteCourseUsersValidGuidTest()
        {
            var user2 = new Guid("22345200-abe8-4f60-90c8-0d43c5f6c0f6");
            this.Storage.DeleteCourseUsers(user2);
            var users = this.Storage.GetCourseUsers(1);
            Assert.AreEqual(users.Count(), 2);
        }

        [Test]
        [Category("CourseUsersMethods")]
        public void DeleteCourseUsersInvalidGuidTest()
        {
            var user1 = new Guid("00000000-abe8-4f60-90c8-0d43c5f6c0f6");
            var countOldUsers = 0;
            foreach (var course in this.Storage.GetCourses())
            {
                countOldUsers += this.Storage.GetCourseUsers(course.Id).Count();
            }

            this.Storage.DeleteCourseUsers(user1);

            var countNewUsers = 0;
            foreach (var course in this.Storage.GetCourses())
            {
                countNewUsers += this.Storage.GetCourseUsers(course.Id).Count();
            }

            Assert.AreEqual(countOldUsers, countNewUsers);
        }

        #endregion

        #region Test UpdateCourse Methods

        [Test]
        [Category("UpdateCourseMethodsTest")]
        public void UpdateCourseTest()
        {
            var oldCourse = this.Storage.GetCourse(2);

            var newCourse = new Course { Name = "New Course" };

            this.Storage.UpdateCourse(2, newCourse);
            Assert.AreEqual(oldCourse.Owner, newCourse.Owner);
            Assert.AreEqual(oldCourse.Name, newCourse.Name);
            Assert.AreEqual(oldCourse.Updated, newCourse.Updated);
            Assert.AreEqual(oldCourse.Created, newCourse.Created);
            Assert.AreEqual(oldCourse.Locked, newCourse.Locked);
        }

        #endregion

        #region Test DeletaCourse Methods

        [Test]
        [Category("DeleteCourse")]
        public void DeleteOwnCourseValidIdTest()
        {
            this.Storage.DeleteCourse(1);
            try
            {
                var course = this.Storage.GetCourse(1);  
                Assert.IsTrue(course.Deleted);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex.Message == "Object reference not set to an instance of an object.");
            }
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteAlienCourseValidIdTest()
        {
            this.Storage.DeleteCourse(2);
            var course = this.Storage.GetCourse(2);
            Assert.IsFalse(course.Deleted);
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteCourseInvalidIdTest()
        {
            try
            {
                this.Storage.DeleteCourse(200);
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
            var countCourses = this.Storage.GetCourses().Count();
            this.Storage.DeleteCourses(new List<int>());

            Assert.AreEqual(countCourses, this.Storage.GetCourses().Count());
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteOwnCoursesList()
        {
            var coursesIds = new List<int>();
            coursesIds.Add(1);
            coursesIds.Add(7);
            this.Storage.DeleteCourses(coursesIds);

            Assert.AreEqual(0, this.Storage.GetCourses("lex").Count());
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteOwnAndAlienCoursesList()
        {
            var coursesIds = new List<int>();
            coursesIds.Add(1);
            coursesIds.Add(2);
            this.Storage.DeleteCourses(coursesIds);

            Assert.AreEqual(1, this.Storage.GetCourses("lex").Count());
        }

        [Test]
        [Category("DeleteCourse")]
        public void DeleteAlienCoursesList()
        {
            var coursesIds = new List<int>();
            coursesIds.Add(2);
            coursesIds.Add(3);
            this.Storage.DeleteCourses(coursesIds);

            Assert.AreEqual(2, this.Storage.GetCourses("lex").Count());
        }

        #endregion

        #region Test Import Methods

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        [Category("ImportMethods")]
        //importing the unnamed course
        public void ImportTest()
        {
            // path of course
            var path = Path.Combine(ConfigurationManager.AppSettings["RootTestFolder"], @"CourseManagement\\Data\\20.zip");

            //count of courses before the import
            int beginAmount = tests.Storage.GetCourses().Count();

            //importimg the course
            this.Storage.Import(path, "lex");

            //count of courses after the import
            int endAmount = tests.Storage.GetCourses().Count();
            Assert.IsTrue(beginAmount < endAmount);

            //Getting all courses with the owner "lex" from db
            var courses = this.Storage.GetCourses("lex");

            // Getting from "courses" course with the name "20"
            var course = courses.Single(i => i.Name == "20");

            Assert.AreEqual("lex", course.Owner);
            Assert.AreEqual(true, course.Locked);

            path = Path.Combine(ConfigurationManager.AppSettings["RootTestFolder"], @"CourseManagement\\Data\\0.zip");
            // Watching was the folder for course created
            Assert.IsTrue(File.Exists(path));

            File.Delete(path);
        }

        #endregion

        #region Test Export Methods

        [Test]
        [Category("ExportMethods")]
        public void ExportInvalidId()
        {
            try
            {
                this.Storage.Export(-200);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message == "Object reference not set to an instance of an object.");
            }

           // Assert.Fail();
        }

        [Test]
        [Category("ExportMethods")]
        public void ExportLockedValidIdCourse()
        {
            var path = this.Storage.Export(2);

            Assert.AreEqual(this.tests._CourseStoragePath + "\\2.zip", path);
        }

        #endregion

        #region Test GetCoursePath Methods

        [Test]
        [Category("GetCoursePathMethods")]
        public void GetCoursePathTest()
        {
            var current = HttpContext.Current;
            HttpContext.Current = new HttpContext(
                new HttpRequest(string.Empty, "http://something.org", string.Empty), 
                new HttpResponse(new StringWriter()));

            var coursePath = this.Storage.GetCoursePath(1);

            HttpContext.Current = current;

            var path = Path.Combine(this.tests._CourseStoragePath, "1");

            Assert.AreEqual(path, coursePath);
        }

        #endregion

        #endregion

        #region Test Couse Editor methods

        const int ID = 100;

        /// <summary>
        /// Create new node with id = ID and add it in storage.
        /// </summary>
        private void createNode()
        {
            // get course with id=1
            var course = this.Storage.GetCourse(1);
            // create new node
            Node someNode = new Node();
            someNode.Name = "SomeNode";
            // set course and couse id
            someNode.CourseId = course.Id;
            someNode.Course = course;
            // set id
            someNode.Id = ID;
            // add node to storage
            this.Storage.AddNode(someNode);
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void CreateNodeTest()
        {
            try
            {
                // create new node with name "SomeNode", id=ID, courseId=1, and add it to storage
                this.createNode();
                // try get node with id = ID, if error - throw exeption
                var node = this.Storage.GetNode(ID);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void CreateBlankNodeTest()
        {
            try
            {
                // get couse
                var course = this.Storage.GetCourse(1);
                // create new node
                Node someNode = new Node();
                // sei node id
                someNode.Id = ID;
                // set node course
                someNode.Course = course;
                // add node to storage
                this.Storage.AddNode(someNode);

                var node = this.Storage.GetNode(ID);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void RenameNodeTest()
        {
            // create new node with name "SomeNode", id=ID, courseId=1, and add it to storage
            this.createNode();
            // try get node with id = ID, if error - throw exeption
            var node = this.Storage.GetNode(ID);
            // rename node
            node.Name = "RenamedNode";
            // if node is renamed test passed, else - failed
            if (this.Storage.GetNode(ID).Name == "RenamedNode")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Node was not  renamed.");
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void DeleteNodeTest()
        {
            // create new node with name "SomeNode", id=ID, courseId=1, and add it to storage
            this.createNode();
            // delete node with id=ID
            this.Storage.DeleteNode(ID);

            try
            {
                // try get deleted node
                var node = this.Storage.GetNode(ID);
                // if node isn't deleted
                Assert.Fail("Node was not deleted");
            }
            catch
            {
                // if node is deleted
                Assert.Pass();
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void PreviewNodeTest()
        {
            try
            {
                // create new node with name "SomeNode", id=ID, courseId=1, and add it to storage
                this.createNode();
                // try to get preview nod path
                this.Storage.GetPreviewNodePath(100);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void ShowPropertiesTest([Values("ControlMode",
                                               "LimitConditions",
                                               "ConstrainedChoiceConsiderations",
                                               "RandomizationControls",
                                               "DeliveryControls",
                                               "RollupRules",
                                               "RollupConsiderations")]
                                       string type)
        {
            try
            {
                // create new node with name "SomeNode", id=ID, courseId=1, and add it to storage
                this.createNode();
                // create new xml serializer
                var xml = new XmlSerializer(typeof(Sequencing));
                // get Sequencing from node with id = ID
                var xelement = this.Storage.GetNode(ID).Sequencing;
                // deserialize sequencing from xelement
                var sequencing = xelement == null ? new Sequencing() : (Sequencing)xml.DeserializeXElement(xelement);
                // new model
                NodeProperty model;
                // for different types get node property and initialise model
                if (type == "ControlMode")
                {
                    model = sequencing.ControlMode ?? new ControlMode();
                }
                else if (type == "LimitConditions")
                {
                    model = sequencing.LimitConditions ?? new LimitConditions();
                }
                else if (type == "ConstrainedChoiceConsiderations")
                {
                    model = sequencing.ConstrainedChoiceConsiderations ?? new ConstrainedChoiceConsiderations();
                }
                else if (type == "RandomizationControls")
                {
                    model = sequencing.RandomizationControls ?? new RandomizationControls();
                }
                else if (type == "DeliveryControls")
                {
                    model = sequencing.DeliveryControls ?? new DeliveryControls();
                }
                else if (type == "RollupRules")
                {
                    model = sequencing.RollupRules ?? new RollupRules();
                }
                else if (type == "RollupConsiderations")
                {
                    model = sequencing.RollupConsiderations ?? new RollupConsiderations();
                }
                else
                {
                    throw new NotImplementedException();
                }

                //model.CourseId = 1;
                //model.NodeId = ID;
                //model.Type = type;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void ApplyPatternTest([Values(SequencingPattern.ControlChapterSequencingPattern, 
                                             SequencingPattern.RandomSetSequencingPattern, 
                                             SequencingPattern.OrganizationDefaultSequencingPattern)]
                                     SequencingPattern pattern)
        {
            try
            {
                // create new node with name "SomeNode", id=ID, courseId=1, and add it to storage
                this.createNode();
                // get node with id = ID
                var node = this.Storage.GetNode(ID);
                // create new xml serializer
                var xml = new XmlSerializer(typeof(Sequencing));
                // get Sequencing from node with id = ID
                var xelement = this.Storage.GetNode(ID).Sequencing;
                // deserialize sequencing from xelement
                var sequencing = xelement == null ? new Sequencing() : (Sequencing)xml.DeserializeXElement(xelement);
                // for different patterns type apply different patterns
                switch (pattern)
                {
                    case SequencingPattern.ControlChapterSequencingPattern:
                        sequencing = SequencingPatternManager.ApplyControlChapterSequencing(sequencing);
                        break;
                    case SequencingPattern.RandomSetSequencingPattern:
                        sequencing = SequencingPatternManager.ApplyRandomSetSequencingPattern(sequencing, 1);
                        break;
                    case SequencingPattern.OrganizationDefaultSequencingPattern:
                        sequencing = SequencingPatternManager.ApplyDefaultChapterSequencing(sequencing);
                        break;
                }
                // serialize sequencing
                node.Sequencing = xml.SerializeToXElemet(sequencing);
                // try update node
                this.Storage.UpdateNode(ID, node);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void EditPropertiesTest([Values("ControlMode",
                                               "LimitConditions",
                                               "ConstrainedChoiceConsiderations",
                                               "RandomizationControls",
                                               "DeliveryControls",
                                               "RollupRules",
                                               "RollupConsiderations")]
                                       string type)
        {
            try
            {
                // create new node with name "SomeNode", id=ID, courseId=1, and add it to storage
                this.createNode();
                // create new xml serializer
                var xml = new XmlSerializer(typeof(Sequencing));
                // get Sequencing from node with id = ID
                var xelement = this.Storage.GetNode(ID).Sequencing;
                // deserialize sequencing from xelement
                var sequencing = xelement == null ? new Sequencing() : (Sequencing)xml.DeserializeXElement(xelement);
                // new model
                object model;
                // for different type initialise sequencing property
                if (type == "ControlMode")
                {
                    model = new ControlMode();
                    
                    //TryUpdateModel(model as ControlMode);
                    sequencing.ControlMode = model as ControlMode;
                }
                else if (type == "LimitConditions")
                {
                    model = new LimitConditions();
                    //TryUpdateModel(model as LimitConditions);
                    sequencing.LimitConditions = model as LimitConditions;
                }
                else if (type == "ConstrainedChoiceConsiderations")
                {
                    model = new ConstrainedChoiceConsiderations();
                    //TryUpdateModel(model as ConstrainedChoiceConsiderations);
                    sequencing.ConstrainedChoiceConsiderations = model as ConstrainedChoiceConsiderations;
                }
                else if (type == "RandomizationControls")
                {
                    model = new RandomizationControls();
                    //TryUpdateModel(model as RandomizationControls);
                    sequencing.RandomizationControls = model as RandomizationControls;
                }
                else if (type == "DeliveryControls")
                {
                    model = new DeliveryControls();
                    //TryUpdateModel(model as DeliveryControls);
                    sequencing.DeliveryControls = model as DeliveryControls;
                }
                else if (type == "RollupRules")
                {
                    model = new RollupRules();
                    //TryUpdateModel(model as RollupRules);
                    sequencing.RollupRules = model as RollupRules;
                }
                else if (type == "RollupConsiderations")
                {
                    model = new RollupConsiderations();
                    //TryUpdateModel(model as RollupConsiderations);
                    sequencing.RollupConsiderations = model as RollupConsiderations;
                }
                else
                {
                    throw new NotImplementedException();
                }
                // serialize sequencing
                xelement = xml.SerializeToXElemet(sequencing);
                // update node
                this.Storage.UpdateNode(ID, this.Storage.GetNode(ID));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #endregion  
    }
}