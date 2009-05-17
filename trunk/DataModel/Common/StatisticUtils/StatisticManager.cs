using System;
using System.Collections.Generic;
using System.Web.Security;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common.StatisticUtils
{
    public class StatisticManager
    {
        public static bool IsContainCompiledQuestions(TblPages page)
        {
            var questions = StudentRecordFinder.GetQuestionsForPage(page);

            foreach (var question in questions)
                if (question.IsCompiled)
                    return true;

            return false;
        }

        public static UserThemeRankHolder GetUserRankForTheme(int userId, int themeId)
        {
            int userRank = 0;
            int themeRank = 0;

            var results = GetStatisticForThemeForUser(userId, themeId);

            foreach (var r in results)
                if (r.Status != ResultStatus.NotIncluded)
                {
                    userRank += r.UserRank;
                    themeRank += r.PageRank;
                }

            return new UserThemeRankHolder(userRank, themeRank);
        }


        public static IList<UserResultForPageForDate> GetUserLatestAnswers(int userId)
        {
            var answers = StudentRecordFinder.GetAnswersForUser(userId);

            var includedAnswers = StudentRecordFinder.ExtractIncludedAnswers(answers);

            var sortAnswers = new List<UserResultForPageForDate>();

            foreach (var answer in includedAnswers)
            {
                var currAnswer = new UserResultForPageForDate(answer, userId);

                if (!sortAnswers.Contains(currAnswer))
                    sortAnswers.Add(currAnswer);
            }

            sortAnswers.Sort();

            return sortAnswers;
        }

        public static IList<UserResultForPage> GetStatisticForThemeForUser(int userId, int themeId)
        {
            var pages = StudentRecordFinder.GetPagesForTheme(themeId);

            var result = new List<UserResultForPage>();

            foreach (var p in pages)
            {
                var ur = new UserResultForPage(userId, p, null);
                ur.Calc();
                result.Add(ur);
            }

            return result;
        }


        public static TblUserAnswers FindUserAnswerForDate(IList<TblUserAnswers> userAnswers, DateTime? date)
        {
            foreach (var o in userAnswers)
                if (o.Date.ToString().Equals(date.ToString()))
                    return o;

            return null;
        }

        public static TblUserAnswers FindLatestUserAnswer(IList<TblUserAnswers> userAnswers)
        {
            if (userAnswers != null)
            {
                var includedAnswers = StudentRecordFinder.ExtractIncludedAnswers(userAnswers);
                
                if (includedAnswers.Count > 0)
                {
                    var latestUserAnswer = includedAnswers[0];
                    foreach (var o in includedAnswers)
                        if (o.Date > latestUserAnswer.Date)
                            latestUserAnswer = o;

                    return latestUserAnswer;
                }
            }
            return null;
        }


        public static void MarkUsedPages(IList<TblPages> usedPages)
        {
            foreach (var page in usedPages)
            {
                var questions = StudentRecordFinder.GetQuestionsForPage(page);

                foreach (var q in questions)
                {
                    var ua = new TblUserAnswers
                    {
                        QuestionRef = q.ID,
                        Date = DateTime.Now,
                        UserRef = ((CustomUser)Membership.GetUser()).ID,
                        UserAnswer = string.Empty,
                        IsCompiledAnswer = false,
                        AnswerTypeRef = FxAnswerType.EmptyAnswer.ID
                    };

                    ServerModel.DB.Insert(ua);
                }
            }
        }

        public static void MarkNotIncludedPages(IList<TblPages> usedPages)
        {
            foreach (var page in usedPages)
            {
                var questions = StudentRecordFinder.GetQuestionsForPage(page);

                foreach (var q in questions)
                {
                    var ua = new TblUserAnswers
                    {
                        QuestionRef = q.ID,
                        Date = DateTime.Now,
                        UserRef = ((CustomUser)Membership.GetUser()).ID,
                        UserAnswer = string.Empty,
                        IsCompiledAnswer = false,
                        AnswerTypeRef = FxAnswerType.NotIncludedAnswer.ID
                    };

                    ServerModel.DB.Insert(ua);
                }
            }
        }
    }

    public class UserThemeRankHolder
    {
        private readonly int _userRank;

        private readonly int _themeRank;

        public UserThemeRankHolder(int userRank, int themeRank)
        {
            _userRank = userRank;
            _themeRank = themeRank;
        }

        public int UserRank
        {
            get { return _userRank; }
        }

        public int ThemeRank
        {
            get { return _themeRank; }
        }
    }
}
