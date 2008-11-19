using System.IO;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class PageManager
    {
        protected static byte[] GetByteFile(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            var br = new BinaryReader(fs);
            byte[] res = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return res;
        }

        protected static void StoreFiles(int pageRef, string pageFileName)
        {
            string directoryName = pageFileName.Replace(FileExtentions.Html, FileExtentions.WordHtmlFolder);
            
            var d = new DirectoryInfo(directoryName);
            
            int dirID = StoreFolder(directoryName, pageRef);

            foreach (var file in d.GetFiles())
            {
                StoreFile(dirID, file, pageRef);
            }
        }

        private static void StoreFile(int dirID, FileSystemInfo file, int pageRef)
        {
            var f = new TblFiles
                        {
                            Name = file.Name,
                            IsDirectory = false,
                            PID = dirID,
                            PageRef = pageRef,
                            File = GetByteFile(file.FullName)
                        };

            ServerModel.DB.Insert(f);
        }

        private static int StoreFolder(string directoryName, int pageRef)
        {
            var f = new TblFiles
                        {
                            Name = directoryName,
                            IsDirectory = true,
                            PageRef = pageRef
                        };

            ServerModel.DB.Insert(f);

            return f.ID;
        }
    }
}
