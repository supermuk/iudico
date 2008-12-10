using System;
using System.IO;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class CourseManager
    {
        public static void Import(ProjectPaths projectPaths, string name, string description, DeletedItem deletedItems)
        {
            projectPaths.PathToAnswerXml = Path.Combine(projectPaths.PathToTempCourseFolder, "answers.xml");

            int id = Store(name, description);

            ManageThemes(id, projectPaths, deletedItems);
        }

        private static void ManageThemes(int courseId, ProjectPaths projectPaths, DeletedItem deletedItems)
        {
            XmlDocument imsmanifest = GetImsmanifest(projectPaths.PathToTempCourseFolder);

            if (imsmanifest.DocumentElement != null)
            {
                XmlNode document = imsmanifest.DocumentElement.FirstChild;

                foreach (XmlNode node in document)
                    SearchThemes(node, courseId, projectPaths, deletedItems);
            }
        }

        private static XmlDocument GetImsmanifest(string pathToTempCourseFolder)
        {
            var imsmanifest = new XmlDocument();
            imsmanifest.Load(Path.Combine(pathToTempCourseFolder, "imsmanifest.xml"));
            return imsmanifest;
        }

        private static void SearchThemes(XmlNode document, int courseId, ProjectPaths projectPaths, DeletedItem deletedItems)
        {
            foreach (XmlNode node in document.ChildNodes)
            {
                if (XmlUtility.isItem(node))
                {
                    if (!deletedItems.DeletedThemes.Contains(XmlUtility.getIdentifier(node)))
                        ThemeManager.Import(node, courseId, projectPaths, deletedItems);
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