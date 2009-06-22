using System.Collections.Generic;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.TestingUtils
{
    public class AnswerFiller
    {
        public static string GetUserAnswer(int questionId, int userId)
        {
            TblQuestions q = GetQuestion(questionId);

            return FindAnswer(userId, q);
        }

        public static string GetCorrectAnswer(int questionId)
        {
            TblQuestions q = GetQuestion(questionId);

            if (ServerModel.User.Current.Islector())
                return q.CorrectAnswer;

            return string.Empty;
        }

        private static TblQuestions GetQuestion(int questionId)
        {
            return ServerModel.DB.Load<TblQuestions>(questionId);
        }

        private static string FindAnswer(int userId, TblQuestions question)
        {
            var answersForQuestion =
                ServerModel.DB.Load<TblUserAnswers>(ServerModel.DB.LookupIds<TblUserAnswers>(question, null));

            var answers = new List<TblUserAnswers>();

            foreach (var ans in answersForQuestion)
            {
                if (ans.UserRef == userId)
                    answers.Add(ans);
            }

            if (answers.Count > 0)
            {
                var lstUserAnswer = StatisticManager.FindLatestUserAnswer(answers);

                if (lstUserAnswer != null)
                    return lstUserAnswer.UserAnswer;
            }

            return string.Empty;
        }
    }
}