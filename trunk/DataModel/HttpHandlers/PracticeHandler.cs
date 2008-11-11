using System.Text;
using System.Web;
using IUDICO.DataModel.Dao;

namespace IUDICO.DataModel.HttpHandlers
{
    class PracticeHandler : PageHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var practicePageId = int.Parse(context.Request.QueryString[pageIdRequestParameter]);
            var pe = DaoFactory.PageDao.Select(practicePageId);
            var aspx = Encoding.UTF8.GetString(pe.PageFile);
            context.Response.Write(aspx); // Sorry but this don't work
            //TODO: make this work (compile aspx to html)
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
