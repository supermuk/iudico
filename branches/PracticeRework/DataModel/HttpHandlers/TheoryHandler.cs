using System;
using System.Text;
using System.Web;
using IUDICO.DataModel.Common.TestRequestUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.HttpHandlers
{
    public class TheoryHandler : PageHandlerBase, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            RequestConditionChecker.IsRequestCorrectForTheory(context.Request);

            var theoryPageId = int.Parse(context.Request[pageIdRequestParameter]);
            var page = ServerModel.DB.Load<TblPages>(theoryPageId);

            CheckPageType(page);

            var html = Encoding.Unicode.GetString(page.PageFile.ToArray());
            context.Response.Write(ChangeImageUrl(html, page));
        }

        private static void CheckPageType(TblPages page)
        {
            if(page.PageTypeRef != (int?) FX_PAGETYPE.Theory)
                throw new Exception("Wrong handler for page");
        }
    }
}
