namespace IUDICO.DataModel.HttpHandlers
{
    public class RequestBuilder
    {
        private readonly string pageUrl;

        private string submit = "false";

        private string answers = "false";

        private string pageId = "0";

        private string themeId = "0";

        private string pageIndex = "0";

        private RequestBuilder(string pageUrl)
        {
            this.pageUrl = pageUrl;
        }

        public static RequestBuilder newRequest(string pageUrl)
        {
            return new RequestBuilder(pageUrl);
        }

        public RequestBuilder AddSubmit(string parameter)
        {
            submit = parameter;
            return this;
        }

        public RequestBuilder AddAnswers(string parameter)
        {
            answers = parameter;
            return this;
        }

        public RequestBuilder AddThemeId(string parameter)
        {
            themeId = parameter;
            return this;
        }

        public RequestBuilder AddPageIndex(string parameter)
        {
            pageIndex = parameter;
            return this;
        }

        public RequestBuilder AddPageId(string parameter)
        {
            pageId = parameter;
            return this;
        }

        public string BuildRequestForTest()
        {
            if (answers.Equals("correct"))
                submit = "false";

            return string.Format("{0}?submit={1}&answers={2}&themeId={3}&pageIndex={4}",
                pageUrl, submit, answers, themeId, pageIndex);
        }

        public string BuildRequestForHandler()
        {
            return string.Format("{0}?pageId={1}&submit={2}&answers={3}&themeId={4}&pageIndex={5}",
                pageUrl, pageId, submit, answers, themeId, pageIndex);
        }
    }
}
