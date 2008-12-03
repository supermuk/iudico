using System.IO;
using System.Text;
using System.Web;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    class PracticeHandler : PageHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var testPage = "Tests";
            var practicePageId = int.Parse(context.Request.QueryString[pageIdRequestParameter]);
            var page = ServerModel.DB.Load<TblPages>(practicePageId);
            string pathToTests = Path.Combine(context.Request.PhysicalApplicationPath, testPage);
            if (!Directory.Exists(pathToTests))
                Directory.CreateDirectory(pathToTests);
            var path = Path.Combine(pathToTests, page.PageName);
           
            string url = context.Request.Url.ToString().Remove(context.Request.Url.ToString().LastIndexOf("/"));
            var aspx = Encoding.UTF8.GetString(page.PageFile.ToArray());
            File.WriteAllText(path, changeImageUrl(aspx, page));
            context.Response.Redirect(string.Format("{0}/{1}/{2}", url, testPage, page.PageName));
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
