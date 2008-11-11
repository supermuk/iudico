using System.IO;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.Dao.Entity;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.ImportManagers
{
    public class CourseManager
    {
        public static void Import(ProjectPaths projectPaths, string name, string description)
        {
            projectPaths.PathToAnswerXml = Path.Combine(projectPaths.PathToTempCourseFolder, "answers.xml");

            var ce = new CourseEntity(name, description, 0);

            Store(ce);

            ManageChapters(ce.Id, projectPaths);
        }

        private static void ManageChapters(int courseId, ProjectPaths projectPaths)
        {
            XmlDocument imsmanifest = GetImsmanifest(projectPaths.PathToTempCourseFolder);

            if (imsmanifest.DocumentElement != null)
            {
                XmlNode document = imsmanifest.DocumentElement.FirstChild;

                foreach (XmlNode node in document)
                    SearchChapters(node, courseId, projectPaths);
            }
        }

        private static XmlDocument GetImsmanifest(string pathToTempCourseFolder)
        {
            var imsmanifest = new XmlDocument();
            imsmanifest.Load(Path.Combine(pathToTempCourseFolder, "imsmanifest.xml"));
            return imsmanifest;
        }

        private static void SearchChapters(XmlNode document, int courseId, ProjectPaths projectPaths)
        {
            foreach (XmlNode node in document.ChildNodes)
            {
                if (XmlUtility.isItem(node))
                {
                    ThemeManager.Import(node, courseId, projectPaths);
                }
            }
        }

        private static void Store(CourseEntity ce)
        {
            DaoFactory.CourseDao.Insert(ce);
        }
    }
}