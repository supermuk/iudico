using System.Web;
using IUDICO.DataModel.Common.TestRequestUtils;

namespace IUDICO.DataModel.Common.TestingUtils
{
    /// <summary>
    /// Summary description for PageTest
    /// </summary>
    public class Tester
    {
        public void TryToSubmit(HttpRequest request)
        {
            if (RequestConditionChecker.IsSubmitEnabled(request)) 
                Submit();
        }

        private void Submit()
        {
           // foreach (Test t in _tests)
                //TestManager.StartTesting(t);
        }

        public void NextTestPage(HttpResponse response, HttpRequest request)
        {
            int theme = TestRequestParser.GetThemeId(request);
            int page = TestRequestParser.GetPageIndex(request);
            
            int nextPage = page + 1;

            string pageUrl = string.Format("../Student/OpenTest.aspx?OpenThema={0}&PageIndex={1}&CurriculumnId={2}&StageId={3}&PagesIds={4}",
                                           theme, nextPage, TestRequestParser.GetCurriculumnId(request), TestRequestParser.GetStageId(request), TestRequestParser.GetTestPagesIds(request));

            response.Write(string.Format("<script>window.open('{0}','_parent','copyhistory=no');</script>", pageUrl));
        }
    }
}