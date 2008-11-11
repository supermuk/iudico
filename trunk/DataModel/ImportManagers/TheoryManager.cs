using System.IO;
using System.Xml;
using CourseImport.Dao.Entity;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.ImportManagers
{
    public enum FileType
    {
        Directory = 1,
        File = 2
    }

    public class TheoryManager
    {
        public static void Import(XmlNode node, int themeId, ProjectPaths projectPaths)
        {
            string pageName = XmlUtility.getIdentifierRef(node) + FileExtentions.Html;
            string fileName  = Path.Combine(projectPaths.PathToTempCourseFolder, pageName);

            byte[] file = GetByteFile(fileName);
            var te = new PageEntity(themeId, pageName, file, PageTypeEnum.Theory);
            Store(te);
            StoreFiles(te.Id, fileName);
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
            var d  = new DirectoryInfo(directoryName);
            var fe = FilesEntity.newDirectory(pageRef, Path.GetFileName(directoryName));
            
            DaoFactory.FilesDao.Insert(fe);
           
            foreach(FileInfo file in d.GetFiles())
            {
                var f = FilesEntity.newFile(fe.Id, GetByteFile(file.FullName), file.Name);

                DaoFactory.FilesDao.Insert(f);
            }
        }

        private static void Store(PageEntity te)
        {
            DaoFactory.PageDao.Insert(te);
        }
    }
}