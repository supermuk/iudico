using System;
using System.Web;

namespace IUDICO.DataModel.Common.TestRequestUtils
{
    class TestRequestParser
    {
        public static int GetPageId(HttpRequest request)
        {
            if(request["PageId"] == null)
                return 0;

            return Convert.ToInt32(request["PageId"]);
        }

        public static int GetCurriculumnId(HttpRequest request)
        {
            if (request["CurriculumnId"] == null)
                return 0;

            return Convert.ToInt32(request["CurriculumnId"]);
        }

        public static int GetStageId(HttpRequest request)
        {
            if (request["StageId"] == null)
                return 0;

            return Convert.ToInt32(request["StageId"]);
        }

        public static int GetThemeId(HttpRequest request)
        {
            if (request["ThemeId"] == null)
                return 0;

            return Convert.ToInt32(request["ThemeId"]);
        }

        public static int GetPageIndex(HttpRequest request)
        {
            if (request["PageIndex"] == null)
                return 0;

            return Convert.ToInt32(request["PageIndex"]);
        }

        public static TestSessionType GetTestSessionType(HttpRequest request)
        {
            if (request["TestSessionType"] == null)
                return TestSessionType.UnitTesting;

            int type = Convert.ToInt32(request["TestSessionType"]);

            return (TestSessionType)type;
        }

        public static string GetTestPagesIds(HttpRequest request)
        {
            if (request["PagesIds"] == null)
                return string.Empty;

            return request["PagesIds"];
        }

        public static int GetUserId(HttpRequest request)
        {
            if (request["UserId"] == null)
                return 0;

            return Convert.ToInt32(request["UserId"]);
        }
    }
}
