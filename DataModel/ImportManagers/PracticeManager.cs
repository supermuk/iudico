using System.Collections.Generic;
using System.IO;
using System.Xml;
using CourseImport.Dao.Entity;
using CourseImport.WebControl;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.Dao.Entity;
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



            List<WebControl.WebControl> tests = SaveAspX(tempFileName, fileName);

            var pe = new PageEntity(themeRef, pageName, GetByteFile(fileName), PageTypeEnum.Practice, rank);

            Store(pe);

            foreach (WebControl.WebControl w in tests)
                if (w is WebCodeSnippet)
                    StoreFiles(pe.Id, Path.Combine(projectPaths.PathToTempCourseFolder, w.Name) + FileExtentions.WordHtmlFolder);


            QuestionManager.Import(pe.Id, answerNode, tests);
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

        private static byte[] GetByteFile(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            var br = new BinaryReader(fs);
            byte[] res = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return res;
        }

        private static void StoreFiles(int pageRef, string pageFileName)
        {
            string directoryName = pageFileName.Replace(FileExtentions.Html, FileExtentions.WordHtmlFolder);
            var d = new DirectoryInfo(directoryName);
            var fe = FilesEntity.newDirectory(pageRef, Path.GetFileName(directoryName));

            DaoFactory.FilesDao.Insert(fe);

            foreach (FileInfo file in d.GetFiles())
            {
                var f = FilesEntity.newFile(fe.Id, GetByteFile(file.FullName), file.Name);

                DaoFactory.FilesDao.Insert(f);
            }
        }

        private static void Store(PageEntity pe)
        {
            DaoFactory.PageDao.Insert(pe);
        }
    }
}