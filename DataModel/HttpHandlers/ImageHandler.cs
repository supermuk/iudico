using System.IO;
using System.Web;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.HttpHandlers
{
    class ImageHandler : IudicoHttpHandler 
    {
        public override void ProcessRequest(HttpContext context)
        {
            var imageFileId = int.Parse(context.Request[imageIdRequestParameter]);
            TblFiles files = ServerModel.DB.Load<TblFiles>(imageFileId);
            context.Response.ContentType = Path.GetExtension(files.Name);
            context.Response.OutputStream.Write(files.File.ToArray(), 0, files.File.Length);
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
