using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    class UserSubmitCountChecker
    {
        public static bool IsUserCanSubmitOnPage(int userId, int pageId)
        {
            TblThemes theme = StudentRecordFinder.GetThemeForPage(pageId);

            return CheckCountOfSubmits(theme.MaxCountToSubmit, userId, pageId);
        }

        private static bool CheckCountOfSubmits(int? maxCountToSubmit, int userId, int pageId)
        {
            if (IsCountOfSubmitsUnlimited(maxCountToSubmit))
                return true;

            return GetCountOfUserSubmits(pageId, userId) < maxCountToSubmit;
        }

        private static bool IsCountOfSubmitsUnlimited(int? maxCountToSubmit)
        {
            return (maxCountToSubmit == null);
        }

        private static int GetCountOfUserSubmits(int pageId, int userId)
        {
            var allQuestionsFromPage = StudentRecordFinder.GetQuestionsForPage(pageId);

            var allUsersAnswersForQuestion = StudentRecordFinder.GetUserAnswersForQuestion(allQuestionsFromPage[0], userId);

            return StudentRecordFinder.ExtractIncludedAnswers(allUsersAnswersForQuestion).Count;
        }
    }
}
