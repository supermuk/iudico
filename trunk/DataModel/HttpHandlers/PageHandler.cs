using System.Collections.Generic;
using System.Text.RegularExpressions;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    abstract class PageHandler : IudicoHttpHandler
    {
        private const string imageRegexGroup = "image";
        private const string urlRegexGroup = "url";
        private static readonly string imageFileRequest = string.Format("DisplayImage.iif?{0}=", imageIdRequestParameter);

        protected static string changeImageUrl(string pageText, TblPages page)
        {
            var imageUrlRegex = new Regex(string.Format(@"src=""(?<{0}>(\w+.files/(?<{1}>\w+.\w+)))""", urlRegexGroup, imageRegexGroup));
            MatchCollection matches = imageUrlRegex.Matches(pageText);
            foreach (Match m in matches)
            {
                string imageName = m.Groups[imageRegexGroup].Value;
                var list = ServerModel.DB.LookupIds<TblFiles>(page);
                var files = ServerModel.DB.Load<TblFiles>(list);

                
                string newUrl = string.Concat(imageFileRequest, FindFileId(files, imageName));
                pageText = pageText.Replace(m.Groups[urlRegexGroup].Value, newUrl);
            }

            return pageText;
        }

        private static int FindFileId(IEnumerable<TblFiles> files, string imageName)
        {
            foreach (var file in files)
            {
                if(file.Name == imageName)
                {
                    return file.ID;
                }
            }
            return 0;
        }
    }
}
