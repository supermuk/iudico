using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public enum OperationType { Pass, View };

    static class StudentPermissionsHelper
    {
        public static IList<DatePeriod> GetPermissionsDatePeriods(int userId, int objectId, NodeType type, OperationType opType)
        {
            return DatePeriod.ExtractPeriodsFromPermissions(GetPermissions(userId, objectId, type, opType));
        }

        public static ControlInfo IsTimeForControl(int userId, TblStages stage, TblThemes theme)
        {
            IList<TblPermissions> permissions = GetPermissions(userId, stage.ID, NodeType.Stage, OperationType.Pass);

            if (theme.IsControl)
                if (!IsAllDatesAreNull(permissions))// If dates are nulls --> control is notvisible
                    if (IsDateAllowed(DateTime.Now, permissions))
                        return new ControlInfo(stage, theme, DatePeriod.ExtractPeriodsFromPermissions(permissions));

            return new ControlInfo();
        }

        public static bool IsDateAllowed(DateTime? date, int userId, int objectId, NodeType type, OperationType opType)
        {
            var permissions = GetPermissions(userId, objectId, type, opType);

            if(IsAllDatesAreNull(permissions))// If dates are nulls --> period is notlimited
                return true;

            return CheckPermissionsDate(permissions, date);
        }


        private static IList<TblPermissions> GetPermissions(int userId, int objectId, NodeType type, OperationType opType)
        {
            if (NodeType.Stage == type)
            {
                var result = StudentRecordFinder.GetPermissionsForStage(userId, objectId, GetStageOperationId(opType));

                if (IsAllDatesAreNull(result))
                {
                    var stage = ServerModel.DB.Load<TblStages>(objectId);
                    return StudentRecordFinder.GetPermissionsForCurriculumn(userId, (int)stage.CurriculumRef, GetCurriculumOperationId(opType));
                }
                return result;
            }
            if (NodeType.Curriculum == type)
            {
                return StudentRecordFinder.GetPermissionsForCurriculumn(userId, objectId, GetCurriculumOperationId(opType));
            }

            return new List<TblPermissions>();
        }

        private static bool IsDateAllowed(DateTime? date, IList<TblPermissions> permissions)
        {
            return CheckPermissionsDate(permissions, date);
        }

        private static bool CheckPermissionsDate(IList<TblPermissions> permissions, DateTime? date)
        {
            if (permissions == null || permissions.Count == 0)
                return false;

            bool b = false;

            foreach (var permission in permissions)
                b = b || IsDateInPeriod(date, permission.DateSince, permission.DateTill);

            return b;
        }

        private static bool IsAllDatesAreNull(IList<TblPermissions> permissions)
        {

            bool b = false;

            foreach (var permission in permissions)
            {
                b = b || IsBothDatesAreNull(permission.DateSince, permission.DateTill);
            }

            return b;
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

        private static int GetCurriculumOperationId(OperationType type)
        {
            if (OperationType.View == type)
                return FxCurriculumOperations.View.ID;

            if(OperationType.Pass == type)
                return FxCurriculumOperations.Pass.ID;

            return 0;
        }

        private static int GetStageOperationId(OperationType type)
        {
            if (OperationType.View == type)
                return FxStageOperations.View.ID;

            if (OperationType.Pass == type)
                return FxStageOperations.Pass.ID;

            return 0;
        }
    }
}