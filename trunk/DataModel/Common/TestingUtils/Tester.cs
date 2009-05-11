using System.Collections.Generic;
using System.Web;
using IUDICO.DataModel.WebTest;

namespace IUDICO.DataModel.Common.TestingUtils
{
    /// <summary>
    /// Summary description for PageTest
    /// </summary>
    public class Tester
    {
        private readonly List<Test> _tests = new List<Test>();

        public void AddTest(Test newTest)
        {
            _tests.Add(newTest);
        }

        public void TryToSubmit(HttpRequest request)
        {
            Submit();
        }

        private void Submit()
        {
            foreach (Test t in _tests)
                TestManager.StartTesting(t);
        }

        public void NextTestPage(HttpResponse response, HttpRequest request)
        {
            int theme = int.Parse(request["ThemeId"]);
            int page = int.Parse(request["PageIndex"]);
            int nextPage = page + 1;

            string pageUrl = string.Format("../Student/OpenTest.aspx?OpenThema={0}&PageIndex={1}&CurriculumnId={2}&StageId={3}&PagesIds={4}",
                                           theme, nextPage, request["CurriculumnId"], request["StageId"], request["PagesIds"]);

            response.Write(string.Format("<script>window.open('{0}','_parent','copyhistory=no');</script>", pageUrl));
        }
    }
}