using System.Text;
using System.Web;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    class TheoryHandler : PageHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var theoryPageId = int.Parse(context.Request[pageIdRequestParameter]);
            var page = ServerModel.DB.Load<TblPages>(theoryPageId);
            var html = Encoding.GetEncoding(1251).GetString(page.PageFile.ToArray());
            context.Response.Write(changeImageUrl(html, page));
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
