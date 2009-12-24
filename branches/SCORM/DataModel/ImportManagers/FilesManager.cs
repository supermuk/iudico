using System;
using System.IO;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.ImportManagers
{
    [DBEnum("fxPageTypes")]
    public enum FX_PAGETYPE
    {
        Theory = 1,
        Practice = 2
    }

    public class FilesManager
    {
        public static void StoreAllPageFiles(int pageRef, string pageFileName)
        {
            string directoryName = pageFileName.Replace(FileExtentions.Html, FileExtentions.WordHtmlFolder);

            if (Directory.Exists(directoryName))
            {
                var d = new DirectoryInfo(directoryName);

                int dirId = StoreFolder(directoryName, pageRef);

                foreach (var file in d.GetFiles())
                {
                    StoreFile(dirId, file, pageRef);
                }
            }
        }

        private static void StoreFile(int pid, FileSystemInfo file, int pageRef)
        {
            var f = new TblFiles
                        {
                            Name = file.Name,
                            IsDirectory = false,
                            PageRef = pageRef,
                            File = File.ReadAllBytes(file.FullName),
                            PID = pid
                        };

            ServerModel.DB.Insert(f);
        }

        private static int StoreFolder(string directoryName, int pageRef)
        {
            var f = new TblFiles
                        {
                            Name = Path.GetFileName(directoryName),
                            IsDirectory = true,
                            PageRef = pageRef,
                        };

            ServerModel.DB.Insert(f);

            return f.ID;
        }
    }
}
