using System;
using System.Collections.Generic;
using IUDICO.DataModel;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.Common.TestRequestUtils;
using IUDICO.DataModel.DB;
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
            //using (var c = new DataObjectCleaner())
            {
                int courseId = ImportTestCourse(@"D:\Iudico\Courses\C++Course [21 questions].zip");

                var pagesIds = GetPagesIds(courseId);

                var pages = ServerModel.DB.Load<TblPages>(pagesIds); 

                foreach (var p in pages)
                {
                    string extension = (p.PageTypeRef == (int)FX_PAGETYPE.Practice)
                        ? FileExtentions.IudicoPracticePage
                        : FileExtentions.IudicoTheoryPage;

                    var url = TestRequestBuilder.NewRequestForHandler(p.ID, extension).AddTestSessionType(TestSessionType.UnitTesting).Build();
                    Console.WriteLine(url); 
                }
            }
        }

        private static int ImportTestCourse(string path)
        {
            var projectPath = new ProjectPaths();
            projectPath.Initialize(path);
            CourseManager.ExtractZipFile(projectPath);

            return CourseManager.Import(projectPath, "TestCourse", "For Testing");
        }

        private static List<int> GetPagesIds(int courseId)
        {
            var course = ServerModel.DB.Load<TblCourses>(courseId);

            var themesIds = ServerModel.DB.LookupIds<TblThemes>(course, null);
            var themes = ServerModel.DB.Load<TblThemes>(themesIds);

            var allPagesIds = new List<int>();

            foreach (var theme in themes)
            {
                var pagesIds = ServerModel.DB.LookupIds<TblPages>(theme, null);
                allPagesIds.AddRange(pagesIds);
            }

            return allPagesIds;
        }
    }
}
