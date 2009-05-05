using System;
using System.IO;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class CourseManager
    {
        public static void ExtractZipFile(ProjectPaths projectPaths)
        {
            Zipper.ExtractZipFile(projectPaths.PathToCourseZipFile, projectPaths.PathToTempCourseFolder);
        }

        public static int Import(ProjectPaths projectPaths, string name, string description)
        {
            projectPaths.PathToAnswerXml = Path.Combine(projectPaths.PathToTempCourseFolder, "answers.xml");

            int id = Store(name, description);

            ManageThemes(id, projectPaths);

            return id;
        }

        private static void ManageThemes(int courseId, ProjectPaths projectPaths)
        {
            XmlDocument imsmanifest = GetImsmanifest(projectPaths.PathToTempCourseFolder);

            if (imsmanifest.DocumentElement != null)
            {
                XmlNode document = imsmanifest.DocumentElement.FirstChild;

                foreach (XmlNode node in document)
                    SearchThemes(node, courseId, projectPaths);
            }
        }

        private static XmlDocument GetImsmanifest(string pathToTempCourseFolder)
        {
            var imsmanifest = new XmlDocument();
            imsmanifest.Load(Path.Combine(pathToTempCourseFolder, "imsmanifest.xml"));
            return imsmanifest;
        }

        private static void SearchThemes(XmlNode document, int courseId, ProjectPaths projectPaths)
        {
            foreach (XmlNode node in document.ChildNodes)
            {
                if (XmlUtility.isItem(node))
                {
                    ThemeManager.Import(node, courseId, projectPaths);
                }
            }
        }

        private static int Store(string name, string description)
        {
            var c = new TblCourses
            {
                Name = name,
                Description = description,
                UploadDate = DateTime.Now,
                Version = 1
            };

            ServerModel.DB.Insert(c);

            return c.ID;
        }
    }
}