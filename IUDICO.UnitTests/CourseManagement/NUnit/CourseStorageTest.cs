using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Caching;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using File = System.IO.File;
using System.IO;
using System.Linq;
using System.Net.Mime;
using IUDICO.CourseManagement.Helpers;
using NUnit.Framework;
using System.Configuration;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class CourseStorageTest : BaseCourseManagementTest
    {
        private CourseManagementTest tests = CourseManagementTest.GetInstance();
        // field with the path to new course
        private string path = Path.Combine(ConfigurationManager.AppSettings["RootTestFolder"], @"CourseManagement\\Data\\tempCourse.zip");

        // field with the folder to delete after tests ending
        private string fileToDel = Path.Combine(ConfigurationManager.AppSettings["RootTestFolder"], @"CourseManagement\\Data\\0.zip");

        [TearDown]
        // deleting the added course and folder after every test 
        public void FileClose()
        {
            this.tests.Storage.DeleteCourse(0);
            File.Delete(fileToDel);
        }

        /// <summary>
        /// Author - Oleh Garasymchuk
        /// </summary>
        [Test]
        // import the course with the name
        public void ImportNamedCourse()
        {
            // importing named course
            this.tests.Storage.Import(path, "Course1", "lex");
            // getting this course
            var tmp = this.tests.Storage.GetCourse(0);

            // Watching the name and owner
            Assert.IsTrue(tmp.Name == "Course1" && tmp.Owner == "lex");
        }

    }
}
