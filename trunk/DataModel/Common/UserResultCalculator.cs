using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using TestingSystem;

namespace IUDICO.DataModel.Common
{
    public class UserResultCalculator
    {
        private static int CalculateUserRank(TblQuestions question, int userId, IUserAnswerFinder finder)
        {
            int userRank = 0;

            var userAnswers = ServerModel.DB.Load<TblUserAnswers>(ServerModel.DB.LookupIds<TblUserAnswers>(question, null));

            if (userAnswers != null)
            {
                var currUserAnswers = FindUserAnswers(userAnswers, userId);
                
                if (currUserAnswers.Count != 0)
                {
                    var userAnswerWithNeededDate = finder.FindUserAnswer(currUserAnswers);

                    if (userAnswerWithNeededDate.UserAnswer == question.CorrectAnswer)
                    {
                        userRank += (int) question.Rank;
                    }
                    else if (userAnswerWithNeededDate.IsCompiledAnswer)
                    {
                        var userCompiledAnswers = ServerModel.DB.Load<TblCompiledAnswers>(ServerModel.DB.LookupIds<TblCompiledAnswers>(userAnswerWithNeededDate, null));

                        bool allAcepted = true;

                        foreach (var compiledAnswer in userCompiledAnswers)
                            allAcepted &= (compiledAnswer.StatusRef == (int)Status.Accepted);

                        if (allAcepted)
                            userRank += (int)question.Rank;
                    }
                }
                else
                {
                    userRank = -1;
                }
            }

            return userRank;
        }

        private static IList<TblUserAnswers> FindUserAnswers(IList<TblUserAnswers> userAnswers, int userId)
        {
            var currentUserAnswers = new List<TblUserAnswers>();

            foreach (var o in userAnswers)
            {
                if (o.UserRef == userId)
                    currentUserAnswers.Add(o);
            }

            return currentUserAnswers;
        }

        public static int GetUserRank(TblPages page, int userId)
        {
            return GetUserRankWithFinder(page, userId, new LatestUserAnswerFinder());
        }

        public static int GetUserRank(TblPages page, int userId, DateTime date)
        {
            return GetUserRankWithFinder(page, userId, new UserAnswerByDateFinder(date));
        }

        private static int GetUserRankWithFinder(TblPages page, int userId, IUserAnswerFinder finder)
        {
            var questions = GetQuestionsForPage(page);

            int userRank = 0;

            foreach (var question in questions)
                userRank += CalculateUserRank(question, userId, finder);

            return userRank;
        }

        private static IList<TblQuestions> GetQuestionsForPage(TblPages page)
        {
            var questionsIDs = ServerModel.DB.LookupIds<TblQuestions>(page, null);
            return ServerModel.DB.Load<TblQuestions>(questionsIDs);
        }

        public static bool IsContainCompiledQuestions(TblPages page)
        {
            var questions = GetQuestionsForPage(page);
            
            foreach (var question in questions)
            {
                if (question.IsCompiled)
                    return true;
            }

            return false;
        }

        public static IList<UserAnswer> GetLatestResultsByQuestions()
        {
            if (ServerModel.User.Current == null)
                throw new Exception("There no current user");

            var user = ServerModel.DB.Load<TblUsers>(ServerModel.User.Current.ID);
            var userAnswersIds = ServerModel.DB.LookupIds<TblUserAnswers>(user, null);

            var sortAnswers = new List<UserAnswer>();

            foreach (var answerId in userAnswersIds)
            {
                var currAnswer = new UserAnswer(answerId);

                if (!sortAnswers.Contains(currAnswer))
                    sortAnswers.Add(currAnswer);
            }

            sortAnswers.Sort();

            return sortAnswers;    
        }
    }


    public class UserAnswer : IComparable<UserAnswer>
    {
        public string ThemeName { get; set; }

        public string PageName
        {
            get
            {
                return _page.PageName;
            }
        }

        private readonly TblPages _page;

        public DateTime Date { get; set; }

        public UserAnswer(int userAnswerId)
        {
            var ans = ServerModel.DB.Load<TblUserAnswers>(userAnswerId);

            Date = (DateTime)ans.Date;

            var que = ServerModel.DB.Load<TblQuestions>((int)ans.QuestionRef);

            _page = ServerModel.DB.Load<TblPages>((int)que.PageRef);

            var theme = ServerModel.DB.Load<TblThemes>((int)_page.ThemeRef);

            ThemeName = theme.Name;

        }

        public int CompareTo(UserAnswer other)
        {
            return other.Date.CompareTo(Date);
        }

        public override bool Equals(object obj)
        {
            var obj2 = obj as UserAnswer;

            if (obj2 != null)
                return (_page.ID == obj2._page.ID && Date.ToString().Equals(obj2.Date.ToString()));

            return false;
        }

        public override int GetHashCode()
        {
            int result = _page.ID;
            result = (result * 397) ^ (ThemeName != null ? ThemeName.GetHashCode() : 0);
            result = (result * 397) ^ (PageName != null ? PageName.GetHashCode() : 0);
            result = (result * 397) ^ Date.GetHashCode();
            return result;
        }

        public string GetStatus()
        {
            if (ServerModel.User.Current == null)
                throw new Exception("There no current user");

            var userRank = UserResultCalculator.GetUserRank(_page, ServerModel.User.Current.ID, Date);

            return userRank >= (int) _page.PageRank ? "pass" : "fail";
        }
    }


    interface IUserAnswerFinder
    {
        TblUserAnswers FindUserAnswer(IList<TblUserAnswers> userAnswers);
    }

    class LatestUserAnswerFinder : IUserAnswerFinder
    {
        public TblUserAnswers FindUserAnswer(IList<TblUserAnswers> userAnswers)
        {
            var latestUserAnswer = userAnswers[0];
            foreach (var o in userAnswers)
            {
                if (o.Date > latestUserAnswer.Date)
                    latestUserAnswer = o;
            }

            return latestUserAnswer;
        }
    }

    class UserAnswerByDateFinder : IUserAnswerFinder
    {
        private DateTime _date;

        public UserAnswerByDateFinder(DateTime date)
        {
            _date = date;
        }

        public TblUserAnswers FindUserAnswer(IList<TblUserAnswers> userAnswers)
        {
            foreach (var o in userAnswers)
            {
                if (o.Date.ToString().Equals(_date.ToString()))
                    return o;
            }

            return null;
        }
    }
}
