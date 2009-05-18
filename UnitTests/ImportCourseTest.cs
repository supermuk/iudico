using System;
using System.IO;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.ImportManagers;
using IUDICO.UnitTest.Base;
using NUnit.Framework;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class ImportCourseTest : TestFixtureDB
    {
        protected override bool NeedToRecreateDB
        {
            get
            {
                return false;
            }
        }

        [Test]
        public void TestImportCourse()
        {
            var coursesPath = Path.Combine(Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(Environment.CurrentDirectory))), "Courses");
            var courses = Directory.GetFiles(coursesPath, "*.zip", SearchOption.TopDirectoryOnly);

            foreach (var coursePath in courses)
            {
                Logger.WriteLine("[COURSE IMPORTER] Importing course: " + coursePath);
                int courseId = ImportTestCourse(coursePath);
                Logger.WriteLine("[COURSE IMPORTER] ID = " + courseId + ". Pages: ");
                var pages = StudentRecordFinder.GetCoursePages(courseId);
                foreach (var p in pages)
                {
                    var url = string.Format(@"[COURSE IMPORTER] ~/Student/IudicoPage{0}?PageId={1}", ((FX_PAGETYPE)p.PageTypeRef).GetHandlerExtention(), p.ID);
                    Logger.WriteLine(url);
                }
            }
        }

        private static int ImportTestCourse(string path)
        {
            var projectPath = new ProjectPaths();
            projectPath.Initialize(path);
            CourseManager.ExtractZipFile(projectPath);

            return CourseManager.Import(projectPath, Path.GetFileNameWithoutExtension(path), "[UNITTESTS]");
        }
    }
}
