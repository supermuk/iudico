using System.IO;
using System.Web;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    public class ImageHandler : IudicoHttpHandlerBase, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // TODO: Check security

            var imageFileId = int.Parse(context.Request[imageIdRequestParameter]);
            var files = ServerModel.DB.Load<TblFiles>(imageFileId);
            context.Response.ContentType = Path.GetExtension(files.Name);
            context.Response.OutputStream.Write(files.File.ToArray(), 0, files.File.Length);
        }
    }
}
