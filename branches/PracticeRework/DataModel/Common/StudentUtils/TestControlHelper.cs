using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    static class TestControlHelper
    {
        private const string ImageRegexGroup = "image";
        private const string UrlRegexGroup = "url";
        private const string FolderRegexGroup = "folder";
        public const string ImageIdRequestParameter = "imageId";
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




        private static string GetControlString(TblPages page)
        {
            string controlString = Encoding.Unicode.GetString(page.PageFile.ToArray());

            return controlString;
        }

        public static Control GetTheoryControl(TblPages page, Panel p)
        {
            p.ScrollBars = ScrollBars.None;

            return p.Page.ParseControl(string.Format(@"<IFRAME ID=""_iFrame""  width=""100%"" height=""100%"" Runat=""Server""  src=""DisplayTheory.itp?PageId={0}""></IFRAME>", page.ID));
        }

        public static Control GetPracticeControl(TblPages page, Panel p)
        {
            p.ScrollBars = ScrollBars.Auto;

            string aspText = GetControlString(page);

            string aspTextWithCorrectImagesUrls = ChangeImageUrl(aspText, page);

            var control = p.Page.ParseControl(aspTextWithCorrectImagesUrls);

            var divFromPageControl = control.Controls[0];

            return divFromPageControl;
        }
    }
}
