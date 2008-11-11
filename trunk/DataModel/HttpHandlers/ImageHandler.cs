using System.IO;
using System.Web;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.HttpHandlers
{
    class ImageHandler : IudicoHttpHandler 
    {
        public override void ProcessRequest(HttpContext context)
        {
            var imageFileId = int.Parse(context.Request.QueryString[imageIdRequestParameter]);
            FilesEntity fe = DaoFactory.FilesDao.Select(imageFileId);
            context.Response.ContentType = Path.GetExtension(fe.Name);
            context.Response.OutputStream.Write(fe.File, 0, fe.File.Length);
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
