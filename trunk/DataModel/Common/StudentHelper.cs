using System;
using System.Collections.Generic;
using System.Web.Security;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common
{
    class StudentHelper
    {
        public static bool IsBothDatesAreNull(DateTime? firstDate, DateTime? secondDate)
        {
            return (firstDate == null) && (secondDate == null);
        }

        public static IList<TblPermissions> GetPermissionForNode(IdendtityNode node, SECURED_OBJECT_TYPE type, bool isViewMode)
        {
            var permissions = GetPermission((node).ID, type, isViewMode);

            var list = new List<TblPermissions>();

            if (permissions.Count == 0 && type != SECURED_OBJECT_TYPE.CURRICULUM)
            {
                list.AddRange(GetPermissionForNode(((IdendtityNode)node.Parent), GetParentForObject(type), isViewMode));
            }
            else
            {
                list.AddRange(permissions);
            }


            return list;
        }

        public static IList<TblPermissions> GetPermission(int id, SECURED_OBJECT_TYPE type, bool isViewMode)
        {
            var permissionsIds = PermissionsManager.GetPermissions(type, ((CustomUser)Membership.GetUser()).ID, null, null);

            var permisions = ServerModel.DB.Load<TblPermissions>(permissionsIds);

            return FindObjectPermission(type, id, GetOperationId(type, isViewMode), permisions);
        }

        public static bool IsDateAllowed(DateTime? date, IList<TblPermissions> permissions)
        {
            if (permissions.Count == 0)
            {
                return true;
            }

            bool b = false;

            foreach (var permission in permissions)
            {
                b = b || IsDateInPeriod(date, permission.DateSince, permission.DateTill);
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

        private static IList<TblPermissions> FindObjectPermission(SECURED_OBJECT_TYPE type, int objectId, int operationId, IList<TblPermissions> allPermissions)
        {
            switch (type)
            {
                case (SECURED_OBJECT_TYPE.CURRICULUM):
                    {
                        return FindPerrmisionForCurriculumn(objectId, operationId, allPermissions);
                    }
                case (SECURED_OBJECT_TYPE.STAGE):
                    {
                        return FindPerrmisionForStage(objectId, operationId, allPermissions);
                    }
            }
            return new List<TblPermissions>();
        }

        private static IList<TblPermissions> FindPerrmisionForStage(int stageId, int operationId, IList<TblPermissions> allPermisions)
        {
            var list = new List<TblPermissions>();

            foreach (var permission in allPermisions)
            {
                if (permission.StageRef == stageId && permission.StageOperationRef == operationId)
                {
                    list.Add(permission);
                }
            }

            return list;
        }

        private static IList<TblPermissions> FindPerrmisionForCurriculumn(int curriculumnId, int operationId, IList<TblPermissions> allPermisions)
        {
            var list = new List<TblPermissions>();

            foreach (var permission in allPermisions)
            {
                if (permission.CurriculumRef == curriculumnId && permission.CurriculumOperationRef == operationId)
                {
                    list.Add(permission);
                }
            }

            return list;
        }

        private static FxCurriculumOperations GetFxOperationForCurriculumn(bool isViewMode)
        {
            return isViewMode ? FxCurriculumOperations.View : FxCurriculumOperations.Pass;
        }

        private static FxStageOperations GetFxOperationForStage(bool isViewMode)
        {
            return isViewMode ? FxStageOperations.View : FxStageOperations.Pass;
        }

        private static int GetOperationId(SECURED_OBJECT_TYPE type, bool isViewMode)
        {
            switch (type)
            {
                case (SECURED_OBJECT_TYPE.CURRICULUM):
                    {
                        return GetFxOperationForCurriculumn(isViewMode).ID;

                    }
                case (SECURED_OBJECT_TYPE.STAGE):
                    {
                        return GetFxOperationForStage(isViewMode).ID;
                    }
                default:
                    return 0;
            }
        }

        private static SECURED_OBJECT_TYPE GetParentForObject(SECURED_OBJECT_TYPE type)
        {
            if (type == SECURED_OBJECT_TYPE.STAGE)
                return SECURED_OBJECT_TYPE.CURRICULUM;

            return SECURED_OBJECT_TYPE.CURRICULUM;
        }
    }
}
