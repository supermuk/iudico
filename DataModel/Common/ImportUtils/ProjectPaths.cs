using System;
using System.IO;

namespace IUDICO.DataModel.Common.ImportUtils
{
    public class ProjectPaths
    {
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
        }
    }
}