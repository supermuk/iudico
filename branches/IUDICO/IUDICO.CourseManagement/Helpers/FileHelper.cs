using System.Collections.Generic;
using System.IO;

namespace IUDICO.CourseManagement.Helpers
{
    public static class FileHelper
    {
        public static void FileCopy(string frompath, string topath, bool overwrite)
        {
            File.Copy(frompath, topath, overwrite);
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

        public static void DirectoryCopy(string frompath, string topath)
        {
            if (!Directory.Exists(frompath))
            {
                return;
            }

            foreach (var dirPath in Directory.GetDirectories(frompath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(frompath, topath));
            }

            foreach (var newPath in Directory.GetFiles(frompath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(frompath, topath));
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