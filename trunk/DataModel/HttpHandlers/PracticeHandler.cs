using System;
using System.IO;
using System.Text;
using System.Web;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    public class PracticeHandler : PageHandlerBase, IHttpHandler
    {
        private const string TestsFolder = "Tests";

        public void ProcessRequest(HttpContext context)
        {
            // TODO: Check security

            TblPages page = GetPage(context);
            var pathToTests = Path.Combine(context.Request.PhysicalApplicationPath, TestsFolder);
            
            EnsureDirectoryExists(pathToTests);

            var pageFileName = page.PageName + Guid.NewGuid() + FileExtentions.Aspx;
            var path = Path.Combine(pathToTests, pageFileName);
            File.WriteAllText(path, ChangeImageUrl(Encoding.Unicode.GetString(page.PageFile.ToArray()), page), Encoding.Unicode);

            var virtualPath = "~//" + TestsFolder + "//" + pageFileName;
            context.Server.Execute(virtualPath);

            // TODO: Fix renewing page on postback and uncomment following line:
            //File.Delete(path);
        }

        private static void EnsureDirectoryExists(string fullPath)
        {
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
        }

        private static TblPages GetPage(HttpContext context)
        {
            var practicePageId = int.Parse(context.Request[pageIdRequestParameter]);
            return ServerModel.DB.Load<TblPages>(practicePageId);
        }
    }
}
