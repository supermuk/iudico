using System;
using IUDICO.DataModel.Common.StudentUtils;

namespace IUDICO.DataModel.Common.TestingUtils
{
    public static class ConditionChecker
    {
        public static bool IsSubmitEnabled(int stageId, int pageId)
        {            
            if (ServerModel.User.Current != null)
            {
                int userId = ServerModel.User.Current.ID;

                return /*UserSubmitCountChecker.IsUserCanSubmitOnPage(userId, pageId)
                       &&*/ StudentPermissionsHelper.IsDateAllowed(DateTime.Now, userId, stageId, NodeType.Stage, OperationType.Pass);
            }
            return false;
        }

        public static bool IsViewingAllowed(int stageId)
        {
            if (ServerModel.User.Current != null)
            {
                int userId = ServerModel.User.Current.ID;

                return StudentPermissionsHelper.IsDateAllowed(DateTime.Now, userId, stageId, NodeType.Stage, OperationType.View);
            }
            return false;
        }
    }
}