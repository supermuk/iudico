using System.Collections.Generic;
using System.IO;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.WebControl;

namespace IUDICO.DataModel.ImportManagers
{
    public class PracticeManager : PageManager
    {
        public static void Import(XmlNode node, int themeRef, ProjectPaths projectPaths)
        {
            string pageName = XmlUtility.getIdentifier(node);

            XmlNode answerNode = GetAnswerNode(pageName, projectPaths);

            int rank = GetPageRank(answerNode);

            string pageFileName = XmlUtility.getIdentifierRef(node) + FileExtentions.Html;
            string tempFileName = Path.Combine(projectPaths.PathToTempCourseFolder, pageFileName);
            string fileName = tempFileName.Replace(FileExtentions.Html, FileExtentions.Aspx);



            List<WebControl.WebControl> tests = SaveAspX(tempFileName, fileName);

            int id = Store(themeRef, pageName, GetByteFile(fileName), rank);

            foreach (WebControl.WebControl w in tests)
                if (w is WebCodeSnippet)
                    StoreFiles(id, Path.Combine(projectPaths.PathToTempCourseFolder, w.Name) + FileExtentions.WordHtmlFolder);


            QuestionManager.Import(id, answerNode, tests);
        }

        private static int Store(int themaRef, string name, byte[] bytes, int rank)
        {
            var p = new TblPages
            {
                ThemeRef = themaRef,
                PageName = name,
                PageFile = bytes,
                PageTypeRef = (int)PageTypeEnum.Practice,
                PageRank = rank
            };

            return p.ID;
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

        private static List<WebControl.WebControl> SaveAspX(string tempFileName, string fileName)
        {
            var page = new WebPage(tempFileName);
            return page.SaveAsAsp(fileName);
        }
    }
}