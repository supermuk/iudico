using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common.ImportUtils
{
    /// <summary>
    /// Puts folder's content to zip archive
    /// </summary>
    /// <param name="zipFileName">Path to result zip file</param>
    /// <param name="folder">Folder to archive</param>
    public static class Zipper
    {
        private const int ReadBufferSize = 2048;

 
        public static void CreateZip([NotNull] string zipFileName, [NotNull] string folder)
        {
#if CHECKERS
            if (!Directory.Exists(folder))
            {
                throw new FireFlyException("Directory '{0}' was not found", folder);
            }
#endif
            using (ZipFile f = ZipFile.Create(zipFileName))
            {
                f.BeginUpdate();
                f.NameTransform = new ZipNameTransform(folder);
                f.UseZip64 = UseZip64.Off;
                foreach (string file in Directory.GetFiles(folder, "*", SearchOption.AllDirectories))
                {
                    f.Add(file);
                }
                f.CommitUpdate();
                f.Close();
            }
        }

        public static void ExtractZipFile([NotNull] string zipFileName, [NotNull] string dirName)
        {
            var data = new byte[ReadBufferSize];
            using (var zipStream = new ZipInputStream(File.OpenRead(zipFileName)))
            {
                ZipEntry entry;
                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    string fullName = Path.Combine(dirName, entry.Name);
                    if (entry.IsDirectory && !Directory.Exists(fullName))
                    {
                        Directory.CreateDirectory(fullName);
                    }
                    else if (entry.IsFile)
                    {
                        string dir = Path.GetDirectoryName(fullName);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        using (FileStream fileStream = File.Create(fullName))
                        {
                            int readed;
                            while ((readed = zipStream.Read(data, 0, ReadBufferSize)) > 0)
                            {
                                fileStream.Write(data, 0, readed);
                            }
                        }
                    }
                }
            }
        }
    }
}