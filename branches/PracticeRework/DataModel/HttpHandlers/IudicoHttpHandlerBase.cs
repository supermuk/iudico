namespace IUDICO.DataModel.HttpHandlers
{
    public class IudicoHttpHandlerBase
    {
        protected const string pageIdRequestParameter = "pageId";
        protected const string imageIdRequestParameter = "imageId";

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
