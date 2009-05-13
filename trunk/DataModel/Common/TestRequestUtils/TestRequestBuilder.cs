using System.Web;
using IUDICO.DataModel.Common.ImportUtils;

namespace IUDICO.DataModel.Common.TestRequestUtils
{
    public class TestRequestBuilder
    {
        private readonly string _extention;

        private TestSessionType _testSessionType = TestSessionType.Ordinary;

        private readonly string _pageUrl;

        private int _pageId;

        private int _themeId;

        private int _pageIndex;

        private int _curriculumnId;

        private int _stageId;

        private int _userId;
         
        private string _pagesIds = string.Empty;


        public static TestRequestBuilder NewRequestForHandler(int pageId, string extension)
        {
            return new TestRequestBuilder("IudicoPage", extension, pageId);
        }

        public static TestRequestBuilder NewRequestForPage(string pageUrl, int pageId)
        {
            return new TestRequestBuilder(pageUrl, FileExtentions.NoExtention, pageId);
        }

        private TestRequestBuilder(string pageUrl, string extention, int pageId)
        {
            _pageUrl = pageUrl;
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

        public TestRequestBuilder ExtractParametersFromExistedRequest(HttpRequest request)
        {
            _pageId = TestRequestParser.GetPageId(request);
            _curriculumnId = TestRequestParser.GetCurriculumnId(request);
            _stageId = TestRequestParser.GetStageId(request);
            _themeId = TestRequestParser.GetThemeId(request);
            _pagesIds = TestRequestParser.GetTestPagesIds(request);
            _pageIndex = TestRequestParser.GetPageIndex(request);
            _testSessionType = TestRequestParser.GetTestSessionType(request);
            _userId = TestRequestParser.GetUserId(request);

            return this;
        }

        public string Build()
        {
            return string.Format("{0}{1}?PageId={2}&CurriculumnId={3}&StageId={4}&ThemeId={5}&PagesIds={6}&PageIndex={7}&TestSessionType={8}&UserId={9}",
                                _pageUrl, _extention, _pageId, _curriculumnId, _stageId, _themeId, _pagesIds, _pageIndex, (int)_testSessionType, _userId);
        }
    }
}