using System.Web;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    class TheoryHandler : PageHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var theoryPageId = int.Parse(context.Request[pageIdRequestParameter]);
            var page = ServerModel.DB.Load<TblPages>(theoryPageId);
            var html = StudentEncoding.GetEncoding().GetString(page.PageFile.ToArray());
            context.Response.Write(ChangeImageUrl(html, page));
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
