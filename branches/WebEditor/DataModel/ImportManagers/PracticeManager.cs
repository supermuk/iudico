using System.IO;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.WebControl;
using System.Text;

namespace IUDICO.DataModel.ImportManagers
{
    public class PracticeManager
    {
        public static void Import(XmlNode node, int themeRef, ProjectPaths projectPaths)
        {
            XmlNode answerNode = GetAnswerNode(XmlUtility.GetIdentifier(node), projectPaths);

            int rank = GetPageRank(answerNode);

            string tempFileName = Path.Combine(projectPaths.PathToTempCourseFolder, XmlUtility.GetIdentifierRef(node) + FileExtentions.Html);

            var pageTable = StorePageWithoutPageFile(themeRef, XmlUtility.GetIdentifier(node), rank);

            int fileID = SearchForResources(node, pageTable.ID, projectPaths.PathToTempCourseFolder);

            AddPageFileToPage(pageTable, fileID);
        }

        private static int SearchForResources(XmlNode node, int pageRef, string courseTempPath)
        {
            XmlNode resource = XmlUtility.GetNode(node, "//ns:resource[@identifier='" + node.Attributes["identifierref"].Value + "']");
            string filePath = resource.Attributes["href"].Value;
            int fileID = 0;

            foreach (XmlNode childNode in resource.ChildNodes)
            {
                if (XmlUtility.IsFile(childNode))
                {
                    TblFiles file = StoreFile(childNode, pageRef, Path.Combine(courseTempPath, childNode.Attributes["href"].Value));

                    if (filePath == file.Name)
                    {
                        fileID = file.ID;
                    }
                }
            }

            return fileID;
        }

        private static TblFiles StoreFile(XmlNode node, int pageRef, string filePath)
        {
            var reader = new StreamReader(filePath);
            byte[] file = Encoding.Unicode.GetBytes(reader.ReadToEnd());

            var f = new TblFiles
            {
                PageRef = pageRef,
                File = file,
                Name = node.Attributes["href"].Value,
                IsDirectory = false,
            };

            ServerModel.DB.Insert(f);

            return f;
        }

        private static TblPages StorePageWithoutPageFile(int themaRef, string name, int rank)
        {
            var p = new TblPages
            {
                ThemeRef = themaRef,
                PageName = name,
                PageTypeRef = (int)FX_PAGETYPE.Practice,
                PageRank = rank
            };

            ServerModel.DB.Insert(p);

            return p;
        }

        private static XmlNode GetAnswerNode(string pageName, ProjectPaths projectPaths)
        {
            var answers = new XmlDocument();
            answers.Load(projectPaths.PathToAnswerXml);
            if (answers.DocumentElement != null)
                return FindItem(answers.DocumentElement.FirstChild, pageName);

            return null;
        }

        private static XmlNode FindItem(XmlNode node, string pageName)
        {
            foreach (XmlNode currNode in node.ChildNodes)
            {
                if (XmlUtility.GetId(currNode) != null && XmlUtility.GetId(currNode).Equals(pageName))
                {
                    return currNode;
                }
            }
            return null;
        }

        private static int GetPageRank(XmlNode node)
        {
            return int.Parse(node.LastChild.InnerText);
        }

        private static WebPage CreateAspControl(string tempFileName, int pageRef, XmlNode answerNode, string pathToTempCourseFolder)
        {
            var page = new WebPage(tempFileName);
            page.TransformToAspControl(pageRef, answerNode, pathToTempCourseFolder);
            return page;
        }

        private static void AddPageFileToPage(TblPages page, int fileID)
        {
            page.PageFile = fileID.ToString();

            ServerModel.DB.Update(page);
        }
    
    }
}