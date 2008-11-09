using System;
using System.Data;
using System.Data.SqlClient;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using System.Collections.Generic;

namespace IUDICO.DataModel.Security
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class DBObjects : Attribute
    {
        public DBObjects(int dbVersion)
        {
            DBVersion = dbVersion;
        }

        public int DBVersion { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SecuredObjectTypeAttribute : Attribute
    {
        public readonly string Name;
        public readonly string TableName;
        public readonly string OperationTableName;

        public SecuredObjectTypeAttribute([NotNull] string name, [NotNull]string tableName, [NotNull]string operationTableName, [NotNull] Type runtimeClass)
        {
            Name = name;
            TableName = tableName;
            OperationTableName = operationTableName;
            _RuntimeClass = runtimeClass;
        }
        
        public SecuredObjectTypeAttribute([NotNull] string objectName, [NotNull] Type runtimeClass)
            : this(objectName, "tbl" + objectName, "fx" + objectName, runtimeClass)
        {
        }

        public string GetRetrieveObjectsForUserProcName()
        {
            return "Security_Get" + Name.Capitalize().Pluralize();
        }

        public string GetRetrieveOperationsForObjectProcName()
        {
            return "Security_GetOperationsFor" + Name.Capitalize();
        }

        public string GetCheckPermissionProcName()
        {
            return "Security_CheckPermission" + Name.Capitalize();
        }

        public Type RuntimeClass
        {
            get { return _RuntimeClass; }
        }

        private readonly Type _RuntimeClass;
    }

    public class PermissionsManager
    {
        public static readonly PermissionsManager Current;

        public List<int> GetObjectsForUser(DB_OBJECT_TYPE objectType, int UserID, int? OperationID, DateTime? targetDate)
        {
            using (var c = ServerModel.AcruireOpenedConnection())
            {
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var at = typeof (DB_OBJECT_TYPE).GetField(objectType.ToString()).GetAtr<SecuredObjectTypeAttribute>();
                    cmd.CommandText = at.GetRetrieveObjectsForUserProcName();
                    cmd.Parameters.Add(USER_ID_PARAMETER, SqlDbType.Int).Value = UserID;
                    cmd.Parameters.Add(at.Name + "OperationID", SqlDbType.Int).Value = OperationID;
                    cmd.Parameters.Add(TARGET_DATE_PARAMETER, SqlDbType.DateTime).Value = targetDate;

                    return SqlUtils.FullReadInts(cmd);
                }
            }
        }

        public List<int> GetOperationsForObject(DB_OBJECT_TYPE objectType, int UserID, int? ObjectID, DateTime? targetDate)
        {
            using (var c = ServerModel.AcruireOpenedConnection())
            {
                using (var cmd = c.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var at = typeof (DB_OBJECT_TYPE).GetField(objectType.ToString()).GetAtr<SecuredObjectTypeAttribute>();
                    cmd.CommandText = at.GetRetrieveOperationsForObjectProcName();
                    cmd.Parameters.Add(USER_ID_PARAMETER, SqlDbType.Int).Value = UserID;
                    cmd.Parameters.Add(at.Name + "ID", SqlDbType.Int).Value = ObjectID;
                    cmd.Parameters.Add(TARGET_DATE_PARAMETER, SqlDbType.DateTime).Value = targetDate;

                    return SqlUtils.FullReadInts(cmd);
                }
            }
        }

        public void Initialize([NotNull] SqlConnection connection)
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

                    foreach (var f in typeof(DB_OBJECT_TYPE).GetFields())
                    {
                        if (f.IsSpecialName)
                            continue;

                        var a = f.GetAtr<SecuredObjectTypeAttribute>();
                        if (a == null)
                        {
                            throw new DMError("{0}.{1} MUST have {2} applied",
                                              typeof(DB_OBJECT_TYPE).Name, f.Name,
                                              typeof(SecuredObjectTypeAttribute).Name);
                        }

                        CreateGetObjectsForUserProc(a, c);
                        CreateGetOperationsForObjectProc(a, c);
                        CreateCheckPermissionProc(a, c);
                    }
                }
            }
        }

        static PermissionsManager()
        {
            Current = new PermissionsManager();
        }

        private void CreateDBVersionFunc([NotNull] IDbCommand cmd)
        {
            cmd.CommandText = "IF EXISTS(SELECT * FROM sys.objects WHERE name = 'getsecurityid' and type = 'FN') DROP FUNCTION GetSecurityID";
            cmd.ExecuteNonQuery();

            cmd.CommandText = string.Format(SECURITY_ID_FUNCTION_TEMPLATE, ID.Value);
            cmd.ExecuteNonQuery();
        }

        private static void CreateGetObjectsForUserProc([NotNull] SecuredObjectTypeAttribute a, [NotNull] IDbCommand cmd)
        {
            string procName = a.GetRetrieveObjectsForUserProcName();
            SqlUtils.RecreateProc(procName, string.Format(RETRIEVE_OBJECTS_PROC_TEMPLATE, 
                procName, a.Name, PERMISSION_DATE_FILTER), cmd);
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

        private Guid? ID;

        private const string TARGET_DATE_PARAMETER = "TargetDate";
        private const string USER_ID_PARAMETER = "UserID";
        private const string PERMISSION_DATE_FILTER = "((DateFrom IS NULL) OR (DateFrom <= @TargetDate)) AND ((DateTo IS NULL) OR (DateTo >= @TargetDate))";
        private const string PERMISSION_DB_ERROR_MESSAGE = "Not enough permission to perform this operation";
        private const string RETRIEVE_OBJECTS_PROC_TEMPLATE = @"CREATE PROCEDURE {0}
	@UserID int,
	@{1}OperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	SELECT DISTINCT {1}Ref 
    FROM tblPermission WHERE
		@UserID = UserRef AND
		((@{1}OperationID IS NULL) OR 
			(@{1}OperationID = {1}OperationRef)
		) AND
		{2}
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
	FROM tblPermission WHERE
		@UserID = UserRef AND
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

	IF	(NOT EXISTS (SELECT ID FROM tblPermission WHERE 
		@UserID = UserRef AND
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
