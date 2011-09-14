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
    /// <summary>
    /// Class to work with resources
    /// </summary>
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

        public static Dictionary<string, bool> Dependencies
        {
            get
            {
                return dependancies;
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

                    StoreFile(href, projectPaths, courseID);
                }

                foreach (string file in dependencyFiles)
                {
                    StoreFile(file, projectPaths, courseID);
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

                XmlNode dependancy = XmlUtility.GetNode(resource.ParentNode, "ns:resource[@identifier='" + identifier + "']");

                files = GetDependencyFiles(dependancy);

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

        private static void StoreFile(string href, ProjectPaths projectPaths, int courseID)
        {
            string FilePath = Path.Combine(projectPaths.PathToTempCourseFolder, href);

            if (File.Exists(FilePath))
            {
                string AssetFilePath = Path.Combine(CourseManager.GetCoursePath(courseID), href);

                string AssetDirectoryPath = Directory.GetParent(AssetFilePath).ToString();
                RecursiveCreateDirectory(AssetDirectoryPath);

                if (!File.Exists(AssetFilePath))
                {
                    CopyFile(FilePath, AssetFilePath);
                }
            }
        }

        private static void CopyFile(string From, string To)
        {
            File.Copy(From, To, true);
            /*
            string Extention = Path.GetExtension(From).ToLower();

            if (Extention == ".htm" || Extention == ".html" || Extention == ".js")
            {
                byte[] buffer = File.ReadAllBytes(From);
                Encoding Enc = GetFileEncoding(From);

                buffer = Encoding.Convert(Enc, Encoding.Unicode, buffer);

                string contents = Encoding.Unicode.GetString(buffer);
                File.WriteAllText(To, contents, Encoding.Unicode);
            }
            else
            {
                File.Copy(From, To, true);
            }
            */
        }

        private static Encoding GetFileEncoding(string From)
        {
            FileStream ReadFile = new FileStream(From, FileMode.Open, FileAccess.Read, FileShare.Read);

            if (ReadFile.CanSeek)
            {
                byte[] bom = new byte[4];

                ReadFile.Read(bom, 0, 4);
                ReadFile.Seek(0, SeekOrigin.Begin);
                ReadFile.Close();

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
                ReadFile.Close();

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
    }
}