using System.Web;

namespace IUDICO.DataModel.HttpHandlers
{
    internal abstract class IudicoHttpHandler : IHttpHandler
    {
        protected const string pageIdRequestParameter = "PageId";
        protected const string imageIdRequestParameter = "ImageId";
        public abstract bool IsReusable { get; }
        public abstract void ProcessRequest(HttpContext context);
    }
}
