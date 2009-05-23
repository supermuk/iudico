namespace IUDICO.DataModel.Common.TestRequestUtils
{
    public class TestRequestBuilder
    {
        private readonly string _extention;

        private TestSessionType _testSessionType = TestSessionType.Ordinary;

        private readonly int _pageId;

        private int _themeId;

        private int _pageIndex;

        private int _curriculumnId;

        private int _stageId;

        private int _userId;
         
        private string _pagesIds = string.Empty;


        public static TestRequestBuilder NewRequestForHandler(int pageId, string extension)
        {
            return new TestRequestBuilder(extension, pageId);
        }

        private TestRequestBuilder(string extention, int pageId)
        {
            _extention = extention;
            _pageId = pageId;
        }

        public TestRequestBuilder AddTestSessionType(TestSessionType parameter)
        {
            _testSessionType = parameter;
            return this;
        }

        public TestRequestBuilder AddThemeId(int parameter)
        {
            _themeId = parameter;
            return this;
        }

        public TestRequestBuilder AddPageIndex(int parameter)
        {
            _pageIndex = parameter;
            return this;
        }

        public TestRequestBuilder AddCurriculumnId(int parameter)
        {
            _curriculumnId = parameter;
            return this;
        }

        public TestRequestBuilder AddStageId(int parameter)
        {
            _stageId = parameter;
            return this;
        }

        public TestRequestBuilder AddPagesIds(string parameter)
        {
            _pagesIds = parameter;
            return this;
        }

        public TestRequestBuilder AddUserId(int parameter)
        {
            _userId = parameter;
            return this;
        }

        public string Build()
        {
            return string.Format("IudicoPage{0}?PageId={1}&CurriculumnId={2}&StageId={3}&ThemeId={4}&PagesIds={5}&PageIndex={6}&TestSessionType={7}&UserId={8}",
                                _extention, _pageId, _curriculumnId, _stageId, _themeId, _pagesIds, _pageIndex, (int)_testSessionType, _userId);
        }
    }
}