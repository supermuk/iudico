using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    class StudentHelper
    {
        public static bool IsBothDatesAreNull(DateTime? firstDate, DateTime? secondDate)
        {
            return (firstDate == null) && (secondDate == null);
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

        private static IIntKeyedDataObject GetSecureObject(int id, NodeType type)
        {
            if (NodeType.Curriculum == type)
                return ServerModel.DB.Load<TblCurriculums>(id);

            if (NodeType.Stage == type)
                return ServerModel.DB.Load<TblStages>(id);

            throw new Exception("Wrong node type");
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

        public static bool IsDateInPeriod(DateTime? date, DateTime? startPeriod, DateTime? endPeriod)
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
            if(NodeType.Curriculum == type)
                return isViewMode ? FxCurriculumOperations.View.ID : FxCurriculumOperations.Pass.ID;

            if(NodeType.Stage == type)
                return isViewMode ? FxStageOperations.View.ID : FxStageOperations.Pass.ID;
                

            return 0;

        }
    }
} 
