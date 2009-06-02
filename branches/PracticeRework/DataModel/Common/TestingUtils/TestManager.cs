using System;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common.TestingUtils
{
    static class TestManager
    {
        public static void StartTesting()
        {
            var ua = ExtractAndSaveAnswerFromTest();

            if (ua.IsCompiledAnswer)
                CompilationTestManager.GetNewManager(ua).StartCompilation();
        }

        private static TblUserAnswers ExtractAndSaveAnswerFromTest()
        {
            var ua = new TblUserAnswers
                         {
                             //QuestionRef = t.Id,
                             Date = DateTime.Now,
                             UserRef = ((CustomUser)Membership.GetUser()).ID,
                             //UserAnswer = t.UserAnswer,
                            // IsCompiledAnswer = t is CompiledTest,
                             AnswerTypeRef = FxAnswerType.UserAnswer.ID
                         };

            ServerModel.DB.Insert(ua);

            return ua;
        }
    }
}