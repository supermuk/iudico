using System;
using System.Web;

namespace IUDICO.DataModel.Common.TestRequestUtils
{
    class TestRequestParser
    {
        public static int GetPageId(HttpRequest request)
        {
            if(request["PageId"] == null)
                throw new Exception("Wrong Request. No PageId");

            return Convert.ToInt32(request["PageId"]);
        }

        public static int GetCurriculumnId(HttpRequest request)
        {
            if (request["CurriculumnId"] == null)
                throw new Exception("Wrong Request. No CurriculumnId");

            return Convert.ToInt32(request["CurriculumnId"]);
        }

        public static int GetStageId(HttpRequest request)
        {
            if (request["StageId"] == null)
                throw new Exception("Wrong Request. No StageId");

            return Convert.ToInt32(request["StageId"]);
        }

        public static int GetThemeId(HttpRequest request)
        {
            if (request["ThemeId"] == null)
                throw new Exception("Wrong Request. No ThemeId");

            return Convert.ToInt32(request["ThemeId"]);
        }

        public static int GetPageIndex(HttpRequest request)
        {
            if (request["PageIndex"] == null)
                throw new Exception("Wrong Request. No PageIndex");

            return Convert.ToInt32(request["PageIndex"]);
        }

        public static TestSessionType GetTestSessionType(HttpRequest request)
        {
            if (request["TestSessionType"] == null)
                throw new Exception("Wrong Request. No TestSessionType");

            int type = Convert.ToInt32(request["TestSessionType"]);

            return (TestSessionType)type;
        }
    }
}
