using System.Web;

namespace IUDICO.DataModel.HttpHandlers
{
    internal abstract class IudicoHttpHandler : IHttpHandler
    {
        protected const string pageIdRequestParameter = "pageId";
        protected const string imageIdRequestParameter = "imageId";
        public abstract bool IsReusable { get; }
        public abstract void ProcessRequest(HttpContext context);
    }
}
