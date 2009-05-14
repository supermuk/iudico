using System;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.WebTest;

namespace IUDICO.DataModel.Common.TestingUtils
{
    static class TestManager
    {
        public static void StartTesting(Test t)
        {
            var ua = ExtractAndSaveAnswerFromTest(t);

            if (ua.IsCompiledAnswer)
                CompilationTestManager.GetNewManager(ua).StartCompilation();
        }

        private static TblUserAnswers ExtractAndSaveAnswerFromTest(Test t)
        {
            var ua = new TblUserAnswers
                         {
                             QuestionRef = t.Id,
                             Date = DateTime.Now,
                             UserRef = ((CustomUser)Membership.GetUser()).ID,
                             UserAnswer = t.UserAnswer,
                             IsCompiledAnswer = t is CompiledTest,
                             AnswerTypeRef = FxAnswerType.UserAnswer.ID
                         };

            ServerModel.DB.Insert(ua);

            return ua;
        }
    }
}