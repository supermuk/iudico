using System;
using System.IO;

namespace IUDICO.DataModel.Common.ImportUtils
{
    /// <summary>
    /// Constants of necessary pathes to files
    /// </summary>
    public class ProjectPaths
    {
        public string PathToManifestXml;
        public string PathToAnswerXml;
        public string PathToTempCourseFolder;
        public string PathToCourseZipFile;
        public string PathToTemp;

        public void Initialize(string fileName)
        {
            PathToTemp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(PathToTemp);
            PathToCourseZipFile = Path.Combine(PathToTemp, fileName);
            PathToTempCourseFolder = Path.Combine(PathToTemp, Path.GetFileNameWithoutExtension(fileName));
            PathToManifestXml = Path.Combine(PathToTempCourseFolder, "imsmanifest.xml");
            PathToAnswerXml = Path.Combine(PathToTempCourseFolder, "answers.xml");
        }
    }
}