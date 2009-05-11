using System.Web;

namespace IUDICO.DataModel.Common.TestRequestUtils
{
    public static class RequestConditionChecker
    {
        public static bool DoFillCorrectAnswers(HttpRequest request)
        {
            return TestRequestParser.GetTestSessionType(request).Equals(TestSessionType.CorrectAnswer);
        }

        public static bool DoFillUserAnswers(HttpRequest request)
        {
            return TestRequestParser.GetTestSessionType(request).Equals(TestSessionType.UserAnswer); 
        }

        public static bool DoFillAnswers(HttpRequest request)
        {
            return DoFillCorrectAnswers(request) || DoFillUserAnswers(request);
        }

        public static bool IsForUnitTesting(HttpRequest request)
        {
            return TestRequestParser.GetTestSessionType(request).Equals(TestSessionType.UnitTesting);
        }

        public static bool IsSubmitEnabled(HttpRequest request)
        {
            int pageId = TestRequestParser.GetPageId(request);

            return StudentHelper.IsUserCanSubmit(pageId);
        }
    }
}
