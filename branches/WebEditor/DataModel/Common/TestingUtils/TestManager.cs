using System;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common.TestingUtils
{
    public static class TestManager
    {
        public static void StartTesting(int questionId, string userAnswer, bool isCompiledTest)
        {
            var ua = SaveAnswer(questionId, userAnswer, isCompiledTest);

            if (ua.IsCompiledAnswer)
                CompilationTestManager.GetNewManager(ua).StartCompilation();
        }

        private static TblUserAnswers SaveAnswer(int questionId, string userAnswer, bool isCompiledTest)
        {
            var ua = new TblUserAnswers
                         {
                             QuestionRef = questionId,
                             Date = DateTime.Now,
                             UserRef = ((CustomUser)Membership.GetUser()).ID,
                             UserAnswer = userAnswer,
                             IsCompiledAnswer = isCompiledTest,
                             AnswerTypeRef = FxAnswerType.UserAnswer.ID
                         };

            ServerModel.DB.Insert(ua);

            return ua;
        }
    }
}