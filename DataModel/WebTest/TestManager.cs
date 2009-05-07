using System;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.WebTest
{
    static class TestManager
    {
        public static void StartTesting(Test t)
        {
            var ua = ExtractAndSaveAnswerFromTest(t);

            if (ua.IsCompiledAnswer)
                CompilationManager.GetNewManager(ua).StartCompilation();
        }

        private static TblUserAnswers ExtractAndSaveAnswerFromTest(Test t)
        {
            var ua = new TblUserAnswers
            {
                QuestionRef = t.Id,
                Date = DateTime.Now,
                UserRef = ((CustomUser)Membership.GetUser()).ID,
                UserAnswer = t.UserAnswer,
                IsCompiledAnswer = t is CompiledTest
            };

            ServerModel.DB.Insert(ua);

            return ua;
        }
    }
}
