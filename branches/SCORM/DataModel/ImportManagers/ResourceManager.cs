using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;
using System.IO;

namespace IUDICO.DataModel.ImportManagers
{
    public class ResourceManager
    {
        private static Dictionary<string, int> resources = new Dictionary<string, int>();

        public static Dictionary<string, int> Resources
        {
            get
            {
                return resources;
            }
        }

        public static int ParseResource(ProjectPaths projectPaths, int courseID, XmlNode resource)
        {
            string identifier = resource.Attributes["identifier"].Value;
            string type = resource.Attributes["adlcp:scormType"].Value;

            if (resources.ContainsKey(identifier))
                return resources[identifier];

            int ID = Store(identifier, type, courseID);
            resources[identifier] = ID;
            
            XmlNodeList dependencyNodes = XmlUtility.GetNodes(resource, "ns:dependency");
            foreach (XmlNode node in dependencyNodes)
            {
                string dependencyIdentifier = node.Attributes["identifierref"].Value;
                int dependencyID = 0;

                if (!resources.ContainsKey(dependencyIdentifier))
                {
                    XmlNode resourceNode = XmlUtility.GetNode(resource.ParentNode, "ns:resource[@id='" + dependencyIdentifier + "']");
                    dependencyID = ParseResource(projectPaths, courseID, resourceNode);
                }
                else
                {
                    dependencyID = resources[dependencyIdentifier];
                }

                LinkDependency(ID, dependencyID);
            }

            XmlNodeList fileNodes = XmlUtility.GetNodes(resource, "ns:file");
            foreach (XmlNode node in fileNodes)
            {
                string href = node.Attributes["href"].Value;
                string path = Path.Combine(projectPaths.PathToTempCourseFolder, href);    
                byte[] file = null;

                //TODO: check if relative, absolute, url?

                if (File.Exists(path))
                {
                    file = File.ReadAllBytes(path);
                }

                int fileID = StoreFile(href, file);
                LinkFile(ID, fileID);
            }

            return ID;
        }

        private static int Store(string identifier, string type, int courseID)
        {
            TblResources r = new TblResources
            {
                CourseRef = courseID,
                Identifier = identifier,
                Type = type
            };

            return ServerModel.DB.Insert(r);
        }

        private static int StoreFile(string href, byte[] file)
        {
            TblFiles f = new TblFiles
            {
                Path = href,
                File = file
            };

            return ServerModel.DB.Insert(f);
        }

        private static void LinkDependency(int dependantID, int dependencyID)
        {
            RelResourcesDependency r = new RelResourcesDependency
            {
                DependantRef = dependantID,
                DependencyRef = dependencyID
            };

            TblResources dependant = ServerModel.DB.Load<TblResources>(dependantID);
            TblResources dependancy = ServerModel.DB.Load<TblResources>(dependencyID);

            ServerModel.DB.Link(dependant, dependancy);
        }

        private static void LinkFile(int resourceID, int fileID)
        {
            TblResources resource = ServerModel.DB.Load<TblResources>(resourceID);
            TblFiles file = ServerModel.DB.Load<TblFiles>(fileID);

            ServerModel.DB.Link(resource, file);
        }
        

    }
}
