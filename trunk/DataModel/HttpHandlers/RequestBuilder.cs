namespace IUDICO.DataModel.HttpHandlers
{
    public class RequestBuilder
    {
        private readonly string _pageUrl;

        private string _submit = "false";

        private string _answers = "false";

        private string _pageId = "0";

        private string _themeId = "0";

        private string _pageIndex = "0";

        private string _curriculumnName = string.Empty;

        private string _stageName = string.Empty;

        private string _shiftedPagesIds = string.Empty;

        private RequestBuilder(string pageUrl)
        {
            _pageUrl = pageUrl;
        }

        public static RequestBuilder NewRequest(string pageUrl)
        {
            return new RequestBuilder(pageUrl);
        }

        public RequestBuilder AddSubmit(string parameter)
        {
            _submit = parameter;
            return this;
        }

        public RequestBuilder AddAnswers(string parameter)
        {
            _answers = parameter;
            return this;
        }

        public RequestBuilder AddThemeId(string parameter)
        {
            _themeId = parameter;
            return this;
        }

        public RequestBuilder AddPageIndex(string parameter)
        {
            _pageIndex = parameter;
            return this;
        }

        public RequestBuilder AddPageId(string parameter)
        {
            _pageId = parameter;
            return this;
        }

        public RequestBuilder AddCurriculumnName(string parameter)
        {
            _curriculumnName = parameter;
            return this;
        }

        public RequestBuilder AddStageName(string parameter)
        {
            _stageName = parameter;
            return this;
        }

        public RequestBuilder AddShiftedPagesIds(string parameter)
        {
            _shiftedPagesIds = parameter;
            return this;
        }

        public string BuildRequestForTest()
        {
            if (_answers.Equals("correct") || _answers.Equals("user"))
                _submit = "false";

            return string.Format("{0}?Submit={1}&Answers={2}&ThemeId={3}&PageIndex={4}&CurriculumnName={5}&StageName={6}&ShiftedPagesIds={7}",
                _pageUrl, _submit, _answers, _themeId, _pageIndex, _curriculumnName, _stageName, _shiftedPagesIds);
        }

        public string BuildRequestForHandler()
        {
            return string.Format("{0}?PageId={1}&Submit={2}&Answers={3}&ThemeId={4}&PageIndex={5}&CurriculumnName={6}&StageName={7}&ShiftedPagesIds={8}",
                _pageUrl, _pageId, _submit, _answers, _themeId, _pageIndex, _curriculumnName, _stageName, _shiftedPagesIds);
        }
    }
}
