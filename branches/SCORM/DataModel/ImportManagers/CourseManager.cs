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
    public class CourseManager
    {
        public static int Import(ProjectPaths projectPaths, string name, string description)
        {
            ResourceManager.Resources.Clear();
            ResourceManager.Dependencies.Clear();

            if (!File.Exists(projectPaths.PathToManifestXml))
            {
                throw new Exception("No imsmanifest.xml file found");
            }

            // load manifest
            XmlDocument imsmanifest = new XmlDocument();
            imsmanifest.Load(projectPaths.PathToManifestXml);

            // store the course in db
            int courseID = Store(name, description);

            string CoursePath = GetCoursePath(courseID);

            if (!Directory.Exists(CoursePath))
            {
                Directory.CreateDirectory(CoursePath);
            }

            string ManifestPath = Path.Combine(CoursePath, "imsmanifest.xml");

            if (!File.Exists(ManifestPath))
            {
                File.Copy(projectPaths.PathToManifestXml, ManifestPath);
            }

            XmlNodeList resources = XmlUtility.GetNodes(imsmanifest.DocumentElement, "/ns:manifest/ns:resources/ns:resource");
            
            foreach (XmlNode resource in resources)
            {
                ResourceManager.ParseResource(projectPaths, courseID, resource);
            }

            // import list of <organization> elements
            XmlNodeList organizations = XmlUtility.GetNodes(imsmanifest.DocumentElement, "/ns:manifest/ns:organizations/ns:organization");
            
            foreach (XmlNode node in organizations)
            {
                OrganizationManager.Import(node, courseID);
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
            string AssetsPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Assets");
            string CoursePath = Path.Combine(AssetsPath, courseID.ToString());

            return CoursePath;
        }
    }
}