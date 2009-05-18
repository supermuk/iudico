using System.Text;
using System.Web;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    public class TheoryHandler : PageHandlerBase, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // TODO: Check security

            var theoryPageId = int.Parse(context.Request[pageIdRequestParameter]);
            var page = ServerModel.DB.Load<TblPages>(theoryPageId);
            var html = Encoding.Unicode.GetString(page.PageFile.ToArray());
            context.Response.Write(ChangeImageUrl(html, page));
        }
    }
}
