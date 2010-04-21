using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Security
{
    [AttributeUsage(AttributeTargets.Field)]
    [BaseTypeRequired(typeof(ISecuredDataObject))]
    public class SecuredObjectTypeAttribute : Attribute
    {
        public readonly string Name;
        public readonly string TableName;
        public readonly string OperationTableName;
        public readonly Type OperationsClass;
        public readonly Type RuntimeClass;

        protected SecuredObjectTypeAttribute([NotNull] string name, [NotNull]string tableName, [NotNull]string operationTableName, [NotNull] Type runtimeClass, [NotNull] Type operationsClass)
        {
            Name = name;
            TableName = tableName;
            OperationTableName = operationTableName;
            OperationsClass = operationsClass;
            RuntimeClass = runtimeClass;
        }
        
        public SecuredObjectTypeAttribute([NotNull] string objectName, [NotNull] Type runtimeClass, [NotNull] Type operationsClass)
            : this(objectName, "tbl" + objectName, "fx" + objectName, runtimeClass, operationsClass)
        {
        }

        public string GetRetrievePermissionsProcName()
        {
            return "Security_GetPermissions" + Name;
        }

        public string GetRetrieveOperationsForObjectProcName()
        {
            return "Security_GetOperationsFor" + Name.Capitalize();
        }

        public string GetCheckPermissionProcName()
        {
            return "Security_CheckPermission" + Name.Capitalize();
        }

        public string GetRetrievePermissionsForGroupProcName()
        {
            return "Security_GetGroupPermissions" + Name;
        }
    }

    public struct DataObjectOperationInfo
    {
        public readonly int ID;
        public readonly string Name;

        public DataObjectOperationInfo(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    public static class PermissionsManager
    {
        public static readonly Func<SECURED_OBJECT_TYPE, ReadOnlyCollection<DataObjectOperationInfo>> GetPossibleOperations =
            new Memorizer<SECURED_OBJECT_TYPE, ReadOnlyCollection<DataObjectOperationInfo>>(GetPossibleOperationsInternal);

        [NotNull]
        public static IList<int> GetObjectsForUser(SECURED_OBJECT_TYPE objectType, int userID, int? operationID, DateTime? targetDate)
        {
            return GetIDsFromPermissionsInternal(objectType, userID, targetDate, operationID, objectType.GetSecurityAtr().Name + "Ref");
        }

        [NotNull]
        public static IList<int> GetOperationsForObject(SECURED_OBJECT_TYPE objectType, int userID, int? objectID, DateTime? targetDate)
        {
            using (var c = ServerModel.AcruireOpenedConnection())
            {
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var at = objectType.GetSecurityAtr();
                    cmd.CommandText = at.GetRetrieveOperationsForObjectProcName();
                    cmd.Parameters.Assign(new { UserID = userID, TargetDate = targetDate });
                    cmd.Parameters.AddWithValue(at.Name + "ID", objectID);

                    return cmd.FullReadInts();
                }
            }
        }

        [NotNull]
        public static IList<int> GetPermissions(SECURED_OBJECT_TYPE type, int userID, DateTime? targetDate, int? operationID)
        {
            return GetIDsFromPermissionsInternal(type, userID, targetDate, operationID, "ID");
        }

        [NotNull]
        public static IList<int> GetObjectsForGroup(SECURED_OBJECT_TYPE objectType, int groupID, int? operationID, DateTime? targetDate)
        {
            return GetIDsFromPermissionsForGroupInternal(objectType, groupID, operationID, targetDate, objectType.GetSecurityAtr().Name + "Ref");
        }

        public static IList<int> GetUsersForObject(SECURED_OBJECT_TYPE objectType, int objectID, int? operationID, DateTime? targetDate)
        {
            var condtion = GetQueryForObject(objectType, objectID, operationID, targetDate);
            condtion.Append(new IsNotNullCondition<int>(DataObject.Schema.OwnerUserRef));
            var prms = ServerModel.DB.Query<TblPermissions>(condtion);
            var res = new List<int>(prms.Select(p => p.OwnerUserRef.Value));
            res.RemoveDuplicates();
            return res;
        }

        public static IList<int> GetGroupsForObject(SECURED_OBJECT_TYPE objectType, int objectID, int? operationID, DateTime? targetDate)
        {
            var condtion = GetQueryForObject(objectType, objectID, operationID, targetDate);
            condtion.Append(new IsNotNullCondition<int>(DataObject.Schema.OwnerGroupRef));
            var prms = ServerModel.DB.Query<TblPermissions>(condtion);
            var res = new List<int>(prms.Select(p => p.OwnerGroupRef.Value));
            res.RemoveDuplicates();
            return res;
        }

        public static void Grand<TSecuredDataObject, TOperation>([NotNull] TSecuredDataObject dataObject, [NotNull]TOperation operation, int? userID, int? groupID, DateTimeInterval interval)
            where TSecuredDataObject : class, ISecuredDataObject<TOperation>
            where TOperation : class, IFxDataObject
        {
            if (dataObject == null)
                throw new ArgumentNullException("dataObject");

            if (operation == null)
                throw new ArgumentNullException("operation");   

            if ((userID == null && groupID == null) || (userID != null && groupID != null))
            {
                throw new ArgumentException(Translations.PermissionsManager_Grand_One_and_only_one_of_parameters__userID__groupID__must_be_specified);
            }
            var doType = ObjectTypeHelper.GetObjectType(dataObject.GetType());
            var p = new TblPermissions
            {
                OwnerUserRef = userID,
                OwnerGroupRef = groupID,
                CanBeDelagated = true,
                WorkingInterval = interval
            };
            p.SetObjectID(doType, dataObject.ID);
            p.SetOperationID(doType, operation.ID);
            ServerModel.DB.Insert(p);
        }

        public static void Delegate<TSecuredDataObject, TOperation>(int ownerUserID, [NotNull] TSecuredDataObject dataObject, [NotNull] TOperation operation, int? targetUserID, int? targetGroupID, DateTimeInterval interval)
            where TSecuredDataObject : class, ISecuredDataObject<TOperation>
            where TOperation : class, IFxDataObject
        {
            if (dataObject == null)
                throw new ArgumentNullException("dataObject");

            if ((targetUserID == null && targetGroupID == null) || (targetUserID != null && targetGroupID != null))
                throw new ArgumentException(Translations.PermissionsManager_Delegate_One_and_only_one_of_parameters__targetUserID__targetGroupID__must_be_specified);

            if (operation == null)
                throw new ArgumentNullException("operation");           

            var doType = ObjectTypeHelper.GetObjectType(dataObject.GetType());

            var prm = GetPermissions(doType, ownerUserID, null, operation.ID);
            if (prm.Count < 0)
                throw new SecurityException(string.Format(Translations.PermissionsManager_Delegate_, ownerUserID, operation.Name, dataObject.GetType().Name, dataObject.ID));
            
            var p = new TblPermissions
            {
                CanBeDelagated = true,
                OwnerGroupRef = targetGroupID,
                OwnerUserRef = targetUserID,
                WorkingInterval = interval,
                ParentPermitionRef = prm[0]
            };
            p.SetObjectID(doType, dataObject.ID);
            p.SetOperationID(doType, operation.ID);
            ServerModel.DB.Insert(p);
        }

        public static void Initialize()
        {
            using (Logger.Scope(Translations.PermissionsManager_Initialize_Initializing_Security))
            {
                if (IsInitialized())
                {
                    throw new DMError(Translations.PermissionsManager_Initialize__0__is_already_initialized, typeof(PermissionsManager).Name);
                }
                ID = Guid.NewGuid();

                using (var cn = ServerModel.AcruireOpenedConnection())
                {
                    using (var c = cn.CreateCommand())
                    {
                        CreateDBVersionFunc(c);

                        foreach (var f in typeof(SECURED_OBJECT_TYPE).GetFields())
                        {
                            if (f.IsSpecialName)
                                continue;

                            Logger.WriteLine(f.Name);
                            
                            var a = f.GetAtr<SecuredObjectTypeAttribute>();
                            CheckSupport(a.RuntimeClass, typeof (ISecuredDataObject));
                            CheckSupport(a.OperationsClass, typeof (IFxDataObject));
                            CheckSupport(a.OperationsClass, typeof(INamedDataObject));

                            CreateGetOperationsForObjectProc(a, c);
                            CreateGetPermissionsForUser(a, c);
                            CreateCheckPermissionProc(a, c);
                            CreateGetPermissionsForGroup(a, c);
                        }
                    }
                }
            }
        }

        public static void UnInitialize()
        {
            ID = null;
        }

        public static bool IsInitialized()
        {
            return ID != null;
        }

        [NotNull]
        private static IList<int> GetIDsFromPermissionsInternal(SECURED_OBJECT_TYPE objectType, int userID, DateTime? targetDate, int? operationID, string columnName)
        {
            using (var c = ServerModel.AcruireOpenedConnection())
            {
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var at = objectType.GetSecurityAtr();
                    cmd.CommandText = at.GetRetrievePermissionsProcName();
                    cmd.Parameters.Assign(new { UserID = userID, TargetDate = targetDate });
                    cmd.Parameters.AddWithValue(at.Name + "OperationID", operationID);

                    var r = cmd.FullReadInts(columnName);
                    r.RemoveDuplicates();
                    return r;
                }
            }
        }

        [NotNull]
        private static IList<int> GetIDsFromPermissionsForGroupInternal(SECURED_OBJECT_TYPE objectType, int groupID, int? operationID, DateTime? targetDate, string columnName)
        {
            using (var c = ServerModel.AcruireOpenedConnection())
            {
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var at = objectType.GetSecurityAtr();
                    cmd.CommandText = at.GetRetrievePermissionsForGroupProcName();
                    cmd.Parameters.Assign(new { GroupID = groupID, TargetDate = targetDate});
                    cmd.Parameters.AddWithValue(at.Name + "OperationID", operationID);

                    var r = cmd.FullReadInts(columnName);
                    r.RemoveDuplicates();
                    return r;
                }
            }
        }

        private static void CreateDBVersionFunc([NotNull] IDbCommand cmd)
        {
            cmd.CommandText = "IF EXISTS(SELECT * FROM sys.objects WHERE name = 'getsecurityid' and type = 'FN') DROP FUNCTION GetSecurityID";
            cmd.LexExecuteNonQuery();

            cmd.CommandText = string.Format(SECURITY_ID_FUNCTION_TEMPLATE, ID.Value);
            cmd.LexExecuteNonQuery();
        }

        private static void CreateGetPermissionsForUser([NotNull] SecuredObjectTypeAttribute a, [NotNull] IDbCommand cmd)
        {
            string procName = a.GetRetrievePermissionsProcName();
            SqlUtils.RecreateProc(procName, string.Format(RETRIEVE_PERMISSIONS_PROC_TEMPLATE, procName, a.Name), cmd);
        }

        private static void CreateGetOperationsForObjectProc([NotNull] SecuredObjectTypeAttribute a, [NotNull] IDbCommand cmd)
        {
            var procName = a.GetRetrieveOperationsForObjectProcName();
            SqlUtils.RecreateProc(procName, string.Format(RETRIEVE_OBJECT_OPERATIONS_PROC_TEMPLATE,
                procName, a.Name, PERMISSION_DATE_FILTER), cmd);
        }

        private static void CreateCheckPermissionProc([NotNull] SecuredObjectTypeAttribute a, [NotNull] IDbCommand cmd)
        {
            var procName = a.GetCheckPermissionProcName();
            SqlUtils.RecreateProc(procName, string.Format(CHECK_PERMISSION_PROC_TEMPLATE,
                procName, a.Name, PERMISSION_DATE_FILTER, PERMISSION_DB_ERROR_MESSAGE), cmd);
        }

        private static void CreateGetPermissionsForGroup([NotNull] SecuredObjectTypeAttribute a, [NotNull] IDbCommand cmd)
        {
            string procName = a.GetRetrievePermissionsForGroupProcName();
            SqlUtils.RecreateProc(procName, string.Format(RETRIEVE_PERMISSIONS_FOR_GROUP_PROC_TEMPLATE, procName, a.Name), cmd);
        }

        private static void CheckSupport(Type @class, Type @intf)
        {
            if (@class.GetInterface(@intf.Name) == null)
            {
                throw new DMError(Translations.PermissionsManager_CheckSupport_DataObject__0__must_support__1__interface_to_be_a_secured_object, @class.Name, intf.Name);
            }
        }

        private static ReadOnlyCollection<DataObjectOperationInfo> GetPossibleOperationsInternal(SECURED_OBJECT_TYPE doType)
        {
            var ops = (IList)DatabaseModel.FIXED_METHOD.MakeGenericMethod(new[] { doType.GetSecurityAtr().OperationsClass }).Invoke(ServerModel.DB, Type.EmptyTypes);
            var res = new List<DataObjectOperationInfo>(ops.Count);
            foreach (IFxDataObject o in ops)
            {
                res.Add(new DataObjectOperationInfo(o.ID, o.Name));
            }
            return new ReadOnlyCollection<DataObjectOperationInfo>(res);
        }

        private static AndCondition GetQueryForObject(SECURED_OBJECT_TYPE objectType, int objectID, int? operationID, DateTime? targetDate)
        {
            var name = objectType.GetSecurityAtr().Name;

            var conds = new IDBPredicate[operationID != null ? 3 : 2];
            conds[0] = new CompareCondition<int>(new PropertyCondition<int>(name + "Ref"), new ValueCondition<int>(objectID), COMPARE_KIND.EQUAL);
            conds[1] = new DateTimeBetweenCondition(new ValueCondition<DateTime>(targetDate ?? DateTime.Now), DataObject.Schema.DateSince, DataObject.Schema.DateTill);
            if (operationID != null)
                conds[2] = new CompareCondition<int>(new PropertyCondition<int>(name + "OperationRef"), new ValueCondition<int>(operationID.Value), COMPARE_KIND.EQUAL);
            return new AndCondition(conds);
        }

        private static Guid? ID;
        private const string PERMISSION_DATE_FILTER = "((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))";
        private const string PERMISSION_DB_ERROR_MESSAGE = "Not enough permission to perform this operation";
        private static readonly string[] PERMISSIONS_SELECT_COLUMNS_1 = new[] { "[ID]" , "[ParentPermitionRef]", "[DateSince]", "[DateTill]", "[OwnerUserRef]", "[OwnerGroupRef]", "[CanBeDelagated]" };
        private static readonly string[] PERMISSIONS_SELECT_COLUMNS_2 = new[] { "[{1}Ref]", "[{1}OperationRef]" };
        private static readonly string[] PERMISSIONS_SELECT_COLUMNS = new List<string>(PERMISSIONS_SELECT_COLUMNS_1.Concat(PERMISSIONS_SELECT_COLUMNS_2)).ToArray();
        private static readonly string PERMISSIONS_SELECT_COLUMNS_SQL = PERMISSIONS_SELECT_COLUMNS.ConcatComma();
        private static readonly string RETRIEVE_PERMISSIONS_PROC_TEMPLATE = @"CREATE PROCEDURE {0}
	@UserID int,
	@{1}OperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList (" + PERMISSIONS_SELECT_COLUMNS_SQL + @") AS
	(
		SELECT " + PERMISSIONS_SELECT_COLUMNS_SQL + @"
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@{1}OperationID IS NULL) OR (@{1}OperationID = {1}OperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT " + PERMISSIONS_SELECT_COLUMNS_1.Select(p => "p." + p).Concat(PERMISSIONS_SELECT_COLUMNS_2.Select(p => "parent_prms." + p)).ConcatComma() + @"
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END";

        private static readonly string RETRIEVE_PERMISSIONS_FOR_GROUP_PROC_TEMPLATE = @"CREATE PROCEDURE {0}
    @GroupID int,
    @{1}OperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList (" + PERMISSIONS_SELECT_COLUMNS_SQL + @") AS
	(
		SELECT " + PERMISSIONS_SELECT_COLUMNS_SQL + @"
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@{1}OperationID IS NULL) OR (@{1}OperationID = {1}OperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT " + PERMISSIONS_SELECT_COLUMNS_1.Select(p => "p." + p).Concat(PERMISSIONS_SELECT_COLUMNS_2.Select(p => "parent_prms." + p)).ConcatComma() + @"
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END";
        private static readonly string RETRIEVE_OBJECT_OPERATIONS_PROC_TEMPLATE =
            @"CREATE PROCEDURE {0}
	@UserID int,
	@{1}ID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList (" + PERMISSIONS_SELECT_COLUMNS_SQL + @") AS
	(
		SELECT " + PERMISSIONS_SELECT_COLUMNS_SQL + @"
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT " + PERMISSIONS_SELECT_COLUMNS_1.Select(p => "p." + p).Concat(PERMISSIONS_SELECT_COLUMNS_2.Select(p => "parent_prms." + p)).ConcatComma() + @"
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT {1}OperationRef from FlatPermissionList		
END";

        private const string CHECK_PERMISSION_PROC_TEMPLATE =
            @"CREATE PROCEDURE {0}
	@UserID int,
	@{1}OperationID int,   
    @{1}ID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@{1}ID = {1}Ref AND
		@{1}OperationID = {1}OperationRef AND
		{2}
	)) RAISERROR ('{3}', 16, 16);
END";

        private const string SECURITY_ID_FUNCTION_TEMPLATE = @"CREATE function GetSecurityID()
RETURNS uniqueidentifier
AS
BEGIN
	RETURN '{0}';
END";
    }
}
