using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using System.Collections.Generic;

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

        public static void Grand([NotNull] ISecuredDataObject dataObject, IFxDataObject operation, int? userID, int? groupID, DateTimeInterval interval)
        {
            if ((userID == null && groupID == null) || (userID != null && groupID != null))
            {
                throw new ArgumentException("One and only one of parameters (userID, groupID) must be specified");
            }
            var doType = dataObject.GetObjectType();
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

        public static void Initialize([NotNull] DbConnection connection)
        {
            using (Logger.Scope("Initializing Security"))
            {
                if (ID != null)
                {
                    throw new DMError("{0} is already initialized", typeof(PermissionsManager).Name);
                }
                ID = Guid.NewGuid();

                using (var c = connection.CreateCommand())
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
            SqlUtils.RecreateProc(procName, string.Format(RETRIEVE_PERMISSIONS_PROC_TEMPLATE, procName, a.Name, PERMISSION_DATE_FILTER), cmd);
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
            SqlUtils.RecreateProc(procName, string.Format(RETRIEVE_PERMISSIONS_FOR_GROUP_PROC_TEMPLATE, procName, a.Name, PERMISSION_DATE_FILTER), cmd);
        }

        private static void CheckSupport(Type @class, Type @intf)
        {
            if (@class.GetInterface(@intf.Name) == null)
            {
                throw new DMError("DataObject {0} must support {1} interface to be a secured object", @class.Name, intf.Name);
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

        private static Guid? ID;
        private const string PERMISSION_DATE_FILTER = "((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))";
        private const string PERMISSION_DB_ERROR_MESSAGE = "Not enough permission to perform this operation";
        private const string PERMISSIONS_SELECT_COLUMNS = "[ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [{1}Ref], [{1}OperationRef]";
        private const string RETRIEVE_PERMISSIONS_PROC_TEMPLATE = @"CREATE PROCEDURE {0}
	@UserID int,
	@{1}OperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	SELECT " + PERMISSIONS_SELECT_COLUMNS + @"
    FROM tblPermissions WHERE
        ({1}Ref IS NOT NULL) AND
		(@UserID = OwnerUserRef OR OwnerGroupRef IN (
			SELECT GroupRef FROM relUserGroups WHERE UserRef = @UserID)) AND
		((@{1}OperationID IS NULL) OR 
			(@{1}OperationID = {1}OperationRef)
		) AND
		{2}
END";
        private const string RETRIEVE_PERMISSIONS_FOR_GROUP_PROC_TEMPLATE = @"CREATE PROCEDURE {0}
    @GroupID int,
    @{1}OperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    
    SELECT " + PERMISSIONS_SELECT_COLUMNS + @"
    FROM tblPermissions 
    WHERE (@GroupID = OwnerGroupRef) AND ((@{1}OperationID IS NULL) OR (@{1}OperationID = {1}OperationRef)) AND {2}
END";
        private const string RETRIEVE_OBJECT_OPERATIONS_PROC_TEMPLATE =
            @"CREATE PROCEDURE {0}
	@UserID int,
	@{1}ID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	SELECT DISTINCT {1}OperationRef
	FROM tblPermissions WHERE
		@UserID = OwnerUserRef AND
		((@{1}ID IS NULL) OR
			(@{1}ID = {1}Ref)
		)  AND 
        {2}
		
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
