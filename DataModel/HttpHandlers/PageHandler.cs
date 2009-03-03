using System.Collections.Generic;
using System.Text.RegularExpressions;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    abstract class PageHandler : IudicoHttpHandler
    {
        private const string imageRegexGroup = "image";
        private const string urlRegexGroup = "url";
        private const string folderRegexGroup = "folder";
        private static readonly string imageFileRequest = string.Format("DisplayImage.iif?{0}=", imageIdRequestParameter);

        protected static string ChangeImageUrl(string pageText, TblPages page)
        {
            var imageUrlRegex = new Regex(string.Format(@"src=""(?<{0}>(?<{1}>\w+.files)/(?<{2}>\w+.\w+))""", urlRegexGroup, folderRegexGroup, imageRegexGroup));
            MatchCollection matches = imageUrlRegex.Matches(pageText);
            foreach (Match m in matches)
            {
                string imageName = m.Groups[imageRegexGroup].Value;
                string folderName = m.Groups[folderRegexGroup].Value;

                var list = ServerModel.DB.LookupIds<TblFiles>(page, null);
                var files = ServerModel.DB.Load<TblFiles>(list);

                
                string newUrl = string.Concat(imageFileRequest, FindFileId(files, folderName, imageName));
                pageText = pageText.Replace(m.Groups[urlRegexGroup].Value, newUrl);
            }

            return pageText;
        }

        private static int FindFolderId(IEnumerable<TblFiles> files, string folderName)
        {
            foreach (var folder in files)
            {
                if (folder.Name == folderName && (bool)folder.IsDirectory)
                {
                    return folder.ID;
                }
            }
            return 0; 
        }

        private static int FindFileId(IEnumerable<TblFiles> files, string folderName, string imageName)
        {
            int folderId = FindFolderId(files, folderName);

            foreach (var file in files)
            {
                if(file.Name == imageName && file.PID == folderId)
                {
                    return file.ID;
                }
            }
            return 0;
        }
    }
}
