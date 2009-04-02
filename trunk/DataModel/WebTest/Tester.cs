using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.WebTest
{
    /// <summary>
    /// Summary description for PageTest
    /// </summary>
    public class Tester
    {
        private readonly List<Test> tests = new List<Test>();

        public void AddTest(Test newTest)
        {
            tests.Add(newTest);
        }

        public void Submit()
        {
            foreach (Test t in tests)
            {
                var ua = new TblUserAnswers();
                ua.QuestionRef = t.Id;
                ua.Date = DateTime.Now;
                ua.UserRef = ((CustomUser) Membership.GetUser()).ID;
                ua.UserAnswer = t.UserAnswer;
                ua.IsCompiledAnswer = t is CompiledTest;
                ServerModel.DB.Insert(ua);
                
                if(ua.IsCompiledAnswer)
                    (new CompilationManager()).Compile(ua);
            }
        }

        public void NextTestPage(HttpResponse response, HttpRequest request)
        {
            int theme = int.Parse(request["ThemeId"]);
            int page = int.Parse(request["PageIndex"]);
            int nextPage = page + 1;

            string pageUrl = string.Format("../Student/OpenTest.aspx?OpenThema={0}&PageIndex={1}&CurriculumnName={2}&StageName={3}",
                theme, nextPage, request["CurriculumnName"], request["StageName"]);

            response.Write(string.Format("<script>window.open('{0}','_parent');</script>", pageUrl));
        }
    }
}