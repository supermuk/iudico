using System.IO;
using System.Web;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;


namespace IUDICO.DataModel.HttpHandlers
{
    public class ImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var imageFileId = ImageHandlerHelper.GetImageIdFromRequest(context.Request);
            var files = ServerModel.DB.Load<TblFiles>(imageFileId);
            context.Response.ContentType = Path.GetExtension(files.Name);
            context.Response.OutputStream.Write(files.File.ToArray(), 0, files.File.Length);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
