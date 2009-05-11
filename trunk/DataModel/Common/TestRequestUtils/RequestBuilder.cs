using System;
using System.Web;
using IUDICO.DataModel.Common.ImportUtils;

namespace IUDICO.DataModel.Common.TestRequestUtils
{
    public class RequestBuilder
    {
        private readonly string _extention;

        private TestSessionType _testSessionType = TestSessionType.Ordinary;

        private readonly string _pageUrl;

        private int _pageId;

        private int _themeId;

        private int _pageIndex;

        private int _curriculumnId;

        private int _stageId;
         
        private string _pagesIds = string.Empty;


        public static RequestBuilder NewRequestForHandler(int pageId)
        {
            return new RequestBuilder("IudicoPage", FileExtentions.IudicoPracticePage, pageId);
        }

        public static RequestBuilder NewRequest(string pageUrl, int pageId)
        {
            return new RequestBuilder(pageUrl, FileExtentions.NoExtention, pageId);
        }

        public static RequestBuilder NewRequestForHandler(string extention, int pageId)
        {
            return new RequestBuilder("IudicoPage", extention, pageId);
        }

        private RequestBuilder(string pageUrl, string extention, int pageId)
        {
            _pageUrl = pageUrl;
            _extention = extention;
            _pageId = pageId;
        }

        public RequestBuilder AddTestSessionType(TestSessionType parameter)
        {
            _testSessionType = parameter;
            return this;
        }

        public RequestBuilder AddThemeId(int parameter)
        {
            _themeId = parameter;
            return this;
        }

        public RequestBuilder AddPageIndex(int parameter)
        {
            _pageIndex = parameter;
            return this;
        }

        public RequestBuilder AddCurriculumnId(int parameter)
        {
            _curriculumnId = parameter;
            return this;
        }

        public RequestBuilder AddStageId(int parameter)
        {
            _stageId = parameter;
            return this;
        }

        public RequestBuilder AddPagesIds(string parameter)
        {
            _pagesIds = parameter;
            return this;
        }

        public RequestBuilder ExtractParametersFromExistedRequest(HttpRequest request)
        {
            _pageId = Convert.ToInt32(request["PageId"]);
            _curriculumnId = Convert.ToInt32(request["CurriculumnId"]);
            _stageId = Convert.ToInt32(request["StageId"]);
            _themeId = Convert.ToInt32(request["ThemeId"]);
            _pagesIds = request["PagesIds"];
            _pageIndex = Convert.ToInt32(request["PageIndex"]);
            _testSessionType = (TestSessionType) Convert.ToInt32(request["TestSessionType"]);

            return this;
        }

        public string Build()
        {
            return string.Format("{0}{1}?PageId={2}&CurriculumnId={3}&StageId={4}&ThemeId={5}&PagesIds={6}&PageIndex={7}&TestSessionType={8}",
                                _pageUrl, _extention, _pageId, _curriculumnId, _stageId, _themeId, _pagesIds, _pageIndex, (int)_testSessionType);
        }
    }
}