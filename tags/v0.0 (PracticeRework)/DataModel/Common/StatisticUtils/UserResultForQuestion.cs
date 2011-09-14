using System;
using System.Collections.Generic;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;
using TestingSystem;

namespace IUDICO.DataModel.Common.StatisticUtils
{
    class UserResultForQuestion
    {
        private readonly List<TblUserAnswers> _answers;

        private readonly TblQuestions _question;
        private readonly DateTime? _date;

        public UserResultForQuestion(int userId, TblQuestions question, DateTime? date)
        {
            _date = date;
            _question = question;
            _answers = (List<TblUserAnswers>) StudentRecordFinder.GetUserAnswersForQuestion(question, userId);

            _answers.Sort(new UserAnswersComparer());
        }

        public ResultStatus Calc()
        {
            if (_answers.Count == 0)
                return ResultStatus.Empty;

            var answer = GetTestedAnswer(_answers, _date);

            if(answer == null)
                return ResultStatus.Fail;

            if (answer.AnswerTypeRef == FxAnswerType.NotIncludedAnswer.ID)
                return ResultStatus.NotIncluded;

            if (answer.AnswerTypeRef == FxAnswerType.EmptyAnswer.ID)
                return FindFirstAnswered();

            if (answer.AnswerTypeRef == FxAnswerType.UserAnswer.ID)
                return GetStatus(answer);

            return ResultStatus.Empty;
        }

        private static TblUserAnswers GetTestedAnswer(List<TblUserAnswers> answers, DateTime? date)
        {
            if (date == null)
                return answers[0];

            return StatisticManager.FindUserAnswerForDate(answers, date);
        }

        private ResultStatus GetStatus(TblUserAnswers answer)
        {
            if (_question.IsCompiled)
                return GetCompiledStatus(answer);

            if (answer.UserAnswer.Equals(_question.CorrectAnswer))
                return ResultStatus.Pass;

            return ResultStatus.Fail;
        }

        private static ResultStatus GetCompiledStatus(TblUserAnswers answer)
        {
            var userCompiledAnswers = StudentRecordFinder.GetCompiledAnswersForAnswer(answer);

            bool allAcepted = true;

            foreach (var compiledAnswer in userCompiledAnswers)
            {
                if (compiledAnswer.StatusRef == (int)Status.Enqueued)
                    return ResultStatus.Enqueued;

                allAcepted &= (compiledAnswer.StatusRef == (int) Status.Accepted);
            }

            if (allAcepted)
                return ResultStatus.Pass;

            return ResultStatus.Fail;
        }

        private ResultStatus FindFirstAnswered()
        {
            foreach (var a in _answers)
                if (a.AnswerTypeRef == FxAnswerType.UserAnswer.ID)
                    return GetStatus(a);

            return ResultStatus.Empty;
        }
    }

    class UserAnswersComparer : Comparer<TblUserAnswers>
    {
        public override int Compare(TblUserAnswers x, TblUserAnswers y)
        {
            return y.Date.Value.CompareTo(x.Date.Value);
        }
    }

    public enum ResultStatus
    {
        Pass,
        Fail,
        Enqueued,
        Empty,
        NotIncluded
    }
}