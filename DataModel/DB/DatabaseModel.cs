using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using IUDICO.DataModel.Common;
using System.Data;
using LEX.CONTROLS;
using System.Text;
using System.Data.Common;

namespace IUDICO.DataModel.DB
{
    public class DBEnumAttribute : Attribute
    {
        public readonly string TableName;

        public DBEnumAttribute(string tableName)
        {
            TableName = tableName;
        }
    }

    public interface IIntKeyedDataObject
    {
        int ID { get; }
    }

    public class DBEnum<T>
        where T : struct
    {
        static DBEnum()
        {
            Values = new ReadOnlyCollection<string>(new List<string>(
                from f in typeof(T).GetFields()
                where !f.IsSpecialName
                select f.Name));
        }

        public static readonly ReadOnlyCollection<string> Values;
    }

    public abstract class DataObject
    {
    }

    public abstract class IntKeyedDataObject
    {
    }

    public partial class TblPermissions : IntKeyedDataObject, IIntKeyedDataObject
    {

    }

    public partial class TblCompiledAnswers : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestions : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestionsData : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCourses : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCurriculums : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblFiles : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblGroups : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblPages : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblQuestions : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblSampleBusinesObject : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblStages : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblThemes : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblUserAnswers : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblUsers : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class DatabaseModel
    {
        public DbConnection GetConnectionSafe()
        {
            if (Connection.State != ConnectionState.Open)
            {
                throw new DMError("Incorrect connection state");
            }
            return Connection;
        }

        public void Initialize()
        {
        }

        public int Insert<TDataObject>(TDataObject obj)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand) cmd);
                CompiledDataObjectSqlHelper<TDataObject>.AppendInsertSql(sc, obj);
                cmd.CommandText = sc.GetSql();

                using (var r = cmd.LexExecuteReader())
                {
                    r.Read();
                    var id = Convert.ToInt32(r.GetDecimal(0));
                    CompiledDataObjectSqlHelper<TDataObject>.KeyColumn.Storage.SetValue(obj, id);
                    return id;
                }
            }
        }

        public void Insert<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            DbConnection cn = GetConnectionSafe();
            var transaction = cn.BeginTransaction();
            try
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    var sc = new SqlSerializationContext((SqlCommand) cmd);
                    foreach (var o in objs)
                    {
                        if (o.ID != 0)
                        {
                            throw new DMError("DataObject has been already inserted.");
                        }
                        CompiledDataObjectSqlHelper<TDataObject>.AppendInsertSql(sc, o);
                        sc.Next();
                    }

                    cmd.CommandText = sc.GetSql();
                    using (var r = cmd.LexExecuteReader())
                    {
                        int i = 0, c = objs.Count;

                        while (true)
                        {
                            if (!r.Read())
                                throw new DMError("Invalid Data Reader");
                            int id = Convert.ToInt32(r.GetDecimal(0));
                            CompiledDataObjectSqlHelper<TDataObject>.KeyColumn.Storage.SetValue(objs[i], id);
                            ++i;
                            if (i < c)
                                r.NextResult();
                            else
                                break;
                        }
                    }
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();                
            }
        }

        public void Update<TDataObject>(TDataObject obj)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand) cmd);
                CompiledDataObjectSqlHelper<TDataObject>.AppendUpdateSql(sc, obj);
                cmd.CommandText = sc.GetSql();
                cmd.LexExecuteNonQuery();
            }
        }

        public void Update<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            var connection = GetConnectionSafe();
            var transaction = connection.BeginTransaction();
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand) cmd);
                    foreach (var o in objs)
                    {
                        CompiledDataObjectSqlHelper<TDataObject>.AppendUpdateSql(sc, o);
                        sc.Next();
                    }

                    cmd.Transaction = transaction;
                    cmd.CommandText = sc.GetSql();
                    cmd.LexExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public TDataObject Load<TDataObject>(int id)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                cmd.CommandText = CompiledDataObjectSqlHelper<TDataObject>.SelectSql + " WHERE [ID] = " + id;
                using (var r = cmd.LexExecuteReader())
                {
                    return CompiledDataObjectSqlHelper<TDataObject>.Read(r);
                }
            }
        }

        public IList<TDataObject> Load<TDataObject>(IList<int> ids)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var result = new List<TDataObject>(ids.Count);
                cmd.CommandText = CompiledDataObjectSqlHelper<TDataObject>.SelectSql + " WHERE ID IN (" + ids.ConcatComma() + ")";
                using (var r = cmd.LexExecuteReader())
                {
                    while (r.Read())
                    {
                        result.Add(CompiledDataObjectSqlHelper<TDataObject>.Read(r));
                    }
                }
                if (result.Count != ids.Count)
                {
                    throw new DMError("Count of populated objects ({0}) lower than requested to extract {1}", result.Count, ids.Count);
                }
                return result;
            }
        }

        public void Delete<TDataObject>(int id)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand)cmd);
                CompiledDataObjectSqlHelper<TDataObject>.AppendDeleteSql(sc, id);
                cmd.CommandText = sc.GetSql();
                cmd.LexExecuteNonQuery();
            }
        }

        public void Delete<TDataObject>(IList<int> ids)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand) cmd);
                CompiledDataObjectSqlHelper<TDataObject>.AppendDeleteSql(sc, ids);
                cmd.CommandText = sc.GetSql();
                cmd.LexExecuteNonQuery();
            }
        }

        public void Delete<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            Delete<TDataObject>(new List<int>(objs.Select(o => o.ID)));
        }

        #region CompiledDataObjectSqlHelper

        private struct ColumnInfo
        {
            public ColumnInfo(Type table, MemberInfo column)
                : this(column.Name, 
                table.GetField(column.GetAtr<ColumnAttribute>().Storage, BindingFlags.Instance | BindingFlags.NonPublic))
            {
            }

            public ColumnInfo([NotNull] string name, [NotNull] FieldInfo storage)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }
                if (storage == null)
                {
                    throw new ArgumentNullException("storage");
                }

                Name = name;
                Storage = storage;
            }

            public readonly string Name;
            public readonly FieldInfo Storage;
        }

        private static class CompiledDataObjectSqlHelper<TDataObject>
            where TDataObject : IIntKeyedDataObject, new()
        {
            public static readonly string SelectSql;
            public static readonly ColumnInfo KeyColumn;

            private static readonly List<ColumnInfo> __Columns = new List<ColumnInfo>();
            private static readonly string __ColumnNames;
            private static readonly string __ColumnNamesWithoutID;
            private static readonly string __TableName;
            private static readonly string UpdateSqlHeader;
            public static readonly string DeleteSqlHeader;
            public static readonly string InsertSqlHeader;

            static CompiledDataObjectSqlHelper()
            {
                // Precalculation dataobject information
                Type type = typeof(TDataObject);

                var ta = type.GetAtr<TableAttribute>();
                if (ta == null)
                {
                    throw new DMError("{0} must have {1} attribute", type.FullName, typeof(TableAttribute).Name);
                }
                __TableName = ExtractTableName(ta.Name);

                __Columns.AddRange(
                    from p in type.GetProperties()
                    where p.HasAtr<ColumnAttribute>()
                    select new ColumnInfo(type, p));
                KeyColumn = new ColumnInfo(type, type.GetProperty("ID"));

                __ColumnNames = __Columns.Select(c => SqlUtils.WrapDbId(c.Name)).ConcatComma();
                __ColumnNamesWithoutID = (from c in __Columns where c.Name != "ID" select SqlUtils.WrapDbId(c.Name)).ConcatComma(); 
                SelectSql = "SELECT " + __ColumnNames + " FROM " + SqlUtils.WrapDbId(__TableName);
                UpdateSqlHeader = "UPDATE " + SqlUtils.WrapDbId(__TableName) + " SET ";
                InsertSqlHeader = "INSERT INTO " + SqlUtils.WrapDbId(__TableName) + " " + SqlUtils.WrapArc(__ColumnNamesWithoutID) + " VALUES ";
                DeleteSqlHeader = "DELETE " + SqlUtils.WrapDbId(__TableName) + " WHERE ID";
            }

            public static TDataObject Read(IDataRecord reader)
            {
                var result = new TDataObject();

                for (int i = __Columns.Count - 1; i >= 0; --i)
                {
                    AssignValue(__Columns[i].Storage, reader.GetValue(i), result);
                }

                return result;
            }

            public static void AppendUpdateSql([NotNull]SqlSerializationContext context,[NotNull]TDataObject instance)
            {
                context.Write(UpdateSqlHeader + 
                    (from ci in __Columns
                     where ci.Name != "ID"
                     select SqlUtils.WrapDbId(ci.Name) + "=@" + context.AddParameter(ci.Storage.GetValue(instance))
                     ).ConcatComma()
                     + " WHERE " + "ID = @" + context.AddParameter(instance.ID)
                );
            }

            public static void AppendInsertSql([NotNull]SqlSerializationContext context, [NotNull]TDataObject obj)
            {
                context.Write(InsertSqlHeader);
                context.Write(
                    SqlUtils.WrapArc(
                        (from ci in __Columns 
                         where ci.Name != "ID"
                         select "@" + context.AddParameter(ci.Storage.GetValue(obj))
                        ).ConcatComma()
                    )
                );
                context.Next();
                context.Write("select SCOPE_IDENTITY()");
            }

            public static void AppendDeleteSql([NotNull]SqlSerializationContext context, int id)
            {
                context.Write(DeleteSqlHeader);
                context.Write("=" + id);
            }

            public static void AppendDeleteSql([NotNull]SqlSerializationContext context, [NotNull]IList<int> objs)
            {
                if (objs == null || objs.Count == 0)
                {
                    throw new ArgumentException("Collection cannot be empty", "objs");
                }
                context.Write(DeleteSqlHeader + " IN " + SqlUtils.WrapArc(
                    objs.ConcatComma()
                ));
            }

            private static void AssignValue(FieldInfo f, object value, TDataObject instance)
            {
                if (value != DBNull.Value)
                {
                    f.SetValue(instance, value);
                }
            }

            private static string ExtractTableName(string fullName)
            {
                int ind = fullName.LastIndexOf('.');
                return ind < 0 ? fullName : fullName.Substring(ind + 1);
            }
        }

        private sealed class SqlSerializationContext
        {
            public SqlSerializationContext(SqlCommand cmd)
            {
                Cmd = cmd;
            }

            public string AddParameter(object value)
            {
                var p = "P" + ++__ParameterID;
                Cmd.Parameters.AddWithValue(p, value ?? DBNull.Value);
                return p;
            }

            public void Next()
            {
                SqlBuilder.AppendLine();
            }

            public string GetSql()
            {
                return SqlBuilder.ToString();
            }

            [StringFormatMethod("fmt")]
            public void Write(string fmt, params object[] args)
            {
                SqlBuilder.AppendFormat(fmt, args);
            }

            public void Write(string str)
            {
                SqlBuilder.Append(str);
            }

            private readonly StringBuilder SqlBuilder = new StringBuilder();
            private readonly SqlCommand Cmd;
            private int __ParameterID;
        }

        #endregion
    }

    [DBEnum("fxRoles")]
    public enum FX_ROLE
    {
        STUDENT,
        LECTOR,
        TRAINER,
        ADMIN,
        SUPER_ADMIN
    }
}
