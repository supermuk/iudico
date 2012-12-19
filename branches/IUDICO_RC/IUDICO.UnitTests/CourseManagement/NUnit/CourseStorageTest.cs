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
        private string path = Path.Combine(ConfigurationManager.AppSettings["RootTestFolder"], @"CourseManagement\\Data\\tempCourse.zip");
        private string fileToDel = Path.Combine(ConfigurationManager.AppSettings["RootTestFolder"], @"CourseManagement\\Data\\0.zip");

        [Test]
        // import changes the count of courses
        public void ImportTest1()
        {
            int beginAmount = tests.Storage.GetCourses().Count();

            this.tests.Storage.Import(path, "Course1", "lex");

            int endAmount = tests.Storage.GetCourses().Count();

            this.tests.Storage.DeleteCourse(0);
            File.Delete(fileToDel);

            Assert.IsTrue(beginAmount < endAmount);         
        }

        [Test]
        // import the course with the name
        public void ImportTest2()
        {
            this.tests.Storage.Import(path, "Course1", "lex");
            var tmp = this.tests.Storage.GetCourse(0);

            this.tests.Storage.DeleteCourse(0);
            File.Delete(fileToDel);

            Assert.IsTrue(tmp.Name == "Course1" && tmp.Owner == "lex");          
        }
        
        [Test]
        // import the course without the name
        public void ImportTest3()
        {
            this.tests.Storage.Import(path, "lex");

            Assert.IsTrue(path.Contains(this.tests.Storage.GetCourse(0).Name));

            this.tests.Storage.DeleteCourse(0);
            File.Delete(fileToDel);
        }

        [Test]
        // creating the folder with the imported test
        public void ImportTest4()
        {
            this.tests.Storage.Import(path, "Course1", "lex");

            Assert.IsTrue(File.Exists(fileToDel));

            this.tests.Storage.DeleteCourse(0);
            File.Delete(fileToDel);
            
        }

         [Test]
         // importing many courses
         public void ImportTest5()
         {
             int beginAmount = this.tests.Storage.GetCourses().Count();
             string name = "Course";
             for(int i=0;i<5;i++)
             {
                 this.tests.Storage.Import(path,name + i.ToString(),"lex");
                 File.Delete(fileToDel);
             }
             int endAmount = this.tests.Storage.GetCourses().Count();

             Assert.IsTrue(endAmount - beginAmount == 5);

             List<int> id= new List<int>(){0,0,0,0,0};

             this.tests.Storage.DeleteCourses(id);
         }

    }
}
