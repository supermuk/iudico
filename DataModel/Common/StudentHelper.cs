using System;
using System.Collections.Generic;
using System.Text;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    static class StudentHelper
    {
        public const char PagesIdSplitter = '*';


        public static Encoding GetEncoding()
        {
            return Encoding.GetEncoding(1251);
        }

        public static string GetShiftedPagesIds(int themeId)
        {
            var theme = ServerModel.DB.Load<TblThemes>(themeId);
            var pagesIds = ServerModel.DB.LookupIds<TblPages>(theme, null);

            if (theme.PageOrderRef == FxPageOrders.Random.ID)
                RandomShuffle(pagesIds);

            return CreateParameterRequestFromCollection(pagesIds, theme.PageCountToShow);
        }

        public static IList<TblPermissions> GetPermissionForNode(int userId, IdendtityNode node, bool isView)
        {
            if (node.Type == NodeType.Theme) //If Node is Theme take Parent Stage permission 
                return GetPermissionForNode(userId, ((IdendtityNode)node.Parent), isView);

            var permissions = GetPermission(node.ID, userId, node.Type, isView);

            if (permissions.Count == 0) //If no permission for Stage take Parent Curriculumn permission
                return GetPermissionForNode(userId, ((IdendtityNode) node.Parent), isView);

            if (node.Type == NodeType.Stage && IsAllDatasAreNull(permissions)) //If node is stage and dates is null take Parent Curriculumn
                return GetPermissionForNode(userId, ((IdendtityNode)node.Parent), isView);

            return permissions;
        }

        public static IList<TblPermissions> GetPermission(int secureObjectId, int userId, NodeType type, bool isView)
        {
            var secureObject = GetSecureObject(secureObjectId, type);

            var allPermissionsForObject = ServerModel.DB.Load<TblPermissions>(ServerModel.DB.LookupIds<TblPermissions>(secureObject, null));

            var permissionForUser = ExtractPermissionsForUser(userId, allPermissionsForObject);

            return FindPermissionsForOperation(type, isView, permissionForUser);
        }

        public static bool IsDateAllowed(DateTime? date, IList<TblPermissions> permissions)
        {
            if (permissions == null || permissions.Count == 0)
            {
                return false;
            }

            bool b = false;

            foreach (var permission in permissions)
            {
                b = b || IsDateInPeriod(date, permission.DateSince, permission.DateTill);
            }

            return b;
        }

        public static bool IsAllDatasAreNull(IList<TblPermissions> permissions)
        {

            bool b = false;

            foreach (var permission in permissions)
            {
                b = b || IsBothDatesAreNull(permission.DateSince, permission.DateTill);
            }

            return b;
        }

        public static bool IsUserCanSubmit(int pageId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            var theme = ServerModel.DB.Load<TblThemes>((int) page.ThemeRef);

            return CheckCountOfSubmits(page, theme.MaxCountToSubmit);
        }


        private static bool CheckCountOfSubmits(TblPages p, int? maxCountToSubmit)
        {
            if (maxCountToSubmit == null)
                return true;

            return GetCountOfUserSubmits(p) < maxCountToSubmit;
        }

        private static int GetCountOfUserSubmits(TblPages p)
        {
            var allQuestionsFromPageIds = ServerModel.DB.LookupIds<TblQuestions>(p, null);
            if (allQuestionsFromPageIds.Count > 0)
            {
                int userSubmitCount = 0;

                var allUsersAnswersIdsForQuestion =
                    ServerModel.DB.LookupIds<TblUserAnswers>(ServerModel.DB.Load<TblQuestions>(allQuestionsFromPageIds[0]),
                                                             null);

                var allUsersAnswersForQuestion = ServerModel.DB.Load<TblUserAnswers>(allUsersAnswersIdsForQuestion);

                int currentUserId = 0;
                if (ServerModel.User.Current != null) currentUserId = ServerModel.User.Current.ID;

                foreach (var ua in allUsersAnswersForQuestion)
                {
                    if (ua.UserRef == currentUserId)
                        userSubmitCount++;

                }

                return userSubmitCount;
            }
            return 0;
        }

        private static bool IsBothDatesAreNull(DateTime? firstDate, DateTime? secondDate)
        {
            return (firstDate == null) && (secondDate == null);
        }

        private static bool IsDateInPeriod(DateTime? date, DateTime? startPeriod, DateTime? endPeriod)
        {
            if (date == null)
            {
                return true;
            }

            if (IsBothDatesAreNull(startPeriod, endPeriod))
            {
                return true;
            }

            if (startPeriod == null)
            {
                return date <= endPeriod;
            }

            if (endPeriod == null)
            {
                return date >= startPeriod;
            }
            return ((startPeriod <= date) && (date <= endPeriod));
        }

        private static IIntKeyedDataObject GetSecureObject(int id, NodeType type)
        {
            if (NodeType.Curriculum == type)
                return ServerModel.DB.Load<TblCurriculums>(id);

            if (NodeType.Stage == type)
                return ServerModel.DB.Load<TblStages>(id);

            throw new Exception("Wrong node type");
        }

        private static IList<TblPermissions> ExtractPermissionsForUser(int userId, IList<TblPermissions> allPermissions)
        {
            var groupsIds = GetGroupsIdsForUser(userId);

            var list = new List<TblPermissions>();

            foreach (var p in allPermissions)
            {
                if (p.OwnerGroupRef != null && groupsIds.Contains((int)p.OwnerGroupRef))
                    list.Add(p);
            }

            return list;
        }

        private static IList<int> GetGroupsIdsForUser(int userId)
        {
            return ServerModel.DB.LookupMany2ManyIds<TblGroups>(ServerModel.DB.Load<TblUsers>(userId), null);
        }

        private static IList<TblPermissions> FindPermissionsForOperation(NodeType type, bool isView, IList<TblPermissions> allPermissions)
        {
            switch (type)
            {
                case (NodeType.Curriculum):
                    return FindPerrmisionForCurriculumOperation(GetOperationId(type, isView), allPermissions);
                case (NodeType.Stage):
                    return FindPerrmisionForStageOperation(GetOperationId(type, isView), allPermissions);
            }
            return new List<TblPermissions>();
        }

        private static IList<TblPermissions> FindPerrmisionForStageOperation(int operationId, IList<TblPermissions> allPermisions)
        {
            var list = new List<TblPermissions>();

            foreach (var permission in allPermisions)
                if (permission.StageOperationRef == operationId)
                    list.Add(permission);

            return list;
        }

        private static IList<TblPermissions> FindPerrmisionForCurriculumOperation(int operationId, IList<TblPermissions> allPermisions)
        {
            var list = new List<TblPermissions>();

            foreach (var permission in allPermisions)
                if (permission.CurriculumOperationRef == operationId)
                    list.Add(permission);

            return list;
        }

        private static int GetOperationId(NodeType type, bool isViewMode)
        {
            if (NodeType.Curriculum == type)
                return isViewMode ? FxCurriculumOperations.View.ID : FxCurriculumOperations.Pass.ID;

            if (NodeType.Stage == type)
                return isViewMode ? FxStageOperations.View.ID : FxStageOperations.Pass.ID;


            return 0;

        }

        private static string CreateParameterRequestFromCollection(List<int> pagesIds, int? countOfPageToShow)
        {
            var pagesIdsParameter = new StringBuilder();

            int count = (countOfPageToShow == null) ? pagesIds.Count : Math.Min(pagesIds.Count, (int)countOfPageToShow);

            for (int i = 0; i < count; i++ )
                pagesIdsParameter.Append(pagesIds[i] + PagesIdSplitter.ToString());

            var pagesIdsParameterString = pagesIdsParameter.ToString();

            if (pagesIdsParameterString.Equals(string.Empty))
                return pagesIdsParameterString;
            
            return pagesIdsParameterString.Remove(pagesIdsParameterString.Length - 1); //Remove last character and return
        }

        private static void RandomShuffle(List<int> collectionOfPagesIds)
        {
            collectionOfPagesIds.Sort(new RandomPageComparer());
        }
    }

    class RandomPageComparer : Comparer<int>
    {
        private readonly Random _randomizer = new Random();

        public override int Compare(int x, int y)
        {
            if (x.Equals(y))
                return 0;

            return _randomizer.Next(-1, 1);
        }
    }
} 
