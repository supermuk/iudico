using System;
using System.Text;
using System.Web;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.HttpHandlers
{
    public class TheoryHandler : IHttpHandler
    {
        protected const string PageIdRequestParameter = "pageId";


        public void ProcessRequest(HttpContext context)
        {
            var theoryPageId = int.Parse(context.Request[PageIdRequestParameter]);
            var page = ServerModel.DB.Load<TblPages>(theoryPageId);

            CheckPageType(page);

            var html = Encoding.Unicode.GetString(page.PageFile.ToArray());
            context.Response.Write(ImageHandlerHelper.ChangeImageUrl(html, page));
        }

        public bool IsReusable
        {
            get { return false; }
        }

        private static void CheckPageType(TblPages page)
        {
            if(page.PageTypeRef != (int?) FX_PAGETYPE.Theory)
                throw new Exception("Wrong handler for page");
        }
    }
}
