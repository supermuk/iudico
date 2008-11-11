using System.Text;
using System.Web;
using IUDICO.DataModel.Dao;

namespace IUDICO.DataModel.HttpHandlers
{
    class TheoryHandler : PageHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var theoryPageId = int.Parse(context.Request.QueryString[pageIdRequestParameter]);
            var pe = DaoFactory.PageDao.Select(theoryPageId);
            var html = Encoding.UTF8.GetString(pe.PageFile);
            context.Response.Write(changeImageUrl(html, theoryPageId));
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
