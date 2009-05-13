using System;
using System.IO;
using System.Web;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.Common.TestRequestUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    class PracticeHandler : PageHandler
    {
        private const string TestsFolder = "Tests";

        public override void ProcessRequest(HttpContext context)
        {
            TblPages page = GetPage(context);
            var pathToTests = Path.Combine(context.Request.PhysicalApplicationPath, TestsFolder);
            
            CreateTestsDirectory(pathToTests);

            var pageFileName = page.PageName + Guid.NewGuid() + FileExtentions.Aspx;
            var path = Path.Combine(pathToTests, pageFileName);
           
            string url = GetUrl(context);

            var requestBuilder =
                TestRequestBuilder.NewRequestForPage(CreateUrlForPage(pageFileName, url), page.ID)
                .ExtractParametersFromExistedRequest(context.Request);

            WritePageToFile(page, path);
            context.Response.Redirect(requestBuilder.Build());
        }

        private static string CreateUrlForPage(string pageFileName, string url)
        {
            return string.Format("{0}/{1}/{2}", url, TestsFolder, pageFileName);
        }

        private static void CreateTestsDirectory(string pathToTests)
        {
            if (!Directory.Exists(pathToTests))
                Directory.CreateDirectory(pathToTests);
        }

        private static string GetUrl(HttpContext context)
        {
            var url = context.Request.Url.ToString().Remove(context.Request.Url.ToString().LastIndexOf("/"));
            url = url.Remove(url.LastIndexOf("/"));
            return url;
        }

        private static TblPages GetPage(HttpContext context)
        {
            var practicePageId = int.Parse(context.Request[pageIdRequestParameter]);
            return ServerModel.DB.Load<TblPages>(practicePageId);
        }

        private static void WritePageToFile(TblPages page, string path)
        {
            var aspxPageText = StudentEncoding.GetEncoding().GetString(page.PageFile.ToArray());
            
            File.WriteAllText(path, ChangeImageUrl(aspxPageText, page));
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
