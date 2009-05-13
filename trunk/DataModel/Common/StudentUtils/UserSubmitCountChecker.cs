using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    class UserSubmitCountChecker
    {
        public static bool IsUserCanSubmitOnPage(int userId, int pageId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            var theme = ServerModel.DB.Load<TblThemes>((int)page.ThemeRef);

            return CheckCountOfSubmits(page, theme.MaxCountToSubmit, pageId);
        }

        private static bool CheckCountOfSubmits(TblPages p, int? maxCountToSubmit, int userId)
        {
            if (IsCountOfSubmitsUnlimited(maxCountToSubmit))
                return true;

            return GetCountOfUserSubmits(p, userId) < maxCountToSubmit;


        }

        private static bool IsCountOfSubmitsUnlimited(int? maxCountToSubmit)
        {
            return (maxCountToSubmit == null);
        }

        private static int GetCountOfUserSubmits(TblPages p, int userId)
        {
            var allQuestionsFromPageIds = ServerModel.DB.LookupIds<TblQuestions>(p, null);
            
            if (allQuestionsFromPageIds.Count > 0)
            {
                int userSubmitCount = 0;

                var allUsersAnswersIdsForQuestion =
                    ServerModel.DB.LookupIds<TblUserAnswers>(ServerModel.DB.Load<TblQuestions>(allQuestionsFromPageIds[0]),
                                                             null);

                var allUsersAnswersForQuestion = ServerModel.DB.Load<TblUserAnswers>(allUsersAnswersIdsForQuestion);


                foreach (var ua in allUsersAnswersForQuestion)
                {
                    if (ua.UserRef == userId)
                        userSubmitCount++;

                }

                return userSubmitCount;
            }
            return 0;
        }
    }
}
