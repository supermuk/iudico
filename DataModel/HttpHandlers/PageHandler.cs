using System.Text.RegularExpressions;

namespace IUDICO.DataModel.HttpHandlers
{
    abstract class PageHandler : IudicoHttpHandler
    {
        private const string imageRegexGroup = "image";
        private const string urlRegexGroup = "url";
        private static readonly string imageFileRequest = string.Format("DisplayImage.iif?{0}=", imageIdRequestParameter);

        protected static string changeImageUrl(string pageText, int pageRef)
        {
            var imageUrlRegex = new Regex(string.Format(@"src=""(?<{0}>(\w+.files/(?<{1}>\w+.\w+)))""", urlRegexGroup, imageRegexGroup));
            MatchCollection matches = imageUrlRegex.Matches(pageText);
            foreach (Match m in matches)
            {
                string imageName = m.Groups[imageRegexGroup].Value;
               // int fileId = DaoFactory.FilesDao.SelectId(pageRef, imageName);
               // string newUrl = string.Concat(imageFileRequest, fileId);
               // pageText = pageText.Replace(m.Groups[urlRegexGroup].Value, newUrl);
            }

            return pageText;
        }
    }
}
