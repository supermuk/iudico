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

        private static Dictionary<string, bool> dependancies = new Dictionary<string, bool>();

        public static Dictionary<string, int> Resources
        {
            get
            {
                return resources;
            }
        }

        public static void ParseResource(ProjectPaths projectPaths, int courseID, XmlNode resource)
        {
            string identifier = resource.Attributes["identifier"].Value;
            string type = resource.Attributes["adlcp:scormType"].Value;

            if (resources.ContainsKey(identifier))
            {
                return;
            }

            if (resource.Attributes["href"] != null)
            {
                string resourseHref = resource.Attributes["href"].Value;
                int ID = Store(identifier, type, resourseHref, courseID);
                resources[identifier] = ID;

                HashSet<string> dependencyFiles = GetDependencyFiles(resource);
                XmlNodeList fileNodes = XmlUtility.GetNodes(resource, "ns:file");

                foreach (XmlNode node in fileNodes)
                {
                    string href = node.Attributes["href"].Value;

                    int fileID = StoreFile(href, projectPaths, courseID);
                    //LinkFile(ID, fileID);
                }

                foreach (string file in dependencyFiles)
                {
                    int fileID = StoreFile(file, projectPaths, courseID);
                    //LinkFile(ID, fileID);
                }

                resources[identifier] = ID;
            }
        }

        public static HashSet<string> GetDependencyFiles(XmlNode resource)
        {
            XmlNodeList dependencyNodes = XmlUtility.GetNodes(resource, "ns:dependency");
            HashSet<string> files = new HashSet<string>();

            foreach (XmlNode dependancyNode in dependencyNodes)
            {
                string identifier = dependancyNode.Attributes["identifierref"].Value;

                if (dependancies.ContainsKey(identifier))
                {
                    continue;
                }

                files.Concat(GetDependencyFiles(dependancyNode));

                XmlNode dependancy = XmlUtility.GetNode(resource.ParentNode, "ns:resource[@identifier='" + identifier + "']");

                if (dependancy != null)
                {
                    XmlNodeList fileNodes = XmlUtility.GetNodes(dependancy, "ns:file");

                    foreach (XmlNode fileNode in fileNodes)
                    {
                        files.Add(fileNode.Attributes["href"].Value);
                    }

                    dependancies[identifier] = true;
                }
            }

            return files;
        }

        private static int Store(string identifier, string type, string href, int courseID)
        {
            TblResources r = new TblResources
            {
                CourseRef = courseID,
                Identifier = identifier,
                Type = type,
                Href = href
            };

            return ServerModel.DB.Insert(r);
        }

        private static int StoreFile(string href, ProjectPaths projectPaths, int courseID)
        {
            string FilePath = Path.Combine(projectPaths.PathToTempCourseFolder, href);

            if (File.Exists(FilePath))
            {
                string AssetFilePath = Path.Combine(CourseManager.GetCoursePath(courseID), href);

                string AssetDirectoryPath = Directory.GetParent(AssetFilePath).ToString();
                RecursiveCreateDirectory(AssetDirectoryPath);

                CopyFile(FilePath, AssetFilePath);
            }

            TblFiles f = new TblFiles
            {
                Path = href,
            };

            return ServerModel.DB.Insert(f);
        }

        private static void CopyFile(string From, string To)
        {
            string Extention = Path.GetExtension(From).ToLower();

            if (Extention == "htm" || Extention == "html" || Extention == "js")
            {
                FileStream ReadFile = new FileStream(From, FileMode.Open, FileAccess.Read, FileShare.Read);
                File.Create(To);
                //FileStream WriteFile = new FileStream(To, FileMode.Create, FileAccess.Write, FileShare.None);
                Encoding Enc = GetFileEncoding(ReadFile);
                
                // A good buffer size; should always be base2 in case of Unicode
                byte[] buffer = new byte[4096];

                while (ReadFile.Read(buffer, 0, 4096) > 0)
                {
                    // Uses the encoding we defined above
                    string line = Enc.GetString(buffer);
                    File.AppendAllText(To, line, Enc);
                }
            }
            else
            {
                File.Copy(From, To, true);
            }
        }

        private static Encoding GetFileEncoding(FileStream File)
        {
            if (File.CanSeek)
            {
                byte[] bom = new byte[4];
                File.Read(bom, 0, 4);
                File.Seek(0, SeekOrigin.Begin);

                if ((bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) || // utf-8
                    (bom[0] == 0xff && bom[1] == 0xfe) || // ucs-2le, ucs-4le, and ucs-16le
                    (bom[0] == 0xfe && bom[1] == 0xff) || // utf-16 and ucs-2
                    (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)) // ucs-4
                {
                    return Encoding.Unicode;
                }
                else
                {
                    return Encoding.ASCII;
                }
            }
            else
            {
                return Encoding.ASCII;
            }
        }

        private static void RecursiveCreateDirectory(string Path)
        {
            string ParentPath = Directory.GetParent(Path).ToString();

            if (!Directory.Exists(ParentPath))
            {
                RecursiveCreateDirectory(ParentPath);
            }

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
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
