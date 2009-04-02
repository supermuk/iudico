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

        private string curriculumnName = string.Empty;

        private string stageName = string.Empty;

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

        public RequestBuilder AddCurriculumnName(string parameter)
        {
            curriculumnName = parameter;
            return this;
        }

        public RequestBuilder AddStageName(string parameter)
        {
            stageName = parameter;
            return this;
        }

        public string BuildRequestForTest()
        {
            if (answers.Equals("correct"))
                submit = "false";

            return string.Format("{0}?Submit={1}&Answers={2}&ThemeId={3}&PageIndex={4}&CurriculumnName={5}&StageName={6}",
                pageUrl, submit, answers, themeId, pageIndex, curriculumnName, stageName);
        }

        public string BuildRequestForHandler()
        {
            return string.Format("{0}?PageId={1}&Submit={2}&Answers={3}&ThemeId={4}&PageIndex={5}&CurriculumnName={6}&StageName={7}",
                pageUrl, pageId, submit, answers, themeId, pageIndex, curriculumnName, stageName);
        }
    }
}
