using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using System.Text.RegularExpressions;
using System.Web;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public static class ImageHandlerHelper
    {
        private const string ImageRegexGroup = "image";
        private const string UrlRegexGroup = "url";
        private const string FolderRegexGroup = "folder";
        private const string ImageIdRequestParameter = "imageId";
        private const string ImageFileRequest = "DisplayImage.iif?" + ImageIdRequestParameter + "=";

        public static string ChangeImageUrl(string pageText, TblPages page)
        {
            var imageUrlRegex = new Regex(string.Format(@"src=""(?<{0}>(?<{1}>\w+.files)/(?<{2}>\w+.\w+))""", UrlRegexGroup, FolderRegexGroup, ImageRegexGroup));
            MatchCollection matches = imageUrlRegex.Matches(pageText);
            foreach (Match m in matches)
            {
                string imageName = m.Groups[ImageRegexGroup].Value;
                string folderName = m.Groups[FolderRegexGroup].Value;

                var list = ServerModel.DB.LookupIds<TblFiles>(page, null);
                var files = ServerModel.DB.Load<TblFiles>(list);


                string newUrl = string.Concat(ImageFileRequest, FindFileId(files, folderName, imageName));
                pageText = pageText.Replace(m.Groups[UrlRegexGroup].Value, newUrl);
            }

            return pageText;
        }

        public static int GetImageIdFromRequest(HttpRequest req)
        {
            return int.Parse(req[ImageIdRequestParameter]);
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
                if (file.Name == imageName && file.PID == folderId)
                {
                    return file.ID;
                }
            }
            return 0;
        }
    }
}
