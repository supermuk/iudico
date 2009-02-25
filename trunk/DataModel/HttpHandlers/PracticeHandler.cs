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
            var practicePageId = int.Parse(context.Request.QueryString[pageIdRequestParameter]);
            var page = ServerModel.DB.Load<TblPages>(practicePageId);
            var pathToTests = Path.Combine(context.Request.PhysicalApplicationPath, testsFolder);
            
            if (!Directory.Exists(pathToTests))
                Directory.CreateDirectory(pathToTests);

            var pageFileName = page.PageName + FileExtentions.Aspx;
            var path = Path.Combine(pathToTests, pageFileName);
           
            var url = context.Request.Url.ToString().Remove(context.Request.Url.ToString().LastIndexOf("/"));
            url = url.Remove(url.LastIndexOf("/"));    
            
            var aspxPageText = Encoding.GetEncoding(1251).GetString(page.PageFile.ToArray());
            
            File.WriteAllText(path, changeImageUrl(aspxPageText, page));
            context.Response.Redirect(string.Format("{0}/{1}/{2}", url, testsFolder, pageFileName));
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
