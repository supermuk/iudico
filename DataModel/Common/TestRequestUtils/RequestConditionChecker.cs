using System;
using System.Web;
using IUDICO.DataModel.Common.StudentUtils;

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
            
            if (ServerModel.User.Current != null)
            {
                int userId = ServerModel.User.Current.ID;

                return UserSubmitCountChecker.IsUserCanSubmitOnPage(userId, pageId)
                    && StudentPermissionsHelper.IsDateAllowed(DateTime.Now, userId, TestRequestParser.GetStageId((request)), NodeType.Stage, OperationType.Pass);
            }
            return false;
        }

        public static bool IsViewingAllowed(HttpRequest request)
        {
            if (ServerModel.User.Current != null)
            {
                int userId = ServerModel.User.Current.ID;

                return StudentPermissionsHelper.IsDateAllowed(DateTime.Now, userId, TestRequestParser.GetStageId((request)), NodeType.Stage, OperationType.View);
            }
            return false;
        }

        public static void IsRequestCorrectForPractice(HttpRequest request)
        {
            if(ServerModel.User.Current != null)
            {
                var type = TestRequestParser.GetTestSessionType(request);

                if (TestSessionType.CorrectAnswer == type)
                    CheckTypeCorrectAnswer(request);
                 
                if (TestSessionType.UserAnswer == type)
                     CheckTypeUserAnswer(request);

                if (TestSessionType.UnitTesting == type)
                    CheckTypeUnitTest(request);

                if (TestSessionType.Ordinary == type)
                    CheckTypeOrdinary(request);
             }
        }

        public static void IsRequestCorrectForTheory(HttpRequest request)
        {
            if (ServerModel.User.Current != null)
            {
                var type = TestRequestParser.GetTestSessionType(request);

                if (TestSessionType.CorrectAnswer == type)
                    throw new Exception("You cant see correct answer for theory page"); 

                if (TestSessionType.UserAnswer == type)
                    throw new Exception("You cant see user answer for theory page"); 

                if (TestSessionType.UnitTesting == type)
                    CheckTypeUnitTest(request);

                if (TestSessionType.Ordinary == type)
                    CheckTypeOrdinary(request);
            }
        }

        private static void CheckTypeCorrectAnswer(HttpRequest request)
        {
            if (ServerModel.User.Current.Islector())
            {
                if(TestRequestParser.GetCurriculumnId(request) == 0 &&
                        TestRequestParser.GetStageId(request) == 0 &&
                            TestRequestParser.GetThemeId(request) == 0 &&
                                TestRequestParser.GetTestPagesIds(request) == string.Empty &&
                                    TestRequestParser.GetPageIndex(request) == 0 &&
                                        TestRequestParser.GetUserId(request) == 0 && 
                                            TestRequestParser.GetPageId(request) > 0)
                {
                    if (!ServerModel.User.Current.Islector())
                        throw new Exception("You must be Lector to see this page");       
                }
                else
                    throw new Exception("Url is Not correct");
            }
            else
                throw new Exception("You not allowed to see this page");
        }

        private static void CheckTypeUserAnswer(HttpRequest request)
        {
                if (TestRequestParser.GetCurriculumnId(request) == 0 &&
                        TestRequestParser.GetStageId(request) == 0 &&
                            TestRequestParser.GetThemeId(request) == 0 &&
                                TestRequestParser.GetTestPagesIds(request) == string.Empty &&
                                    TestRequestParser.GetPageIndex(request) == 0 &&
                                        TestRequestParser.GetUserId(request) > 0 &&
                                            TestRequestParser.GetPageId(request) > 0)
                {
                    return;
                }
                else
                {
                    throw new Exception("Url is Not correct");
                }
        }

        private static void CheckTypeUnitTest(HttpRequest request)
        {
            if (ServerModel.User.Current.IsSuperAdmin())
            {
                if (TestRequestParser.GetCurriculumnId(request) == 0 &&
                        TestRequestParser.GetStageId(request) == 0 &&
                            TestRequestParser.GetThemeId(request) == 0 &&
                                TestRequestParser.GetTestPagesIds(request) == string.Empty &&
                                    TestRequestParser.GetPageIndex(request) == 0 &&
                                        TestRequestParser.GetUserId(request) == 0 &&
                                            TestRequestParser.GetPageId(request) > 0)
                {
                    return;
                }
                else
                    throw new Exception("Url is Not correct");
            }
            else
                throw new Exception("You must be SUPER ADMIN to test");
        }

        private static void CheckTypeOrdinary(HttpRequest request)
        {
            if (TestRequestParser.GetCurriculumnId(request) > 0 &&
                    TestRequestParser.GetStageId(request) > 0 &&
                        TestRequestParser.GetThemeId(request) > 0 &&
                                TestRequestParser.GetPageIndex(request) >= 0 &&
                                       TestRequestParser.GetPageId(request) > 0)
            {
                if(!IsViewingAllowed(request))
                    throw new Exception("You not allowed to see this page");
            }
            else
            {
                throw new Exception("Url is Not correct");
            }
        }
    }
}
