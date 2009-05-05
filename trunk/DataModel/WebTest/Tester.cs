using System;
using System.Collections.Generic;
using System.Threading;
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
        private readonly List<Test> _tests = new List<Test>();

        public void AddTest(Test newTest)
        {
            _tests.Add(newTest);
        }

        public void Submit()
        {
            foreach (Test t in _tests)
            {
                var ua = new TblUserAnswers
                             {
                                 QuestionRef = t.Id,
                                 Date = DateTime.Now,
                                 UserRef = ((CustomUser) Membership.GetUser()).ID,
                                 UserAnswer = t.UserAnswer,
                                 IsCompiledAnswer = t is CompiledTest
                             };

                ServerModel.DB.Insert(ua);

                if (ua.IsCompiledAnswer)
                {
                    var ca = new CompilationManager(ua);
                    ca.SetCompilationStatusesToEnqueued();

                    ThreadPool.QueueUserWorkItem(delegate { ca.Compile(); });
                }
            }
        }

        public void NextTestPage(HttpResponse response, HttpRequest request)
        {
            int theme = int.Parse(request["ThemeId"]);
            int page = int.Parse(request["PageIndex"]);
            int nextPage = page + 1;

            string pageUrl = string.Format("../Student/OpenTest.aspx?OpenThema={0}&PageIndex={1}&CurriculumnName={2}&StageName={3}&ShiftedPagesIds={4}",
                theme, nextPage, request["CurriculumnName"], request["StageName"], request["ShiftedPagesIds"]);

            response.Write(string.Format("<script>window.open('{0}','_parent','copyhistory=no');</script>", pageUrl));
        }
    }
}