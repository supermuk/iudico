using System;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.ImportManagers;
using IUDICO.UnitTest.Base;
using NUnit.Framework;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class OpenPageTest : TestFixtureDB
    {
        [Test]
        public void TestOpenPage()
        {
                int courseId = ImportTestCourse(@"D:\Iudico\Courses\C++Course [21 questions].zip");

                var pages = StudentRecordFinder.GetPagesForCourse(courseId);

                foreach (var p in pages)
                {
                    string extension = (p.PageTypeRef == (int)FX_PAGETYPE.Practice)
                        ? FileExtentions.IudicoPracticePage
                        : FileExtentions.IudicoTheoryPage;

                    var url = string.Format(@"/Site/Student/IudicoPage{0}?PageId={1}", extension, p.ID);
                    Console.WriteLine(url); 
                }
        }

        private static int ImportTestCourse(string path)
        {
            var projectPath = new ProjectPaths();
            projectPath.Initialize(path);
            CourseManager.ExtractZipFile(projectPath);

            return CourseManager.Import(projectPath, "TestCourse", "For Testing");
        }


    }
}
