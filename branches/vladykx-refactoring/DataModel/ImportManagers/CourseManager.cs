using System;
using System.IO;
using System.Xml;
using System.Web;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.ImportManagers
{
    /// <summary>
    /// Class to work with courses
    /// </summary>
    public class CourseManager
    {
        public static int Import(ProjectPaths projectPaths, string name, string description)
        {
            ResourceManager.Resources.Clear();
            ResourceManager.Dependencies.Clear();

            if (!File.Exists(projectPaths.PathToManifestXml))
            {
                throw new Exception(Translations.CourseManager_Import_No_imsmanifest_xml_file_found);
            }
            if(!File.Exists(projectPaths.PathToAnswerXml))
            {
                throw new Exception(Translations.CourseManager_Import_No_answers_xml_file_found);
            }

            // load manifest
            XmlDocument imsmanifest = new XmlDocument();
            imsmanifest.Load(projectPaths.PathToManifestXml);
            // load answers
            XmlDocument answers = new XmlDocument();
            answers.Load(projectPaths.PathToAnswerXml);

            // store the course in db
            int courseID = Store(name, description);

            string CoursePath = GetCoursePath(courseID);

            if (!Directory.Exists(CoursePath))
            {
                Directory.CreateDirectory(CoursePath);
            }

            string ManifestPath = Path.Combine(CoursePath, "imsmanifest.xml");
            string AnswersPath = Path.Combine(CoursePath, "answers.xml");

            if (!File.Exists(ManifestPath))
            {
                File.Copy(projectPaths.PathToManifestXml, ManifestPath);
            }
            if (!File.Exists(AnswersPath))
            {
              File.Copy(projectPaths.PathToAnswerXml, AnswersPath);
            }

            XmlNodeList resources = XmlUtility.GetNodes(imsmanifest.DocumentElement, "/ns:manifest/ns:resources/ns:resource");
            
            foreach (XmlNode resource in resources)
            {
                ResourceManager.ParseResource(projectPaths, courseID, resource);
            }

            // import list of <organization> elements in imsmanifest.xml
            XmlNodeList organizationList = XmlUtility.GetNodes(imsmanifest.DocumentElement, "/ns:manifest/ns:organizations/ns:organization");
            // import list of <organization> elements in answers.xml
            XmlNodeList answerList = XmlUtility.GetNodes(answers.DocumentElement, "/answers/organization");

            foreach (XmlNode node in organizationList)
            {
                OrganizationManager.Import(node, XmlUtility.GetNodeById(answerList, XmlUtility.GetIdentifier(node)), courseID);
            }

            return courseID;
        }

        private static int Store(string name, string description)
        {
            TblCourses t = new TblCourses
            {
                Name = name,
                Description = description,
                UploadDate = DateTime.Now,
                Version = 1
            };

            ServerModel.DB.Insert(t);

            return t.ID;
        }

        public static string GetCoursePath(int courseID)
        {
            string path;

            if (HttpContext.Current == null)
            {
                path = Path.Combine(System.Environment.CurrentDirectory, "Site");
            }
            else
            {
                path = HttpContext.Current.Request.PhysicalApplicationPath;
            }

            string AssetsPath = Path.Combine(path, "Assets");
            string CoursePath = Path.Combine(AssetsPath, courseID.ToString());

            return CoursePath;
        }
    }
}