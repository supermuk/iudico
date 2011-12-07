using System.Collections.Generic;
using System.IO;

namespace IUDICO.CourseManagement.Helpers
{
    public static class FileHelper
    {
        public static void FileCopy(string fromPath, string toPath, bool overwrite)
        {
            File.Copy(fromPath, toPath, overwrite);
        }

        public static void FileDelete(string path)
        {
            File.Delete(path);
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static string FileRead(string path)
        {
            return File.ReadAllText(path);
        }

        public static void FileWrite(string path, string data)
        {
            File.WriteAllText(path, data);
        }

        public static void DirectoryCreate(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void DirectoryCopy(string fromPath, string toPath)
        {
            foreach (var dirPath in Directory.GetDirectories(fromPath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(fromPath, toPath));
            }

            foreach (var newPath in Directory.GetFiles(fromPath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(fromPath, toPath));
            }
        }

        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static IEnumerable<string> DirectoryGetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}