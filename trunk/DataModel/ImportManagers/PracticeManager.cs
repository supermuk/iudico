using System.IO;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.WebControl;

namespace IUDICO.DataModel.ImportManagers
{
    public class PracticeManager
    {
        public static void Import(XmlNode node, int themeRef, ProjectPaths projectPaths)
        {
            string pageName = XmlUtility.getIdentifier(node);

            XmlNode answerNode = GetAnswerNode(pageName, projectPaths);

            int rank = GetPageRank(answerNode);

            string pageFileName = XmlUtility.getIdentifierRef(node) + FileExtentions.Html;
            string tempFileName = Path.Combine(projectPaths.PathToTempCourseFolder, pageFileName);
            string fileName = tempFileName.Replace(FileExtentions.Html, FileExtentions.Aspx);

            var pageTable = StorePageWithoutPageFile(themeRef, pageName, rank);
            
            WebPage webPage = CreateAspxPage(tempFileName, Path.GetFileNameWithoutExtension(fileName), pageTable.ID, answerNode, projectPaths.PathToTempCourseFolder);

            AddPageFileToPage(pageTable, webPage.ByteRepresentation);
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
                if (XmlUtility.getId(currNode) != null && XmlUtility.getId(currNode).Equals(pageName))
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

        private static WebPage CreateAspxPage(string tempFileName, string fileName, int pageRef, XmlNode answerNode, string pathToTempCourseFolder)
        {
            var page = new WebPage(tempFileName);
            page.TransformToAspx(fileName, pageRef, answerNode, pathToTempCourseFolder);
            return page;
        }

        private static void AddPageFileToPage(TblPages page, byte[] pageFile)
        {
            page.PageFile = pageFile;

            ServerModel.DB.Update(page);
        }
    
    }
}