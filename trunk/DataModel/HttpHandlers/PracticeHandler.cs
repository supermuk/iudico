using System.IO;
using System.Text;
using System.Web;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    class PracticeHandler : PageHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var testsFolder = "Tests";

            TblPages page = GetPage(context);
            var pathToTests = Path.Combine(context.Request.PhysicalApplicationPath, testsFolder);
            
            CreateTestsDirectory(pathToTests);

            var pageFileName = page.PageName + FileExtentions.Aspx;
            var path = Path.Combine(pathToTests, pageFileName);
           
            string url = GetUrl(context);

            var requestBuilder = RequestBuilder.newRequest(string.Format("{0}/{1}/{2}", url, testsFolder, pageFileName))
                .AddSubmit(context.Request["submit"]).AddAnswers(context.Request["answers"])
                    .AddThemeId(context.Request["themeId"]).AddPageIndex(context.Request["pageIndex"]);

            WritePageToFile(page, path);
            context.Response.Redirect(requestBuilder.BuildRequestForTest());
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
            var aspxPageText = Encoding.GetEncoding(1251).GetString(page.PageFile.ToArray());
            
            File.WriteAllText(path, ChangeImageUrl(aspxPageText, page));
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
